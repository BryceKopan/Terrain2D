using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public List<Task> taskQueue;
	Camera cam;

	// Use this for initialization
	void Start ()
	{
		taskQueue = new List<Task>();
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update ()
	{
		HandleInput();
	}

	void HandleInput()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Vector3 worldPoint = cam.ScreenToWorldPoint(Input.mousePosition);
			taskQueue.Add(new TMove(new Vector2(worldPoint.x, worldPoint.y)));
		}
	}
}
