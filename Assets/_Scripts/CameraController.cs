using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour {

	public Transform player;
	public float cameraSmoothing;

	public float offSetX;
	public float offSetY;

	public float rightLimit;
	public float leftLimit;
	public float topLimit;
	public float bottomLimit;

	public float xPos;
	public float yPos;

	private Vector3 targetPosition;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		xPos = Mathf.Clamp(player.position.x + offSetX, leftLimit + offSetX, rightLimit + offSetX);
		yPos = Mathf.Clamp(player.position.y + offSetY, bottomLimit + offSetY, topLimit + offSetY);

		targetPosition = new Vector3(xPos, yPos, transform.position.z);

		transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSmoothing * Time.deltaTime);

	}
}
