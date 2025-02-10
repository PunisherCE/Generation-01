using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    public int damageAmount = 10;

    private bool canDealDamage = false;

    public void EnableDamage()
    {
        canDealDamage = true;
    }

    public void DisableDamage()
    {
        canDealDamage = false;
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
