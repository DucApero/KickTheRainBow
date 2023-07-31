using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBodyPart : MonoBehaviour
{
    private Vector2 mousePos;
    private bool isDraging;
    private DistanceJoint2D distanceJoint;

    // Start is called before the first frame update
    void Start()
    {
        distanceJoint = GetComponent<DistanceJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDraging)
        {
            MoveObjectWithMouse();
        }
    }

    private void OnMouseDown()
    {
        isDraging = true;
    }

    private void OnMouseUp()
    {
        isDraging = false;
    }

    private void MoveObjectWithMouse()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = mousePos;
        //distanceJoint.connectedBody.transform.position = new Vector2(mousePos.x, mousePos.y - 1);
    }
}
