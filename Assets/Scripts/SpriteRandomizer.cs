using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRandomizer : MonoBehaviour {

	[SerializeField]
	private Sprite[] sprites;

	private SpriteRenderer sr; 
	// Use this for initialization
	void Start () {
		this.sr = GetComponent<SpriteRenderer> ();

		sr.sprite = sprites [Random.Range (0, this.sprites.Length)];
	}

}
