﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicator : MonoBehaviour {

    [SerializeField]
    GameObject[] partIndicators;

    CarParts car;

	// Use this for initialization
	void Start () {
        car = FindObjectOfType<CarParts>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateIndicator()
	{	if (car == null) {
			car = FindObjectOfType<CarParts> ();
		}
        for (int i = 0; i < partIndicators.Length; i++)
        {
            partIndicators[i].SetActive(!car.partsArray[i]);
        }
    }
}
