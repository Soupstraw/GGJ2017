using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChangeZoneController : MonoBehaviour {

	public int musicToPlayID;
	public int musicRequirement;
	public DigitalRuby.SoundManagerNamespace.SoundsManager soundManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider other)
	{
		if (soundManager.currentMusic == musicRequirement) {
			soundManager.PlayMusic (musicToPlayID);
		}
	}
}
