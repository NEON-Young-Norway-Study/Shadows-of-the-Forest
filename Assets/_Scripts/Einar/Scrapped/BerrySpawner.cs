using System.Collections.Generic;
using UnityEngine;

public class BerrySpawner : MonoBehaviour
{
    public List<GameObject> bushSpheres; // Assign your sphere GameObjects in the inspector
    public GameObject normalBerryPrefab;
    public GameObject poisonousBerryPrefab;
    public int berriesPerSphere = 10;
    public float poisonousChance = 0.2f;

    void Start()
    {
        SpawnBerriesOnSpheres();
    }

    void SpawnBerriesOnSpheres()
    {
        foreach (GameObject sphere in bushSpheres)
        {
            float radius = sphere.transform.localScale.x * 0.55f; // Assumes uniform scale on x
            for (int i = 0; i < berriesPerSphere; i++)
            {
                // Generate a random point on the surface of the sphere
                Vector3 direction = Random.onUnitSphere; // Random point on surface
                Vector3 spawnPosition = sphere.transform.position + direction * radius;

                bool isPoisonous = Random.value < poisonousChance;
                GameObject berryPrefab = isPoisonous ? poisonousBerryPrefab : normalBerryPrefab;

                GameObject berry = Instantiate(berryPrefab, spawnPosition, Quaternion.identity);

                //// Optional: assign berry type
                //Berry berryComponent = berry.GetComponent<Berry>();
                //if (berryComponent != null)
                //{
                //    berryComponent.isPoisonous = isPoisonous;
            
            }
        }
    }
}
