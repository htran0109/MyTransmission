using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nonPlayableCarMovement : MonoBehaviour {
	Vector3 movementDirection; 

	[SerializeField]
	float movementSpeed_UnityUnitPerSecond; 
	// Use this for initialization
	void Start () {
		movementDirection = transform.up * -1; 
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += movementSpeed_UnityUnitPerSecond * movementDirection * Time.deltaTime;
	}
}
