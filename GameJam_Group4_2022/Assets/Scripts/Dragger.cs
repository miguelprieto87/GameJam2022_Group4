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

    public float maxDragHeight = 2;//How high can the object be dragged into the air
    public float minDragHeight = 0;//How high will the object go when initially grabbed

    private void Start()
    {
        myRB = GetComponent<Rigidbody>();
        length = GameManager.instance.levelZ;//Gets the size of the current level this object is in from the GameManager
        width = GameManager.instance.levelX;
    }
    void OnMouseDown()
    {
        if (canDrag)//On mouse click, checks to see if the object can be dragged
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
    }
    void OnMouseDrag()
    {
        if (canDrag && !GameManager.instance.isPaused)//Constantly checks if the object can be dragged by seeing if the game is
        {//paused or if the game hasnt yet ended (See update function)
            Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint);

            myRB.velocity = new Vector3(0, 0, 0);

            //Limits the object's drag by clamping the XYZ values accordingly
            cursorPosition.x = Mathf.Clamp(cursorPosition.x, -width, width);
            cursorPosition.y = Mathf.Clamp(cursorPosition.y, minDragHeight, maxDragHeight);
            cursorPosition.z = Mathf.Clamp(cursorPosition.z, -length, length);
            transform.position = cursorPosition;
        }
    }
    private void Update()
    {
        if (GameManager.instance.playerState == playerState.End || GameManager.instance.playerState == playerState.Failed)
            if (canDrag) canDrag = false;//If the game has ended, set this to false
    }
}
