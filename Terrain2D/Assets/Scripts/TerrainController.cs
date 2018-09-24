using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour {
    public float PointDistance = 1f;
    private float pointDistance;

    public bool TriangleBlended;
    private bool triangleBlended;

    public bool VertexBlended;
    private bool vertexBlended;

    public bool VertexMajority;
    private bool vertexMajority;
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
        if(TriangleBlended != triangleBlended)
        {
            triangleBlended = TriangleBlended;
            TerrainMeshGenerator.triangleBlended = triangleBlended;
            Gen();
        }
        if(VertexBlended != vertexBlended)
        {
            vertexBlended = VertexBlended;
            TerrainMeshGenerator.vertexBlended = vertexBlended;
            Gen();
        }
        if(VertexMajority != vertexMajority)
        {
            vertexMajority = VertexMajority;
            TerrainMeshGenerator.vertexMajority = vertexMajority;
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
