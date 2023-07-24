using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPosition : MonoBehaviour
{
    public Transform targetTransform; // The target point's Transform

    private TargetJoint2D targetJoint;
    private Vector2 mousePos;
    private bool isDragging = false;

    private void Start()
    {
        targetJoint = GetComponent<TargetJoint2D>();
        //argetJoint.enabled = false;
        // Ensure you have a reference to the targetTransform or set it in the Inspector.
        if (targetTransform == null)
        {
            Debug.LogWarning("Target Transform not assigned! Please assign it in the Inspector.");
        }
    }

    private void Update()
    {
        if (targetTransform != null)
        {
            // Set the target point of the TargetJoint2D to the position of the targetTransform.
            targetJoint.target = targetTransform.position;
        }
        if (isDragging)
        {
            Debug.Log("dragging");
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetTransform.position = mousePos;
        }
    }

    private void OnMouseDown()
    {
        isDragging = true;
        targetJoint.enabled = true;
    }

    private void OnMouseUp()
    {
        StartCoroutine(SetTargetJoinOff(1f));
        isDragging = false;
    }

    IEnumerator SetTargetJoinOff(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        targetJoint.enabled = false;
    }
}
