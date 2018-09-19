using UnityEngine;

enum Materials{Empty, Stone, Dirt};

public class TerrainMap
{
    private int[,] map;

    public TerrainMap(int mapWidth, int mapHeight)
    {
        map = new int[mapWidth, mapHeight];
        GenTerrain();
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

    void GenTerrain()
    {
        for(int px = 0; px < map.GetLength(0); px++)
        {
            int stone = Noise(px, 0, 80, 15, 1);
            stone += Noise(px, 0, 50, 30, 1);
            stone += Noise(px, 0, 10, 10, 1);
            stone += 75;

            int dirt = Noise(px, 0, 100, 35, 1);
            dirt += Noise(px, 0, 50, 30, 1);
            dirt += 75;

            for(int py = 0; py < map.GetLength(1); py++)
            {
                if(py < stone)
                {
                    map[px, py] = 1;

                    if(Noise(px, py, 12, 16, 1) > 10)
                        map[px, py] = 2;

                    if(Noise(px, py*2, 16, 14, 1) > 10)
                        map[px, py] = 0;
                }
                else if(py < dirt)
                    map[px, py] = 2;
            }
        }
    }

    int Noise(int x, int y, float scale, float mag, float exp)
    {
        return (int) Mathf.Pow (Mathf.PerlinNoise(x / scale, y / scale) * mag, exp);
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
