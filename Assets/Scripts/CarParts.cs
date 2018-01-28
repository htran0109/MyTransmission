using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarParts : MonoBehaviour {

    public float minBreakTime; //variables to determine when the next random break will happen
    public float maxBreakTime;
    public float nextBreakTime;
    private float breakCounter;

    public int shellHealth;
	[SerializeField]
    public int maxShellHealth;

    public float invincibilityCounter = 0;
    public float healthInvincCounter = 0;

    public Animator animator;
    public float dampenRate;
    public float brokeSteerAccelRate;
    public float slowAccelRate;//for broken parts
    public float normalAccelRate;//for normal parts
    public float slowMaxSpeed; //broken stuff
    public float normalMaxSpeed; // for normal parts
    public bool[] partsArray;
    public enum partsList {LEFT_WHEEL, RIGHT_WHEEL, LIGHTS, STEERING, TRACTION}
    int debugPartsIndex = 0;

    private CarMovement carMove;
	private PartsFlungOutOfCar partsFlinger; 
	// Use this for initialization
	void Start () {
        carMove = GetComponent<CarMovement>();
		partsFlinger = GetComponentInChildren<PartsFlungOutOfCar> ();
        for(int i = 0; i < partsArray.Length; i++)
        {
            partsArray[i] = true;
        }
        nextBreakTime = Random.Range(minBreakTime, maxBreakTime);

		shellHealth = maxShellHealth;
	}
	
	// Update is called once per frame
	void Update () {
        animator.SetBool("left_wheel", partsArray[(int)partsList.LEFT_WHEEL]);
        animator.SetBool("right_wheel", partsArray[(int)partsList.RIGHT_WHEEL]);


        updateCarFunctions();
        if (breakCounter > nextBreakTime)
        {
            damageCar();
            breakCounter = 0;
            nextBreakTime = Random.Range(minBreakTime, maxBreakTime);
        }
        else
        {
            breakCounter += Time.deltaTime;
        }
        invincibilityCounter += Time.deltaTime;
        healthInvincCounter += Time.deltaTime;
	}

   public void damageCar()
    {
        if (invincibilityCounter > 0.5)
        {
            invincibilityCounter = 0;
            ArrayList workingParts = new ArrayList();
            for (int i = 0; i < partsArray.Length; i++)
            {
                if (partsArray[i])
                {
                    workingParts.Add(i);
                }
            }

            if (workingParts.Count > 0)
            {
                float brokenPart = Random.Range(0, workingParts.Count);
				partsFlinger.throwParts ((partsList)brokenPart);
                partsArray[(int)workingParts[(int)brokenPart]] = false;
                Debug.Log("Broke:" + (int)workingParts[(int)brokenPart]);
            }
        }
    }

    void updateCarFunctions()
    {
        if (partsArray[(int)partsList.LEFT_WHEEL]) // for left wheel
        {
            carMove.leftAccelRate = normalAccelRate;
            carMove.leftMaxSpeed = normalMaxSpeed;
        }
        else
        {
            carMove.leftAccelRate = slowAccelRate;
            carMove.leftMaxSpeed = slowMaxSpeed;
        }

        if (partsArray[(int)partsList.RIGHT_WHEEL])//for right wheel
        {
            carMove.rightAccelRate = normalAccelRate;
            carMove.rightMaxSpeed = normalMaxSpeed;
        }
        else
        {
            carMove.rightAccelRate = slowAccelRate;
            carMove.rightMaxSpeed = slowMaxSpeed;
        }

        if(partsArray[(int)partsList.LIGHTS])
        {

        }
        else
        {

        }

        if(partsArray[(int)partsList.STEERING])
        {
            
        }
        else
        {
            carMove.leftAccelRate = brokeSteerAccelRate;
            carMove.rightAccelRate = brokeSteerAccelRate;
        }

        if(partsArray[(int)partsList.TRACTION])
        {
            carMove.dampenRate = dampenRate;
            carMove.speedLimit = true;
        }
        else
        {
            carMove.dampenRate = -dampenRate;
            carMove.speedLimit = false;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log(coll.gameObject.name + ": Trigger hit");
        if (coll.gameObject.tag == "obstacle")//rock hit car
        {
            //do some damage step
            if (healthInvincCounter > 0.5)
            {
                healthInvincCounter = 0;
                shellHealth--;
                animator.SetInteger("shell_health", shellHealth);
            }


            if (shellHealth <= 0) {
				this.gameObject.SetActive (false);
				Debug.Log ("GAME OVER");
			}
            
            damageCar();
        }
    }

	public bool restoreParts(partsList typeRestored){

		if (partsArray [(int)typeRestored]) {
			return false;
		}

		partsArray [(int)typeRestored] = true; 
	
		switch (typeRestored) {
		case partsList.LEFT_WHEEL:
			animator.SetBool("left_wheel", partsArray[(int)partsList.LEFT_WHEEL]);
			break; 
		case partsList.RIGHT_WHEEL:
			animator.SetBool("right_wheel", partsArray[(int)partsList.RIGHT_WHEEL]);
			break; 
		}

		return true;
	}
}
