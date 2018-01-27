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
	private bool hasLanded; 

	private int rotationDirection;
	void Awake() {
		this.originalPosition = transform.position;
		this.originalScale = transform.localScale;
		isMoving = false; 
		hasLanded = false; 
	}

	// Update is called once per frame
	private float totalDistance;
	private float totalStep; 
	private Vector3 scale;
	void Update () {
		if (isMoving) {
			if (transform.position == targetPosition) {
				Debug.Log ("target has been reacehed");
				isMoving = false;
                hasLanded = true;
				return;
			}
			float step = flungSpeed_UnityUnitPerSecond * Time.deltaTime;
			float zoomAmount = calculateZoom (totalStep)/totalDistance;
			Debug.Log ("zomming: " + zoomAmount);	
			transform.position = Vector3.MoveTowards (transform.position, targetPosition, step);
			transform.Rotate (new Vector3 (0, 0, this.rotationDirection * rotation_DegreePerSecond * Time.deltaTime));
			transform.localScale =  new Vector3(originalScale.x + zoomAmount , 
				originalScale.y + zoomAmount ,
				this.transform.localScale.z);
			Debug.Log (transform.localScale);
			totalStep += step;
		}

		if (hasLanded) {
			this.transform.position += movementSpeed * Vector3.up* -1 * Time.deltaTime;
		}
	}


	public void flungTo(Vector3 targetPosition){
		this.targetPosition = targetPosition;
		rotationDirection =(int) Mathf.Pow (-1.0F, Random.Range (1, 5));
		isMoving = true; 
		totalDistance = Vector3.Distance (targetPosition, transform.position);
	}

	float calculateZoom(float currentTotalStep){
		Debug.Log ("current Step: " + currentTotalStep);
		return Mathf.Pow ( (totalStep / totalDistance * 2) - 1, 2) * -1 +  1;
	}
}
