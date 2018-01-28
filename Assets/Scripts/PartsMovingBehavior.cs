using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsMovingBehavior : MonoBehaviour {
	private Vector3 targetPosition; 	
	private Vector3 originalPosition;
	private Vector3 originalScale; 
	private Vector3 direction; 

	[SerializeField]
	private float flungSpeed_UnityUnitPerSecond;
	[SerializeField]
	private float rotation_DegreePerSecond;
	[SerializeField]
	private float movementSpeed; 

	private bool isMoving; 
	public bool hasLanded; 

	private int rotationDirection;
	void Awake() {
		this.originalPosition = transform.position;
		this.originalScale = transform.localScale;
		isMoving = false; 
		hasLanded = false; 
		direction = new Vector3(0, (.52f-.89f), -1.17f);
	}

	// Update is called once per frame
	private float totalDistance;
	private float totalStep; 
	private Vector3 scale;
	void Update () {
		if (isMoving) {

			float step = flungSpeed_UnityUnitPerSecond * Time.deltaTime;
			float additionalHeight = calculateAdditionalHeight (totalStep);
			Vector3 stepVector = (targetPosition - originalPosition).normalized;
			Vector3 displacement = new Vector3 (originalPosition.x + stepVector.x * totalStep, originalPosition.y + stepVector.y * totalStep * additionalHeight, originalPosition.z + stepVector.z * totalStep);
			transform.position = displacement;
			//Debug.Log ("zomming: " + zoomAmount);	
			//transform.position = Vector3.MoveTowards (transform.position, targetPosition, step);
			transform.Rotate (new Vector3 (0, 0, this.rotationDirection * rotation_DegreePerSecond * Time.deltaTime));

			Debug.Log (transform.localScale);
			totalStep += step;
			if (transform.position.z >= 0.0f || totalStep >= totalDistance) {
				//Debug.Log ("target has been reacehed");
				isMoving = false;
				hasLanded = true;
				return;
			}
		}

		if (hasLanded) {
			this.transform.position += movementSpeed * direction  * Time.deltaTime;
		}
	}


	public void flungTo(Vector3 targetPosition){
		//Debug.Log ("current position: " + this.transform.position);
		//Debug.Log ("its target position: " + targetPosition);
		this.targetPosition = targetPosition;
		rotationDirection =(int) Mathf.Pow (-1.0F, Random.Range (1, 5));
		isMoving = true; 
		totalDistance = Vector3.Distance (targetPosition, transform.position);
	}

	float calculateAdditionalHeight(float currentTotalStep){
		return Mathf.Pow ( (totalStep / totalDistance * 2) - 1, 2) * -1 +  1;
	}
}
