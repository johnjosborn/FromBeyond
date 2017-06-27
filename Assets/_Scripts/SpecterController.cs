using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecterController : MonoBehaviour {


	private Rigidbody2D playerRigidbody;
	private SpriteRenderer anchorSprite;
	private SpecterData specterData;

	private bool actionInUse;


	public bool canMove;

	public float effectiveMoveSpeed;
	public float maxMoveSpeed;
	public float actionTime;


	// Use this for initialization

	void Start () {
		playerRigidbody = GetComponent<Rigidbody2D>();
		specterData = GetComponent<SpecterData>();
		canMove = true;
		effectiveMoveSpeed = maxMoveSpeed;
		actionTime = -1f;
	}
		
	// Update is called once per frame
	void Update () {

		if (canMove){
			if(Input.GetAxisRaw("Horizontal") > 0){
				playerRigidbody.velocity = new Vector3(effectiveMoveSpeed, playerRigidbody.velocity.y, 0f);
			} else if(Input.GetAxisRaw("Horizontal") < 0){
				playerRigidbody.velocity = new Vector3(-effectiveMoveSpeed, playerRigidbody.velocity.y, 0f);
			} else {
				playerRigidbody.velocity = new Vector3(0f, playerRigidbody.velocity.y, 0f);
			} 

			if(Input.GetAxisRaw("Vertical") > 0){
				playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, effectiveMoveSpeed, 0f);
			} else if(Input.GetAxisRaw("Vertical") < 0){
				playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, -effectiveMoveSpeed, 0f);
			} else {
				playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, 0f, 0f);
			}
		} 

		if (Time.time - actionTime > 1f){

			if (Input.GetAxisRaw ("Fire1") != 0) {
				if (!actionInUse) {
					actionInUse = true;
					actionTime = Time.time;
					specterData.CallNPC();
				}
			}
			if (Input.GetAxisRaw ("Fire1") == 0) {
				actionInUse = false;
			}

			if (Input.GetAxisRaw ("Fire2") != 0) {
				if (!actionInUse) {
					actionInUse = true;
					actionTime = Time.time;
					specterData.ScaryNoise();
				}
			}
			if (Input.GetAxisRaw ("Fire2") == 0) {
				actionInUse = false;
			}
		}

	}




		
}
