using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset = new Vector3(0, 2.0f, -10f);
    [SerializeField] private float xRange = 0;

    void LateUpdate()
    {
        Vector3 targetPosition = player.position + offset;

        targetPosition.x = Mathf.Clamp(targetPosition.x, xRange, xRange);

        transform.position = targetPosition;
    }
}
