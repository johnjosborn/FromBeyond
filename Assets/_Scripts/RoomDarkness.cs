using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDarkness : MonoBehaviour {

	private RoomController roomController;
	private SpecterData specterData;

	private float startTime;



	// Use this for initialization
	void Start () {
		roomController = GetComponentInParent<RoomController>();
		specterData = FindObjectOfType<SpecterData>();
		startTime = Time.time;
		roomController.roomTension += specterData.darknessTension;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - startTime >= specterData.darkDuration){
			gameObject.SetActive(false);
		}
	}

	void OnEnable() {
		startTime = Time.time;
	}

	void OnDisable(){
		roomController.roomTension -= specterData.darknessTension;
	}
		
}
