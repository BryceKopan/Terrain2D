using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour
{
	Task task;
	public float range = 1f;
	public Rigidbody2D rb;
	public Player player;

	// Use this for initialization
	void Start ()
	{
		task = new Task();
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		task.DoTask(this);
		if(task.IsDone())
		{
			GetNewTask();
		}
	}

	void GetNewTask()
	{
		if(player.taskQueue.Count > 0)
		{
			task = player.taskQueue[0];
			player.taskQueue.RemoveAt(0);
		}
	}
}
