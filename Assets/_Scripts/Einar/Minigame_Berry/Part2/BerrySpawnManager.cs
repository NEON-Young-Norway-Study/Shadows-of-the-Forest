using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerrySpawnManager : MonoBehaviour
{
    public List<GameObject> prefabs; // List of prefabs to spawn
    public float spawnInterval = 1f; // Time between spawns
    public float moveSpeed = 2f; // Speed at which objects move in -Y

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
            GameObject prefab = spawnQueue.Dequeue();
            GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity);
            StartCoroutine(MoveObject(obj));
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator MoveObject(GameObject obj)
    {
        while (obj != null)
        {
            obj.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
