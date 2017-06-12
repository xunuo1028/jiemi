/// <summary>
/// FileName:    AssetBundleTool
/// Author:      JackalLiu
/// CreateTime:  4/10/2017 5:02:10 PM
/// Description: Assetbundle dependence collector and settings. build assetbundle under current platform
/// </summary>

using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;

public class AssetBundleSetting
{
	public static EditorString androidBundleBuildPath = new EditorString ("AssetBundleSetting.AndroidBundleBuildPath");
	public static EditorString iosBundleBuildPath = new EditorString ("AssetBundleSetting.iosBundleBuildPath");
	public static EditorString winBundleBuildPath = new EditorString ("AssetBundleSetting.winBundleBuildPath");

	public static EditorString luaFilePath = new EditorString ("AssetBundleSetting.luaFilePath");
	public static EditorString luajitExePath = new EditorString ("AssetBundleSetting.luajitExePath");

	public static EditorString resourcePrefabPath = new EditorString ("AssetBundleSetting.resourcePrefabPath");
	public static EditorString uiPrefabPath = new EditorString ("AssetBundleSetting.uiPrefabPath");

	public static EditorFloat maxStreamingZipMB = new EditorFloat ("AssetBundleSetting.maxStreamingZipMB");

	[PreferenceItem("AssetBundle")]
	static void OnPreferenceGUI()
	{
		
		///export paths
		EditorGUILayout.BeginVertical (EditorStyles.helpBox);
		EditorGUILayout.BeginHorizontal ();
		#if UNITY_ANDROID
		EditorGUILayout.Toggle (true, EditorStyles.radioButton, GUILayout.Width (10));
		#else
		EditorGUILayout.Toggle (false, EditorStyles.radioButton, GUILayout.Width (10));
		#endif
		GUILayout.Label ("android bundles:",GUILayout.Width(100));
		androidBundleBuildPath.Value = EditorGUILayout.TextField (androidBundleBuildPath.Value);
		if (GUILayout.Button ("..",EditorStyles.miniButton)) 
		{
			androidBundleBuildPath.Value = EditorUtility.OpenFolderPanel ("Select your android bundle build place:)", Application.persistentDataPath, "Android");
		}
		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal ();
		#if UNITY_IPHONE
		EditorGUILayout.Toggle (true, EditorStyles.radioButton, GUILayout.Width (10));
		#else
		EditorGUILayout.Toggle (false, EditorStyles.radioButton, GUILayout.Width (10));
		#endif
		GUILayout.Label ("ios bundles:",GUILayout.Width(100));
		iosBundleBuildPath.Value = EditorGUILayout.TextField (iosBundleBuildPath.Value);
		if (GUILayout.Button ("..",EditorStyles.miniButton)) 
		{
			iosBundleBuildPath.Value = EditorUtility.OpenFolderPanel ("Select your ios bundle build place:)", Application.persistentDataPath, "iOS");
		}
		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal ();
		#if UNITY_STANDALONE
		EditorGUILayout.Toggle (true, EditorStyles.radioButton, GUILayout.Width (10));
		#else
		EditorGUILayout.Toggle (false, EditorStyles.radioButton, GUILayout.Width (10));
		#endif
		GUILayout.Label ("win bundles:",GUILayout.Width(100));
		winBundleBuildPath.Value = EditorGUILayout.TextField (winBundleBuildPath.Value);
		if (GUILayout.Button ("..",EditorStyles.miniButton)) 
		{
			winBundleBuildPath.Value = EditorUtility.OpenFolderPanel ("Select your win bundle build place:)", Application.persistentDataPath, "Win");
		}
		EditorGUILayout.EndHorizontal ();
		EditorGUILayout.EndVertical ();

		//lua paths
		EditorGUILayout.BeginVertical (EditorStyles.helpBox);
		EditorGUILayout.BeginHorizontal ();
		GUILayout.Label ("lua files:",GUILayout.Width(100));
		luaFilePath.Value = EditorGUILayout.TextField (luaFilePath.Value);
		if (GUILayout.Button ("..",EditorStyles.miniButton)) 
		{
			luaFilePath.Value = EditorUtility.OpenFolderPanel ("Select your lua files build place:)", Application.dataPath, "Lua");
		}
		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal ();
		GUILayout.Label ("Encoder.exe:",GUILayout.Width(100));
		luajitExePath.Value = EditorGUILayout.TextField (luajitExePath.Value);
		if (GUILayout.Button ("..",EditorStyles.miniButton)) 
		{
			luajitExePath.Value = EditorUtility.OpenFilePanelWithFilters ("Select your LuaJitEncoder.exe build place:)", Application.dataPath.Replace("Assets","LuaEncoder"), new string[2]{ "*","exe" });
		}
		EditorGUILayout.EndHorizontal ();
		EditorGUILayout.EndVertical ();

		//resource in project
		EditorGUILayout.BeginVertical (EditorStyles.helpBox);
		EditorGUILayout.BeginHorizontal ();
		GUILayout.Label ("model prefabs:",GUILayout.Width(100));
		resourcePrefabPath.Value = EditorGUILayout.TextField (resourcePrefabPath.Value);
		if (GUILayout.Button ("..",EditorStyles.miniButton)) 
		{
			resourcePrefabPath.Value = EditorUtility.OpenFolderPanel ("Select your model prefabs place:)", Application.dataPath, "Prefabs");
		}
		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal ();
		GUILayout.Label ("ui prefabs:",GUILayout.Width(100));
		uiPrefabPath.Value = EditorGUILayout.TextField (uiPrefabPath.Value);
		if (GUILayout.Button ("..",EditorStyles.miniButton)) 
		{
			uiPrefabPath.Value = EditorUtility.OpenFolderPanel ("Select your ui prefabs place:)", Application.dataPath, "UI");
		}
		EditorGUILayout.EndHorizontal ();
		EditorGUILayout.EndVertical ();

		//zips operation
		EditorGUILayout.BeginVertical (EditorStyles.helpBox);
		EditorGUILayout.BeginHorizontal ();
		GUILayout.Label ("Max zip size(MB):",GUILayout.Width(100));
		maxStreamingZipMB.Value = EditorGUILayout.Slider (maxStreamingZipMB.Value, 2f, 100f);
		EditorGUILayout.EndHorizontal ();
		EditorGUILayout.EndVertical ();

		//buttons
		EditorGUILayout.BeginVertical (EditorStyles.helpBox);
		if (GUILayout.Button ("OpenBundleFolder")) {DoOpenBundleFolder ();}
		if (GUILayout.Button ("GenerateBundleListFile(asset.bytes)")) {DoGenerateBundleListFile ();}
		if (GUILayout.Button ("GenerateBundleZipFiles(assets_0.bytes..assets_1.bytes..)")) {DoGenerateStreamingAssetZips ();}
		EditorGUILayout.EndVertical ();
	}

