using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsMovingBehavior : MonoBehaviour {
	private Vector3 targetPosition; 	
	private Vector3 originalPosition;
	private Vector3 direction; 

	[SerializeField]
	private float flungSpeed;


	private bool isMoving; 

	void Awake() {
		this.originalPosition = transform.position;
		isMoving = false; 
	}

	// Update is called once per frame
	void Update () {
		if (isMoving) {
			if (transform.position == targetPosition) {
				Debug.Log ("target has been reacehed");
				isMoving = false; 
				return;
			}
			float step = flungSpeed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, targetPosition, step);
		}
	}

	public void flungTo(Vector3 targetPosition){
		this.targetPosition = targetPosition;
		isMoving = true; 
	}
}
