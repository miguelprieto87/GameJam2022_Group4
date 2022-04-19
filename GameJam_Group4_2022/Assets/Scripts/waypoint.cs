using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypoint : MonoBehaviour
{
    private GameObject target;
    void Start()
    {
        if (target == null) target = GameObject.FindGameObjectWithTag("Player");
    }
    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < 1)
        {
            Debug.Log("Point reached!");
            gameObject.SetActive(false);
        }
    }
}
