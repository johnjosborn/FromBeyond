using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecterController : MonoBehaviour {


	private Rigidbody2D playerRigidbody;
	private bool returnToAnchor;
	private SpriteRenderer anchorSprite;

	public float anchorDistance;

	public float effectiveMoveSpeed;

	public float maxMoveSpeed;
	public bool canMove;
	public SpecterAnchor activeAnchor;

	// Use this for initialization

	void Start () {
		playerRigidbody = GetComponent<Rigidbody2D>();
		canMove = true;
		effectiveMoveSpeed = maxMoveSpeed;
	}


	// Update is called once per frame
	void Update () {

		float distFromAnchor = Vector3.Distance(activeAnchor.transform.position, transform.position);
		if (!returnToAnchor){
			if ((distFromAnchor / anchorDistance) > 1f){
				returnToAnchor = true;
				anchorSprite.color = Color.red;
			} else if (( distFromAnchor / anchorDistance) > .75f){
				effectiveMoveSpeed = Mathf.Clamp(maxMoveSpeed * (1- (distFromAnchor / anchorDistance)) * 4f, maxMoveSpeed / 2f, maxMoveSpeed);
			} else {
				effectiveMoveSpeed = maxMoveSpeed;
			}

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

		} else {
			transform.position = Vector2.MoveTowards(transform.position, activeAnchor.transform.position, maxMoveSpeed * Time.deltaTime * 2);
		}

		if ((distFromAnchor / anchorDistance) < .75f){
			returnToAnchor = false;
			anchorSprite.color = Color.white;
		}
	}

	public void SetActiveAnchor (SpecterAnchor anchor){
		activeAnchor = anchor;
		anchorSprite = activeAnchor.GetComponent<SpriteRenderer>();
	}


}
