using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsFlungOutOfCar : MonoBehaviour {

	[SerializeField]
	private Sprite[] CarPartsSpriteArray;
	[SerializeField]
	private GenericObjectPool junkPartsPool;
	// Use this for initialization
	void Start () {
	}
		

	bool isFlung = false;

	// Update is called once per frame
	public void throwParts(CarParts.partsList partsType){
		
		GameObject partsThrown = junkPartsPool.pullObject(); 
		SpriteRenderer sp = partsThrown.GetComponent<SpriteRenderer> ();
		sp.sprite = CarPartsSpriteArray [(int)partsType];
		partsThrown.GetComponent<FlungPartsMovement>().throws ();
		AudioController.Play ("SFX_PartsFlying");

	}
}


