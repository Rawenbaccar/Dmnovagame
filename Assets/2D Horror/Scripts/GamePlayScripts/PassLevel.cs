using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassLevel : MonoBehaviour {
	public GameObject panelLevel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//When Player walk into PANELLEVEL it shows panel with button
	//to pass the level
	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player")){
		panelLevel.SetActive (true);
	}
}
}
