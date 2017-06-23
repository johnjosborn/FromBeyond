using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour {

	public RoomBlock roomBlock;
	private SpriteRenderer spriteRenderer;
	private LocationController locController;
	private SpecterController specterController;
	private int occupiedNPC;
	private bool occupiedSpec;
	public bool isHaunted;
	public bool isLocked;

	private float hauntTimeInc = 1f;
	private float hauntTime = 0f;
	private float hauntingAsAPercentage;

	public float dwellTime;
	public int hauntingLevel;
	public int requiredToHaunt;


	void Start () {
		roomBlock = GetComponentInChildren<RoomBlock>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		locController = FindObjectOfType<LocationController>();
		specterController = FindObjectOfType<SpecterController>();
	}


	void Update () {

		if (!isHaunted && Time.time > hauntTime){
			hauntTime += hauntTimeInc;
			if( occupiedSpec && occupiedNPC <=0){
				//player only
				ChangeHaunting(1);
			} else if(occupiedSpec && occupiedNPC > 0){
				//player and npc
				ChangeHaunting(-2);
			} else if(!occupiedSpec && occupiedNPC > 0){
				//npc only
				ChangeHaunting(-5);
			} else if(!occupiedSpec && occupiedNPC <= 0){
				//empty
				ChangeHaunting(-1);
			}
		}

		if (isHaunted){
			spriteRenderer.color = new Color(.59f,.75f,.8f, 1f);
		} else if (!isLocked){
			hauntingAsAPercentage = (float)hauntingLevel / requiredToHaunt;
			spriteRenderer.color = new Color(hauntingAsAPercentage,hauntingAsAPercentage,hauntingAsAPercentage, 1f);
		} else {
			spriteRenderer.color = Color.red;
		}
			
	}

	void ChangeHaunting(int haunt){
		hauntingLevel = Mathf.Clamp(hauntingLevel + haunt, 0, requiredToHaunt);
		if (hauntingLevel >= requiredToHaunt / 2){
			locController.UnblockAdjacentRooms(transform.position.x, transform.position.y);
		}
		if (hauntingLevel >= requiredToHaunt){
			isHaunted = true;
			if (occupiedSpec){
				specterController.inSafeRoom = true;
			}
		}
	}

	public void RemoveBlock(){
		roomBlock.gameObject.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player"){
			Debug.Log("Player enter: " + this);
			occupiedSpec = true;
		} else if (other.tag == "NPC"){
			Debug.Log("NPC enter: " + this);
			occupiedNPC += 1;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player"){
			occupiedSpec = false;
		} else if (other.tag == "NPC"){
			occupiedNPC -= 1;
		}
	}
}
