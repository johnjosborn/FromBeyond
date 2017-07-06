using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLock : MonoBehaviour {

	public List<NPCController> npcControls;
	private SpecterData specterData;
	private BoxCollider2D doorCollider;
	public GameObject doorImage;

	private float startTime;
	public float targetScale;
	public float moveDuration;
	private bool doorClosed;

	public float defaultXScale;
	public float lerpTimer;

	// Use this for initialization
	void Start () {
		specterData = FindObjectOfType<SpecterData>();
		doorCollider = GetComponent<BoxCollider2D>();
		doorCollider.enabled = false;
		lerpTimer = 1;
		defaultXScale = doorImage.transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - startTime >= specterData.lockDuration && doorClosed){
			doorCollider.enabled = false;
			OpenDoor();
		}

		if(lerpTimer < 1f){
			doorImage.transform.localScale = new Vector3(Mathf.Lerp(doorImage.transform.localScale.x, targetScale, lerpTimer),doorImage.transform.localScale.y, 0f);
			lerpTimer += Time.deltaTime/moveDuration;
		}
	}

	public void CloseDoor() {
		startTime = Time.time;
		doorCollider.enabled = true;
		lerpTimer = 0;
		targetScale = 0;
		doorClosed = true;
		Debug.Log("Close doors");
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "NPC"){
			npcControls.Add(other.gameObject.GetComponent<NPCController>());
		} 
	}

	void OpenDoor(){
		foreach (NPCController item in npcControls) {
			item.canMove = true;
		}
		lerpTimer = 0;
		targetScale = defaultXScale;
		doorClosed = false;
		Debug.Log("Open doors");
	}


}
