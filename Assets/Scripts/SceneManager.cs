using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

	public enum gameState {
		TITLE,
		PLAYING,
		END
	};

	public gameState currentState;


	public GameObject titleContainer;
	public GameObject gameContainer;
	public GameObject endGameContainer;

	public BackgroundMirage mirageScript;


	// Use this for initialization
	void Start () {
		AudioController.PlayMusicPlaylist ("MainTheme");

		currentState = gameState.TITLE;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentState == gameState.TITLE) {
			if (Input.GetKey (KeyCode.Space)) {
				currentState = gameState.PLAYING;

				startGame ();
			}
		} else if (currentState == gameState.END) {
			if (Input.GetKey (KeyCode.Space)) {
				currentState = gameState.TITLE;
			}
		}
	}

	void startGame() {
		mirageScript.startMirage();

		// Disable all title game object
		titleContainer.SetActive(false);

		// Enable game object
		gameContainer.SetActive(true);
	}

	public void endGame() {
		mirageScript.stopMirage();
		mirageScript.reset ();

		// Disable game object
		gameContainer.SetActive(false);

		// Enable all title game object
		titleContainer.SetActive(true);
	}
}
