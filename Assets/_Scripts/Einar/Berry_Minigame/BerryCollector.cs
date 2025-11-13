using UnityEngine;
using UnityEngine.InputSystem;

public class BerryCollector : MonoBehaviour
{
    [SerializeField] private Transform bucketTransform;
    [SerializeField] float counter;
    [SerializeField] float maxCollect;
    [SerializeField] private string targetSceneName;
    [SerializeField] private AudioClip berryPickingSound;


    public void OnClickAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            Debug.Log("World Pos: " + worldPos);

            // Draw a circle in scene view for debugging
            Debug.DrawLine(worldPos + Vector2.up * 0.1f, worldPos + Vector2.down * 0.1f, Color.red, 2f);
            Debug.DrawLine(worldPos + Vector2.left * 0.1f, worldPos + Vector2.right * 0.1f, Color.red, 2f);

            Collider2D collider = Physics2D.OverlapPoint(worldPos);
            if (collider != null)
            {
                Debug.Log("Hit collider: " + collider.gameObject.name);
                var collectible = collider.GetComponent<BerryCollectible>();

                SoundEffectManager.Instance.PlaySoundFXClip(berryPickingSound, transform, 0.2f);

                if (collectible != null)
                {
                    collectible.bucketTransform = this.bucketTransform;
                    collectible.Collect();
                    counter++;
                    if (counter == maxCollect)
                    {
                        SceneController.Instance.LoadScene(targetSceneName);
                    }
                }
            }
            else
            {
                Debug.Log("No collider detected");
            }
        }
    }
}