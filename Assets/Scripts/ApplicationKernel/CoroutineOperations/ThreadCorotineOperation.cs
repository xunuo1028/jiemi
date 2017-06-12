/// <summary>
/// FileName:    ThreadCorotineOperation
/// Author:      LiuTaiyan
/// CreateTime:  4/6/2017 2:10:56 PM
/// Description: this corotine can help you to call complex function in thread.just yield-return the demon function finish, it's enough:)
/// </summary>

using System.Threading;
using System.Collections;
using System.Collections.Generic;

public interface IThreadCorotineable
{
	void Excute();
	bool IsFinished();
	string Error();
}

public class ThreadCorotineOperation : BaseCoroutineOperation 
{
	private static ArrayList allThreads = null;

	//chairman thread 
	private static Thread chairmanThread = null;
	private static Queue chairmanThreadTaskQueue = null;

	private IThreadCorotineable thisTask = null;
	private Thread thisThread = null;

	/// <summary>
	/// if inChairmanThread is true, function pointer will be done one by one.
	/// if inChairmanThread is false, function will allocate a new thread to process the thing you want.
	/// </summary>
	public ThreadCorotineOperation(IThreadCorotineable task, bool inChairmanThread = true)
	{
		thisTask = task;
		if (inChairmanThread)
		{
			if (chairmanThreadTaskQueue == null)
			{
				chairmanThreadTaskQueue = Queue.Synchronized (new Queue ());
			}

			chairmanThreadTaskQueue.Enqueue (task);

			if (chairmanThread == null || !chairmanThread.IsAlive)
			{
				chairmanThread = new Thread (ChairmanThreadWorkingFunction);
				chairmanThread.Start ();
			}
		}
		else
		{
			if (allThreads == null)
			{
				allThreads = ArrayList.Synchronized (new ArrayList());
			}

			thisThread = new Thread (ThreadWorkingFunction);
			allThreads.Add (thisThread);
			thisThread.Start ();
		}
	}
		
	public string Error()
	{
		if (thisTask != null)
		{
			return thisTask.Error();
		}
		return string.Empty;
	}

	private static void ChairmanThreadWorkingFunction()
	{
		while (true)
		{
			while (chairmanThreadTaskQueue.Count > 0)
			{
				IThreadCorotineable task = (IThreadCorotineable)chairmanThreadTaskQueue.Dequeue ();
				task.Excute ();
				Thread.Sleep (300);
			}
			Thread.Sleep (1000);
		}
	}

	private void ThreadWorkingFunction()
	{
		thisTask.Excute ();

		allThreads.Remove (thisThread);
		thisThread.Abort ();
		thisThread = null;
	}

	public override bool IsDone ()
	{
		return thisTask.IsFinished();
	}
		
	public static void DisposeAllThreads()
	{
		if (allThreads != null)
		{
			lock (allThreads.SyncRoot)
			{
				for (int i = 0, imax = allThreads.Count; i < imax; i++)
				{
					Thread runningThread = (Thread)allThreads[i];
					if (runningThread != null && runningThread.IsAlive)
					{
						runningThread.Abort ();
					}
					runningThread = null;
				}
				allThreads.Clear ();
			}
		}

		if (chairmanThread != null && chairmanThread.IsAlive)
		{
			lock (chairmanThread)
			{
				chairmanThread.Abort ();
				chairmanThread = null;
			}
		}
	}
}