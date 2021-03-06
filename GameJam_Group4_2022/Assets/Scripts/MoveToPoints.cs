using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class MoveToPoints : MonoBehaviour
{
    public NavMeshAgent agent;
    public List<Transform> points;
    public int pointIndex;
    public playerState playerState;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        agent.SetDestination(points[pointIndex].position);//Sets the agent's destination to the first point in the list on startup
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState != GameManager.instance.playerState) playerState = GameManager.instance.playerState;

        if (playerState != playerState.End && playerState != playerState.Failed && agent.pathEndPosition 
            != points[pointIndex].position && agent.remainingDistance < 0.5)
        {
            try
            {
                pointIndex++;
                if (pointIndex > points.Count - 1) GameManager.instance.playerState = playerState.End;
                agent.SetDestination(points[pointIndex].transform.position);
                Debug.Log("Setting next destination " + points[pointIndex].name);
            }
            catch(System.ArgumentOutOfRangeException)
            {
                Debug.Log("Agent has made it to the end");
            }
        }
    }

    public void stopAgent()
    {
        //Forces the agent to stop and sets their velocity to zero, halting the agent in place
        agent.isStopped = true;
        agent.velocity = new Vector3(0,0,0);
    }

    public void playFootstep()
    {
        if (GameManager.instance.playerState == playerState.Neutral)
        {
            int x;
            x = Random.Range(0, 2);
            if (x <= 0)
            {
                FindObjectOfType<AudioManager>().Play("Walk1");
            }
            else if (x == 1)
            {
                FindObjectOfType<AudioManager>().Play("Walk2");
            }
            else if (x >= 2)
            {
                FindObjectOfType<AudioManager>().Play("Walk3");
            }
        }
    }
}
