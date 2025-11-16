using UnityEngine;
using System.Collections.Generic;

public class PlatformSpawner : MonoBehaviour
{
    [Header("Platform Settings")]
    public GameObject platformPrefab;
    public int startPlatformCount = 15;
    
    [Header("Spawn Distance")]
    public float minYDistance = 1.5f;
    public float maxYDistance = 2.5f;
    
    private float highestY;
    private float screenLeft;
    private float screenRight;
    private float platformWidth;
    private List<GameObject> platforms = new List<GameObject>();

    void Start()
    {
        CalculateScreenBounds();
        CalculatePlatformWidth();
        SpawnInitialPlatforms();
    }

    void Update()
    {
        SpawnPlatformsAboveCamera();
        RemoveOldPlatforms();
    }

    void CalculateScreenBounds()
    {
        Camera cam = Camera.main;
        screenLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        screenRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        
        Debug.Log($"Screen: Left={screenLeft:F2}, Right={screenRight:F2}");
    }

    void CalculatePlatformWidth()
    {
        if (platformPrefab.GetComponent<SpriteRenderer>() != null)
        {
            platformWidth = platformPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        }
        else
        {
            platformWidth = 1f;
        }
        
        Debug.Log($"Platform width: {platformWidth:F2}");
    }

    void SpawnInitialPlatforms()
    {
        highestY = Camera.main.transform.position.y - 4f;
        
        for (int i = 0; i < startPlatformCount; i++)
        {
            SpawnPlatform();
        }
        
        Debug.Log($"Spawned {startPlatformCount} platforms at start");
    }

    void SpawnPlatform()
    {
        highestY += Random.Range(minYDistance, maxYDistance);
        
        float minX = screenLeft + (platformWidth / 2f) + 0.2f;
        float maxX = screenRight - (platformWidth / 2f) - 0.2f;
        float randomX = Random.Range(minX, maxX);
        
        Vector3 spawnPosition = new Vector3(randomX, highestY, 0f);
        GameObject platform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        platform.transform.SetParent(transform);
        platforms.Add(platform);
    }

    void SpawnPlatformsAboveCamera()
    {
        float cameraTopY = Camera.main.transform.position.y + Camera.main.orthographicSize;
        
        while (highestY < cameraTopY + 10f && platforms.Count < 50)
        {
            SpawnPlatform();
        }
    }

    void RemoveOldPlatforms()
    {
        float cameraBottomY = Camera.main.transform.position.y - Camera.main.orthographicSize;
        
        for (int i = platforms.Count - 1; i >= 0; i--)
        {
            if (platforms[i] != null && platforms[i].transform.position.y < cameraBottomY - 5f)
            {
                Destroy(platforms[i]);
                platforms.RemoveAt(i);
            }
        }
    }
}