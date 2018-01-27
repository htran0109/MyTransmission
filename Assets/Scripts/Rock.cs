using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {
    Vector3 movementDirection;
    CarParts playerCar;
    // Use this for initialization
    [SerializeField]
    float movementSpeed_UnityUnitPerSecond;

    void Start () {
        movementDirection = new Vector3(0, (.52f-.89f), -1.17f);
        playerCar = GameObject.FindGameObjectWithTag("playerCar").GetComponent<CarParts>();

    }

    // Update is called once per frame
    void Update () {
        transform.position += movementSpeed_UnityUnitPerSecond * movementDirection * Time.deltaTime;

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("Trigger hit");
        if(coll.gameObject.tag == "playerCar")//rock hit car
        {
            //do some damage step

            Destroy(this.gameObject);
            //playerCar.damageCar();
        }
        if(coll.gameObject.tag == "skater") //rock hit skater
        {
            //do some stun step
            Destroy(this.gameObject);
        } 
    }
    
    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
