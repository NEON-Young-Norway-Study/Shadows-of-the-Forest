using UnityEngine;
using UnityEngine.SceneManagement;

public class Floor_Trigger : MonoBehaviour
{
    [SerializeField] BerryBasket berryBasket;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Berry"))
        {
            berryBasket.BerryTouchfloor();
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
