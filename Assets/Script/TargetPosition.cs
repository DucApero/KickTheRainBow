using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPosition : MonoBehaviour
{
    private Vector2 mousePos;
    private bool isDragging = false;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (isDragging)
        {
            MonsterController.Instance.TurnOffAllColliderBodyParts();
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }
    }

    private void OnMouseDown()
    {
        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
        StartCoroutine(TurnOnAllObjects(2f));
    }

    IEnumerator TurnOnAllObjects(float time)
    {
        yield return new WaitForSeconds(time);
        MonsterController.Instance.TurnOnAllColliderBodyParts();
    }
}
