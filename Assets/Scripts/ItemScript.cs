using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemScript : MonoBehaviour {

	public delegate void ItemAction(ItemScript item);

	public ItemType itemType;
	public Image itemIcon;
	public SkinnedMeshRenderer itemMeshRenderer;

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
