using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    // The seed of the curret map, gets randomly generated
    int mapSeed = 0;

    // The maximal building height
    public int maxBlockHeight = 100;

    // The size of a loading "chunk"
    public int chunkSize = 16;

    // The prefab of the block at surface level
    public Object SurfaceBlock = null;

    void Start()
    {
        // We randomly generate the map
        mapSeed = Random.Range(-1000000000, 1000000000);
        GenerateZone(new Vector2Int(0, 0), new Vector2Int(16, 16), 100);
        print("map generated");
    }

    // Generates all the block that must go in a zone as an int[,,] grid
    int[,,]
    GenerateBlockGrid( // The position at which we start to generate the zone
        Vector2Int zoneStartPosition,
        // The size of the zone we're gonna generate
        Vector2Int zoneSize,
        // The height multiplier of the generated zone
        int heightMultiplier
    )
    {
        float[,] perlinNoisemap =
            Noise
                .GenerateNoiseMap(new Vector2Int(zoneSize.x, zoneSize.y),
                new Vector2Int(0, 0),
                1,
                0);

        int[,,] BlockGrid = new int[zoneSize.x, maxBlockHeight, zoneSize.y];

        for (int x = 0; x < BlockGrid.GetLength(0); x++)
        {
            for (int z = 0; z < BlockGrid.GetLength(2); z++)
            {
                float surfacePosition = perlinNoisemap[x, z] * heightMultiplier;
                BlockGrid[x, (int) surfacePosition, z] = 1;
                // print(perlinNoisemap[x, z]);
            }
        }

        return BlockGrid;
    }

    // We place a chosen block
    public void PlaceBlock( // The position where we want to place the block
        Vector3 position,
        // The block we want to place
        Object block
    )
    {
        Object placedBlock =
            Instantiate(block, position, Quaternion.Euler(Vector3.forward));
    }

    // We generate and place the blocks in a selected zone
    void GenerateZone( // The position at which we start to generate the zone
        Vector2Int zoneStartPosition,
        // The size of the zone we're gonna generate
        Vector2Int zoneSize,
        // The height multiplier of the generated zone
        int heightMultiplier
    )
    {
        int[,,] BlockGrid =
            GenerateBlockGrid(zoneStartPosition, zoneSize, heightMultiplier);

        for (int x = 0; x < BlockGrid.GetLength(0); x++)
        {
            for (int y = 0; y < BlockGrid.GetLength(1); y++)
            {
                for (int z = 0; z < BlockGrid.GetLength(2); z++)
                {
                    switch (BlockGrid[x, y, z])
                    {
                        case 0:
                            break;
                        case 1:
                            PlaceBlock(new Vector3(x, y, z), SurfaceBlock);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
