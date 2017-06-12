using System.Collections;
using System.Collections.Generic;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using System.Text;

/// <summary>
/// ex:
/// UnCompressTask task = new UnCompressTask();
/// ThreadCorotineOperation operation = new ThreadCorotineOperation(task.Excute(),true);
/// yield return operation;
/// task is finished and add your script...
/// </summary>
public class UnCompressTask : IThreadCorotineable
{
	public readonly string zipFilePath;
	public readonly byte[] zipFileBytes;
	public string releaseFolderPath{ get; private set;}
	public readonly string password;

	public int doneBytes {get;private set;}
	public int totalBytes {get;private set;}
	public string error {get;private set;}

	public bool finished {get;private set;}

	private bool bytemode = false;

	public UnCompressTask (string zipFilePath, string releaseFolderPath, string password = "")
	{
		this.zipFilePath = zipFilePath;
		this.releaseFolderPath = releaseFolderPath;
		this.password = password;
		this.bytemode = false;
	}

	public UnCompressTask (byte[] zipFileBytes, string releaseFolderPath, string password = "")
	{
		this.zipFileBytes = zipFileBytes;
		this.releaseFolderPath = releaseFolderPath;
		this.password = password;
		this.bytemode = true;
	}

	public void Excute()
	{
		try
		{
			if(!bytemode)
			{
				ExcuteFileMode();
			}
			else
			{
				ExcuteByteMode();
			}
		}
		catch(System.Exception e)
		{
			error = e.ToString ();
		}
		finally
		{
			finished = true;
		}
	}
	private void ExcuteByteMode()
	{
		using(MemoryStream zipFileStream = new MemoryStream(zipFileBytes))
		{
			UnZip (zipFileStream);
			zipFileStream.Close();
			zipFileStream.Dispose();
		}
	}

	private void ExcuteFileMode()
	{
		if (!File.Exists (zipFilePath))
		{
			throw new System.Exception(string.Format ("zip file : {0} not found.", zipFilePath));
		}
			
		using(FileStream zipFileStream = new FileStream(zipFilePath,FileMode.Open,FileAccess.Read))
		{
			UnZip (zipFileStream);
			zipFileStream.Close();
			zipFileStream.Dispose();
		}
	}


	private void UnZip(Stream stream)
	{
		using(ZipInputStream zipStream = new ZipInputStream(stream))
		{
			releaseFolderPath = string.IsNullOrEmpty(releaseFolderPath)?zipFilePath.Replace(Path.GetFileName(zipFilePath),""):releaseFolderPath;

			//prepair path
			string rootFolderName = releaseFolderPath;
			if (!Directory.Exists (rootFolderName))
			{
				Directory.CreateDirectory (rootFolderName);
			}

			//totalBytes = (int) zipStream.Length;
			zipStream.Password = password;
			ZipEntry entry;
			while ((entry = zipStream.GetNextEntry ()) != null)
			{
				if(entry.IsDirectory)
				{
					string folderName = Path.GetDirectoryName (entry.Name);
					string fullFolderName = ApplicationRoute.ChangeSeparator (string.Concat (rootFolderName, "/", folderName));
					if (!string.IsNullOrEmpty (folderName) && !Directory.Exists(fullFolderName))
					{
						Directory.CreateDirectory (fullFolderName);
					}
				}
				else
				{
					string fullFileName = ApplicationRoute.ChangeSeparator (string.Concat (rootFolderName, "/", entry.Name));
					if (File.Exists (fullFileName))
					{
						File.Delete (fullFileName);
					}

					string fileDirectory = Path.GetDirectoryName (fullFileName);
					if (!Directory.Exists (fileDirectory)) 
					{
						Directory.CreateDirectory (fileDirectory);
					}

					using (FileStream fileStream = new FileStream (fullFileName, FileMode.Create, FileAccess.ReadWrite))
					{
						int size = 2048;
						byte[] buffer = new byte[size];
						do
						{
							size = zipStream.Read(buffer,0,buffer.Length);
							fileStream.Write(buffer,0,size);
							doneBytes += size;
						} while(size > 0);
						fileStream.Flush ();
						fileStream.Close ();
					}
				}
			}
			zipStream.Close ();
			zipStream.Dispose ();
		}
	}

	public bool IsFinished()
	{
		return finished;
	}

	public string Error()
	{
		return error;
	}
}
