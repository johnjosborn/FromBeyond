using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecterController : MonoBehaviour {


	private Rigidbody2D playerRigidbody;
	private SpriteRenderer anchorSprite;
	private SpecterData specterData;

	private bool actionInUse;

	public Camera livingCamera;

	public bool canMove;

	public float effectiveMoveSpeed;
	public float maxMoveSpeed;
	public float actionTime;
	public float timePerAction;


	// Use this for initialization

	void Start () {
		playerRigidbody = GetComponent<Rigidbody2D>();
		specterData = GetComponent<SpecterData>();
		canMove = true;
		effectiveMoveSpeed = maxMoveSpeed;
		actionTime = -2f;
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

		//if (Time.time - actionTime > timePerAction){

		if (Input.GetAxisRaw ("Fire1") != 0) {
			if (!actionInUse) {
				actionInUse = true;
				//actionTime = Time.time;
				specterData.Ability1();
			}
		}

		if (Input.GetAxisRaw ("Fire2") != 0) {
			if (!actionInUse) {
				actionInUse = true;
				//actionTime = Time.time;
				specterData.Ability2();
			}
		}

		if (Input.GetAxisRaw ("Fire3") != 0) {
			if (!actionInUse) {
				actionInUse = true;
				//actionTime = Time.time;
				specterData.Ability3();
			}
		}

		if (Input.GetAxisRaw ("Fire1") == 0 && Input.GetAxisRaw ("Fire2") == 0 && Input.GetAxisRaw ("Fire3") == 0) {
			actionInUse = false;
		}	

		if (Input.GetButtonDown("LightsOut")){ //K
			specterData.Ability4();
			//actionTime = Time.time;
		}

		//}
			
		if (Input.GetButtonDown("LivingView")){ //L
			livingCamera.enabled = !livingCamera.enabled;
		}
	}




		
}
