using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platforms_script : MonoBehaviour
{
    public GameObject player;
    public platformPoolManager_script platformPool;  // Reference to the PlatformPool script
    public float spawnInterval = 4f;  // Adjust as needed
    public float spawnHeight = 5f;   // Height above the player's current position to spawn platforms
    public float minSpawnHeight = 3f; // Minimum height difference between platforms
    public float maxSpawnHeight = 12f; // Maximum height difference between platforms

    private float nextSpawnY;
    private int initialPlatformsInRow = 2; // Start with more platforms
    private int platformsInRow; // Current number of platforms in a row
    private int nextDifficultyIncrease = 100; // Increase difficulty every 100 meters
    
    void Start()
    {
        nextSpawnY = transform.position.y + spawnHeight;
        platformsInRow = initialPlatformsInRow;
    }

    void Update()
    {
        // Check if it's time to spawn a new platform
        if (player.transform.position.y + spawnHeight >= nextSpawnY)
        {
            SpawnPlatform();
            nextSpawnY += spawnInterval;

        }

        if (player.transform.position.y >= nextDifficultyIncrease)
        {
            if(platformsInRow > 1) platformsInRow--; // Decrease the spawn interval
            nextDifficultyIncrease += 100; // Increase the next difficulty increase
            
            platformPool.changeGameStage();
        }

        // Recycle platforms that are out of view
        platformPool.RecyclePlatforms(player.transform.position.y);
    }

    void SpawnPlatform()
    {
        float rowWidth = 7f; // Increase the width of the row for more erratic positions
        float spacing = rowWidth / (platformsInRow + 1); // Space between platforms

        for (int i = 0; i < platformsInRow; i++)
        {
            GameObject platform = platformPool.GetPooledPlatform();
            float spawnX = -rowWidth / 2 + (i + 1) * spacing + Random.Range(-spacing / 2, spacing / 2); // Increase randomness
            float randomHeight = Random.Range(minSpawnHeight, maxSpawnHeight);
            platform.transform.position = new Vector2(spawnX, nextSpawnY + randomHeight);
            platform.SetActive(true);
        }
    }
}