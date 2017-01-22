using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCursorScript : MonoBehaviour
{

    public float cursorSpeed;
    public Boundary boundary;

    public ControllerType controller;

    // Use this for initialization
    void Start()
    {

    }

    public enum ControllerType
    {
        GamePad1,
        GamePad2,
        GamePad3,
        GamePad4
    }

    [System.Serializable]
    public class Boundary
    {
        public float xMin, xMax, yMin, yMax;
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = 0;
        float moveVertical = 0;
        //PlaceHolder
        string fireStr = "P1Fire";

        //MultiPlayer
        switch (controller)
        {
            case ControllerType.GamePad1:
                moveHorizontal = Input.GetAxis("P1Horizontal") * Time.deltaTime * cursorSpeed;
                moveVertical = Input.GetAxis("P1Vertical") * Time.deltaTime * cursorSpeed;
                fireStr = "P1Fire";
                break;
            case ControllerType.GamePad2:
                moveHorizontal = Input.GetAxis("P2Horizontal") * Time.deltaTime * cursorSpeed;
                moveVertical = Input.GetAxis("P2Vertical") * Time.deltaTime * cursorSpeed;
                fireStr = "P2Fire";
                break;
            case ControllerType.GamePad3:
                moveHorizontal = Input.GetAxis("P3Horizontal") * Time.deltaTime * cursorSpeed;
                moveVertical = Input.GetAxis("P3Vertical") * Time.deltaTime * cursorSpeed;
                fireStr = "P3Fire";
                break;
            case ControllerType.GamePad4:
                moveHorizontal = Input.GetAxis("P4Horizontal") * Time.deltaTime * cursorSpeed;
                moveVertical = Input.GetAxis("P4Vertical") * Time.deltaTime * cursorSpeed;
                fireStr = "P4Fire";
                break;
            default:
                break;
        }

        if (Input.GetButtonDown(fireStr))
        {
            Fire();
        }


    }

    public void Fire()
    {
    }
}