using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] private Transform[] checkpoints;
    [SerializeField] private Animator animator;

    [Header("Body part references")]
    [SerializeField] private GameObject front;
    [SerializeField] private GameObject back;
    [SerializeField] private GameObject side;

    [Header("Arm references")]
    [SerializeField] private GameObject front_arms;
    [SerializeField] private GameObject back_arms;

    private NavMeshAgent agent;
    private int currentCheckpoint = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agent.updateRotation = false;

        if (checkpoints.Length > 0)
        {
            agent.SetDestination(checkpoints[currentCheckpoint].position);
        }
    }

    void Update()
    {
        AnimCheck();
    }

    public void MoveToNextCheckpoint()
    {
        if (checkpoints.Length == 0) return;

        currentCheckpoint++;
        if (currentCheckpoint >= checkpoints.Length)
        {
            currentCheckpoint = checkpoints.Length - 1;
        }

        agent.SetDestination(checkpoints[currentCheckpoint].position);
    }

    private void AnimCheck()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(agent.velocity);
        bool isMoving = agent.velocity.magnitude > 0.1f;
        animator.SetBool("isMoving", isMoving);

        if (isMoving)
        {
            Vector3 direction = agent.velocity.normalized;
            float absX = Mathf.Abs(direction.x);
            float absZ = Mathf.Abs(direction.z);

            if (absZ > absX)
            {
                // Moving forward/backward
                if (direction.z > 0)
                {
                    front.SetActive(false);
                    side.SetActive(false);
                    back.SetActive(true);
                    back_arms.SetActive(true);
                    animator.SetInteger("Direction", 0); // Forward
                }
                else
                {
                    side.SetActive(false);
                    back.SetActive(false);
                    front.SetActive(true);
                    front_arms.SetActive(true);
                    animator.SetInteger("Direction", 1); // Backward
                }
            }
            else
            {
                // Moving left/right
                back.SetActive(false);
                front.SetActive(false);
                side.SetActive(true);

                if (direction.x > 0)
                {
                    side.transform.rotation = Quaternion.Euler(0, 180, 0);
                    animator.SetInteger("Direction", 2); // Right
                }
                else
                {
                    side.transform.rotation = Quaternion.Euler(0, 0, 0);
                    animator.SetInteger("Direction", 3); // Left
                }
            }
        }
        else
        {
            // Idle
            back.SetActive(false);
            side.SetActive(false);
            back_arms.SetActive(false);
            front_arms.SetActive(true);
            front.SetActive(true);
            animator.SetInteger("Direction", -1);
        }
    }
}
