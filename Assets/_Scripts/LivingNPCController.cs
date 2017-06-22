using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingNPCController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.position = new Vector3(transform.position.x + 100, transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
