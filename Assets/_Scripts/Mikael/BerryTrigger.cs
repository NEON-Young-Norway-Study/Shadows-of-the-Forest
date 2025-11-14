using UnityEngine;

public class BerryTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip BerryBasketSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Berry")){
            Debug.Log("Sound Played");
            SoundEffectManager.Instance.PlaySoundFXClip(BerryBasketSound, transform, 0.2f);
        }
    }
}
