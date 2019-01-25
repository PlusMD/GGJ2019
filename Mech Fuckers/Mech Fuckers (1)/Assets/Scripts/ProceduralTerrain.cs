using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralTerrain : MonoBehaviour
{
    public int Depth = 20;
    public int Width = 384;
    public int Height = 384;
    public int Scale = 20;
    public float OffsetX = 100f;
    public float OffsetY = 100f;

    void Start()
    {
        OffsetX = Random.Range(0f, 9999f);
        OffsetY = Random.Range(0f, 9999f);
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    void Update()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = Width + 1;
        terrainData.size = new Vector3(Width, Depth, Height);
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] Heights = new float[Width, Height];
        for (int X = 0; X < Width; X++)
        {
            for (int Y = 0; Y < Height; Y++)
            {
                Heights[X, Y] = CalculateHeight(X, Y);
            }
        }
        return Heights;
    }

    float CalculateHeight (int X, int Y)
    {
        float XCoord = (float)X / Width * Scale + OffsetX;
        float YCoord = (float)Y / Height * Scale + OffsetY;
        return Mathf.PerlinNoise(XCoord, YCoord);
    }

}
