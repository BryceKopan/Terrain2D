using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        TerrainMap tMap = new TerrainMap(92, 128, 1);        

        GetComponent<MeshFilter>().mesh = TerrainMeshGenerator.CreateMeshFilter(tMap);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
