using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecterData : MonoBehaviour {


	private UIController uiControl;
	private PlayManager playManager;
	private AudioSource audioSource;

	public AudioClip scarySound;
	public AudioClip lockSound;

	public AudioClip insufficientAbility;

	public GameObject sound;
	public GameObject roomDarkness;
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

	public float darkCost;
	public float darkDuration;
	public float darknessTension;

	public float scarySoundPoints = 2f;

	public float lockDuration = 3f;
	public float lockCost;

	public AbilityController ability1;
	public AbilityController ability2;
	public AbilityController ability3;
	public AbilityController ability4;
	public AbilityController ability5;


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

	public void Ability1(){
		if (!ability1.isCharging){
			ability1.UseAbility();
			CallNPC();
		} else {
			audioSource.PlayOneShot(insufficientAbility);
		}
	}

	public void Ability2(){
		if (!ability2.isCharging){
			ability2.UseAbility();
			ScaryNoise();
		} else {
			audioSource.PlayOneShot(insufficientAbility);
		}
	}

	public void Ability3(){
		if (!ability3.isCharging){
			ability3.UseAbility();
			LockDoors();
		} else {
			audioSource.PlayOneShot(insufficientAbility);
		}
	}

	public void Ability4(){
		if (!ability4.isCharging){
			ability4.UseAbility();
			LightsOff();
		} else {
			audioSource.PlayOneShot(insufficientAbility);
		}
	}

	public void Ability5(){
		if (!ability5.isCharging){
			ability5.UseAbility();
		} else {
			audioSource.PlayOneShot(insufficientAbility);
		}
	}

	void CallNPC(){
		GameObject newSound = Instantiate (sound) as GameObject;
		ChangeEnergy(soundCost);
	}

	void ScaryNoise(){
		audioSource.PlayOneShot(scarySound);
		playManager.ScarySound();
		ChangeEnergy(soundCost);
	}

	void LockDoors(){
		audioSource.PlayOneShot(lockSound);
		currentRoom.ActivateLocks();
		ChangeEnergy(lockCost);
	}

	void LightsOff(){
		currentRoom.ActivateDarkness();
		ChangeEnergy(darkCost);
	}
}
