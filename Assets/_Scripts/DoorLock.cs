using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLock : MonoBehaviour {

	public List<NPCController> npcControls;
	private SpecterData specterData;

	private float startTime;

	// Use this for initialization
	void Start () {
		specterData = FindObjectOfType<SpecterData>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - startTime >= specterData.lockDuration){
			Deactivate();
		}
	}

	void OnEnable() {
		startTime = Time.time;
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "NPC"){
			npcControls.Add(other.gameObject.GetComponent<NPCController>());
		} 
	}

	void Deactivate(){
		foreach (NPCController item in npcControls) {
			item.canMove = true;
		}
		gameObject.SetActive(false);
	}


}
