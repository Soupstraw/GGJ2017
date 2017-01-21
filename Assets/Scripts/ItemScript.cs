using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemScript : MonoBehaviour {

	public delegate void ItemAction(ItemScript item);
	public static event ItemAction OnItemUse;

	public ItemType itemType;
	public Sprite itemIcon;
	public SkinnedMeshRenderer itemMeshRenderer;

	public bool isUsed = false;

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

	public void Use(){
		if (!isUsed) {
			isUsed = true;
			OnItemUse (this);
		}
	}
}
