﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematic : MonoBehaviour {

	public Transform camEndPos;
	private Vector3 camStartPos;
	private bool soundPlayed = false;

	public float panTime = 3f;

	private Camera mainCam;

	// Use this for initialization
	void Start () {
		camStartPos = transform.position;
		mainCam = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			StartCoroutine (Play());
		}
	}

	public IEnumerator Play(){
		if (mainCam.enabled) {
			if (!soundPlayed) {
				DigitalRuby.SoundManagerNamespace.SoundsManager soundManager = GameObject.FindWithTag ("Soundscontainer").GetComponent<DigitalRuby.SoundManagerNamespace.SoundsManager> ();
				soundManager.PlaySound (6);
				soundPlayed = true;
			}

			mainCam.enabled = false;
			GetComponent<Camera> ().enabled = true;

			float time = 0f;
			while (time <= panTime) {
				time += Time.deltaTime;
				transform.position = Vector3.Lerp (camStartPos, camEndPos.position, time / panTime);
				yield return null;
			}

			StartCoroutine(GameObject.Find ("sheep").GetComponentInChildren<SheepWalk> ().Walk ());
			yield return new WaitForSeconds (6f);

			GetComponent<Camera> ().enabled = false;
			mainCam.enabled = true;
		}
	}
}
