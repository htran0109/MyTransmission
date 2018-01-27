using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour {

	[SerializeField] float speed = 0.1f/60;
	public MeshRenderer roadTex;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		roadTex.material.mainTextureOffset = new Vector2(0, roadTex.material.mainTextureOffset.y - speed);
	}
}
