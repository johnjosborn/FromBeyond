using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecterAnchor : MonoBehaviour {

	private SpecterController player;

	private bool returnToAnchor;

	public float anchorDistance;

	void Start(){
		player = FindObjectOfType<SpecterController>();
		transform.localScale = new Vector3(player.anchorDistance, player.anchorDistance, 0f);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player"){
			player.SetActiveAnchor(this);
		}
	}
}
