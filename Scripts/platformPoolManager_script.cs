using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformPoolManager_script : MonoBehaviour
{
    public GameObject[] platformPrefabs;  // Assign your platform prefabs (e.g., normal, broken, jump boost)
    public int poolSize = 15;             // Number of platforms in the pool
    public float recycleThreshold = 5f;   // Y-coordinate below which platforms are recycled
    private List<GameObject> normalPlatformsPool;
    private List<GameObject> fallingPlatformsPool;
    private List<GameObject> boostPlatformsPool;
    private int gameStage = 0;

    void Start()
    {
        // Initialize the platform pools
        normalPlatformsPool = new List<GameObject>();
        fallingPlatformsPool = new List<GameObject>();
        boostPlatformsPool = new List<GameObject>();

        // Populate the normal platforms pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newPlatform = Instantiate(platformPrefabs[0]);
            newPlatform.SetActive(false);
            normalPlatformsPool.Add(newPlatform);
        }
    }
    
    // Get an inactive platform from the pool based on the game stage
    public GameObject GetPooledPlatform()
    {
        if (gameStage == 0)
        {
            return GetNormalPlatform();
        }
        else if (gameStage == 1)
        {
            int randomPlatform = Random.Range(0, 10);
            if (randomPlatform % 2 == 0)
            {
                return GetNormalPlatform();
            }
            else
            {
                return GetFallingPlatform();
            }
        }
        else if (gameStage == 2)
        {
            int randomPlatform = Random.Range(0, 3);
            if (randomPlatform == 0)
            {
                return GetNormalPlatform();
            }
            else if (randomPlatform == 1)
            {
                return GetFallingPlatform();
            }
            else
            {
                return GetBoostPlatform();
            }
        }
        else
        {
            int randomPlatform = Random.Range(0, 10 + gameStage);
            if (randomPlatform % gameStage == 0)
            {
                return GetNormalPlatform();
            }
            else
            {
                int random2 = Random.Range(0, 10);
                if (randomPlatform % 2 == 0)
                {
                    return GetBoostPlatform();
                }
                else
                {
                    return GetFallingPlatform();
                }
            }
        }
    }

    // Get an inactive normal platform from the pool
    private GameObject GetNormalPlatform()
    {
        foreach (var platform in normalPlatformsPool)
        {
            if (!platform.activeInHierarchy)
            {
                return platform;
            }
        }
        return AddNewPlatformToPool();
    }

    // Get an inactive falling platform from the pool
    private GameObject GetFallingPlatform()
    {
        foreach (var platform in fallingPlatformsPool)
        {
            if (!platform.activeInHierarchy)
            {
                return platform;
            }
        }
        return AddNewPlatformToPool();
    }

    // Get an inactive boost platform from the pool
    private GameObject GetBoostPlatform()
    {
        foreach (var platform in boostPlatformsPool)
        {
            if (!platform.activeInHierarchy)
            {
                return platform;
            }
        }
        return AddNewPlatformToPool();
    }

    // Add a new platform to the normal platforms pool
    private GameObject AddNewPlatformToPool()
    {
        GameObject newPlatform = Instantiate(platformPrefabs[0]);
        newPlatform.SetActive(false);
        normalPlatformsPool.Add(newPlatform);
        return newPlatform;
    }

    // Recycle platforms that are below the recycle threshold
    public void RecyclePlatforms(float playerY)
    {
        foreach (var platform in normalPlatformsPool)
        {
            if (platform.activeInHierarchy && (playerY > (platform.transform.position.y + recycleThreshold)))
            {
                platform.SetActive(false);  // Deactivate and make it reusable
            }
        }

        foreach (var platform in fallingPlatformsPool)
        {
            if (platform.activeInHierarchy && (playerY > (platform.transform.position.y + recycleThreshold)))
            {
                platform.SetActive(false);  // Deactivate and make it reusable
            }
        }

        foreach (var platform in boostPlatformsPool)
        {
            if (platform.activeInHierarchy && (playerY > (platform.transform.position.y + recycleThreshold)))
            {
                platform.SetActive(false);  // Deactivate and make it reusable
            }
        }
    }

    // Change the game stage and add new platforms to the pool accordingly
    public void changeGameStage()
    {
        gameStage += 1;
        if (gameStage == 1)
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject newPlatform = Instantiate(platformPrefabs[1]);
                newPlatform.SetActive(false);
                fallingPlatformsPool.Add(newPlatform);
            }
        }
        else if (gameStage == 2)
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject newPlatform = Instantiate(platformPrefabs[2]);
                newPlatform.SetActive(false);
                boostPlatformsPool.Add(newPlatform);
            }
        }
    }

}