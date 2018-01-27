using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsDescriptor : MonoBehaviour {
	public enum DUMMY_PARTS_TYPE {DUMMY}

	public CarParts.partsList partsType; 
	public Sprite[] listOfPartsSpriteInOrderOfEnum;

	// Use this for initialization
	void Awake () {
		RandomizeType ();
	}

	void RandomizeType() {
		partsType = (CarParts.partsList) UnityEngine.Random.Range(0,Enum.GetNames(typeof(CarParts.partsList)).Length - 1);
		GetComponent<SpriteRenderer> ().sprite = listOfPartsSpriteInOrderOfEnum [(int)partsType];
	}
	
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.tag == "playerCar") {
			Player2 possiblePlayer2 = coll.GetComponent<Player2> (); 
			if (possiblePlayer2 != null) {
				possiblePlayer2.restoreCarPartsToPlayer1 (this.partsType);
				Destroy (this.gameObject);
			}

		}
	}
}
