using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    private LineRenderer rope;
    public Transform anchor;
    public Transform target;
    
    // Start is called before the first frame update
    void Start()
    {
        rope = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rope.SetPosition(0, anchor.position);
        rope.SetPosition(1, target.position);
    }
}
