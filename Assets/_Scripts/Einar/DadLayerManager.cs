using System.Collections.Generic;
using UnityEngine;

public class DadLayerManager : MonoBehaviour
{
    [SerializeField]
    private Transform mainCharacter; // Assign your main character's transform in inspector

    [SerializeField]
    private List<SpriteRenderer> objectsInScene; // List of objects to modify

    // Store original sortingOrder for each object
    private Dictionary<SpriteRenderer, int> originalSortingOrders = new Dictionary<SpriteRenderer, int>();

    void Start()
    {
        // Save the original sortingOrder for each object
        foreach (var sprite in objectsInScene)
        {
            if (sprite != null && !originalSortingOrders.ContainsKey(sprite))
            {
                originalSortingOrders[sprite] = sprite.sortingOrder;
            }
        }
    }

    void Update()
    {
        foreach (var sprite in objectsInScene)
        {
            if (sprite != null && mainCharacter != null && originalSortingOrders.ContainsKey(sprite))
            {
                int baseOrder = originalSortingOrders[sprite];
                if (sprite.transform.position.z > mainCharacter.position.z)
                {
                    // In front: decrease relative to original
                    sprite.sortingOrder = baseOrder - 100;
                }
                else
                {
                    // Behind: increase relative to original
                    sprite.sortingOrder = baseOrder + 100;
                }
            }
        }
    }
}
