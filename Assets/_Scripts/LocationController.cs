using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationController : MonoBehaviour {

	public Transform stairsPosition;

	public TravelLocation[] travelLocations;

	private TravelLocation location;

	private RoomController room;

	// Use this for initialization
	void Start () {
		travelLocations = FindObjectsOfType<TravelLocation>();

	}

	public TravelLocation GetLocation(){
		do {
			location = travelLocations[Random.Range(0,travelLocations.Length)];
			room = location.GetComponentInParent<RoomController>();

		}
		while (room.isHaunted == true);

		return location;
	}
		
}
