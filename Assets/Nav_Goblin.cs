using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Vector3 pointA;
    [SerializeField] Vector3 pointB;
    [SerializeField] Vector3 pointC;


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
}
