using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carExplosion : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	void hideSelf() {
		gameObject.SetActive (false);
	}
}
