/// <summary>
/// FileName:    DebugUtils
/// Author:      LiuTaiyan
/// CreateTime:  4/6/2017 2:23:37 PM
/// Description:
/// </summary>

using UnityEngine;

using System.IO;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class DebugUtils 
{
	public enum CallSource
	{
		CSharp,
		Lua
	}

	public enum Level
	{
		EveryLevel,
		Info,
		Warning,
		Error,
		Assert,
		None
	}

	private class BaseLogInfo
	{
		public const string MAIN_LOG_FORMAT = "[{0}]{1}(@frame{2})\n";
		public const string STACK_LOG_FORMAT = "{1} @line {2} in {0}\n";

		public string logContent = string.Empty;
		public int callFrameNum = 0;
		public Level level = Level.None;

		protected string MainLogString()
		{
			return string.Format (MAIN_LOG_FORMAT, level.ToString (), logContent, callFrameNum);
		}

		protected string StackInfomationString (string fileName, string functionName, string line)
		{
			return string.Format (STACK_LOG_FORMAT, fileName, functionName, line);
		}
	}

	private class CSharpLogInfo : BaseLogInfo
	{
		public StackTrace callStack = null;

		public override string ToString ()
		{
			return string.Concat ("[ C# ]",MainLogString (), CallStackToString (callStack));
		}

		private string CallStackToString(StackTrace stack)
		{
			string returnString = string.Empty;
			if (stack != null)
			{
				returnString = "\tStackTrace:\n";
				string tabHead = "\t\t\t";
				for (int i = 1, imax = callStack.FrameCount; i < imax; i++)
				{
					returnString = string.Concat (returnString, tabHead, StackFrameToString (callStack.GetFrame (i)));
					tabHead = string.Concat (tabHead, "\t");
				}
			}
			return returnString;
		}

		private string StackFrameToString(StackFrame stackFrame)
		{
			return StackInfomationString (Path.GetFileName (stackFrame.GetFileName ()), stackFrame.GetMethod ().ToString (), stackFrame.GetFileLineNumber ().ToString());
		}
	}

	private class LuaLogInfo : BaseLogInfo
	{
		private const string LUATAIL = ".lua";
		public string luaStack = null;

		public override string ToString ()
		{
			return string.Concat ("[ulua]",MainLogString (), CallStackToString (luaStack));
		}

		private string CallStackToString(string stack)
		{
			string returnString = string.Empty;
			if (!string.IsNullOrEmpty(luaStack))
			{
				returnString = "\tStackTrace:\n";
				string tabHead = "\t\t\t";
				string[] stackFrames = stack.Split ('\n');

				for (int i = 1, imax = stackFrames.Length; i < imax; i++)
				{
					returnString = string.Concat (returnString, tabHead, StackFrameToString (stackFrames[i]));
					tabHead = string.Concat (tabHead, "\t");
				}
			}
			return returnString;
		}

		//given example :"29|[string \"TestErrorStack\"]|bt"
		private string StackFrameToString(string stackFrame)
		{
			string[] stackFrameSplit = stackFrame.Split ('|');
			string line = stackFrameSplit[0];
			string fileName = stackFrameSplit[1];
			fileName = fileName.Substring (9, fileName.Length - 11);
			fileName = string.Concat (fileName, LUATAIL);
			string functionName = stackFrameSplit[2];
			return StackInfomationString (fileName, functionName, line);
		}
	}

	public static Dictionary<int,string> moduelMap = new Dictionary<int,string> ();

	public static bool printToConsole = false;
	public static bool printToFile = false;
	public static string targetFilePath = string.Empty;
	public static Level printCallStackLevel = Level.None;

	private static Thread writeLogThread = null;
	private static Queue writeContentQueue = null;

	private static FileStream logFileStream = null;
	private static StreamWriter fileWriter = null;

	/// <summary>
	/// Initalize will be called after set operators:
	/// 1.printToConsole print to Unity debug window
	/// 2.printToFile attemp to real txt log file with given format
	/// 3.targetFilePath [only needed when printToFile is true] log file path,if not exsist,will create one name "log.txt" all charactors lower
	/// 4.printCallStackLevel [only needed when printToFile is true] log file will print detail of call stack.ex: if set to Error,will only print Error & Assert
	/// </summary>
	public static void Initalize()
	{
		if (printToFile)
		{
			targetFilePath = string.IsNullOrEmpty (targetFilePath) ? string.Concat (Application.persistentDataPath, "/log.txt") : targetFilePath;

			if (!File.Exists (targetFilePath))
			{
				File.Create (targetFilePath);
			}

			logFileStream = new FileStream (targetFilePath, FileMode.Append, FileAccess.Write);
			fileWriter = new StreamWriter (logFileStream);

			writeLogThread = new Thread (WriteLogFunction);
			writeLogThread.Start ();

			writeContentQueue = Queue.Synchronized (new Queue ());
		}
	}

	/// <summary>
	/// you have to call dispose when ApplicationQuit called
	/// </summary>
	public static void Dispose()
	{
		if (writeLogThread != null && writeLogThread.IsAlive)
		{
			writeLogThread.Abort ();
		}
		writeLogThread = null;

		if (fileWriter != null)
		{
			string titleString = string.Format("*******************END:{0}*******************\n",System.DateTime.Now);
			fileWriter.Write (titleString);
			fileWriter.Flush ();
			fileWriter.Dispose ();
			logFileStream.Close ();
		}
	}

	private static void WriteLogFunction()
	{
		//print title
		string titleString = string.Format("*******************LOG:{0}*******************\n",System.DateTime.Now);
		fileWriter.Write (titleString);
		fileWriter.Flush ();

		while (true)
		{
			if (writeContentQueue.Count > 0)
			{
				object writeObject = writeContentQueue.Dequeue ();
				BaseLogInfo writeLog = (BaseLogInfo)writeObject;
				fileWriter.Write (writeLog.ToString ());
				fileWriter.Flush ();
			}
			Thread.Sleep (50);
		}
	}

	public static void Log (int module, string content)
	{
		string moduleName = string.Empty;
		if (moduelMap.TryGetValue (module,out moduleName))
		{
			string logContent = string.Format ("[{0}]{1}", moduleName, content);
			if (printToConsole)
			{
				UnityEngine.Debug.Log (logContent);
			}
			if (printToFile)
			{
				CSharpLogInfo info = new CSharpLogInfo ();
				info.level = Level.Info;
				info.logContent = logContent;
				info.callFrameNum = Time.frameCount;
				if ((int)info.level >= (int)DebugUtils.printCallStackLevel)
				{
					info.callStack = new StackTrace (true);
				}
				writeContentQueue.Enqueue (info);
			}
		}
	}
}