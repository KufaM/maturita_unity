using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.Impl;

public class ZombieSpawn : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform playerTransform;
    public float score = scorehighscore.score;
    public float health;
    private float respawnDistance = 300f;
    private Animator animator;
    private Collider zombieCollider;
    private bool dead;
    private bool firstSpawn = true;
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        zombieCollider = GetComponent<Collider>();
        zombieCollider.enabled = true;
        health = 3;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        SpeedAdjust();

        if (firstSpawn)
        {
            Vector3 respawnPosition = RandomNavMeshPosition();
            transform.position = respawnPosition;
            firstSpawn = false;
        }
    }

    // Method to apply damage to the zombie
    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0f && !dead)
        {
            dead = true;
            animator = GetComponent<Animator>();
            zombieCollider = GetComponent<Collider>();
            zombieCollider.enabled = false;
            animator.SetTrigger("Death");
            navMeshAgent.speed = 0;

            Destroy(this.gameObject, 3f);
            if (Random.Range(0f, 1f) <= 0.2f)
            {
                LifeDisplay.currentHp = Mathf.Min(LifeDisplay.currentHp + 5, LifeDisplay.maxHp);
            }

            scorehighscore.Kill();
        }
    }

    // Find a random position on the NavMesh within respawnDistance of the player
    private Vector3 RandomNavMeshPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * respawnDistance;
        randomDirection += playerTransform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, respawnDistance, NavMesh.AllAreas))
        {
            RaycastHit terrainHit;

            // Raycast downwards to find the ground position
            if (Physics.Raycast(hit.position + Vector3.up * 100f, Vector3.down, out terrainHit, 200f, LayerMask.GetMask("whatIsGround")))
            {
                return terrainHit.point;
            }
            else
            {
                Debug.LogWarning("Sampled position not on ground: " + hit.position);
            }
        }
        else
        {
            Debug.LogWarning("Failed to sample position: " + randomDirection);
        }

        return playerTransform.position;
    }

    // Adjust zombie speed based on player's score
    private void SpeedAdjust()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        int speedbuff = scorehighscore.score / 5;

        agent.speed = 2;
        for (int i = 0; i < speedbuff; i++)
        {
            agent.speed += 0.2f;
        }
        Debug.Log("Zombie Speed: " + agent.speed);
    }

    // Respawn a new zombie when this one is destroyed
    private void OnDestroy()
    {
        Vector3 respawnPosition = RandomNavMeshPosition();

        GameObject newZombie = Instantiate(zombiePrefab, respawnPosition, Quaternion.identity);

        newZombie.SetActive(true);
    }
}