using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class perlinNoise : MonoBehaviour
{
   
    public int height = 256;
    public int depth = 20;
    public int width = 256;

    public float scale = 20f;

    public float offsetx = 100f;
    public float offsety = 100f;
    public float Speed = 1f;

    

     void Start()
    {
        
    }

    void Update()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);

        if (Input.GetAxis("Mouse ScrollWheel")<0) //changes speed and direction of terrain
        {
            Speed += 1;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Speed -= 1;
        }


        offsety += Time.deltaTime * Speed; // moves the terrain in the y direction relative to the speed by time


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
        float xCoord = (float) x / width * scale + offsetx; 
        float yCoord = (float) y / height * scale + offsety;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }

    
   
}
