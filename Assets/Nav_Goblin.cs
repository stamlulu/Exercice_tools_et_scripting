using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Vector3 pointA;
    [SerializeField] Vector3 pointB;
    [SerializeField] Vector3 pointC;
    public float detectionRange = 20f;
    public LayerMask obstacleMask;
    private Vector3 patrolPoint;
    private object hero;


    void Start()
    {

        StartCoroutine(Patrol());
        //Vector3.Distance(transform.position, new Vector3(10f, 50f, -6f));
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            yield return GoTo(pointA);
            yield return GoTo(pointB);
            yield return GoTo(pointC);
        }

    }
    IEnumerator GoTo(Vector3 destination)
    {
        //PointA
        agent.SetDestination(destination);
        while (Vector3.Distance(transform.position, destination) > 1f)
        { //ou yield return null
            yield return 0;
        }
        Debug.Log($"At point destination {destination}");

        float randomWait = Random.Range(5f, -7f);
        yield return new WaitForSeconds(Random.Range(5f, -7f));
        Debug.Log($"Waited {randomWait}secs");
        //Random.Range(minVal, maxVal);
    }
    private void Update()
    {
        float distanceToHero = Vector3.Distance(transform.position, agent.nextPosition);

        if (distanceToHero <= detectionRange && HasLineOfSight())
        {
            agent.SetDestination(agent.nextPosition);
        }
        else
        {
            Patrol();
           
        }
    }
    bool HasLineOfSight()
    {
        Vector3 directionToHero = (agent.nextPosition - transform.position).normalized;
        if (!Physics.Raycast(transform.position, directionToHero, detectionRange, obstacleMask))
        {
            return true;
        }
        return false;

    }
    private EnemyPatrol()
    {
        if (Vector3.Distance(transform.position, patrolPoint) < 1f)
        {
            SetRandomPatrolPoint();
        }
        agent.SetDestination(patrolPoint);
    }
    void SetRandomPatrolPoint()
    {
        float randomX = Random.Range(-10f, 10f);
        float randomZ = Random.Range(-10f, 10f);
        patrolPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
    }
}
