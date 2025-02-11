using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    public int damageAmount;

    private bool canDealDamage = false;

    public void EnableDamage(int damage)
    {
        canDealDamage = true;
        damageAmount = damage;
    }

    public void DisableDamage()
    {
        canDealDamage = false;
        damageAmount = 10;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canDealDamage && other.CompareTag("Enemy"))
        {
            Debug.Log("Hit " + other.name);
            other.GetComponent<EnemyHealth>()?.TakeDamage(damageAmount);
        }
    }
}
