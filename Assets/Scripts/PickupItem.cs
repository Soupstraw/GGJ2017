using System;
using UnityEngine;
using UnityEngine.UI;

public class PickupItem : ItemScript{

	public static event ItemAction OnItemPickup;

	public void OnTriggerEnter(Collider col){
		GameObject.FindWithTag ("Soundscontainer").GetComponent<DigitalRuby.SoundManagerNamespace.SoundsManager> ().PlaySound (5);
		GetComponentInChildren<Collider> ().enabled = false;
		GetComponentInChildren<MeshRenderer> ().enabled = false;
		GetComponentInChildren<ParticleSystem> ().Stop ();
		OnItemPickup (this);

	}
}

