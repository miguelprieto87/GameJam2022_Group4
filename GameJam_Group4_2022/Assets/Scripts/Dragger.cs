using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : MonoBehaviour
{
    private Vector3 screenPoint;
    private Rigidbody myRB;

    bool canDrag = true;

    int width;
    int length;

    public float maxDragHeight = 2;
    public float minDragHeight = 0;

    private void Start()
    {
        myRB = GetComponent<Rigidbody>();
        length = GameManager.instance.levelZ;
        width = GameManager.instance.levelX;
    }
    void OnMouseDown()
    {
        if (canDrag)
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
    }
    void OnMouseDrag()
    {
        if (canDrag && !GameManager.instance.isPaused)
        {
            Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint);

            myRB.velocity = new Vector3(0, 0, 0);

            cursorPosition.x = Mathf.Clamp(cursorPosition.x, -width, width);
            cursorPosition.y = Mathf.Clamp(cursorPosition.y, minDragHeight, maxDragHeight);
            cursorPosition.z = Mathf.Clamp(cursorPosition.z, -length, length);
            transform.position = cursorPosition;
        }
    }
    private void Update()
    {
        if (GameManager.instance.playerState == playerState.End || GameManager.instance.playerState == playerState.Failed)
            if (canDrag) canDrag = false;
    }
}
