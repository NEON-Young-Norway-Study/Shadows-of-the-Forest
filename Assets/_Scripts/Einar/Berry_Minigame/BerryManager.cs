using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryManager : MonoBehaviour
{
    public List<GameObject> berries; // List of berries to fall
    public Vector3 topPosition; // Starting position at the top of the screen
    public float fallDuration = 1.0f; // How long each berry takes to fall
    public Vector3 fallTargetPosition; // Where berries will land

    private bool allBerriesCollected = false;

    void Update()
    {
        // You need to set allBerriesCollected to true when all 5 berries are collected
        if (AreAllBerriesCollected() && !allBerriesCollected)
        {
            allBerriesCollected = true;
            StartCoroutine(BerryFallSequence());
        }
    }

    bool AreAllBerriesCollected()
    {
        // Implement your own logic to check if all berries are collected
        // For example, if berries list contains all collected berries
        return berries.Count == 5; // or your own condition
    }

    IEnumerator BerryFallSequence()
    {
        foreach (GameObject berry in berries)
        {
            // Move berry to the top position
            berry.transform.position = topPosition;

            // Animate fall
            float elapsedTime = 0f;
            Vector3 startPos = topPosition;
            Vector3 endPos = fallTargetPosition;

            while (elapsedTime < fallDuration)
            {
                berry.transform.position = Vector3.Lerp(startPos, endPos, (elapsedTime / fallDuration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            berry.transform.position = endPos;

            // Wait 1 second before next berry
            yield return new WaitForSeconds(1f);
        }
    }
}