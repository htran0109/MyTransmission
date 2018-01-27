using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsDescriptor : MonoBehaviour {
	public enum DUMMY_PARTS_TYPE {DUMMY}

	public DUMMY_PARTS_TYPE partsType; 
	public Sprite[] listOfPartsSpriteInOrderOfEnum;

	// Use this for initialization
	void Awake () {
		RandomizeType ();
	}

	void RandomizeType() {
		partsType = (DUMMY_PARTS_TYPE) UnityEngine.Random.Range(0,Enum.GetNames(typeof(DUMMY_PARTS_TYPE)).Length - 1);
		GetComponent<SpriteRenderer> ().sprite = listOfPartsSpriteInOrderOfEnum [(int)partsType];
	}
	
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.tag == "playerCar") {
			Player2 possiblePlayer2 = coll.GetComponent<Player2> (); 
			if (possiblePlayer2 != null) {
				// link back to player 1
				Destroy (this.gameObject);
			}

		}
	}
}
