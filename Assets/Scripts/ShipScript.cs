using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float maxVelocity;

    //public GameObject shipWreck;
    public Animator anim;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Obstacle")
        {
            //Instantiate(shipWreck, gameObject.transform.position, Quaternion.identity);
            anim.SetTrigger("Crashed");
            Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
        }
        //Destroy(gameObject);       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wave")
        {
            float force = other.gameObject.GetComponent<WaveExpand>().magnitude;
            // Calculate Angle Between the collision point and the player
            Vector3 dir = transform.position - other.transform.position;
            dir = dir.normalized;
            rb.AddForce(dir * force);
        }
    }
}
