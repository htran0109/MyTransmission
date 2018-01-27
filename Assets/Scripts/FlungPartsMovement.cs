using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlungPartsMovement : MonoBehaviour {

	bool isFlung; 

	[SerializeField]
	float scalingPerSecond ;
	[SerializeField]
	float upSpeed = 10.0f; 

	[SerializeField]
	private float flingModifier; 
	[SerializeField]
	private float downwardsPull = 50.0f;
	[SerializeField]
	private float objectLifeTime;

	private float currentLifeTime = 0.0f;
	Vector3 originalPosition;
	// Update is called once per frame
	void Awake() {
		flingModifier = Random.Range (0.0f, flingModifier) * Mathf.Pow(-1,Random.Range(0,5));
		currentLifeTime = 0.0f; 
	}
	void Update () {
		if (isFlung) {
			transform.localScale += new Vector3 (1.0f, 1.0f, 0.0f) * scalingPerSecond * Time.deltaTime;
			transform.position += Vector3.up * upSpeed * Time.deltaTime; 
			transform.position += Vector3.right * flingModifier * Time.deltaTime;
			upSpeed -= downwardsPull*Time.deltaTime;
		}

		currentLifeTime += Time.deltaTime; 
		if (currentLifeTime >= objectLifeTime) {
			Destroy (this.gameObject);
		}
	}

	public void throws(){
		isFlung = true; 
	}
}
