using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class perlinNoise : MonoBehaviour
{
    private float pitch = .1f;
    public int height = 256;
    public int depth = 20;
    public int width = 256;

    public float scale = 20f;

    public float offsetx = 0f;
    public float offsety = 0f;

    void Update()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.pitch = pitch;
    }

    TerrainData GenerateTerrain (TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);

        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        for(int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = CalHeight(x, y);
            }
        }

        return heights;
    }

    float CalHeight(int x, int y)
    {
        float xCoord = (float) x / width * scale * pitch;
        float yCoord = (float) y / height * scale * pitch;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }

    
   
}
