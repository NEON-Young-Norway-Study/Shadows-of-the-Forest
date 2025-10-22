using UnityEngine;

public class BeeScript : MonoBehaviour
{
    public float beeSpeed = 1f;
    public Vector3 moveDirection = Vector3.left;

    void Update()
    {
        transform.Translate(moveDirection * beeSpeed * Time.deltaTime); 
    }

}
