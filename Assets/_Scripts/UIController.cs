using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public RawImage energyFill;
	public float energyFillCurrent;

	private float floatLerpTime = 1f;
	private float floatLerpDuration = 1f;
	private float newPercent;

	public void UpdateEnergyFill(float energyPercent){
		newPercent = energyPercent;
		energyFillCurrent = (energyFill.uvRect.x + .5f) / 1.5f;
		//energyFill.uvRect = new Rect(-1f + energyPercent * 2f, 0, 1.0f, 1.0f);
		floatLerpTime = 0f;
	}

	void Update(){
		if(floatLerpTime < 1){
			float fillPercent = Mathf.Lerp(energyFillCurrent, newPercent, floatLerpTime);
			energyFill.uvRect = new Rect(-.5f + fillPercent * 1.5f, 0, 1.0f, 1.0f);
			floatLerpTime += Time.deltaTime/floatLerpDuration;
		}
	}
}
