using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainProceduralScript : MonoBehaviour
{  

    Color[] colors;
    public Gradient gradient;

    public int depth = 15;

    public int width = 100;
    public int height = 200;

    public float scale = 3;

    public float offsetX = 100f;
    public float offsetY = 100f;

    void Start() {
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);

        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        Debug.Log(terrain.terrainData.terrainLayers[0]);
    }

    // void Update() {
    //     Terrain terrain = GetComponent<Terrain>();
    //     terrain.terrainData = GenerateTerrain(terrain.terrainData);
    // }

    TerrainData GenerateTerrain(TerrainData terrainData) {
        
        terrainData.heightmapResolution = height + 1;

        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    // public void TerrainColor(Terrain terrain) {

    //     var maxX = terrain.terrainData.alphamapWidth;
    //     var maxY = terrain.terrainData.alphamapHeight;
    //     var layerCount = terrain.terrainData.alphamapLayers;
    //     TerrainLayer[] terrainLayers = terrain.terrainData.terrainLayers;

    //     float[,,] Splat = new float[maxX, maxY, layerCount];

    //     bool usedTopology = false;
    //     for (int x=0; x < maxX; x++) {
    //         for (iny y = 0; y < maxY; y++) {
    //             float percentX = (float)x / (float)maxX;
    //             float percentY = (float)y / (float)maxY;
    //             float height = terrain.transform.position.y
    //                 + terrain.terrainData.heightmapScale.y
    //                 * terrain.Mesh[
    //                     Mathf.RoundToInt(percentX * terrain.HeightRes),
    //                     Mathf.RoundToInt(percentY * terrain.HeightRes)
    //                 ];
                
    //             usedTopology = false;
    //             for (int i =1; i < Heights.Length; i++) {
    //                 if ()
    //             }
    //         } 
    //     }
    // } 

    // public void FillTerrainLayer( Terrain terrain, TerrainLayer[] terrainLayer )
    // {
    
    //     // get the weight data for the alphamaps
    //     int width = terrain.terrainData.alphamapWidth;
    //     int height = terrain.terrainData.alphamapHeight;
    //     float[ , , ] alphamaps = terrain.terrainData.GetAlphamaps( 0, 0, width, height );
    
    //     int numAlphamaps = 5;
    //     for( int i = 0; i < numAlphamaps; ++i )
    //     {
    //         // TODO: loop through the alphamaps and set weights for your filled
    //         //       terrain layer to 1 and all other terrain layers to 0
    //     }
    
    //     // NOTE: normally you'd have to renormalize the weights but since you
    //     //       are filling the weights of one layer and clearing the rest, you can
    //     //       skip that step since they will technically be normalized
    
    //     // NOTE: probably want to track Undo step here before modifying the TerrainData
    
    //     // set the new alphamap weights in the terrain data
    //     terrain.terrainData.SetAlphamaps( 0, 0, alphamaps );
    // }

    float[,] GenerateHeights() {
        float[,] heights = new float[256, 256];
        for (int x=0; x < 256; x++) {
            for (int y = 0; y < 256; y++) {
                heights[x, y] = CalculateHeight(x, y);
            }
        }

        return heights;
    }
    
    float CalculateHeight(int x, int y) {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
