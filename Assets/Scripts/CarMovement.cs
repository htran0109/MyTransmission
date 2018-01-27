using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour {

	[SerializeField]
    float xVelocity = 0f;
	[SerializeField]
    float accelRate = .1f;
	[SerializeField]
    float dampenRate = .02f;
	[SerializeField]
    float vertSpeed = .25f;
	[SerializeField]
    float maxSpeed = .4f;
	[SerializeField]
    private float horizMov = 0;
    
    private float vertMov = 0;
    private Rigidbody2D rb2d; //for physics on the object (used for acceleration)
	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        readKeys();
	}

    void readKeys()
    {
        xVelocity = horizMov; //keep old velocity for this frame
        vertMov = Input.GetAxis("Vertical") * vertSpeed;
        if(Input.GetButton("Left")) //check for acceleration left or right
        {
            horizMov = -1;
        }
        else if (Input.GetButton("Right"))
        {
            horizMov = 1;
        }
        else
        {
            horizMov = 0;
        }
        horizMov *= accelRate;
        Debug.Log(horizMov);
        if (Mathf.Abs(horizMov) == 0)//not moving left or right, straighten car 
        {
            if(Mathf.Abs(xVelocity) < dampenRate)
            {
                horizMov = 0;
            }
            else
            {
                if(xVelocity< 0)
                {
                    horizMov = xVelocity + dampenRate;
                }
                else
                {
                    horizMov = xVelocity - dampenRate;
                }
            }
        }
        else if (xVelocity < 0) 
        {
            if(xVelocity + horizMov > -maxSpeed)
            {
                horizMov = xVelocity + horizMov;
            }
            else
            {
                horizMov = -maxSpeed;
            }

        }
        else
        {
            if (xVelocity + horizMov < maxSpeed)
            {
                horizMov = xVelocity + horizMov;
            }
            else
            {
                horizMov = maxSpeed;
            }
        }
            transform.Translate(horizMov, vertMov, 0); //move according to the given speed.
    }
}
