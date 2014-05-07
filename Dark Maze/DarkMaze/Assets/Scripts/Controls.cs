using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {
    
    public float speed = 5.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (gameObject.GetComponent<ThumbstickControl>())
        {
            Vector2 movement = gameObject.GetComponent<ThumbstickControl>().Displacement;
            rigidbody.AddForce(movement.x * Time.deltaTime * 60.0f, 0, -movement.y * Time.deltaTime * 60.0f);
        }


	    if (Input.GetKey(KeyCode.W))
        {
            //Move up
            rigidbody.AddForce(0, 0, speed * 240.0f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            //Move left
            rigidbody.AddForce(-speed* 240.0f * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            //Move down
            rigidbody.AddForce(0, 0, -speed* 240.0f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            //Move right
            rigidbody.AddForce(speed* 240.0f * Time.deltaTime, 0, 0);
        }
        //
		//if (Input.GetKeyDown(KeyCode.Space))
        //{
            //Use powerup
         //   GetComponent<Player_Script>().UseCurrentPowerUp();
        //}
        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    GetComponent<Player_Script>().ResetPlayerPowerUpChanges();
        //}
        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
	}
}