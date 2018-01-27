using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour {
    //[SerializeField]
   // float slowAccelRate = .005f; //To be implemented in CarParts.cs
    [SerializeField]
    float xVelocity = 0f;
    [SerializeField]
    public float tiltAccelRate = .02f; //to be set in CarParts.cs, how much the car turns because of missing parts
	[SerializeField]
    public float leftAccelRate = .1f;
    [SerializeField]
    public float rightAccelRate = .1f;
	[SerializeField]
    public float dampenRate = .008f;
	[SerializeField]
    public float leftMaxSpeed = .4f;
    [SerializeField]
    public float rightMaxSpeed = .4f;
	[SerializeField]
    private float horizMov = 0;
    [SerializeField]
    public bool speedLimit = true; //car cant steer past a certain speed. Disabled if traction is broken

    
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
        if(Input.GetButton("Left")) //check for acceleration left or right
        {
            horizMov = -leftAccelRate;
        }
        else if (Input.GetButton("Right"))
        {
            horizMov = rightAccelRate;
        }
        else
        {
            horizMov = 0;
        }
        if (Mathf.Abs(horizMov) == 0)//not moving left or right, straighten car 
        {
            if(Mathf.Abs(xVelocity) < dampenRate)
            {
                horizMov = 0;
            }
            else
            {
                if(xVelocity< 0) //dampen right to account for moving left
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
            if(xVelocity + horizMov > -leftMaxSpeed || !speedLimit)
            {
                horizMov = xVelocity + horizMov;
            }
            else
            {
                horizMov = -leftMaxSpeed;
            }

        }
        else
        {
            if (xVelocity + horizMov < rightMaxSpeed || !speedLimit)
            {
                horizMov = xVelocity + horizMov;
            }
            else
            {
                horizMov = rightMaxSpeed;
            }
        }
            transform.Translate(horizMov, vertMov, 0); //move according to the given speed.
    }
}
