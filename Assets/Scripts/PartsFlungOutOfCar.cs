using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsFlungOutOfCar : MonoBehaviour {
	[SerializeField]
	float scalingPerSecond ;
	Vector3 originalPosition;
	[SerializeField]
	float upSpeed = 10.0f; 
	// Use this for initialization
	void Start () {
		originalPosition = transform.position; 
	}

	bool isFlung = false;
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.A)){
			isFlung = true; 
		}
		if (isFlung) {
			transform.localScale += new Vector3 (1.0f, 1.0f, 0.0f) * scalingPerSecond * Time.deltaTime;
			transform.position += Vector3.up * upSpeed * Time.deltaTime; 
			transform.position += Vector3.right * 5f * Time.deltaTime;
			upSpeed -= 50f*Time.deltaTime;
		}

	}
}


