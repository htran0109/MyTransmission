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

			if (numberOfPartsSpawned == 0) {
				return; 
			}

			spawnSpareParts ( numberOfPartsSpawned);
		}
	}

	// taking the center of the car as referencePoint,  
	void spawnSpareParts(int numberOfPartsSpawned) {
		Vector3 startingPosition = this.transform.position; 
		Vector3 movingDirection = this.transform.forward; 


		for (int i = 0; i < numberOfPartsSpawned; i++) {
			Vector3 spawnPosition = startingPosition + transform.forward * this.sparePartsSpawnDistance;
			//spawnPosition += t;

		}

	}


}
