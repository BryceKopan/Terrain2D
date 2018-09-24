using System.Collections.Generic;
using UnityEngine;

public static class TerrainMeshGenerator
{
    static List<Vector3> newVertices = new List<Vector3>();
    static List<int> newTriangles = new List<int>();
    static List<Color32> newVertexColors = new List<Color32>();
    static int vertexCount = 0;
    static float pointDistance;

    public static bool vertexBlended = false;
    public static bool triangleBlended = false;
    public static bool vertexMajority = false;

    public static Mesh CreateMeshFilter(TerrainMap tMap)
    {
        Mesh mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        BuildMesh(tMap);
        
        mesh.Clear();
        mesh.vertices = newVertices.ToArray();
        mesh.triangles = newTriangles.ToArray();
        mesh.colors32 = newVertexColors.ToArray();
        mesh.RecalculateNormals();

        return mesh;
    }

	public static Mesh CreateCollisionMesh()
	{
		Mesh newMesh = new Mesh();
		newMesh.vertices = newVertices.ToArray();
		newMesh.triangles = newTriangles.ToArray();
		return newMesh;
	}

	public static void ClearMeshData() 
	{
		newVertices.Clear();
		newTriangles.Clear();
		newVertexColors.Clear();
		vertexCount = 0;
	}

    static void BuildMesh(TerrainMap tMap)
    {
        int xLength = tMap.GetLength(0);
        int yLength = tMap.GetLength(1);
        pointDistance = tMap.pointDistance;
        Color color1, color2, color3;

        for(int x = 0; x < xLength; x++)
        {
            for(int y = 0; y < yLength; y++)
            {
				if (tMap.Get(x, y) != (int)Materials.Empty
						&& tMap.Get(x + 1, y + 1) != (int)Materials.Empty
						&& tMap.Get(x, y) == tMap.Get(x + 1, y + 1))
                {
                    if(tMap.Get(x + 1, y) != (int) Materials.Empty)
                    {
                        GetTriColor(tMap, x + 1, y, x, y, x + 1, y + 1, out color1, out color2, out color3);
                        GenTriBottomRight(x, y, color1, color2, color3);
                    }
                    if(tMap.Get(x, y + 1) != (int) Materials.Empty)
                    {
                        GetTriColor(tMap, x, y + 1, x + 1, y + 1, x, y, out color1, out color2, out color3);
                        GenTriTopLeft(x, y, color1, color2, color3);
                    }
                } 
                else if(tMap.Get(x, y + 1) != (int) Materials.Empty 
                        && tMap.Get(x + 1, y) != (int) Materials.Empty)
                {
                    if(tMap.Get(x, y) != (int) Materials.Empty)
                    {
                        GetTriColor(tMap, x, y, x, y + 1, x + 1, y, out color1, out color2, out color3);
                        GenTriBottomLeft(x, y, color1, color2, color3);
                    } 
                    if(tMap.Get(x + 1, y + 1) != (int) Materials.Empty)
                    {
                        GetTriColor(tMap, x + 1, y + 1, x + 1, y, x, y + 1, out color1, out color2, out color3);
                        GenTriTopRight(x, y, color1, color2, color3);
                    }
                }
            }
        }
    }

    static void GetTriColor(TerrainMap tMap, int x1, int y1, int x2, int y2, int x3, int y3, out Color color1, out Color color2, out Color color3)
    {
        color1 = tMap.GetColor(tMap.Get(x1, y1));
        color2 = tMap.GetColor(tMap.Get(x2, y2));
        color3 = tMap.GetColor(tMap.Get(x3, y3));
        if(vertexMajority)
        {   
            if(color1 == color2)
                color3 = color1;
            else if(color1 == color3)
                color2 = color1;
            else if(color2 == color3)
                color1 = color2;    
        }
        else if(triangleBlended)
        {
            Color tColor = (color1 + color2 + color3) / 3;
            color1 = color2 = color3 = tColor;
        }
        else if(!vertexBlended)
            color3 = color2 = color1;
    }

    static void GenTriBottomLeft(float x, float y, Color32 color1, Color32 color2, Color32 color3)
    {
        x = x * pointDistance;
        y = y * pointDistance;
        newVertices.Add(new Vector3(x, y, 0));
        newVertices.Add(new Vector3(x, y + pointDistance, 0));
        newVertices.Add(new Vector3(x + pointDistance, y, 0));

        newTriangles.Add(vertexCount);
        newTriangles.Add(vertexCount + 1);
        newTriangles.Add(vertexCount + 2);

        newVertexColors.Add(color1);
        newVertexColors.Add(color2);
        newVertexColors.Add(color3);

        vertexCount += 3;
    }

    static void GenTriTopLeft(float x, float y, Color32 color1, Color32 color2, Color32 color3)
    {
        x = x * pointDistance;
        y = y * pointDistance;
        newVertices.Add(new Vector3(x, y + pointDistance, 0));
        newVertices.Add(new Vector3(x + pointDistance, y + pointDistance, 0));
        newVertices.Add(new Vector3(x, y, 0));

        newTriangles.Add(vertexCount);
        newTriangles.Add(vertexCount + 1);
        newTriangles.Add(vertexCount + 2);

        newVertexColors.Add(color1);
        newVertexColors.Add(color2);
        newVertexColors.Add(color3);

        vertexCount += 3;
    }

    static void GenTriTopRight(float x, float y, Color32 color1, Color32 color2, Color32 color3)
    {
        x = x * pointDistance;
        y = y * pointDistance;
        newVertices.Add(new Vector3(x + pointDistance, y + pointDistance, 0));
        newVertices.Add(new Vector3(x + pointDistance, y, 0));
        newVertices.Add(new Vector3(x, y + pointDistance, 0));

        newTriangles.Add(vertexCount);
        newTriangles.Add(vertexCount + 1);
        newTriangles.Add(vertexCount + 2);

        newVertexColors.Add(color1);
        newVertexColors.Add(color2);
        newVertexColors.Add(color3);

        vertexCount += 3;
    }

    static void GenTriBottomRight(float x, float y, Color32 color1, Color32 color2, Color32 color3)
    {
        x = x * pointDistance;
        y = y * pointDistance;
        newVertices.Add(new Vector3(x + pointDistance, y, 0));
        newVertices.Add(new Vector3(x, y, 0));
        newVertices.Add(new Vector3(x + pointDistance, y + pointDistance, 0));

        newTriangles.Add(vertexCount);
        newTriangles.Add(vertexCount + 1);
        newTriangles.Add(vertexCount + 2);

        newVertexColors.Add(color1);
        newVertexColors.Add(color2);
        newVertexColors.Add(color3);

        vertexCount += 3;
    }
}
