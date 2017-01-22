using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandScript : MonoBehaviour {

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Cursor")
        {
            other.GetComponent<CursorScript>().mana.AddMana(10 * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ship")
        {
            Application.LoadLevel(3);
        }

        if (collision.tag == "Cursor")
        {
            collision.GetComponent<CursorScript>().mana.AddMana(25);
        }
    }
}
