using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,]
    GenerateNoiseMap(
        Vector2Int size,
        Vector2Int startPosition,
        int scale,
        int seed = 0
    )
    {
        float[,] noiseMap = new float[size.x, size.y];
        if (seed == 0)
        {
            seed = Random.Range(-1000000000, 1000000000);
        }

        for (float x = 0; x < size.x / 10; x += 0.1f)
        {
            for (float y = 0; y < size.y / 10; y += 0.1f)
            {
                Vector2 noisePos =
                    new Vector2(x * scale + seed, y * scale + seed) +
                    startPosition;

                noiseMap[(int)(x * 10), (int)(y * 10)] =
                    Mathf.PerlinNoise(noisePos.x, noisePos.y);
                Debug.Log(Mathf.PerlinNoise(noisePos.x, noisePos.y).ToString());
            }
        }

        return noiseMap;
    }
}
