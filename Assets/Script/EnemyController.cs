using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;

    //[SerializeField] float patrolDistance;

    [SerializeField] List<Transform> patrolPoints = new();

    [SerializeField] Transform hero;
    [SerializeField] LayerMask detectionLayermask;
    Coroutine patrolCoroutine;
    Coroutine hunteCoroutine;
    GoblinState goblinState;

    enum GoblinState
    {
        hunt,
        patrol,
        fight,

    }
    // Start is called before the first frame update
    void Start()
    {
        patrolCoroutine = StartCoroutine(Patrol());
        StartCoroutine(searchHero());
    }

    private IEnumerator Patrol()
    {
        goblinState = GoblinState.patrol;
        while (true)
        {
            for (int i = 0; i < patrolPoints.Count; i++)
            {
                yield return GoTo(patrolPoints[i].position);
                yield return new WaitForSeconds(UnityEngine.Random.Range(3f, 9f));
            }
        }

    }
    IEnumerator searchHero()
    {
        while (true)
        {
            if (CanSeeHero() && goblinState == GoblinState.patrol)
            {   //Go Hunt
                Debug.Log("Find Hero");
                StopCoroutine(patrolCoroutine);
                hunteCoroutine = StartCoroutine(Hunt());
                //
            }
            else if (!CanSeeHero() && goblinState == GoblinState.hunt)
            {
                Debug.Log("Go Patrol");
                StopCoroutine(hunteCoroutine);
                patrolCoroutine = StartCoroutine(Patrol());
            }
            yield return null;
        }
    }
    private IEnumerator Hunt()
    {
        goblinState = GoblinState.hunt;
        while (true)
        {
            agent.SetDestination(hero.position);
            yield return 0;
        }
    }

    private IEnumerator GoTo(Vector3 target)
    {
        agent.SetDestination(target);

        while (Vector3.Distance(transform.position, target) > 3f)
        {
            yield return null;
        }
    }

    private bool CanSeeHero()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (hero.position - transform.position), out hit, 20f, detectionLayermask))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Hero"))//if collide with hero
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        else
        {
            return false;
        }


    }
}
