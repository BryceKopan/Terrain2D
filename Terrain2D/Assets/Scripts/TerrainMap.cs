using UnityEngine;

enum Materials{Empty, Stone, Dirt};

public class TerrainMap
{
    int[,] map;
    public float pointDistance;

    public TerrainMap(int mapWidth, int mapHeight, 
            float pointDistance = 1)
    {
        int mapSizeX, mapSizeY;
        mapSizeX = Mathf.RoundToInt(mapWidth / pointDistance);
        mapSizeY = Mathf.RoundToInt(mapHeight / pointDistance);
        
        this.pointDistance = pointDistance;
        map = new int[mapSizeX, mapSizeY];
    }

    public int GetLength(int dimension)
    {
        return map.GetLength(dimension);
    }
    
    public int Get(int x, int y)
    {
        if(x < 0 || x >= map.GetLength(0) 
                || y < 0 || y >= map.GetLength(1))
            return (int) Materials.Stone;

        return map[x, y];
    }

    public void Set(int x, int y, int material)
    {
        if(x < 0 || x >= map.GetLength(0) 
                || y < 0 || y >= map.GetLength(1))
            return;

        map[x, y] = material;
    }

    public Color GetColor(int material)
    {
        switch(material)
        {
            case (int) Materials.Stone:
                return Color.grey;

            case (int) Materials.Dirt:
                return new Color32(139, 69 ,19, 1);
        }
        return new Color32(255, 0, 255, 1);
    }
}
