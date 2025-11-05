using UnityEngine;
using UnityEngine.InputSystem;

public class Cursor_Hitbox : MonoBehaviour
{
    public GameObject colliderPrefab;
    private GameObject colliderInstance;

    void Start()
    {
        colliderInstance = Instantiate(colliderPrefab);

        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        colliderInstance.transform.position = worldPosition;
    }
}
