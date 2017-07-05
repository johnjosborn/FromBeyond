using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryExit : MonoBehaviour {

	private PlayManager playManager;

	// Use this for initialization
	void Start () {
		playManager = FindObjectOfType<PlayManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log("Exit Trigger");
		if (other.tag == "NPC"){
			//Destroy(other.gameObject);
			other.gameObject.SetActive(false);
			playManager.CountNPCs();
		}
	}
}
