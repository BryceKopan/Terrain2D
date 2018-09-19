using System.Collections.Generic;
using UnityEngine;

public static class TerrainMeshGenerator
{
    static List<Vector3> newVertices = new List<Vector3>();
    static List<int> newTriangles = new List<int>();
    static List<Color32> newVertexColors = new List<Color32>();
    static int vertexCount = 0;

    static List<Vector3> colVertices = new List<Vector3>();
    static List<int> colTriangles = new List<int>();
    static int colTriangleCount;

    public static Mesh CreateMeshFilter(TerrainMap tMap)
    {
        Mesh mesh = new Mesh();

        BuildMesh(tMap);
        
        mesh.Clear();
        mesh.vertices = newVertices.ToArray();
        mesh.triangles = newTriangles.ToArray();
        mesh.colors32 = newVertexColors.ToArray();
        mesh.RecalculateNormals();

        return mesh;
    }

    static void BuildMesh(TerrainMap tMap)
    {
        int xLength = tMap.GetLength(0);
        int yLength = tMap.GetLength(1);

        for(int x = 0; x < xLength; x++)
        {
            for(int y = 0; y < yLength; y++)
            {
                if(tMap.Get(x, y + 1) != (int) Materials.Empty 
                        && tMap.Get(x + 1, y) != (int) Materials.Empty) 
                {
                    if(tMap.Get(x, y) != (int) Materials.Empty)
                    {
                        GenTriBottomLeft(x, y, tMap.GetColor(tMap.Get(x, y)));
                    } 
                    if(tMap.Get(x + 1, y + 1) != (int) Materials.Empty)
                    {
                        GenTriTopRight(x, y, tMap.GetColor(tMap.Get(x + 1, y + 1)));
                    }
                }
                else if(tMap.Get(x, y) != (int) Materials.Empty 
                        && tMap.Get(x + 1, y + 1) != (int) Materials.Empty)
                {
                    if(tMap.Get(x + 1, y) != (int) Materials.Empty)
                    {
                        GenTriBottomRight(x, y, tMap.GetColor(tMap.Get(x + 1, y)));
                    }
                    if(tMap.Get(x, y + 1) != (int) Materials.Empty)
                    {
                        GenTriTopLeft(x, y, tMap.GetColor(tMap.Get(x, y + 1)));
                    }
                } 
            }
        }
    }

    static void GenTriBottomLeft(int x, int y, Color32 color)
    {
        newVertices.Add(new Vector3(x, y, 0));
        newVertices.Add(new Vector3(x, y + 1, 0));
        newVertices.Add(new Vector3(x + 1, y, 0));
    
        newTriangles.Add(vertexCount);
        newTriangles.Add(vertexCount + 1);
        newTriangles.Add(vertexCount + 2);

        newVertexColors.Add(color);
        newVertexColors.Add(color);
        newVertexColors.Add(color);

        vertexCount += 3;
    }

    static void GenTriTopLeft(int x, int y, Color32 color)
    {
        newVertices.Add(new Vector3(x, y + 1, 0));
        newVertices.Add(new Vector3(x + 1, y + 1, 0));
        newVertices.Add(new Vector3(x, y, 0));
    
        newTriangles.Add(vertexCount);
        newTriangles.Add(vertexCount + 1);
        newTriangles.Add(vertexCount + 2);

        newVertexColors.Add(color);
        newVertexColors.Add(color);
        newVertexColors.Add(color);

        vertexCount += 3;
    }

    static void GenTriTopRight(int x, int y, Color32 color)
    {
        newVertices.Add(new Vector3(x + 1, y + 1, 0));
        newVertices.Add(new Vector3(x + 1, y, 0));
        newVertices.Add(new Vector3(x, y + 1, 0));
    
        newTriangles.Add(vertexCount);
        newTriangles.Add(vertexCount + 1);
        newTriangles.Add(vertexCount + 2);

        newVertexColors.Add(color);
        newVertexColors.Add(color);
        newVertexColors.Add(color);

        vertexCount += 3;
    }

    static void GenTriBottomRight(int x, int y, Color32 color)
    {
        newVertices.Add(new Vector3(x + 1, y, 0));
        newVertices.Add(new Vector3(x, y, 0));
        newVertices.Add(new Vector3(x + 1, y + 1, 0));
    
        newTriangles.Add(vertexCount);
        newTriangles.Add(vertexCount + 1);
        newTriangles.Add(vertexCount + 2);

        newVertexColors.Add(color);
        newVertexColors.Add(color);
        newVertexColors.Add(color);

        vertexCount += 3;
    }
}
