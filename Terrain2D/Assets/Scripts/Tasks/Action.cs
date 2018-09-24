using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action
{
	public abstract void DoAction(Ant ant);
	public abstract bool IsDone(Ant ant);
}
