using System.Collections;
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

	void OnEnable(){
		UpdateIndicator ();
	}

    public void UpdateIndicator()
    {
        for (int i = 0; i < partIndicators.Length; i++)
        {
            partIndicators[i].SetActive(!car.partsArray[i]);
        }
    }
}
