/// <summary>
/// FileName:    ApplicationRoute
/// Author:      JackalLiu
/// CreateTime:  4/12/2017 4:35:45 PM
/// Description:
/// </summary>

using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// FORCE RULE:
/// 1.given folder have no last '/'
/// 2.file name have no first '/'
/// 3.file name force to lower case
/// 
/// folder map:
/// 1.persistentDataPath
/// 	1./runtime - runtimegenerated apart from win/android/ios folders which are generate by editor(only on pc platform can see this) AND pc will do hot-update operation as well in this folder
/// 		1./bundles - bundles
/// 		2./codes - lua scripts
/// 		3./skillconfigs - skill editor generations
/// 		4./metadata - designers' excell data
/// 		5./log - runtime write log
/// 		6.assetslist.bytes - file & CRC32 record map. End with "#END#"
/// 2.streamingDataPath
/// 	1./assets - bundles zip files
/// 		1.assetsmanifest.bytes - all bundle zip list
/// </summary>
public class ApplicationRoute 
{
	public static string filePathSeparator  { get;private set;}

	#region persistant-path folders
	public static string persistentFolder { get;private set;}
	public static string assetFolder { get;private set;}
	public static string assetBundlesFolder { get;private set;}
	public static string luaScriptFolder { get;private set;}
	public static string skillConfigFolder { get;private set;}
	public static string metaDataFolder { get;private set;}
	public static string logFolder { get;private set;}
	public static string assetListFilePath { get;private set;}
	#endregion

	#region streaming-path folders
	public static string streamingZipManifestFilePath { get;private set;}
	#endregion

	public static void Initalize()
	{
		filePathSeparator = "/";

		persistentFolder = Application.persistentDataPath;
		TryCreateFolder (assetFolder = string.Concat (persistentFolder, "/runtime").Replace("\\","/"));
		TryCreateFolder (assetBundlesFolder = string.Concat (persistentFolder, "/runtime/bundles").Replace("\\","/"));
		TryCreateFolder (luaScriptFolder = string.Concat (persistentFolder, "/runtime/codes").Replace("\\","/"));
		TryCreateFolder (skillConfigFolder = string.Concat (persistentFolder, "/runtime/skillconfigs").Replace("\\","/"));
		TryCreateFolder (metaDataFolder = string.Concat (persistentFolder, "/runtime/metadata").Replace("\\","/"));
		TryCreateFolder (logFolder = string.Concat (persistentFolder, "/runtime/log").Replace("\\","/"));
		assetListFilePath = string.Concat (persistentFolder, "/runtime/assetslist.bytes").Replace("\\","/"); 
		streamingZipManifestFilePath = string.Concat (Application.streamingAssetsPath, "/assetsmanifest.bytes").Replace("\\","/");
	}

	private static void TryCreateFolder(string folder)
	{
		if (!Directory.Exists (folder)) 
		{
			Directory.CreateDirectory (folder);
		}
	}

	public static string ChangeSeparator(string givenString)
	{
		return givenString.Replace('\\','/');
	}
}