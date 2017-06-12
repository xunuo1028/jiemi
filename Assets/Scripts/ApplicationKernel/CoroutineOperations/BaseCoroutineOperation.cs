/// <summary>
/// FileName:    BaseCoroutineOperation
/// Author:      LiuTaiyan
/// CreateTime:  4/6/2017 1:58:55 PM
/// Description: base yield-returnable operation.this is a smart and popular coding style :) I wish you'll enjoy that :) 
/// </summary>

using System.Collections;
using System.Collections.Generic;

public abstract class BaseCoroutineOperation : IEnumerator 
{
	public object Current
	{
		get{ return null;}
	}

	public bool MoveNext()
	{
		return !IsDone ();
	}

	public abstract bool IsDone ();

	public virtual void Reset(){}
}

public class SequenceCoroutineOperation : BaseCoroutineOperation
{
	private Queue<IEnumerator> coroutineQueue = new Queue<IEnumerator> ();
	private IEnumerator current = null;

	public SequenceCoroutineOperation(params IEnumerator[] allCoroutines)
	{
		for (int i = 0, imax = allCoroutines.Length; i < imax; i++)
		{
			coroutineQueue.Enqueue (allCoroutines [i]);
		}	
	}

	public override bool IsDone ()
	{
		if (coroutineQueue.Count == 0)
		{
			return true;
		}

		if (current == null)
		{
			current = coroutineQueue.Dequeue (); 
		}

		if (!current.MoveNext ())
		{
			current = null;
		}

		return false;
	}
}

public class SpawnCoroutineOperation : BaseCoroutineOperation
{
	private List<IEnumerator> coroutineSet = new List<IEnumerator>();

	public SpawnCoroutineOperation(params IEnumerator[] allCoroutines)
	{
		for (int i = 0, imax = allCoroutines.Length; i < imax; i++)
		{
			coroutineSet.Add (allCoroutines [i]);
		}	
	}

	public override bool IsDone ()
	{
		if (coroutineSet.Count == 0)
		{
			return true;
		}

		for (int i = 0; i < coroutineSet.Count; i++)
		{
			IEnumerator current = coroutineSet [i];
			if (!current.MoveNext ())
			{
				coroutineSet.RemoveAt (i);
				i--;
			}
		}
		return false;
	}
}