using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarParts : MonoBehaviour {

    public float minBreakTime; //variables to determine when the next random break will happen
    public float maxBreakTime;
    public float nextBreakTime;
    private float breakCounter;

    public float shellHealth;
    public float maxShellHealth;

    public float invincibilityCounter = 0;


    public float dampenRate;
    public float brokeSteerAccelRate;
    public float slowAccelRate;//for broken parts
    public float normalAccelRate;//for normal parts
    public float slowMaxSpeed; //broken stuff
    public float normalMaxSpeed; // for normal parts
    public bool[] partsArray;
    enum partsList {LEFT_WHEEL, RIGHT_WHEEL, LIGHTS, STEERING, TRACTION}
    int debugPartsIndex = 0;

    private CarMovement carMove;
	// Use this for initialization
	void Start () {
        carMove = GetComponent<CarMovement>();
        for(int i = 0; i < partsArray.Length; i++)
        {
            partsArray[i] = true;
        }
        nextBreakTime = Random.Range(minBreakTime, maxBreakTime);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            //debugPartsIndex = (int)(Random.Range(0, 4));

            partsArray[debugPartsIndex] = false;
            Debug.Log("Broke Part" + debugPartsIndex);
            debugPartsIndex++;
        }
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
                partsArray[(int)workingParts[(int)brokenPart]] = false;
                Debug.Log("Broke:" + (int)brokenPart);
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
        Debug.Log("Trigger hit");
        if (coll.gameObject.tag == "obstacle")//rock hit car
        {
            //do some damage step

            //Destroy(coll.gameObject);
            damageCar();
        }
    }
}
