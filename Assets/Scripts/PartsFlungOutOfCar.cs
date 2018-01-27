using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsFlungOutOfCar : MonoBehaviour {
	[SerializeField]
	private GameObject junkPartsPrefab;  
	[SerializeField]
	private Sprite[] CarPartsSpriteArray;

	// Use this for initialization
	void Start () {
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.V)) {
			throwParts ((CarParts.partsList) UnityEngine.Random.Range (0, Enum.GetNames (typeof(CarParts.partsList)).Length));
		}
	}

	bool isFlung = false;

	// Update is called once per frame
	public void throwParts(CarParts.partsList partsType){
		GameObject partsThrown = GameObject.Instantiate (junkPartsPrefab, this.transform.position, Quaternion.identity);
		SpriteRenderer sp = partsThrown.GetComponent<SpriteRenderer> ();
		sp.sprite = CarPartsSpriteArray [(int)partsType];
		partsThrown.GetComponent<FlungPartsMovement>().throws ();

	}
}


