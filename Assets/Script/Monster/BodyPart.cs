using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject explosion;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = body.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckColisionWithPlatform(collision);
    }

    private void CheckColisionWithPlatform(Collision2D collision)
    {
        if (Mathf.Abs(rb.angularVelocity) >= 100 && collision.gameObject.tag == "Platform")
        {
            GameObject obj = Instantiate(explosion, this.transform.position, Quaternion.identity);
            obj.gameObject.SetActive(true);
            Destroy(obj, .5f);
        }
    }
}
