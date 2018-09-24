using UnityEngine;

public static class TerrainGenerator
{
    public static void GenerateTerrain(ref TerrainMap map)
    {
        float pointDistance = map.pointDistance;

        for(int px = 0; px < map.GetLength(0); px++)
        {
            float stone = Noise(px * pointDistance, 0, 80, 15, 1);
            stone += Noise(px * pointDistance, 0, 50, 30, 1);
            stone += Noise(px * pointDistance, 0, 10, 10, 1);
            stone += 75;
            stone /= pointDistance;

            float dirt = Noise(px * pointDistance, 0, 100, 35, 1);
            dirt += Noise(px * pointDistance, 0, 50, 30, 1);
            dirt += 75;
            dirt /= pointDistance;

            for(int py = 0; py < map.GetLength(1); py++)
            {
                if(py < stone)
                {
                    map.Set(px, py, (int) Materials.Stone);

                    if(Noise(px * pointDistance, py * pointDistance, 12, 16, 1) > 10)
                        map.Set(px, py, (int) Materials.Dirt);

                    if(Noise(px * pointDistance, py * pointDistance * 2, 16, 14, 1) > 10)
                        map.Set(px, py, (int) Materials.Empty);
                }
                else if(py < dirt)
                    map.Set(px, py, (int) Materials.Dirt);
            }
        }
    }

    static int Noise(float x, float y, float scale, float mag, float exp)
    {
        return (int) Mathf.Pow (Mathf.PerlinNoise(x / scale, y / scale) * mag, exp);
    }
}
