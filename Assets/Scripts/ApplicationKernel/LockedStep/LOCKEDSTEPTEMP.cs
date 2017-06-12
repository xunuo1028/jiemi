/// <summary>
/// FileName:    LOCKEDSTEPTEMP
/// Author:      JackalLiu
/// CreateTime:  4/12/2017 5:07:59 PM
/// Description:
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LOCKEDSTEPTEMP : MonoBehaviour 
{
	void Start () 
	{
		ApplicationRoute.Initalize ();
		StartCoroutine (ExtractPipeLine ());
	}

	IEnumerator ExtractPipeLine()
	{
		ApplicationUpdater updater = new ApplicationUpdater ();
		updater.streamingZipManifestFilePath = ApplicationRoute.streamingZipManifestFilePath;
		updater.assetFolderPath = ApplicationRoute.assetBundlesFolder;
		yield return updater.FirstExtract ();
		Debug.Log ("Extract ok");

		GetBundleListTask task = new GetBundleListTask (ApplicationRoute.assetBundlesFolder, ApplicationRoute.assetListFilePath);
		yield return new ThreadCorotineOperation (task);
		Debug.Log (task.result);
	}

	void Update()
	{
		transform.eulerAngles += Vector3.up * 5f * Time.deltaTime;
	}

	void OnApplicationQuit()
	{
		ThreadCorotineOperation.DisposeAllThreads ();
	}
}