using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour {

	private RoomBlock roomBlock;
	private SpriteRenderer spriteRenderer;
	private LocationController locController;
	//private SpecterController specterController;
	private SpecterData specterData;
	private int occupiedNPC;
	private bool occupiedSpec;
	public bool isHaunted;
	public bool isLocked;
	public bool isStairs;

	private float hauntTimeInc = 1f;
	private float hauntTime = 0f;
	private float hauntingAsAPercentage;

	private Color currentColor;
	private float colorLerpTime = 1f;
	private float colorLerpDuration = 1f;

	public float dwellTime;
	public int hauntingLevel;
	public int requiredToHaunt;


	void Start () {
		roomBlock = GetComponentInChildren<RoomBlock>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		locController = FindObjectOfType<LocationController>();
		specterData = FindObjectOfType<SpecterData>();
	}


	void Update () {

		if (!isHaunted && Time.time > hauntTime){
			hauntTime += hauntTimeInc;
			if(!occupiedSpec){
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

		if(colorLerpTime < 1 && !isHaunted && !isLocked){
			spriteRenderer.color = Color.LerpUnclamped(currentColor, new Color(hauntingAsAPercentage,hauntingAsAPercentage,hauntingAsAPercentage, 1f), colorLerpTime);
			colorLerpTime += Time.deltaTime/colorLerpDuration;
		}
			
	}

	public void ChangeHaunting(int haunt){
		hauntingLevel = Mathf.Clamp(hauntingLevel + haunt, 0, requiredToHaunt);
		if (hauntingLevel >= requiredToHaunt / 2){
			locController.UnblockAdjacentRooms(transform.position.x, transform.position.y, isStairs);
		}
		if (hauntingLevel >= requiredToHaunt){
			isHaunted = true;
			specterData.inSafeRoom = true;
		}
		currentColor = spriteRenderer.color;
		colorLerpTime = 0f;
	}

	public void RemoveBlock(){
		roomBlock.gameObject.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player"){
			occupiedSpec = true;
		} else if (other.tag == "NPC"){
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
