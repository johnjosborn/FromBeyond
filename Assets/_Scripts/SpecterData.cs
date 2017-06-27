using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecterData : MonoBehaviour {


	private UIController uiControl;
	private PlayManager playManager;
	private AudioSource audioSource;

	public AudioClip scarySound;

	public GameObject sound;
	public RoomController currentRoom;

	private float energyAsAPercent;
	private int hauntRate = 1;

	private float processTimeInc = 1f;
	private float processTime = 0f;

	private float energyGainRate = 1f;
	private float energyLossRate = -1f;

	public float currentEnergy;
	public float maxEnergy = 100f;

	public bool inSafeRoom;

	public float soundRange;
	public float soundCost;

	public int scarySountPoints = 2;


	// Use this for initialization
	void Start () {
		uiControl = FindObjectOfType<UIController>();
		playManager = FindObjectOfType<PlayManager>();

		audioSource = GetComponent<AudioSource>();

		currentEnergy = maxEnergy;
		energyAsAPercent = currentEnergy / maxEnergy;
	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time > processTime){
			processTime += processTimeInc;

			if (inSafeRoom){
				ChangeEnergy(energyGainRate);
			} else {
				ChangeEnergy(energyLossRate);
			}

			if (!currentRoom.isHaunted){
				currentRoom.ChangeHaunting(hauntRate);
			}

		}

	}

	public void ChangeEnergy(float energy){
		currentEnergy = Mathf.Clamp(currentEnergy += energy, 0f, maxEnergy);
		energyAsAPercent = currentEnergy / maxEnergy;
		uiControl.UpdateEnergyFill(energyAsAPercent);
		if (currentEnergy <=0){
			FailEndLevel();
		}
	}

	void FailEndLevel(){
		//Debug.Log("End Level");
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Room"){
			currentRoom = other.gameObject.GetComponent<RoomController>();
			inSafeRoom = currentRoom.isHaunted;
		} else if (other.tag == "NPC"){
			ChangeEnergy( ( 1-other.gameObject.GetComponent<NPCController>().terrorAsPercent ) * energyLossRate * 10f);
		}

	}

	public void CallNPC(){
		GameObject newSound = Instantiate (sound) as GameObject;
		ChangeEnergy(soundCost);
	}

	public void ScaryNoise(){
		//playScarySound
		//affect npcs in room
		audioSource.PlayOneShot(scarySound);
		playManager.ScarySound();

	}
}
