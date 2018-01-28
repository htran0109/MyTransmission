using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoIntro : MonoBehaviour {

	[SerializeField] Vector3 startPosition;
	[SerializeField] Vector3 endPosition;

	[SerializeField] float duration;
	float timer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		transform.position = Vector3.Lerp (transform.position, endPosition, timer/duration);
	}
}
