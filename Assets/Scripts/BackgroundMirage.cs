using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMirage : MonoBehaviour {

	[SerializeField] float scaleSpeed;
	[SerializeField] float scaleInterval;
	float timer;

	[SerializeField] Transform[] itemsToScale;

	// Use this for initialization
	void Start () {
		timer = scaleInterval;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;

		if (timer <= 0) {
			// Scale the items in the list

			for (int i = 0; i < itemsToScale.Length; i++) {
				itemsToScale [i].localScale = new Vector3 (itemsToScale [i].localScale.x + scaleSpeed, itemsToScale [i].localScale.y + scaleSpeed);
			}

			timer = scaleInterval;
		}
	}
}
