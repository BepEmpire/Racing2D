using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] roadPieces;
    [SerializeField] private float yOffset = 10.2f;
    [SerializeField] private float currentYPos = 0.0f;

    private void Start()
    {
        SpawnRoad();
    }

    public void SpawnRoad()
    {
        GameObject randomRoad = roadPieces[Random.Range(0, roadPieces.Length)];
        GameObject currentRoad = Instantiate(randomRoad, transform);
        currentYPos += yOffset;
        currentRoad.transform.position = new Vector3(0.0f, currentYPos, 0.0f);
    }
}
