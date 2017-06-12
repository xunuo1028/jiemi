/// <summary>
/// FileName:    HttpDownLoadTask
/// Author:      JackalLiu
/// CreateTime:  4/11/2017 4:33:08 PM
/// Description: cdn file download task
/// </summary>

using UnityEngine;
using System;
using System.Net;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class HttpDownLoadTask : IThreadCorotineable 
{
	public readonly string downLoadUrl;
	public readonly string storePath;
	public readonly string fileCRC32;

	public int downBytes { get; private set;}
	public int totalBytes { get; private set;}

	private string error;
	private bool isFinished;

	public HttpDownLoadTask (string downLoadUrl, string storePath, string fileCRC32 = "")
	{
		this.downLoadUrl = downLoadUrl;
		this.storePath = storePath;
		this.fileCRC32 = fileCRC32;
	}

	public void Excute()
	{
		HttpWebRequest request = null;
		HttpWebResponse response = null;

		HttpWebRequest downLoadRequest = null;
		HttpWebResponse downLoadResponse = null;

		bool needReTry = false;
		int retryTimes = 3;
		try
		{
			do
			{
				//check crc
				if(!string.IsNullOrEmpty(fileCRC32) && string.Equals(SecurityUtils.GetCRC32(storePath),fileCRC32))
				{
					//every thing is ok. finish
					break;
				}

				if(string.IsNullOrEmpty(storePath))
				{
					throw new IOException("give an empty store path");
				}

				// get total length
				request = (HttpWebRequest)(WebRequest.Create(new System.Uri(downLoadUrl)));
				response = (HttpWebResponse)(request.GetResponse());

				int fileLength = (int) response.ContentLength;
				totalBytes = fileLength;

				//get exist file length

				int taskLength = totalBytes;
				if(File.Exists(storePath))
				{
					using(FileStream fileStream = new FileStream(storePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
					{
						downBytes = (int) fileStream.Length;
						taskLength = totalBytes - downBytes;
						fileStream.Close();
						fileStream.Dispose();
					}	
				}

				//real downlaod
				downLoadRequest = (HttpWebRequest)(WebRequest.Create(new System.Uri(downLoadUrl)));
				if(taskLength > 0)
				{
					downLoadRequest.AddRange(downBytes, downBytes + taskLength);
					downLoadResponse = (HttpWebResponse)downLoadRequest.GetResponse();

					byte[] buffer = new byte[taskLength];
					int bufferSrcOffset = 0;
					int bufferOffset = 0;

					using(Stream stream = downLoadResponse.GetResponseStream())
					{
						int downLoadLengthInLoop = 0;
						int currentTaskLength = taskLength;
						do
						{
							downLoadLengthInLoop = stream.Read(buffer, bufferSrcOffset, taskLength - bufferOffset);
							bufferOffset += downLoadLengthInLoop;
							if (downLoadLengthInLoop > 0)
							{
								byte[] bufferInLoop = new byte[downLoadLengthInLoop];
								Buffer.BlockCopy(buffer, bufferSrcOffset, bufferInLoop, 0, downLoadLengthInLoop);
								using (FileStream writeFileStream = new FileStream(storePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
								{
									writeFileStream.Position = downBytes;
									writeFileStream.Write(bufferInLoop, 0, downLoadLengthInLoop);
									writeFileStream.Close();
								}
								bufferSrcOffset += downLoadLengthInLoop;
								downBytes += downLoadLengthInLoop;
							}
						} while(downLoadLengthInLoop != 0);
					}
				}

				if(!string.IsNullOrEmpty(fileCRC32) && !string.Equals(SecurityUtils.GetCRC32(storePath),fileCRC32))
				{
					if(retryTimes > 0)
					{
						retryTimes--;
						needReTry = true;

						if(File.Exists(storePath))
						{
							File.Delete(storePath);
						}

						if (response != null) 
						{
							response.Close ();
						}

						if (request != null) 
						{
							request.Abort ();
						}

						if (downLoadResponse != null) 
						{
							downLoadResponse.Close ();
						}

						if (downLoadRequest != null) 
						{
							downLoadRequest.Abort ();
						}
					}
					else
					{
						throw new Exception(string.Format("Given filePath:{0} GivenCRC32:{1} are not matched.we have retryed 3 times.",storePath,fileCRC32));
					}
				}

			}while(needReTry);

		}
		catch(System.Exception e) 
		{
			error = e.Message;
		}
		finally 
		{
			if (response != null) 
			{
				response.Close ();
			}

			if (request != null) 
			{
				request.Abort ();
			}

			if (downLoadResponse != null) 
			{
				downLoadResponse.Close ();
			}

			if (downLoadRequest != null) 
			{
				downLoadRequest.Abort ();
			}

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