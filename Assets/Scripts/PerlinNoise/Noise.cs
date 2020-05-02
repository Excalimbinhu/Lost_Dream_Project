using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise
{
  public static float[,] GenerateNoiseMap(int mapWidth, int mapLength, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset, Vector2 level)
  {
    float[,] noiseMap = new float[mapWidth, mapLength];

    System.Random seedGen = new System.Random(seed);
    Vector2[] octaveOffsets = new Vector2[octaves];

    for (int i = 0; i < octaves; i++)
    {
      float offsetX = seedGen.Next(-100000, 100000) + offset.x;
      float offsetY = seedGen.Next(-100000, 100000) + offset.y;
      octaveOffsets[i] = new Vector2(offsetX, offsetY);
    }


    if (scale <= 0)
    {
      scale = 0.000001f;
    }

    float maxNoiseHeight = float.MinValue;
    float minNoiseHeight = float.MaxValue;

    float halfWidth = mapWidth / 2f;
    float halfLength = mapLength / 2f;

    for (int y = 0; y < mapLength; y++)
    {
      for (int x = 0; x < mapWidth; x++)
      {

        float amplitude = 1;
        float frequency = 1;
        float noiseHeight = 0;


        for (int i = 0; i < octaves; i++)
        {
          float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
          float sampleY = (y - halfLength) / scale * frequency + octaveOffsets[i].y;

          float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
          noiseHeight += perlinValue * amplitude;

          amplitude *= persistance;
          frequency *= lacunarity;
        }

        if (noiseHeight > maxNoiseHeight)
        {
          maxNoiseHeight = noiseHeight;
        }
        else if (noiseHeight < minNoiseHeight)
        {
          minNoiseHeight = noiseHeight;
        }

        noiseMap[x, y] = (noiseHeight *level.x / 10) - level.y / 10;
  }
    }


    for (int y = 0; y < mapLength; y++)
    {
      for (int x = 0; x < mapWidth; x++)
      {
        noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
      }
    }

    return noiseMap;
  }
}
