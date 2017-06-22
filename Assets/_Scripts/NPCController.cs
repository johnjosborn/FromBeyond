using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour {

	public int npcSpeed;
	public int maxTerror;
	public int currentTerror;
	public float terrorAsPercent;
	public float timeArrived;
	public float colorLerpDuration;
	public bool targetReached;
	public bool onCorrectFloor;

	public RoomController finalTarget;
	public Animator livingAnimator;
	public GameObject locationNode;

	private SpriteRenderer spiritSpriteRenderer;

	private LocationController locationController;
	private Animator spiritAnimator;
	private Color targetColor;
	private Color currentColor;
	private float colorLerpTime = 1;
	//private float roomVertOffest = 2;
	private EntryExit entryExit;
	private Vector3 finalTargetPosition;


	// Use this for initialization
	void Start () {
		locationController = FindObjectOfType<LocationController>();
		spiritAnimator = GetComponent<Animator>();
		spiritSpriteRenderer = GetComponent<SpriteRenderer>();
		currentTerror = 0;
		entryExit = FindObjectOfType<EntryExit>();
		finalTargetPosition = finalTarget.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (onCorrectFloor){
			
			if (!targetReached){
				transform.position = Vector2.MoveTowards(transform.position, finalTargetPosition, npcSpeed * Time.deltaTime);
			} else {
				if ((Time.time - timeArrived > finalTarget.dwellTime)){
					SetNewTarget();
					targetReached = false;
				}
			}
			
		} else {

			if (Mathf.Abs(transform.position.x - locationController.stairsPosition.position.x) < .1f){
				//wrong floor, on stairs
				if (transform.position.y > finalTargetPosition.y){
					//move down
					transform.Translate(Vector3.down * Time.deltaTime * npcSpeed);
				} else {
					//move up
					transform.Translate(Vector3.up * Time.deltaTime * npcSpeed);
				}
			} else {
				//move towards stairs
				if (transform.position.x > locationController.stairsPosition.position.x){
					//move left
					transform.Translate(Vector3.left * Time.deltaTime * npcSpeed);
				} else {
					//move up
					transform.Translate(Vector3.right * Time.deltaTime * npcSpeed);
				}
			}

			if (Mathf.Abs(transform.position.y - finalTargetPosition.y) < .1f){
				onCorrectFloor = true;
			}			
		}

		if (transform.position == finalTargetPosition && !targetReached){
			targetReached = true;
			timeArrived = Time.time;
		}

		if(colorLerpTime < 1){
			spiritSpriteRenderer.color = Color.LerpUnclamped(currentColor, new Color(1f,1f - terrorAsPercent,1f - terrorAsPercent,1f), colorLerpTime);
			colorLerpTime += Time.deltaTime/colorLerpDuration;
		}

	}

	void SetNewTarget(){
		finalTarget = locationController.GetEmptyRoom();
		finalTargetPosition = finalTarget.transform.position;
		if (Mathf.Abs(transform.position.y - finalTargetPosition.y) < .1f){
			onCorrectFloor = true;
		} else {
			onCorrectFloor = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			ScareNPC(2);
		}
	}

	void ScareNPC(int terror){
		currentTerror += terror;
		terrorAsPercent = (float)currentTerror / maxTerror;
		currentColor = spiritSpriteRenderer.color;
		targetColor = new Color(1f,1f - terrorAsPercent,1f - terrorAsPercent,1f);
		colorLerpTime = 0;
		if (terrorAsPercent >= 1){
			FleeArea();
		}
	}

	void FleeArea(){
		targetReached = false;
		finalTargetPosition = entryExit.transform.position;
		if (Mathf.Abs(transform.position.y - finalTargetPosition.y) < .1f){
			onCorrectFloor = true;
		} else {
			onCorrectFloor = false;
		}
		npcSpeed = npcSpeed * 2;
	}




		
}
