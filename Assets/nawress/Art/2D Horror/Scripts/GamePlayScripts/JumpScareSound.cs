using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script is: Play sound when Player walk into Trigger
public class JumpScareSound : MonoBehaviour {

	public AudioClip SoundToPlay;
	public new AudioSource audio;
	public bool alreadyPlayed = false; //If Player was in Trigger

	void Start()
	{

		audio = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter2D()
	{
		//When Player walk into trigger it play sound and check
		//if Player walked into it. It can be played only once
		if (!alreadyPlayed) {
			audio.PlayOneShot (SoundToPlay);
			alreadyPlayed = true; //If Player was there, stop playing again
		}
	}

	}