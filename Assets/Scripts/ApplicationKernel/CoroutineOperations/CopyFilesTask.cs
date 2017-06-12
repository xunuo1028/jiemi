/// <summary>
/// FileName:    CopyFilesTask
/// Author:      JackalLiu
/// CreateTime:  4/12/2017 12:15:50 PM
/// Description: this task can copy files in threading work :)
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CopyFilesTask : IThreadCorotineable 
{
	private Dictionary<string,string> fromToMap;

	private string error;
	private bool isFinished;


	public CopyFilesTask (Dictionary<string,string> fromToMap)
	{
		this.fromToMap = fromToMap;
	}

	public void Excute()
	{
		try
		{
			Dictionary<string,string>.Enumerator enumerator = fromToMap.GetEnumerator();
			while(enumerator.MoveNext())
			{
				KeyValuePair<string,string> de = enumerator.Current;
				string fromPath = de.Key;
				string toPath = de.Value;
				System.IO.File.Move(fromPath,toPath);
			}
		}
		catch(System.Exception e) 
		{
			error = e.Message;
		}
		finally 
		{
			isFinished = true;
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