using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawning : MonoBehaviour {
	[SerializeField] GameObject[] treePrefabs;

	[SerializeField] float minTimer;
	[SerializeField] float maxTimer;
	public float timer;

	[SerializeField] float xStart;
	[SerializeField] float yStart;

	// Use this for initialization
	void Start () {
		timer = Random.Range (minTimer, maxTimer);
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;

		if (timer <= 0) {
			int multipler = 1;

			if (Random.value > 0.5) {
				multipler = -1;
			}

			int treeToSpawn = Random.Range (0, treePrefabs.Length);

			// Spawn the tree!
			Instantiate(treePrefabs[treeToSpawn], new Vector3(multipler*Random.Range(xStart, xStart+10), yStart, 0), Quaternion.identity);
			timer = Random.Range (minTimer, maxTimer);
		}
	}
}
