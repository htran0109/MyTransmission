﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsDescriptor : MonoBehaviour {
	public enum DUMMY_PARTS_TYPE {DUMMY}

	public CarParts.partsList partsType; 
	public Sprite[] listOfPartsSpriteInOrderOfEnum;
	PartsMovingBehavior pmb; 
	// Use this for initialization
	void Awake () {
		pmb = this.GetComponent<PartsMovingBehavior> ();
		RandomizeType ();
	}

	void RandomizeType() {
		partsType = (CarParts.partsList) UnityEngine.Random.Range(0,Enum.GetNames(typeof(CarParts.partsList)).Length );
		GetComponent<SpriteRenderer> ().sprite = listOfPartsSpriteInOrderOfEnum [(int)partsType];
	}
	
	void OnTriggerEnter2D(Collider2D coll){

		if(!pmb.hasLanded){
			return;
		}

		if (coll.tag == "playerCar") {
            Debug.Log("Recovered something");
			Player2 possiblePlayer2 = coll.GetComponent<Player2> (); 
			if (possiblePlayer2 != null) {
				bool canRestore = possiblePlayer2.restoreCarPartsToPlayer1 (this.partsType);
				if(canRestore){
					Debug.Log ("COLLECTION???");
					AudioController.Play ("SFX_Pickup");
					Destroy (this.gameObject);	
				}
			}

		}
	}
}
