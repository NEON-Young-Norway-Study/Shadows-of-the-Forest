using UnityEngine;




public class SalmonMovement : MonoBehaviour
{

    private float speed;
    private int direction;

    private bool isStuck = false;

    [SerializeField] private AudioClip clawHitSound;
    [SerializeField] private AudioClip clawDropSound;

    public void SetSpeed(float s, int dir)
    {
        speed = s;
        direction = dir;
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * direction * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isStuck && other.CompareTag("Player"))
        {
            transform.parent = other.transform;
            SoundEffectManager.Instance.PlaySoundFXClip(clawHitSound, transform, 0.3f);
            isStuck = true;
            speed = 0;

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (isStuck && other.CompareTag("TriggerZone"))
        {
            transform.parent = other.transform;
            
            SoundEffectManager.Instance.PlaySoundFXClip(clawDropSound, transform, 1f);

            isStuck = false;
            speed = 100;
        }
    }
}
