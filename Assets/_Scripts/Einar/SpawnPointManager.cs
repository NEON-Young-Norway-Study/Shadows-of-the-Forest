using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    [SerializeField] Transform defaultSpawnPoint;
    [SerializeField] Transform foodMinigame_SpawnPoint;
    [SerializeField] Transform schoolMinigame_SpawnPoint;

    [SerializeField] Transform playerTransform;

    [SerializeField] private string foodMinigame_Finished;
    [SerializeField] private string school_Finished;

    void Start()
    {
        if (PlayerPrefs.HasKey(school_Finished))
        {
            playerTransform.position = schoolMinigame_SpawnPoint.position;
        }
        else if (PlayerPrefs.HasKey(foodMinigame_Finished))
        {
            playerTransform.position = foodMinigame_SpawnPoint.position;
        }
        else
        {
            playerTransform.position = defaultSpawnPoint.position;
        }
    }
}
