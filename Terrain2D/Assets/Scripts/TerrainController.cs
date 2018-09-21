using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour {
    public float PointDistance = 1f;
    private float pointDistance;

    public bool Blended;
    private bool blended;

	// Use this for initialization
	void Start ()
    {
        pointDistance = PointDistance;

        Gen();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(PointDistance != pointDistance && PointDistance > 0)
        {
            pointDistance = PointDistance;
            Gen();
        }
        if(Blended != blended)
        {
            blended = Blended;
            TerrainMeshGenerator.blended = blended;
            Gen();
        }
	}

    void Gen()
    {
        TerrainMap tMap = new TerrainMap(92, 128, pointDistance); 
        TerrainGenerator.GenerateTerrain(ref tMap);

        GetComponent<MeshFilter>().mesh.Clear();
        GetComponent<MeshFilter>().mesh = TerrainMeshGenerator.CreateMeshFilter(tMap);
    }
}
