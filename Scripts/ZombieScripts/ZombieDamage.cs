using UnityEngine;

public class ZombieDamage : MonoBehaviour
{
    private Transform player;
    public float damageAmount = 10f;
    public float damageInterval = 2f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Apply damage to the player when in contact with the zombie
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InvokeRepeating("ApplyDamage", 0f, damageInterval);
        }
    }

    // Stop applying damage when the player exits the zombie's collider
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CancelInvoke("ApplyDamage");
        }
    }

    private void ApplyDamage()
    {
        LifeDisplay.currentHp -= damageAmount;
        LifeDisplay.currentHp = Mathf.Max(LifeDisplay.currentHp, 0);
    }
}