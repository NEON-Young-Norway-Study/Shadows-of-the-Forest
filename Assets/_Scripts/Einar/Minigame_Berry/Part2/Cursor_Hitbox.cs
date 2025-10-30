using UnityEngine;
using UnityEngine.InputSystem;

public class Cursor_Hitbox : MonoBehaviour
{
    public GameObject colliderPrefab; // Assign your collider prefab here
    private GameObject colliderInstance;

    void Start()
    {
        // Instantiate the collider prefab
        colliderInstance = Instantiate(colliderPrefab);
    }

    void Update()
    {
        // Read mouse position in screen space
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        // Convert mouse position to world position (2D)
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Set the collider's position directly (no Vector3 needed!)
        colliderInstance.transform.position = worldPosition;
    }
}
