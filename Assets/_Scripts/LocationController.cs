using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationController : MonoBehaviour {

	public Transform stairsPosition;

	public RoomController[] roomControllers;

	private RoomController room;

	// Use this for initialization
	void Start () {
		roomControllers = FindObjectsOfType<RoomController>();
	}

	public RoomController GetEmptyRoom(){
		do {
			room = roomControllers[Random.Range(0,roomControllers.Length)];
		}
		while (room.isHaunted == true);

		return room;
	}

	public void UnblockAdjacentRooms(float xPos, float yPos){
		foreach (var item in roomControllers){
			float roomX = item.transform.position.x;
			float roomY = item.transform.position.y;
			if (Mathf.Abs(roomX - xPos) <= 5.1f && Mathf.Abs(roomY - yPos) <= 0.1f){
				item.RemoveBlock();
				item.isLocked = false;
			}
			if (Mathf.Abs(roomX - xPos) <= 0.1f && Mathf.Abs(roomY - yPos) <= 5.1f){
				item.RemoveBlock();
				item.isLocked = false;
			}
		}
	}
}
