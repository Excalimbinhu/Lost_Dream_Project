using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
  public int mapWidth;
  public int mapLength;
  public float noiseScale;

  public int octaves;
  [Range(0, 1)]
  public float persistance;
  public float lacunarity;

  public int seed;
  public Vector2 offset;
  public Vector2 level;


  public bool update;

  public void GenerateMap()
  {
    float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapLength, seed, noiseScale, octaves, persistance, lacunarity, offset, level);

    MapDisplay display = FindObjectOfType<MapDisplay>();

    display.DrawNoiseMap(noiseMap);
  }

  void OnValidate()
  {
    if (mapWidth < 1)
    {
      mapWidth = 1;
    }

    if (mapLength < 1)
    {
      mapLength = 1;
    }

    if (lacunarity < 1)
    {
      lacunarity = 1;
    }

    if (octaves < 0)
    {
      lacunarity = 0;
    }
  }
}
