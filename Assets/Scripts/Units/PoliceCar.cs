using System.Collections;
using UnityEngine;

public class PoliceCar : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private bool isChasing = false;
    [SerializeField] private Vector3 initialPositionOffset = new Vector3(0, -3.5f, 0);
    [SerializeField, Range(0, 100)] private int damage = 100;
    [SerializeField] private float chaseSpeed = 6.0f;
    [SerializeField] private float followSpeed = 4.0f;
    [SerializeField] private float safeDistance = 4.0f;
    [SerializeField] private float policeSpawnDelay = 60.0f;
    [SerializeField] private float chasingTime = 10.0f;

    private void Start()
    {
        StartCoroutine(PoliceBehaviour());
    }

    private void Update()
    {
        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            MaintainDistance();
        }
    }

    private void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
    }

    private void MaintainDistance()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < safeDistance)
        {
            Vector3 moveDirection = (transform.position - player.position).normalized;
            transform.position += moveDirection * followSpeed * Time.deltaTime;
        }

        else if (distanceToPlayer > safeDistance + 2.0f && distanceToPlayer < 20.0f)
        {
            Vector3 moveDirection = (player.position - transform.position).normalized;
            transform.position += moveDirection * followSpeed * Time.deltaTime;
        }
    }

    private IEnumerator PoliceBehaviour()
    {
        while (true)
        {
            yield return new WaitForSeconds(policeSpawnDelay);

            ResetPoliceCarPosition();

            isChasing = true;
            yield return new WaitForSeconds(chasingTime);

            isChasing = false;
        }
    }

    private void ResetPoliceCarPosition()
    {
        transform.position = player.position + initialPositionOffset;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.PLAYER_TAG))
        {
            HealthManager healthManager = other.GetComponent<HealthManager>();
            
            if (healthManager != null)
            {
                healthManager.TakeDamage(damage);
            }
        }
    }
}
