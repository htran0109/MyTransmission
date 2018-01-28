using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericObjectPool : MonoBehaviour {
	[SerializeField]	
	GameObject objectPrefab; 
	[SerializeField]
	int objectNumber; 
	List<GameObject> objectPool;
	// Use this for initialization
	void Start () {
		objectPool = new List<GameObject> (); 

		for (int i = 0; i < objectNumber; i++) {
			GameObject newObject = Instantiate (objectPrefab, this.transform.position, Quaternion.identity);
			newObject.SetActive (false);
			objectPool.Add (newObject);
		}
	}
	
	public GameObject pullObject() {
		GameObject objectToReturn = null; 
		for (int i = 0; i < objectPool.Count; i++) {
			if (!objectPool [i].activeSelf) {
				objectToReturn = objectPool [i]; 
				break; 
			}
		}

		if (objectToReturn == null) {
			objectToReturn = Instantiate (objectPrefab, this.transform.position, Quaternion.identity); 
			objectPool.Add (objectToReturn);
		}

		objectToReturn.gameObject.SetActive (true);

		return objectToReturn;
	}
}
