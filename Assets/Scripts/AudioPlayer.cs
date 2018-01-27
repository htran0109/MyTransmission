using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {

	[SerializeField] AudioSource mainAudio;
	[SerializeField] AudioSource mainAudioLoop;

	bool startedLoop = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		if (!mainAudio.isPlaying && !startedLoop){
			mainAudioLoop.Play();
			startedLoop = true;
		}
	}
}
