using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public GameObject[] bodyParts;

    public static MonsterController Instance { get; set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOffAllColliderBodyParts()
    {
        foreach(GameObject obj in bodyParts)
        {
            obj.SetActive(false);
        }
    }

    public void TurnOnAllColliderBodyParts()
    {
        foreach (GameObject obj in bodyParts)
        {
            obj.SetActive(true);
        }
    }
}
