using UnityEngine;

public class TriggerController : MonoBehaviour
{
    [SerializeField] private GameObject ObjectToActivate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ObjectToActivate.SetActive(true);
        }
    }
}
