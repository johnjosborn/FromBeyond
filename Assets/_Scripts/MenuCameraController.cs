using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuCameraController : MonoBehaviour {

	public Transform target;
	public float cameraSmoothing;
	public float durationInSeconds;

	public Vector3 targetPosition;
	public Vector3 startPosiiton;
	public bool inPosition;

	public float lerpTime;
	private float lerpPercent;


	// Use this for initialization
	void Start () {
		inPosition = true;
		lerpTime = 1f;
		targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, Camera.main.transform.position.z);
		startPosiiton = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
	}

	// Update is called once per frame
	void Update () {

		if (transform.position == targetPosition){
			inPosition = true;
		}

		if (lerpTime <1f){
			lerpTime += Time.deltaTime / durationInSeconds;
			lerpPercent = Mathf.Sin(lerpTime * Mathf.PI * 0.5f);
			transform.position = Vector3.Lerp(startPosiiton, targetPosition, lerpPercent);
		}

	}

	void MoveCamera(Vector3 targetLocation){
		
	}

	public void SetNewCameraTarget(Transform newTransform){
		if(newTransform.position == transform.position){
			inPosition = true;
		} else {
			inPosition = false;
			startPosiiton = targetPosition;
			targetPosition = new Vector3(newTransform.transform.position.x, newTransform.transform.position.y, Camera.main.transform.position.z);
			lerpTime = 0;

		}
	}
}
