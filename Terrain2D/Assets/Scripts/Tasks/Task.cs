using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
	protected List<Action> actions;

	public Task()
	{
		actions = new List<Action>();
	}

	public void DoTask(Ant ant)
	{
		if(actions.Count > 0)
		{
			actions[0].DoAction(ant);
			if(actions[0].IsDone(ant))
			{
				actions.RemoveAt(0);
			}
		}
	}

	public bool IsDone()
	{
		if (actions.Count == 0)
			return true;
		return false;
	}
}
