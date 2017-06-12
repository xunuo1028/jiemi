/// <summary>
/// FileName:    GetLocalBundleListTask
/// Author:      JackalLiu
/// CreateTime:  4/10/2017 8:06:17 PM
/// Description:
/// </summary>

using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;


public class GetBundleListTask : IThreadCorotineable 
{
	public readonly string bundlesFolderPath = string.Empty;
	public readonly string bundleListFilePath = string.Empty;

	public string result{ get; private set;}
	public long costMillSeconds{ get; private set;}

	private string error;
	private bool isFinished;

	private Stopwatch stopWatch = null;

	public GetBundleListTask (string bundlesFolderPath, string bundleListFilePath)
	{
		this.bundlesFolderPath = string.IsNullOrEmpty (bundlesFolderPath) ? Application.persistentDataPath + "/assets" : bundlesFolderPath;
		this.bundleListFilePath = string.IsNullOrEmpty (bundleListFilePath) ? Application.persistentDataPath + "/assets/assets.bytes" : bundleListFilePath;
		stopWatch = new Stopwatch ();
	}

	public void Excute()
	{
		stopWatch.Start ();
		try
		{
			bool rebuildAssetFile = true;

			if (File.Exists (bundleListFilePath)) 
			{
				using (FileStream fileStream = new FileStream (bundleListFilePath, FileMode.Open)) 
				{
					using (StreamReader reader = new StreamReader (fileStream))
					{
						result = reader.ReadToEnd ();
						rebuildAssetFile = !result.EndsWith ("#END#");
						reader.Close ();
					}
					fileStream.Close ();
					fileStream.Dispose ();
				}
			}

			if (rebuildAssetFile) 
			{
				if (File.Exists (bundleListFilePath)) 
				{
					File.Delete (bundleListFilePath);
				}

				DirectoryInfo rootFolder = null;
				if (!Directory.Exists (bundlesFolderPath)) 
				{
					rootFolder = Directory.CreateDirectory (bundlesFolderPath);
				}
				else
				{
					rootFolder = new DirectoryInfo (bundlesFolderPath);
				}

				List<FileInfo> fileList = new List<FileInfo> ();
				GetAllFilesInFolder (rootFolder, ref fileList);

				int folderLength = bundlesFolderPath.Length;
				string genenrateContent = string.Empty;
				for (int i = 0, imax = fileList.Count; i < imax; i++) 
				{
					FileInfo file = fileList [i];
					string absoluteFileName = file.FullName;
					absoluteFileName = absoluteFileName.Substring (folderLength + 1, absoluteFileName.Length - folderLength - 1);

					genenrateContent = string.Concat (genenrateContent, absoluteFileName, "|",SecurityUtils.GetCRC32 (file.FullName), "\n");
				}
				genenrateContent = string.Concat (genenrateContent, "#END#");

				using (FileStream fileStream = new FileStream (bundleListFilePath, FileMode.CreateNew, FileAccess.ReadWrite))
				{
					using (StreamWriter writer = new StreamWriter (fileStream)) 
					{
						writer.Write (genenrateContent);
						writer.Flush ();
						writer.Close ();
					}
					fileStream.Close ();
					fileStream.Dispose ();
				}

				result = genenrateContent;
			}	
		}
		catch(System.Exception e) 
		{
			error = e.Message;
		}
		finally 
		{
			System.GC.Collect ();
			stopWatch.Stop ();
			costMillSeconds = stopWatch.ElapsedMilliseconds;
			isFinished = true;
		}
	}

	private void GetAllFilesInFolder (DirectoryInfo rootFolder, ref List<FileInfo> fileList)
	{
		fileList.AddRange (rootFolder.GetFiles());

		DirectoryInfo[] subFolders = rootFolder.GetDirectories ();
		for (int i = 0, imax = subFolders.Length; i < imax; i++)
		{
			DirectoryInfo subFolder = subFolders [i];
			GetAllFilesInFolder (subFolder, ref fileList);
		}
	}

	public bool IsFinished()
	{
		return isFinished;
	}

	public string Error()
	{
		return error;
	}
}