using System.Collections.Generic;
using UnityEngine;

public static class TerrainMeshGenerator
{
    static List<Vector3> newVertices = new List<Vector3>();
    static List<int> newTriangles = new List<int>();
    static List<Color32> newVertexColors = new List<Color32>();
    static int vertexCount = 0;
    static float pointDistance;

    static List<Vector3> colVertices = new List<Vector3>();
    static List<int> colTriangles = new List<int>();
    static int colTriangleCount;

    public static bool blended = false;

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

        newVertices.Clear();
        newTriangles.Clear();
        newVertexColors.Clear();
        vertexCount = 0;

        return mesh;
    }

    static void BuildMesh(TerrainMap tMap)
    {
        int xLength = tMap.GetLength(0);
        int yLength = tMap.GetLength(1);
        pointDistance = tMap.pointDistance;

        for(int x = 0; x < xLength; x++)
        {
            for(int y = 0; y < yLength; y++)
            {
                if(tMap.Get(x, y + 1) != (int) Materials.Empty 
                        && tMap.Get(x + 1, y) != (int) Materials.Empty) 
                {
                    if(tMap.Get(x, y) != (int) Materials.Empty)
                    {
                        Color color1, color2, color3;
                        color1 = tMap.GetColor(tMap.Get(x, y));
                        color2 = tMap.GetColor(tMap.Get(x, y + 1));
                        color3 = tMap.GetColor(tMap.Get(x + 1, y));
                        if(!blended)
                            color3 = color2 = color1;
                        GenTriBottomLeft(x, y, color1, color2, color3);
                    } 
                    if(tMap.Get(x + 1, y + 1) != (int) Materials.Empty)
                    {
                        Color color1, color2, color3;
                        color1 = tMap.GetColor(tMap.Get(x + 1, y + 1));
                        color2 = tMap.GetColor(tMap.Get(x + 1, y));
                        color3 = tMap.GetColor(tMap.Get(x, y + 1));
                        if(!blended)
                            color3 = color2 = color1;
                        GenTriTopRight(x, y, color1, color2, color3);
                    }
                }
                else if(tMap.Get(x, y) != (int) Materials.Empty 
                        && tMap.Get(x + 1, y + 1) != (int) Materials.Empty)
                {
                    if(tMap.Get(x + 1, y) != (int) Materials.Empty)
                    {
                        Color color1, color2, color3;
                        color1 = tMap.GetColor(tMap.Get(x + 1, y));
                        color2 = tMap.GetColor(tMap.Get(x, y));
                        color3 = tMap.GetColor(tMap.Get(x + 1, y + 1));
                        if(!blended)
                            color3 = color2 = color1;
                        GenTriBottomRight(x, y, color1, color2, color3);
                    }
                    if(tMap.Get(x, y + 1) != (int) Materials.Empty)
                    {
                        Color color1, color2, color3;
                        color1 = tMap.GetColor(tMap.Get(x, y + 1));
                        color2 = tMap.GetColor(tMap.Get(x + 1, y + 1));
                        color3 = tMap.GetColor(tMap.Get(x, y));
                        if(!blended)
                            color3 = color2 = color1;
                        GenTriTopLeft(x, y, color1, color2, color3);
                    }
                } 
            }
        }
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
