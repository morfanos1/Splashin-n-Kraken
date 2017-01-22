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
    public bool isBeingDestroyed;

    //Audio
    public AudioSource crash1;
    public AudioSource crash2;

    void Start()
    {
        isBeingDestroyed = false;
    }

    void Update()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Obstacle" && isBeingDestroyed == false)
        {
            //Instantiate(shipWreck, gameObject.transform.position, Quaternion.identity);
            isBeingDestroyed = true;
            crash1.Play();
            crash2.Play();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0.0f;
            anim.SetTrigger("Crashed");
            //Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
            Destroy(gameObject, crash2.clip.length);
        }      
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wave" && isBeingDestroyed == false)
        {
            float force = other.gameObject.GetComponent<WaveExpand>().magnitude;
            // Calculate Angle Between the collision point and the player
            Vector3 dir = transform.position - other.transform.position;
            dir = dir.normalized;
            rb.AddForce(dir * force);
        }
        // Adding a Sprite renderer to KrakenZone in order to display it clearly to players. Red circle that will become more transparent and fade over time or when the user lets goe of the held button
        if (other.gameObject.tag == "KrakenZone" && isBeingDestroyed == false) {
            // Calculate Angle Between the collision point and the player
            isBeingDestroyed = true;
            anim.SetTrigger("KrakenAttack");
            Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);

        }
    }
}
