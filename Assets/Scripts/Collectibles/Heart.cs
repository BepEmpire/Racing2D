using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField, Min(0)] private int healthAmount = 25;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.PLAYER_TAG))
        {
            HealthManager healthManager = other.GetComponent<HealthManager>();
            
            if (healthManager != null)
            {
                healthManager.AddHealth(healthAmount);
                Destroy(gameObject);
            }    
        }
    }
}
