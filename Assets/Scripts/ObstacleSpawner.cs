﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {


    private float spawnCounter;
    [SerializeField]
    private float minSpawnTime;
    [SerializeField]
    private float maxSpawnTime;
    public float topScreenY;
    public float leftBoundaryX;
    public float rightBoundaryX;
    public int maxRocksRow = 4; //maximum number of rocks before car
    private int rocksRow = 0;

    public GameObject rock; //prefab of a rock to spawn at the top of the screen
    public GameObject car; //prefab of a car

	[SerializeField]
	private GenericObjectPool rockPool; 
	[SerializeField]
	private GenericObjectPool carPool;

    public ParticleSystem explosion;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        spawnCounter += Time.deltaTime;
        spawnCheck();
	}

    void spawnCheck()
    {
        if(spawnCounter > Random.Range(minSpawnTime,maxSpawnTime))
        {
            spawnObject();
            spawnCounter = 0;
        }
    }

    void spawnObject()
    {
        float randChoice = Random.Range(0, 4);
        if(randChoice < 3 && rocksRow < maxRocksRow)
        {
            //Instantiate(rock, new Vector3(Random.Range(leftBoundaryX, rightBoundaryX), topScreenY, 0), Quaternion.identity);
			GameObject rock = rockPool.pullObject (); 
			rock.transform.position = new Vector3 (Random.Range (leftBoundaryX, rightBoundaryX), topScreenY, 0);
			rock.transform.rotation = Quaternion.identity;

            rocksRow++;
        }
        else
        {
          // GameObject npcCar = Instantiate(car, new Vector3(Random.Range(leftBoundaryX, rightBoundaryX), topScreenY, 0), Quaternion.identity);
			GameObject npcCar = carPool.pullObject();
			npcCar.transform.position = new Vector3 (Random.Range (leftBoundaryX, rightBoundaryX), topScreenY, 0);
			npcCar.transform.rotation = Quaternion.identity;
            npcCar.GetComponent<CarPartsSpawner>().explosion = explosion;
            rocksRow = 0;
        }
    }
}
