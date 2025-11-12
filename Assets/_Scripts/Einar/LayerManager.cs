using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    public Transform player;
    public List<SpriteRenderer> obstacles;

    public string defaultSortingLayerName = "Default";

    void Update()
    {
        foreach (var obstacle in obstacles)
        {
            if (obstacle.transform.position.z > player.position.z)
            {
                //change to "Default" layer if not already
                if (obstacle.sortingLayerName != defaultSortingLayerName)
                {
                    obstacle.sortingLayerName = defaultSortingLayerName;
                }
            }
        }
    }
}

