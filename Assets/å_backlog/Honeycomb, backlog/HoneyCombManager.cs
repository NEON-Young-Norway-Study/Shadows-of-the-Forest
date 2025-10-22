using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class HoneyCombManager : MonoBehaviour
{
    [Header("Bee Prefabs")]
    public GameObject beesToSpawnRight;
    public GameObject beesToSpawnLeft;

    [Header("Spawn Points")]
    public GameObject[] beeSpawnLocationsRight;
    public GameObject[] beeSpawnLocationsLeft;

    [Header("Spawn Settings")]
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 5f;
    public float beeLifetime = 10f;

    private bool spawning = true;

    void Start()
    {
        StartCoroutine(SpawnBeesRoutine());
    }

    IEnumerator SpawnBeesRoutine()
    {
        while (spawning)
        {
            float waitTime = UnityEngine.Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            bool spawnFromRight = UnityEngine.Random.value > 0.5f;

            if (spawnFromRight)
                SpawnBee(beesToSpawnRight, beeSpawnLocationsRight);
            else
                SpawnBee(beesToSpawnLeft, beeSpawnLocationsLeft);
        }
    }

    void SpawnBee(GameObject beePrefab, GameObject[] spawnPoints)
    {
        if (spawnPoints.Length == 0 || beePrefab == null) return;

        int index = UnityEngine.Random.Range(0, spawnPoints.Length);
        Vector3 spawnPos = spawnPoints[index].transform.position;

        GameObject bee = Instantiate(beePrefab, spawnPos, Quaternion.identity);

        Destroy(bee, beeLifetime);
    }

    public void StopSpawning()
    {
        spawning = false;
    }
}
