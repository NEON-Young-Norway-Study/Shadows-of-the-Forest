using UnityEngine;

public class BerryCollectible : MonoBehaviour
{
    public float slideSpeed = 5f;     // Speed of sliding towards bucket
    private bool isSliding = false;

    public Transform bucketTransform;


    public void Collect()
    {
        isSliding = true;
        GetComponent<Collider2D>().enabled = false; // Optional
    }

    void Update()
    {
        if (isSliding && bucketTransform != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, bucketTransform.position, slideSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, bucketTransform.position) < 0.1f)
            {
                StoreBerry();
            }
        }
    }

    private void StoreBerry()
    {
        //BerryManager.AddBerry(gameObject);
        
    }
}
