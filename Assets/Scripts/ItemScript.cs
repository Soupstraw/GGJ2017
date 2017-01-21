using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col){
		OnItemPickup ();
		GetComponentInChildren<MeshRenderer> ().enabled = false;
		GetComponent<Collider> ().enabled = false;
	}

	public virtual void OnItemPickup(){
		
	}
}
