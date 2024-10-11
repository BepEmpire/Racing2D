using UnityEngine;

public class Fracture : MonoBehaviour
{
    [SerializeField, Min(0)] private int damage = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.PLAYER_TAG))
        {
            CarController carController = other.gameObject.GetComponent<CarController>();

            if (carController == null || carController.IsShieldActive)
                return;
            
            carController.ActivateSlowdown();
            
            HealthManager healthManager = other.GetComponent<HealthManager>();
            
            if (healthManager != null)
            {
                healthManager.TakeDamage(damage);
            }
        }
    }
}
