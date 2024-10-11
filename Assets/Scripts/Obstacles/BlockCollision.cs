using UnityEngine;

public class BlockCollision : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private int damage = 100;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(Tags.PLAYER_TAG))
        {
            HealthManager healthManager = other.gameObject.GetComponent<HealthManager>();

            if (healthManager != null)
            {
                healthManager.TakeDamage(damage);
            }
        }
    }
}