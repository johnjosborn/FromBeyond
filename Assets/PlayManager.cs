using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour {

	public NPCController[] npcControllers;

	private SpecterData specterData;

	// Use this for initialization
	void Start () {
		npcControllers = FindObjectsOfType<NPCController>();

		specterData = FindObjectOfType<SpecterData>();
	}

	public void ScarySound(){

		foreach (var npc in npcControllers) {

			if (npc.currentRoom == specterData.currentRoom && !npc.isFleeing){
				npc.ScareNPC(specterData.scarySountPoints);
			}
		}
	}

}
