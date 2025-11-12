using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    [SerializeField]
    private Transform mainCharacter; // Assign your main character's transform in inspector

    [SerializeField]
    private List<SpriteRenderer> objectsInScene; // List of objects to modify

    void Update()
    {
        foreach (SpriteRenderer sprite in objectsInScene)
        {
            if (sprite != null && mainCharacter != null)
            {
                if (sprite.transform.position.z > mainCharacter.position.z)
                {
                    // Sprite is in front
                    sprite.sortingOrder = -100;
                }
                else
                {
                    // Sprite is behind
                    sprite.sortingOrder = 100;
                }
            }
        }
    }
}

