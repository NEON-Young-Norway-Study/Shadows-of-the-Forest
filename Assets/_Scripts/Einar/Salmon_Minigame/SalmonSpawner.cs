using System;
using UnityEngine;

public class SalmonSpawner : MonoBehaviour
{
    [SerializeField] GameObject salmonPrefab; // Assign your salmon prefab here
    [SerializeField] float spawnInterval = 2f; // Time between spawns
    [SerializeField] float spawnRangeY = 10f; // Horizontal spawn range
    [SerializeField] float spawnX = -5f; // Y position for spawning

    [SerializeField] private float minSpeed = 20f;
    [SerializeField] private float maxSpeed = 40f;

    private float timer = 0;

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        Debug.Log($"Timer: {timer}, SpawnInterval: {spawnInterval}");
        if (timer >= spawnInterval)
        {
            SpawnSalmon();
            timer = 0f;
            Debug.Log("Salmon spawned");
        }
    }

    void SpawnSalmon()
    {
        // Random horizontal spawn position
        float spawnY = UnityEngine.Random.Range(-spawnRangeY, spawnRangeY);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);
        GameObject salmon = Instantiate(salmonPrefab, spawnPosition, Quaternion.identity);

        // Set fixed speed moving to the left
        float speed = UnityEngine.Random.Range(minSpeed, maxSpeed);
        int direction = -1; // Always to the left

        // Assign speed and direction in the Salmon script
        var salmonScript = salmon.GetComponent<SalmonMovement>();
        if (salmonScript != null)
        {
            salmonScript.SetSpeed(speed, direction);
        }
    }
}
