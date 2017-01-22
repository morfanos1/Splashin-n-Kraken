using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float maxVelocity;

    //Animation
    //public GameObject shipWreck;
    public Animator anim;

    //Audio
    public AudioSource a1;
    public AudioSource a2;

    void Start()
    {

    }

    void Update()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Obstacle")
        {
            //Instantiate(shipWreck, gameObject.transform.position, Quaternion.identity);
            a1.Play();
            a2.Play();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0.0f;
            anim.SetTrigger("Crashed");
            //Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
            Destroy(gameObject, a2.clip.length);
        }      
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
