using System;
using UnityEngine;
using UnityEngine.UI;

public class PickupItem : ItemScript{

	public static event ItemAction OnItemPickup;

	public void OnTriggerEnter(Collider col){
		GetComponentInChildren<MeshRenderer> ().enabled = false;
		GetComponentInChildren<Collider> ().enabled = false;
		GetComponentInChildren<ParticleSystem> ().Stop ();
		OnItemPickup (this);
	}
}

