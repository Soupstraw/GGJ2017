using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepWalk : MonoBehaviour {

	public Transform walkPos;
	public float walkTime = 2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator Walk(){
		GameObject.FindWithTag ("Soundscontainer").GetComponent<DigitalRuby.SoundManagerNamespace.SoundsManager> ().PlaySound (6);
		GetComponent<Animator> ().Play ("Walk");

		float time = 0;
		Vector3 startPos = transform.position;

		while (time < walkTime) {
			time += Time.deltaTime;
			transform.position = Vector3.Lerp (startPos, walkPos.position, time/walkTime);
			yield return null;
		}

		GetComponent<Animator> ().SetTrigger ("WalkEnd");
		yield return new WaitForSeconds (1f);
		StartCoroutine(Explode ());
	}

	IEnumerator Explode(){
		GetComponentInChildren<ParticleSystem> ().Play ();
		yield return new WaitForSeconds (0.2f);
		GetComponentInChildren<SkinnedMeshRenderer> ().enabled = false;
		GetComponentInChildren<Collider> ().enabled = false;
	}
}
