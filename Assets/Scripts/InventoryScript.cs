using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour {

	public Image[] inventoryBoxes;

	public SkinnedMeshRenderer penRenderer;
	public SkinnedMeshRenderer bookRenderer;

	// Use this for initialization
	void Start () {
		
	}

	void OnEnable(){
		PickupItem.OnItemPickup += AddItem;
	}

	void OnDisable(){
		PickupItem.OnItemPickup -= AddItem;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void AddItem(ItemScript item){
		switch (item.itemType) {
		case ItemType.BOOK:
			if(bookRenderer != null)
			bookRenderer.enabled = true;
			break;
		case ItemType.PEN:
			if(penRenderer != null)
			penRenderer.enabled = true;
			break;
		}
	}
}
