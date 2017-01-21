using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematic : MonoBehaviour {

	public Transform camEndPos;
	private Vector3 camStartPos;

	public float panTime = 3f;

	private Camera mainCam;

	// Use this for initialization
	void Start () {
		camStartPos = transform.position;
		mainCam = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator Play(){
		if (mainCam.enabled) {
			mainCam.enabled = false;
			GetComponent<Camera> ().enabled = true;

			float time = 0f;
			while (time <= panTime) {
				time += Time.deltaTime;
				transform.position = Vector3.Lerp (camStartPos, camEndPos.position, time / panTime);
				yield return null;
			}
			yield return new WaitForSeconds (3f);

			GetComponent<Camera> ().enabled = false;
			mainCam.enabled = true;
		}
	}
}
