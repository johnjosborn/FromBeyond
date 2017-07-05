using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour {

	public NPCController[] npcControllers;
	private RoomController[] roomControllers;

	private SpecterData specterData;

	public bool houseHaunted;
	public bool allScared;

	// Use this for initialization
	void Start () {
		npcControllers = FindObjectsOfType<NPCController>();
		roomControllers = FindObjectsOfType<RoomController>();

		specterData = FindObjectOfType<SpecterData>();
	}

	public void ScarySound(){

		foreach (var npc in npcControllers) {

			if (npc.currentRoom == specterData.currentRoom && !npc.isFleeing){
				npc.ScareNPC(specterData.scarySoundDamage);
			}
		}
	}

	public void CountNPCs(){

		int activeNPCCount = 0;

		foreach (var npc in npcControllers) {

			if (npc.isActiveAndEnabled){
				activeNPCCount++;
			}
		}

		if(activeNPCCount <= 0){
			allScared = true;
			CompletionCheck();
		} else {
			allScared = false;
		}
	}

	public void CountHauntedRooms(){
		int unHauntedRoomCount = 0;

		foreach (var room in roomControllers) {

			if (!room.isHaunted){
				unHauntedRoomCount++;
			}
		}

		if(unHauntedRoomCount > 0){
			houseHaunted = false;
		} else {
			houseHaunted = true;
			CompletionCheck();
		}

		Debug.Log("# of unhaunted rooms: " + unHauntedRoomCount);
	}

	void CompletionCheck(){
		if (allScared && houseHaunted){
			Debug.Log("Level Complete");
		}
	}

}
