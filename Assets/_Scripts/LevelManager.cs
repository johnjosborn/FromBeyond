using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public float autoLoadNextLevelAfter;
	
	void Start(){
		if (autoLoadNextLevelAfter <= 0){
			Debug.Log("AutoLoad Disabled");
		} else {
			Invoke ("LoadNextLevel", autoLoadNextLevelAfter);
		}
	}
	
	//load a new level
	public void LoadLevel(string Name){
		Debug.Log ("Level Load Requested for " + Name);
		Application.LoadLevel (Name);

	}
	
	//quit the game
	public void QuitGame (){
		Debug.Log ("Quit requested");
		Application.Quit();
	}

	public void LoadNextLevel(){
		Application.LoadLevel(Application.loadedLevel + 1);


	}
	
	

}
