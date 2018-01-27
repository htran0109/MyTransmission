using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeMoving : MonoBehaviour {

	[HeaderAttribute("Position")]
	[SerializeField] public Vector3 startPosition;
	[SerializeField] public Vector3 offSet;
	[SerializeField] public float duration;

	[HeaderAttribute("Scale")]
	[SerializeField] public Vector3 endScale;

	float timer;

	// Use this for initialization
	void Start () {
		startPosition = this.transform.position;
		if (this.transform.position.x < 0) {
			offSet.Scale(new Vector3(-1, 1, 1));
		}
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		this.transform.position = Vector3.Lerp(this.transform.localPosition, startPosition+offSet, timer/duration);
		this.transform.localScale = Vector3.Lerp(this.transform.localScale, endScale, timer/duration);

		if (timer/duration >= 1) {
			Destroy (this.gameObject);
		}
	}
}