	private static void DoOpenBundleFolder()
	{
		string bundlesFolderPath = string.Empty;
		#if UNITY_IPhone
		bundlesFolderPath = iosBundleBuildPath.Value;
		#elif UNITY_ANDROID
		bundlesFolderPath = androidBundleBuildPath.Value;
		#else
		bundlesFolderPath = winBundleBuildPath.Value;
		#endif

		if (string.IsNullOrEmpty (bundlesFolderPath)) 
		{
			EditorUtility.DisplayDialog ("Open bundle folder failed.", "Given bundle folder path is empty.", "ok");
		}
		else
		{
			EditorUtility.OpenWithDefaultApp (bundlesFolderPath);
		}	
	}

	private static void DoGenerateBundleListFile()
	{
		string bundlesFolderPath = string.Empty;
		#if UNITY_IPhone
		bundlesFolderPath = iosBundleBuildPath.Value;
		#elif UNITY_ANDROID
		bundlesFolderPath = androidBundleBuildPath.Value;
		#else
		bundlesFolderPath = winBundleBuildPath.Value;
		#endif

		AssetBundleTool.GenerateBundleListFile (bundlesFolderPath, string.Concat (bundlesFolderPath, "/assets.bytes"));
	}

	private static void DoGenerateStreamingAssetZips()
	{
		string bundlesFolderPath = string.Empty;
		#if UNITY_IPhone
		bundlesFolderPath = iosBundleBuildPath.Value;
		#elif UNITY_ANDROID
		bundlesFolderPath = androidBundleBuildPath.Value;
		#else
		bundlesFolderPath = winBundleBuildPath.Value;
		#endif
		if (!Directory.Exists (Application.streamingAssetsPath))
		{
			Directory.CreateDirectory (Application.streamingAssetsPath);
		}
		AssetBundleTool.GenerateStreamingAssetZips (bundlesFolderPath, Application.streamingAssetsPath, "assets_{0}.bytes", (int)(maxStreamingZipMB.Value * 1024 * 1024));
	}
}


