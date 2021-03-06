﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour {

    public Animator animator;
    public GameObject followTarget;
    public BoxCollider2D weaponBox;
    public Vector3 oldPosition;
    public float followDelay = 0.2f;
    public float followCounter = 0f;

    public float attackCounter = 0f;
    public float attackThreshold = .5f;

	[Header("Movement")]
	[SerializeField] float movementSpeed = 0.1f;
	[SerializeField] float maxXLimit = 2f;
	[SerializeField] float decay = 0.1f;

	[Header("Position")]
	[SerializeField] Vector3 offsetPos = new Vector3(0, 2, 0);		// Target position for player to follow
	private Vector3 movementOffset = new Vector3 (0, 0, 0);			// Additional offset for player movement


	[Header("when getting hit by car")]
	[SerializeField]
	private float paralyzedCooldown;
	private float currentParalyzedDuration;
	[SerializeField]
	private bool isHit;

	[SerializeField]
	private CarParts player1Carparts; 

	float rotationSpeed = 1f;
	float rotationLimit = 30f;


    LineRenderer lineRenderer;
    // Use this for initialization
    void Start () {
        oldPosition = followTarget.transform.position;
        animator = GetComponent<Animator>();

        //put in the 'rope' between the car and this player
		this.currentParalyzedDuration = 0.0f;
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.widthMultiplier = 0.04f;
        float alpha = 1.0f;
        Color c1 = Color.yellow;
        Color c2 = Color.grey;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
            );
        lineRenderer.colorGradient = gradient;
    }

    // Update is called once per frame
    void Update () {
        lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y + .03f, transform.position.z));
        Vector3 secondPos = followTarget.transform.position;
        lineRenderer.SetPosition(1, new Vector3(secondPos.x, secondPos.y - .1f, secondPos.z));

        if (followCounter < followDelay)
        {
            followCounter += Time.deltaTime;
        }

		if (isHit && this.currentParalyzedDuration < paralyzedCooldown) {
			this.currentParalyzedDuration += Time.deltaTime;

			if (this.currentParalyzedDuration >= paralyzedCooldown) {
				isHit = false;
                animator.SetBool("Damaged", isHit);
				currentParalyzedDuration = 0.0f;
			}
		}

		if (followTarget == null) {
			return;
		}

        oldPosition = new Vector3(Mathf.Lerp(oldPosition.x, followTarget.transform.position.x, 0.1f), oldPosition.y, oldPosition.z);
		translatePlayer (oldPosition - offsetPos);
        attack();
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

		if (!isHit) {
			if (Input.GetButton ("Player2_Left")) {
                animator.SetInteger("MoveDir", -1);
				movementOffset.x = Mathf.Clamp(movementOffset.x-movementSpeed, -maxXLimit, maxXLimit);
			} else if (Input.GetButton ("Player2_Right")) {
                animator.SetInteger("MoveDir", 1);
				movementOffset.x = Mathf.Clamp(movementOffset.x+movementSpeed, -maxXLimit, maxXLimit);
			}
            else
            {
                animator.SetInteger("MoveDir", 0);
            }
		}

		this.transform.position = targetPosition + movementOffset;
	}

    void attack()
    {
        if (Input.GetButtonDown("Player2Attack") && attackCounter > attackThreshold)
        {
            animator.SetTrigger("Hit");
            attackCounter = 0;
            weaponBox.enabled = true;
			AudioController.Play ("SFX_WooshHit");
        }
        else if (attackCounter < .25f)
        {
            attackCounter += Time.deltaTime;

        }
        else
        {
            attackCounter += Time.deltaTime;
            weaponBox.enabled = false;

        }

    }

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "playerCar" || attackCounter < .15f) {
			return; 
		} 
		currentParalyzedDuration = 0.0f; 
		isHit = true;
		AudioController.Play ("SFX_MetalSmash");
        animator.SetBool("Damaged", isHit);

    }

    public bool restoreCarPartsToPlayer1(CarParts.partsList type) {
		return player1Carparts.restoreParts (type);
	}
}
