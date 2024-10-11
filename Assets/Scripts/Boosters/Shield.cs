using UnityEngine;

public class Shield : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.PLAYER_TAG))
        {
            CarController carController = other.gameObject.GetComponent<CarController>();
            
            if (carController != null)
            {
                carController.ActivateShield();
            }

            Destroy(gameObject);
        }
    }
}