public class AssetBundleTool 
{
	public static void GenerateBundleListFile(string bundlesFolderPath, string filePath)
	{
		Stopwatch stopWatch = new Stopwatch ();
		stopWatch.Start ();

		if (!Directory.Exists (bundlesFolderPath)) 
		{
			Directory.CreateDirectory (bundlesFolderPath);
		}

		DirectoryInfo rootFolder = new DirectoryInfo (bundlesFolderPath);

		List<FileInfo> fileList = new List<FileInfo> ();
		GetAllFilesInFolder (rootFolder, ref fileList);

		int folderLength = bundlesFolderPath.Length + 1;
		string genenrateContent = string.Empty;

		EditorUtility.DisplayProgressBar ("Generate asset.bytes (bundle list file)", "", 0);
		for (int i = 0, imax = fileList.Count; i < imax; i++) 
		{
			FileInfo file = fileList [i];

			if (file.Name == Path.GetFileName (filePath)) 
			{
				continue;
			}

			string absoluteFileName = file.FullName.Replace ("\\", "/");
			absoluteFileName = absoluteFileName.Substring (folderLength, absoluteFileName.Length - folderLength);

			genenrateContent = string.Concat (genenrateContent, absoluteFileName, "|");

			using (FileStream fileStream = new FileStream (file.FullName, FileMode.Open)) 
			{
				using (BinaryReader reader = new BinaryReader (fileStream))
				{
					byte[] contentBytes = reader.ReadBytes ((int)fileStream.Length);
					genenrateContent = string.Concat (genenrateContent, SecurityUtils.GetCRC32 (contentBytes), "\n");
					reader.Close ();
				}
				fileStream.Close ();
				fileStream.Dispose ();
			}

			if (i % 50 == 0) 
			{
				EditorUtility.DisplayProgressBar ("Generate asset.bytes (bundle list file)", string.Format ("Processing file : {0},({1}/{2})", absoluteFileName, i + 1, imax + 1), (float)i / (float)imax);
			}
		}
		EditorUtility.DisplayProgressBar ("Generate asset.bytes (bundle list file)", string.Format ("Flush to file : {0}", filePath), 1f);
		genenrateContent = string.Concat (genenrateContent, "#END#");

		if (File.Exists (filePath)) 
		{
			File.Delete (filePath);
		}

		using (FileStream fileStream = new FileStream (filePath, FileMode.CreateNew, FileAccess.ReadWrite))
		{
			using (StreamWriter writer = new StreamWriter (fileStream, new System.Text.UTF8Encoding (false))) 
			{
				writer.Write (genenrateContent);
				writer.Flush ();
				writer.Close ();
			}
			fileStream.Close ();
			fileStream.Dispose ();
		}
		EditorUtility.ClearProgressBar ();
		stopWatch.Stop ();

		if (EditorUtility.DisplayDialog ("Generate asset.bytes (bundle list file)", string.Format ("Cost {0}s to generate.Open generated file?", (float)stopWatch.ElapsedMilliseconds * 0.001f), "ok"))
		{
			EditorUtility.OpenWithDefaultApp (bundlesFolderPath);
		}
	}

