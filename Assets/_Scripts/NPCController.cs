using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour {

	private int npcSpeed;
	public int maxNPCSpeed;
	public float timeArrived;
	public float colorLerpDuration;
	public bool targetReached;
	public bool onCorrectFloor;
	public bool isFleeing;
	public bool canMove;
	public bool terrorFalling;
	public bool tensionFalling;

	public float maxTerror;
	public float currentTerror;
	public float terrorAsPercent;

	public float currentTension;
	public float maxTension;

	public float npcBravery;


	public TravelLocation finalTarget; //where the NPC is going
	private Vector3 finalTargetPosition; //coordinates of final target
	private RoomController selectedRoom; //room if final target

	public RoomController currentRoom; //room where NPC is
	public TravelLocation exitRoom; //where to flee house
	private LocationController locationController; //used to get new location

	public Animator livingAnimator;

	//public GameObject locationNode;

	public AudioClip[] npcScaredSounds;

	public AudioClip heardSomething;


	private SpriteRenderer spiritSpriteRenderer;

	private Animator spiritAnimator;
	private Color currentColor;
	private float colorLerpTime = 1;
	private SpecterData specterData;
	private AudioSource audioSource;
	private IEnumerator coroutine;

	// Use this for initialization
	void Start () {
		locationController = FindObjectOfType<LocationController>();
		specterData = FindObjectOfType<SpecterData>();

		spiritAnimator = GetComponent<Animator>();
		spiritSpriteRenderer = GetComponent<SpriteRenderer>();
		audioSource = GetComponent<AudioSource>();

		finalTargetPosition = finalTarget.transform.position;

		currentTerror = 0;
		currentTension = 100f;
		npcSpeed = maxNPCSpeed;
		canMove = true;
	}
	
	// Update is called once per frame
	void Update () {

		if(canMove){
			if (onCorrectFloor){
				if (!targetReached){
					transform.position = Vector2.MoveTowards(transform.position, finalTargetPosition, npcSpeed * Time.deltaTime);
				} else {
						if ((Time.time - timeArrived > currentRoom.dwellTime) && !isFleeing){
						Debug.Log("Set taregt from update");
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
		}

		if (transform.position == finalTargetPosition && !targetReached){
			targetReached = true;
			isFleeing = false;
			npcSpeed = maxNPCSpeed;
			timeArrived = Time.time;
		}

		if(colorLerpTime < 1){
			spiritSpriteRenderer.color = Color.LerpUnclamped(currentColor, new Color(1f,1f - terrorAsPercent,1f - terrorAsPercent,1f), colorLerpTime);
			colorLerpTime += Time.deltaTime/colorLerpDuration;
		}

		if(terrorFalling && !isFleeing){
			currentTerror = Mathf.Clamp(currentTerror += (Time.deltaTime * npcBravery * -.1f), 0f, maxTerror);
		}

		ChangeTension(Time.deltaTime * npcBravery * currentRoom.roomTension);

	}

	void SetNewTarget(){
		do {
			finalTarget = locationController.GetLocation();
			selectedRoom = finalTarget.GetComponentInParent<RoomController>();
		}
		while (selectedRoom == currentRoom);

		//finalTarget = locationController.GetLocation();
		TrackLocation(finalTarget);
	}

	void TrackLocation(TravelLocation location){
		targetReached = false;
		finalTargetPosition = location.transform.position;
		if (Mathf.Abs(transform.position.y - finalTargetPosition.y) < .1f){
			onCorrectFloor = true;
		} else {
			onCorrectFloor = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player" && !isFleeing){
				ScareNPC(2);
		} else if (other.tag == "Sound" && !isFleeing){
			finalTarget = specterData.currentRoom.GetComponentInChildren<TravelLocation>();
			selectedRoom = finalTarget.GetComponentInParent<RoomController>();
			if (selectedRoom == currentRoom){
				ScareNPC(2);
			} else {
				audioSource.PlayOneShot(heardSomething);
				TrackLocation(finalTarget);
			}
		} else if (other.tag == "Room"){
			currentRoom = other.gameObject.GetComponent<RoomController>();
			tensionFalling = !currentRoom.isHaunted;
			terrorFalling = !currentRoom.isHaunted;
		} else if (other.tag == "Lock"){
			Debug.Log("LockEnter");
			canMove = false;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Lock"){
			Debug.Log("LockExit");
			canMove = true;
		}
	}

	public void ScareNPC(float terror){
	
			Debug.Log("Scare value: " + (terror * currentTension / 100f));
			currentTerror = Mathf.Clamp(currentTerror += (terror * currentTension / 100f), 0f, maxTerror);
			terrorAsPercent = (float)currentTerror / maxTerror;

			currentTension = 0;

			currentColor = spiritSpriteRenderer.color;
			colorLerpTime = 0;

			isFleeing = true;

			if (terrorAsPercent >= 1f){
				Debug.Log("Flee house");

				coroutine = PlayNPCSound(0.2f, npcScaredSounds[0]);
				StartCoroutine(coroutine);

				npcSpeed = npcSpeed * 4;
				FleeHouse();
			} else {

				Debug.Log("run to new room");

				coroutine = PlayNPCSound(0.2f, npcScaredSounds[1]);
				StartCoroutine(coroutine);
				npcSpeed = npcSpeed * 2;
				SetNewTarget();
			}
	}

	IEnumerator PlayNPCSound(float delaySound, AudioClip clipToPlay){
		yield return new WaitForSeconds(delaySound);
		audioSource.PlayOneShot(clipToPlay);
	}

	void FleeHouse(){
		TrackLocation(exitRoom);
	}

	public void ChangeTension(float tension){
		currentTension = Mathf.Clamp(currentTension += tension, 50f, maxTension);
		if(currentTension >= maxTension){
			npcSpeed = npcSpeed * 2;
			SetNewTarget();
		}
	}


		
}
