using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public float CameraSpeed = 50f;

    private Vector2 moveDirection = new Vector2(0, 0);

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		HandleInput();

		transform.position += new Vector3(moveDirection.x, moveDirection.y, 0) * CameraSpeed * Time.deltaTime;
	}

	void HandleInput() 
	{
		if (Input.GetKeyDown(KeyCode.W))
		{
			moveDirection += new Vector2(0, 1);
		}
		if (Input.GetKeyUp(KeyCode.W))
		{
			moveDirection -= new Vector2(0, 1);
		}
		if (Input.GetKeyDown(KeyCode.A))
		{
			moveDirection += new Vector2(-1, 0);
		}
		if (Input.GetKeyUp(KeyCode.A))
		{
			moveDirection -= new Vector2(-1, 0);
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			moveDirection += new Vector2(0, -1);
		}
		if (Input.GetKeyUp(KeyCode.S))
		{
			moveDirection -= new Vector2(0, -1);
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			moveDirection += new Vector2(1, 0);
		}
		if (Input.GetKeyUp(KeyCode.D))
		{
			moveDirection -= new Vector2(1, 0);
		}
	}
}
