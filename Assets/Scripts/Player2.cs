using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour {

	public GameObject followTarget;
    public Vector3 oldPosition;
    public float followDelay = 0.2f;
    public float followCounter = 0f;

	[Header("Movement")]
	[SerializeField] float movementSpeed = 0.1f;
	[SerializeField] float maxXLimit = 2f;
	[SerializeField] float decay = 0.1f;

	[Header("Position")]
	[SerializeField] Vector3 offsetPos = new Vector3(0, 2, 0);		// Target position for player to follow
	private Vector3 movementOffset = new Vector3 (0, 0, 0);			// Additional offset for player movement

	float rotationSpeed = 1f;
	float rotationLimit = 30f;

	// Use this for initialization
	void Start () {
        oldPosition = followTarget.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if(followCounter < followDelay) {
            followCounter += Time.deltaTime;
        }

        oldPosition = new Vector3(Mathf.Lerp(
			Mathf.Clamp(oldPosition.x, followTarget.transform.position.x-maxXLimit, followTarget.transform.position.x+maxXLimit), 
			followTarget.transform.position.x, 
			0.1f), oldPosition.y, oldPosition.z);
		translatePlayer (oldPosition - offsetPos);
	}

	void rotatePlayer() {
		if (Input.GetKey(KeyCode.A) && followTarget.transform.localRotation.z <= Mathf.Deg2Rad*rotationLimit) {
			followTarget.transform.Rotate(new Vector3(0, 0, rotationSpeed));

			if (followTarget.transform.localEulerAngles.z < 180 && 
				followTarget.transform.localEulerAngles.z > rotationLimit) {
				followTarget.transform.localEulerAngles = new Vector3 (0, 0, rotationLimit);
			}
		} else if (Input.GetKey(KeyCode.D) && followTarget.transform.localRotation.z >= -Mathf.Deg2Rad*rotationLimit) {
			followTarget.transform.Rotate(new Vector3(0, 0, -rotationSpeed));

			if (followTarget.transform.localEulerAngles.z > 180 && 
				followTarget.transform.localEulerAngles.z < 360-rotationLimit) {
				followTarget.transform.localEulerAngles = new Vector3 (0, 0, -rotationLimit);
			}
		}
	}

	void translatePlayer(Vector3 targetPosition) {
		if (movementOffset.x > 0) {
			movementOffset.x = Mathf.Clamp (movementOffset.x - decay, 0, maxXLimit);
		} else if (movementOffset.x < 0) {
			movementOffset.x = Mathf.Clamp (movementOffset.x + decay, -maxXLimit, 0);
		}

		if (Input.GetButton ("Player2_Left")) {
			movementOffset.x = Mathf.Clamp(movementOffset.x-movementSpeed, -maxXLimit, maxXLimit);
		} else if (Input.GetButton ("Player2_Right")) {
			movementOffset.x = Mathf.Clamp(movementOffset.x+movementSpeed, -maxXLimit, maxXLimit);
		}

		this.transform.position = targetPosition + movementOffset;
	}

    void attack()
    {

    }
}
