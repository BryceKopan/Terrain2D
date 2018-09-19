using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastExample : MonoBehaviour {
    public GameObject terrain;
    private PolygonGenerator tScript;
    public GameObject target;
    private LayerMask layerMask = (1 << 0);

	// Use this for initialization
	void Start ()
    {
	    tScript = terrain.GetComponent<PolygonGenerator>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
		RaycastHit hit;

        float distance = Vector3.Distance(transform.position, target.transform.position);

        if(Physics.Raycast(transform.position, (target.transform.position - transform.position).normalized, out hit, distance, layerMask))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);

            Vector2 point = new Vector2(hit.point.x, hit.point.y);
            point += new Vector2(hit.normal.x, hit.normal.y) * -.5f;

            int x = Mathf.RoundToInt((point.x - .5f) - terrain.transform.position.x);
            int y = Mathf.RoundToInt((point.y + .5f) - terrain.transform.position.y); 

            tScript.blocks[x, y] = 0;
            tScript.update = true;
        }
        else
        {
            Debug.DrawLine(transform.position, target.transform.position, Color.blue);
        }
	}
}
