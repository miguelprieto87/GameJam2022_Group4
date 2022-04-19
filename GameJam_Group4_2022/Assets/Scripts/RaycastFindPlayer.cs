using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastFindPlayer : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject target;
    public int rayCount = 50;
    public float rayDistance;
    public bool playerFound = false;
    public GameObject testSpawn;

    bool isReversing;

    // Start is called before the first frame update
    void Start()
    {
        if (target == null) target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, layerMask) && !playerFound)
        {
            if (hit.collider.tag == "OOB")
            {
                Debug.Log("No hit");
                isReversing = !isReversing;
                moveThis();
            }
            else if (hit.collider.gameObject == target)
            {
                if (target.GetComponent<MoveToPoints>().playerState != playerState.Failed)
                {
                    Debug.Log("<color=red>Player object found!</color>");
                    try
                    {
                        target.GetComponent<MoveToPoints>().playerState = playerState.Failed;
                        target.GetComponent<MoveToPoints>().stopAgent();
                    }
                    catch (System.NullReferenceException)
                    {
                        Debug.Log("GO Only has player tag!");
                    }
                    playerFound = true;
                }
            }
            else
            {
                Debug.Log("<color=green>Just a wall.</color>");
            }
            moveThis();
        }
    }

    void moveThis()
    {
        if (isReversing) transform.Translate(new Vector3(rayDistance, 0, 0));
        else transform.Translate(new Vector3(-rayDistance, 0, 0));
    }
}
