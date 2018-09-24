using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AMove : Action
{
	Vector2 targetLocation;

	public AMove(Vector2 targetLocation)
	{
		this.targetLocation = targetLocation;
	}

	public override void DoAction(Ant ant)
	{
		Debug.Log("MOve to:" + targetLocation.x + ":" + targetLocation.y);
		ant.rb.AddForce(new Vector2(ant.transform.position.x, ant.transform.position.y) - targetLocation * 1000000);
	}

	public override bool IsDone(Ant ant)
	{
		if(Vector2.Distance(ant.transform.position, targetLocation) < ant.range)
			return true;
		return false;
	}
}
