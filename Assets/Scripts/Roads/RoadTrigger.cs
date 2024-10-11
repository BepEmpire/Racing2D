using UnityEngine;

public class RoadTrigger : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    
    private RoadSpawner _roadSpawner;

    private void Start()
    {
        _roadSpawner = FindObjectOfType<RoadSpawner>();
    }

    public void DestroyRoad()
    {
        Destroy(parent);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.PLAYER_TAG))
        {
            _roadSpawner.SpawnRoad();
        }
    }
}
