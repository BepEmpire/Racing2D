using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.ROAD_TAG))
        {
            RoadTrigger roadTrigger = other.GetComponent<RoadTrigger>();
            roadTrigger.DestroyRoad();
        }
    }
}
