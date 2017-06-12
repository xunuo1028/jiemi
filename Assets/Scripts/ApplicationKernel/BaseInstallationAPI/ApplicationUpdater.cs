/// <summary>
/// FileName:    ResourceUpdater
/// Author:      JackalLiu
/// CreateTime:  4/11/2017 4:57:01 PM
/// Description:
/// </summary>

using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class ApplicationUpdater 
{
	private class ResourceInfo
	{
		public string resourcePath;
		public string code;
	}

	public string assetFolderPath = string.Empty;
	public string bundleListFilePath = string.Empty;

	public string cdnServerUrl = string.Empty;

	public string streamingZipManifestFilePath = string.Empty;

	//asset.zip
	public string cdnBundleListFileName = string.Empty;

	/// <summary>
	/// yield-return this function,after yield back, you can get app-carry assets in your persistant path.
	/// maybe there are many zip files under streamingasset path(in order to keep mono heap lower when extracting zip)
	/// Android: streaming assets extract
	/// iOS & win-editor:streaming assets copy in multy thread operation
	/// 
	/// after first extract finish.will create an empty file named finishmark.mark
	/// </summary>
	public IEnumerator FirstExtract()
	{
		string markFilePath = string.Concat (assetFolderPath, "/finishmark.mark");
		if (File.Exists (markFilePath))
		{
			yield break;
		}

		if (Application.platform == RuntimePlatform.Android) 
		{
			WWW manifestWWW = new WWW (streamingZipManifestFilePath);
			yield return manifestWWW;
			if (!string.IsNullOrEmpty (manifestWWW.error)) 
			{
				manifestWWW.Dispose ();
				yield break;
			}

			string manifestContent = System.Text.Encoding.UTF8.GetString (manifestWWW.bytes);
			manifestWWW.Dispose ();
			string[] zipFilesRelativePaths = manifestContent.Split ('\n');

			for (int i = 0, imax = zipFilesRelativePaths.Length; i < imax; i++)
			{
				string fileRelativePath = zipFilesRelativePaths [i];
				if (string.IsNullOrEmpty (fileRelativePath))
				{
					continue;
				}
				string zipFilePath = string.Concat (Application.streamingAssetsPath, "/", zipFilesRelativePaths [i]);
				WWW zipFileWWW = new WWW (zipFilePath);
				yield return zipFileWWW;

				if (!string.IsNullOrEmpty (zipFileWWW.error)) 
				{
					zipFileWWW.Dispose ();
					yield break;
				}

				byte[] zipFileContent = zipFileWWW.bytes;
				zipFileWWW.Dispose ();

				UnCompressTask uncompressTask = new UnCompressTask (zipFileContent, assetFolderPath);
				yield return new ThreadCorotineOperation (uncompressTask);
			}
		}
		else
		{
			string[] zipFilesRelativePaths = null;
			using (FileStream manifestFileStream = new FileStream (streamingZipManifestFilePath, FileMode.Open)) 
			{
				using (StreamReader reader = new StreamReader (manifestFileStream)) 
				{
					string manifestContent = reader.ReadToEnd ();
					zipFilesRelativePaths = manifestContent.Split ('\n');
					reader.Close ();
				}

				manifestFileStream.Close ();
				manifestFileStream.Dispose ();
			}

			for (int i = 0, imax = zipFilesRelativePaths.Length; i < imax; i++)
			{
				string zipFilePath = string.Concat (Application.streamingAssetsPath, "/", zipFilesRelativePaths [i]);
				UnCompressTask uncompressTask = new UnCompressTask (zipFilePath, assetFolderPath);
				yield return new ThreadCorotineOperation (uncompressTask);
			}
		}	

		//create park file
		File.Create(markFilePath);
		System.GC.Collect ();
	}

	/// <summary>
	/// yield-return this function,after yield back, you can enter game.
	/// this operation promise that all assets are AS SAME AS cdn server needed.
	/// </summary>
	public IEnumerator UpdateAssets()
	{
		string cdnBundleListFilePath = string.Concat (Application.persistentDataPath, "/", cdnBundleListFileName);
		if (File.Exists (cdnBundleListFilePath)) 
		{
			File.Delete (cdnBundleListFilePath);
		}

		//down load bundlelist zip
		HttpDownLoadTask bundleListDownLoadTask = new HttpDownLoadTask (string.Concat (cdnServerUrl, "/", cdnBundleListFileName), cdnBundleListFilePath);
		yield return new ThreadCorotineOperation (bundleListDownLoadTask, false);

		if (!string.IsNullOrEmpty (bundleListDownLoadTask.Error ())) 
		{
			yield break;
		}

		//unzip bundlelist
		UnCompressTask bundleListUnCompressTask = new UnCompressTask (cdnBundleListFilePath, string.Concat (Application.persistentDataPath, "/cdntemp"));
		yield return new ThreadCorotineOperation (bundleListUnCompressTask, false);

		if (!string.IsNullOrEmpty (bundleListUnCompressTask.Error ())) 
		{
			yield break;
		}

		bool rewriteLocalBundleList = false;
		do
		{
			//server bundlelist content
			GetBundleListTask getCDNBundleListTask = new GetBundleListTask (assetFolderPath, string.Concat (Application.persistentDataPath, "/cdntemp/", Path.GetFileNameWithoutExtension (cdnBundleListFileName), ".txt"));
			yield return new ThreadCorotineOperation (getCDNBundleListTask, false);

			if (!string.IsNullOrEmpty (getCDNBundleListTask.Error ())) 
			{
				yield break;
			}

			//local bundlelist content
			GetBundleListTask getLocalBundleListTask = new GetBundleListTask (assetFolderPath, bundleListFilePath);
			yield return new ThreadCorotineOperation (getLocalBundleListTask, false);

			if (!string.IsNullOrEmpty (getLocalBundleListTask.Error ())) 
			{
				yield break;
			}

			//compair with local
			List<ResourceInfo> cdnInfoList = DeSerializeToList(getCDNBundleListTask.result);
			Dictionary<string,ResourceInfo> localInfoMap = DeSerializeToMap(getLocalBundleListTask.result);

			List<ResourceInfo> needUpdateList = new List<ResourceInfo>();

			for(int i = 0, imax = cdnInfoList.Count; i<imax; i++)
			{
				ResourceInfo cdnInfo = cdnInfoList[i];
				ResourceInfo localInfo = null;
				if(localInfoMap.TryGetValue(cdnInfo.resourcePath, out localInfo))
				{
					//compair code
					if(string.Equals(localInfo.code,cdnInfo.code))
					{
						continue;
					}
				}
				needUpdateList.Add(cdnInfo);
				rewriteLocalBundleList = true;
			}

			//do down load
			if(needUpdateList.Count > 0)
			{
				for(int i = 0;i < needUpdateList.Count;i++)
				{
					ResourceInfo needUpdateResource = needUpdateList[i];
					string downLoadUrl = string.Concat(cdnServerUrl,"/",needUpdateResource.resourcePath);
					string storePath = string.Concat(assetFolderPath,"/",needUpdateResource.resourcePath);

					HttpDownLoadTask bundleDownLoadTask = new HttpDownLoadTask (downLoadUrl, storePath, needUpdateResource.code);
					yield return new ThreadCorotineOperation (bundleDownLoadTask, true);

					if (!string.IsNullOrEmpty (bundleDownLoadTask.Error ())) 
					{
						yield break;
					}
				}
			}

			//list regenerate,delete local and rewrite will happen in next getbundlelist task
			if(rewriteLocalBundleList)
			{
				File.Delete(bundleListFilePath);
			}
		}
		while(rewriteLocalBundleList);
	}


	private List<ResourceInfo> DeSerializeToList (string dataString)
	{
		List<ResourceInfo> returnList = new List<ResourceInfo> ();
		string[] dataStringSplit = dataString.Split ('\n');
		for (int i = dataStringSplit.Length, imax = dataStringSplit.Length - 1; i < imax; i++) 
		{
			string dataLine = dataStringSplit [i];
			if (!string.IsNullOrEmpty (dataLine)) 
			{
				string[] dataLineSplit = dataLine.Split ('|');
				ResourceInfo resourceInfo = new ResourceInfo ();
				resourceInfo.resourcePath = dataLineSplit [0];
				resourceInfo.code = dataLineSplit [1];
				returnList.Add (resourceInfo);
			}
		}
		return returnList;
	}

	private Dictionary<string,ResourceInfo> DeSerializeToMap (string dataString)
	{
		Dictionary<string,ResourceInfo> returnMap = new Dictionary<string,ResourceInfo> ();
		string[] dataStringSplit = dataString.Split ('\n');
		for (int i = dataStringSplit.Length, imax = dataStringSplit.Length - 1; i < imax; i++) 
		{
			string dataLine = dataStringSplit [i];
			if (!string.IsNullOrEmpty (dataLine)) 
			{
				string[] dataLineSplit = dataLine.Split ('|');
				ResourceInfo resourceInfo = new ResourceInfo ();
				resourceInfo.resourcePath = dataLineSplit [0];
				resourceInfo.code = dataLineSplit [1];
				returnMap.Add (resourceInfo.resourcePath, resourceInfo);
			}
		}
		return returnMap;
	}
}