using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPartsSpawner: MonoBehaviour {

	// for now, assumed that this will be attachhed to non-player car
	// alternatively can change interaction vie physics2D layer
	const int LAYER_OBJECT_PLAYER = 8; 

	[SerializeField]
	private float sparePartsSpawnDistance; 
	private Collider2D objectCollider; 

	[SerializeField]
	private GameObject sparePartsPrefab; 
	[SerializeField]
	private int maximumNumberOfPartsSpawned; 
	[SerializeField]
	private float distanceBetweenSparePartsWhenSpawned; 

	void OnCollisionEnter2D(Collision2D collider2D) {
		if (collider2D.gameObject.layer == LAYER_OBJECT_PLAYER) {
			// flung parts 
			int numberOfPartsSpawned = Random.Range(0,maximumNumberOfPartsSpawned);
			Debug.Log ("numbberOfPartsSpaned: " + numberOfPartsSpawned);
			if (numberOfPartsSpawned == 0) {
				return; 
			}

			spawnSpareParts (numberOfPartsSpawned);
		}
	}

	// taking the center of the car as referencePoint,  
	void spawnSpareParts(int numberOfPartsSpawned) {
		Vector3 startingPosition = this.transform.position; 
		Vector3 movingDirection = this.transform.up; 

		startingPosition += movingDirection * sparePartsSpawnDistance;

		Vector3 farthestPartsFromLeft = startingPosition; 
		if ((numberOfPartsSpawned % 2) == 0) { //is even
	
			farthestPartsFromLeft -= transform.right * distanceBetweenSparePartsWhenSpawned / 2;

			farthestPartsFromLeft -= transform.right * (numberOfPartsSpawned/ 2 - 1) * distanceBetweenSparePartsWhenSpawned;
		} else {
			farthestPartsFromLeft -= transform.right * (numberOfPartsSpawned - 1)/2 * distanceBetweenSparePartsWhenSpawned;
		}
			
		Vector3 SpawnPosition = farthestPartsFromLeft;
		for (int i = 0; i < numberOfPartsSpawned; i++) {
			Vector3 spawnPosition = farthestPartsFromLeft + transform.right * i * distanceBetweenSparePartsWhenSpawned;

			// spawn the object
			//TODO: Create ObjectPool
			GameObject newObject = GameObject.Instantiate(sparePartsPrefab);
			newObject.transform.position = spawnPosition;
		}

	}


}
