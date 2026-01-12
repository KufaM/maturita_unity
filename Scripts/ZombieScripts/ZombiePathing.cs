using UnityEngine;
using UnityEngine.AI;

public class ZombiePathing : MonoBehaviour
{
    private GameObject player;
    private Transform playerTransform;
    private NavMeshAgent agent;
    private float minStoppingDistance = 1f;
    private Animator animator;
    public float distanceToPlayer;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();


        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("Player not found");
        }
    }

    private void Update()
    {
        
        if (agent != null)
        {
            // Calculate distance to player
            distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer > 1.70)
            {
                animator.ResetTrigger("Attack");
                animator.SetTrigger("Walk");
            }
            else
            {
                animator.ResetTrigger("Walk");
                animator.SetTrigger("Attack");
            }

            if (distanceToPlayer >= minStoppingDistance)
            {
                agent.SetDestination(playerTransform.position);
            }
            else
            {
                float decelerationFactor = Mathf.Clamp01(distanceToPlayer / minStoppingDistance);
                agent.speed = agent.speed * decelerationFactor;
            }
        }
        else
        {
            Debug.LogWarning("NavMeshAgent component not found on Zombie");
        }
    }
}