using System.Collections;
using UnityEngine;

public class PoisonBerry : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger entered with " + other.name);
        //if (other.CompareTag("Player"))
        //{
            Animator playerAnimator = other.GetComponent<Animator>();
            if (playerAnimator != null)
            {
                playerAnimator.SetBool("isDead", true);
                Debug.Log("Play dead");

                PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
                 if (playerMovement != null)
                    {
                        playerMovement.enabled = false;
                        Debug.Log("Play dead");
                        StartCoroutine(DestroyBerry());
                    }
            }

            //poison cloud vfx later and sfx
        //}
    }
    IEnumerator DestroyBerry()
    {
        Debug.Log("Destroying GO");
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
