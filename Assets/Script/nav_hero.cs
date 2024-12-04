using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character_navigation : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField] NavMeshAgent agent;
    [SerializeField] LayerMask environmentLayer;

    void Start()
    {
        // agent.SetDestination(new Vector3(18,50,-6));


    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetDestinationToMousePosition();
        }
    }

    void SetDestinationToMousePosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100, environmentLayer))//MathF.Infinity 
        {
            Debug.Log(hit.transform.name);
            Debug.Log(hit.point);
            agent.SetDestination(hit.point);

        }
    }


}
