using UnityEngine;
using System.Collections;

public class CursorScript : MonoBehaviour {

    public float cursorSpeed;
    public Boundary boundary;

    public GameObject wavePrefab;

    public ControllerType controller;

    public ManaManager mana;

    // Use this for initialization
    void Start ()
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
    void Update ()
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

        //SinglePlayer
        //moveHorizontal = Input.GetAxis("P1Horizontal") * Time.deltaTime * cursorSpeed;
        //moveVertical = Input.GetAxis("P1Vertical") * Time.deltaTime * cursorSpeed;

        if (Input.GetButtonDown(fireStr))
        {
            Fire();
        }

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);

        gameObject.transform.Translate(movement);

        gameObject.transform.position = new Vector3
        (
            Mathf.Clamp(gameObject.transform.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(gameObject.transform.position.y, boundary.yMin, boundary.yMax),
            0.0f
        );

    }

   public void Fire()
    {
        Instantiate(wavePrefab, gameObject.transform.position, Quaternion.identity);
        mana.SubMana(30.0f);
    }
}
