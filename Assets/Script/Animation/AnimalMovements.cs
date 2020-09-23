using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalMovements : MonoBehaviour {

    public GameObject[] wayPoints;
    NavMeshAgent agenteLocal;
    Animator animatorLocal;
    public Transform startingPos;
   public  Vector3 nextPosition;
    public float maxStopTime;
    Vector3 currentPosition;
    bool isbreakover = true;

    // Use this for initialization
    void Start () {

        animatorLocal = GetComponent<Animator>();
        agenteLocal = GetComponent <NavMeshAgent>();
        
        agenteLocal.SetDestination(startingPos.position);
        currentPosition = startingPos.position;
        nextPosition = currentPosition;
      


    }
	
	// Update is called once per frame
	void Update () {

        if (!agenteLocal.pathPending && agenteLocal.remainingDistance < 0.5f && isbreakover == true)
      {
            StartCoroutine(Movement());
            isbreakover = false;
      }

        if (agenteLocal.isStopped == false)
        {
            animatorLocal.SetBool("isMoving", true);

        }
        else
        {
            animatorLocal.SetBool("isMoving", false);
        }

	}

    IEnumerator Movement()
    {
       while (currentPosition == nextPosition)
        { 
        nextPosition = wayPoints[Random.Range(0, wayPoints.Length)].transform.position;
        }
        agenteLocal.isStopped = true;
        yield return new WaitForSeconds(3f);

        agenteLocal.SetDestination(nextPosition);
        agenteLocal.isStopped = false;
        

        if (agenteLocal.remainingDistance > agenteLocal.stoppingDistance)
        {
            isbreakover = true;
            currentPosition = nextPosition;
        }

        
    }
}