	public static void GenerateStreamingAssetZips (string bundlesFolderPath,string outPutZipFolder,string outPutZipNameFormat, int maxZipSize)
	{
		Stopwatch stopWatch = new Stopwatch ();
		stopWatch.Start ();

		if (!Directory.Exists (bundlesFolderPath)) 
		{
			Directory.CreateDirectory (bundlesFolderPath);
		}

		DirectoryInfo rootFolder = new DirectoryInfo (bundlesFolderPath);

		List<FileInfo> fileList = new List<FileInfo> ();
		GetAllFilesInFolder (rootFolder, ref fileList);

		List<List<FileInfo>> fileGroups= new List<List<FileInfo>> ();
		fileGroups.Add (new List<FileInfo>());

		int groupIndex = 0;
		int fullGroupSize = 0;

		string outPutZipPathFormat = string.Concat (outPutZipFolder, "/", outPutZipNameFormat);

		EditorUtility.DisplayProgressBar ("Generate assets zips (splited zip files)", "Making files groups.", 0);
		for (int i = 0, imax = fileList.Count; i < imax; i++) 
		{
			FileInfo file = fileList [i];
			List<FileInfo> fileGourp = fileGroups [groupIndex];
			fileGourp.Add (file);
			fullGroupSize += (int)file.Length;

			if (fullGroupSize > maxZipSize) 
			{
				fullGroupSize = 0;
				fileGourp = new List<FileInfo> ();
				fileGroups.Add (fileGourp);
				groupIndex++;
			}
		}

		string zipManifestContent = string.Empty;
		int bunldesFolderLength = bundlesFolderPath.Length + 1;

		int process = 1;
		//dozip
		for (int i = 0, imax = fileGroups.Count; i < imax; i++) 
		{
			string zipFilePath = string.Format (outPutZipPathFormat, i);
			zipManifestContent += zipFilePath.Substring (outPutZipFolder.Length + 1, zipFilePath.Length - outPutZipFolder.Length - 1) + "\n";
			List<FileInfo> fileGourp = fileGroups [i];

			if (File.Exists (zipFilePath)) 
			{
				File.Delete (zipFilePath);
			}

			using (FileStream fileStream = new FileStream (zipFilePath, FileMode.CreateNew)) 
			{
				using (ZipOutputStream zipStream = new ZipOutputStream (fileStream)) 
				{
					zipStream.SetLevel (6);
					for (int j = 0, jmax = fileGourp.Count; j < jmax; j++) 
					{
						FileInfo file = fileGourp [j];

						if (process % 20 == 0) 
						{
							EditorUtility.DisplayProgressBar ("Generate assets zips (splited zip files)", string.Format ("processing : {0} ({1}/{2})", file.FullName, process, fileList.Count), (float)process / (float)fileList.Count);
						}
						process++;

						byte[] contentBytes = new byte[(int)file.Length];
						file.OpenRead ().Read (contentBytes, 0, contentBytes.Length);
						ZipEntry zipEntry = new ZipEntry (file.FullName.Substring (bunldesFolderLength, file.FullName.Length - bunldesFolderLength));
						zipStream.PutNextEntry (zipEntry);
						zipStream.Flush ();
						zipStream.Write (contentBytes, 0, contentBytes.Length);
						zipStream.Flush ();
					}
					zipStream.Close ();
				}
				fileStream.Close ();
				fileStream.Dispose ();
			}
		}

		//create zipmanifest
		string zipFilesManifestPath = outPutZipPathFormat.Replace (Path.GetFileName (outPutZipPathFormat), "assetsmanifest.bytes");
		if (File.Exists (zipFilesManifestPath))
		{
			File.Delete (zipFilesManifestPath);
		}


		using (FileStream fileStream = new FileStream (zipFilesManifestPath, FileMode.CreateNew)) 
		{
			using (StreamWriter writer = new StreamWriter (fileStream,new System.Text.UTF8Encoding(false))) 
			{
				writer.Write (zipManifestContent);
				writer.Flush ();
				writer.Close ();
			}
			fileStream.Close ();
			fileStream.Dispose ();
		}

		EditorUtility.ClearProgressBar ();

		if (EditorUtility.DisplayDialog ("Generate assets zips (splited zip files)", string.Format ("Cost {0}s to generate.Open generated file?", (float)stopWatch.ElapsedMilliseconds * 0.001f), "ok"))
		{
			EditorUtility.OpenWithDefaultApp (outPutZipFolder);
		}
	}
		
	private static void GetAllFilesInFolder (DirectoryInfo rootFolder, ref List<FileInfo> fileList)
	{
		fileList.AddRange (rootFolder.GetFiles());

		DirectoryInfo[] subFolders = rootFolder.GetDirectories ();
		for (int i = 0, imax = subFolders.Length; i < imax; i++)
		{
			DirectoryInfo subFolder = subFolders [i];
			GetAllFilesInFolder (subFolder, ref fileList);
		}
	}
}