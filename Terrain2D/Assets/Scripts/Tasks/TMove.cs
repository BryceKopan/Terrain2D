using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMove : Task
{
	public TMove(Vector2 targetLocation)
	{
		actions.Add(new AMove(targetLocation));
	}
}
