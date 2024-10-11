using UnityEngine;

public class Bubble : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.PLAYER_TAG))
        {
            CarController carController = other.gameObject.GetComponent<CarController>();

            if (carController == null || carController.IsShieldActive) 
                return;
            
            carController.ActivateSlowdown();
        }
    }
}
