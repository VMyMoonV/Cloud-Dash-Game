using UnityEngine;
using System.Collections.Generic;

public class PlatformSpawner : MonoBehaviour
{
    public GameSettings settings;

    [Header("Platform Prefabs")]
    public GameObject jumpPrefab;
    public GameObject strongPrefab;
    public GameObject weakPrefab;

    private float highestY;
    private float screenLeft;
    private float screenRight;
    private List<GameObject> platforms = new List<GameObject>();

    void Start()
    {
        CalculateScreenBounds();
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
        screenLeft = cam.ScreenToWorldPoint(new Vector3(0, 0)).x;
        screenRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0)).x;
    }

    void SpawnInitialPlatforms()
    {
        highestY = Camera.main.transform.position.y - 4f;

        for (int i = 0; i < settings.startPlatformCount; i++)
            SpawnPlatform();
    }

    void SpawnPlatform()
    {
        highestY += Random.Range(settings.minYDistance, settings.maxYDistance);

        GameObject prefab = GetRandomPrefab();
        float width = prefab.GetComponent<SpriteRenderer>().bounds.size.x;

        float minX = screenLeft + width / 2f;
        float maxX = screenRight - width / 2f;
        Vector3 pos = new Vector3(Random.Range(minX, maxX), highestY, 0f);

        GameObject platform = Instantiate(prefab, pos, Quaternion.identity);
        platform.transform.SetParent(transform);
        platforms.Add(platform);
    }

    GameObject GetRandomPrefab()
    {
        float rand = Random.value;
        if (rand < 0.6f) return jumpPrefab;
        else if (rand < 0.85f) return strongPrefab;
        else return weakPrefab;
    }

    void SpawnPlatformsAboveCamera()
    {
        float cameraTop = Camera.main.transform.position.y + Camera.main.orthographicSize;
        while (highestY < cameraTop + 10f)
            SpawnPlatform();
    }

    void RemoveOldPlatforms()
    {
        float cameraBottom = Camera.main.transform.position.y - Camera.main.orthographicSize;
        for (int i = platforms.Count - 1; i >= 0; i--)
        {
            if (platforms[i].transform.position.y < cameraBottom - 5f)
            {
                Destroy(platforms[i]);
                platforms.RemoveAt(i);
            }
        }
    }
}
