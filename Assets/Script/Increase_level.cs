using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Increase_level : MonoBehaviour
{
    public GameObject[] TilePrefabs; // Array to hold different ground prefabs
    public float zSpawn = 0; // The position on the z-axis where new tiles spawn
    public float tileLength = 30; // Length of each tile
    public int numberOfTiles = 10; // Number of active tiles at a time
    private List<GameObject> activeTiles = new List<GameObject>(); // List to keep track of active tiles
    public Transform playerTransform; // Reference to the player’s transform

    void Start()
    {
        // Spawn initial set of tiles
        for (int i = 0; i < numberOfTiles; i++)
        {
            if (i == 0)
                SpawnTile(0); // Spawn initial tile (typically the starting piece)
            else
                SpawnTile(Random.Range(0, TilePrefabs.Length)); // Spawn random tiles from the array
        }
    }

    void Update()
    {
        // Check if the player has moved past a certain point and spawn new tile
        if (playerTransform.position.z - 35 > zSpawn - (numberOfTiles * tileLength))
        {
            SpawnTile(Random.Range(0, TilePrefabs.Length));
            DeleteTile(); // Remove the oldest tile
        }
    }

    // Method to spawn a new tile
    public void SpawnTile(int tileIndex)
    {
        // Instantiate a tile at the zSpawn position
        GameObject road = Instantiate(TilePrefabs[tileIndex], Vector3.forward * zSpawn, Quaternion.identity);
        activeTiles.Add(road); // Add the tile to the list of active tiles
        zSpawn += tileLength; // Increment zSpawn for the next tile
    }

    // Method to delete the oldest tile
    public void DeleteTile()
    {
        Destroy(activeTiles[0]); // Destroy the first tile in the list
        activeTiles.RemoveAt(0); // Remove it from the active list
    }
}
