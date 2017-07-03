using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityController : MonoBehaviour {

	private AudioSource audioSource;

	private float abilityCharge;
	private float abilityPercentFill;
	private float LerpTime;
	private bool soundPlayed;

	public float abilityFillRate;
	public float abilityRequired;

	public RawImage abilityFill;
	public Image abilityFrame;

	public bool isCharging;

	// Use this for initialization
	void Start () {
		audioSource = GetComponentInParent<AudioSource>();
		isCharging = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (isCharging){
			//is filling
			abilityFill.uvRect = new Rect(0, (abilityPercentFill /2), 0.5f, 0.5f);
			abilityCharge += Time.deltaTime * abilityFillRate;
			abilityPercentFill = abilityCharge / abilityRequired;
			abilityFrame.color = new Color(.5f,.5f,.5f, 1f);
		} else {
			abilityFrame.color = new Color(1f,1f,1f, 1f);

		}

		if (!isCharging && !soundPlayed){
			audioSource.Play();
			soundPlayed = true;
		}

		if (abilityPercentFill >= 1){
			isCharging = false;
		}
	}

	public void UseAbility(){
		abilityCharge = 0;
		isCharging = true;
		soundPlayed = false;
	}
}
