using System.Collections;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    [Header("Spawn Points")]
    [SerializeField] Transform defaultSpawnPoint;
    [SerializeField] Transform foodMinigame_SpawnPoint;
    [SerializeField] Transform schoolMinigame_SpawnPoint;

    [Header("PlayerPrefs Keys")]
    [SerializeField] private string foodMinigame_Finished;
    [SerializeField] private string school_Finished;

    private Transform playerTransform;
    private CharacterController controller;

    void Start()
    {
        // Auto-find the player in ALL scenes (build-safe)
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("SpawnPointManager ERROR → No GameObject with tag 'Player' found!");
            return;
        }

        playerTransform = player.transform;
        controller = player.GetComponent<CharacterController>();

        if (controller == null)
        {
            Debug.LogError("SpawnPointManager ERROR → Player has no CharacterController!");
            return;
        }

        // Determine which spawn point to use
        Vector3 spawnPos;

        if (PlayerPrefs.HasKey(school_Finished))
        {
            spawnPos = schoolMinigame_SpawnPoint.position;
        }
        else if (PlayerPrefs.HasKey(foodMinigame_Finished))
        {
            spawnPos = foodMinigame_SpawnPoint.position;
        }
        else
        {
            spawnPos = defaultSpawnPoint.position;
        }

        // CharacterController-safe teleport
        TeleportPlayer(spawnPos);
    }

    private void TeleportPlayer(Vector3 position)
    {
        controller.enabled = false;   // Prevent physics from blocking teleport
        playerTransform.position = position;
        controller.enabled = true;
    }
}