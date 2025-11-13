using System.Numerics;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] private Transform[] checkpoints;
    [SerializeField] private Animator animator;
    private NavMeshAgent agent;
    private int currentCheckpoin = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agent.updateRotation = false;

        if (checkpoints.Length > 0)
        {
            agent.SetDestination(checkpoints[currentCheckpoin].position);
        }        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.rotation = UnityEngine.Quaternion.Euler(0, 180, 0);

        // UnityEngine.Vector3 localVelocity = transform.InverseTransformDirection(agent.velocity);

        // if (agent.velocity.magnitude > 0.1f)
        // {
        //     animator.SetBool("isMoving", true);
        // }

        // if (animator.GetBool("IsMoving"))
        // {
        //     if (agent.velocity.z > 0.1f)
        //     {
        //         animator.SetBool("isMoving", true);
        //         animator.SetInteger("Direction", 0); // Forward
        //     }
        //     else if (agent.velocity.z < -0.1f)
        //     {
        //         animator.SetBool("isMoving", true);
        //         animator.SetInteger("Direction", 1); // Backward
        //     }
        //     else if (agent.velocity.x > 0.1f)
        //     {
        //         animator.SetBool("isMoving", true);
        //         animator.SetInteger("Direction", 2); // Right
        //     }
        //     else if (agent.velocity.x < -0.1f)
        //     {
        //         animator.SetBool("isMoving", true);
        //         animator.SetInteger("Direction", 3); // Left
        //     }
        // }
        // else
        // {
        //     animator.SetBool("isMoving", false);
        // }
        if (agent.velocity.magnitude > 0.1f)
        {
            agent.updateRotation = true;
            animator.SetBool("isMoving", true);
            animator.SetInteger("Direction", 1);
        }

        if (agent.velocity.magnitude <= 0.1f)
        {
            agent.updateRotation = false;
            animator.SetBool("isMoving", false);
        }
    }
    
    public void MoveToNextCheckpoint()
    {
        if (checkpoints.Length == 0) return;

        currentCheckpoin = Mathf.Clamp(currentCheckpoin + 1, 0, checkpoints.Length - 1);
        agent.SetDestination(checkpoints[currentCheckpoin].position);
        currentCheckpoin++;
    }
}
