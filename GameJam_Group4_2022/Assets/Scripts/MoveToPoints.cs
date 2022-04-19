using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum playerState
{
    End,
    Failed,
    Neutral
}
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
        agent.SetDestination(points[pointIndex].position);
        playerState = playerState.Neutral;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState != playerState.End && playerState != playerState.Failed && agent.pathEndPosition 
            != points[pointIndex].position && agent.remainingDistance < 0.5)
        {
            try
            {
                pointIndex++;
                if (pointIndex > points.Count - 1) playerState = playerState.End;
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
        agent.isStopped = true;
        agent.velocity = new Vector3(0,0,0);
    }
}
