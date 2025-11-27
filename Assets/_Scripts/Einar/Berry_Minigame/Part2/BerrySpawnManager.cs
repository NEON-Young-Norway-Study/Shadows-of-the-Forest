using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerrySpawnManager : MonoBehaviour
{
    public List<GameObject> prefabs; 
    public float spawnInterval = 1f; 
    public float moveSpeed = 2f; 
    [SerializeField] float minX = 0; 
    [SerializeField] float maxX = 0;  

    private Queue<GameObject> spawnQueue;

    void Start()
    {
        spawnQueue = new Queue<GameObject>(prefabs);
        StartCoroutine(SpawnPrefabs());
    }

    IEnumerator SpawnPrefabs()
    {
        while (spawnQueue.Count > 0)
        {
            float randomZRotation = Random.Range(0f, 360f);

            Quaternion rotation = Quaternion.Euler(0, 0, randomZRotation);

            GameObject prefab = spawnQueue.Dequeue();

            float randomX = Random.Range(minX, maxX);
            Vector3 spawnPosition = new Vector3(randomX, transform.position.y, transform.position.z);
            GameObject obj = Instantiate(prefab, spawnPosition, rotation);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
