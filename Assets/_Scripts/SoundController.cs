using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

	private SpecterData specData;
	private float floatLerpTime = 1f;
	private float floatLerpDuration = 1f;
	public float maxSize = 5f;

	// Use this for initialization
	void Start () {
		floatLerpTime = 0;
		specData = FindObjectOfType<SpecterData>();
		maxSize = specData.soundRange;
		transform.position = specData.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(floatLerpTime < 1){
			float newScale = Mathf.Lerp(0f, maxSize, floatLerpTime);
			transform.localScale = new Vector3(newScale, newScale, 1f);
			floatLerpTime += Time.deltaTime/floatLerpDuration;
		} else {
			Destroy(gameObject);
		}
	}

}
