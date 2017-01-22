using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour {

	public Image[] inventoryBoxes;
	public ItemScript[] items;

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

	public void UseItem(int slot){
		ItemScript item = items [slot];
		if (item != null) {
			item.Use ();
		}
	}

	void AddItem(ItemScript item){
		
		for (int i = 0; i < items.Length; i++) {
			if (items[i] == null) {
				items [i] = item;
				inventoryBoxes [i].sprite = item.itemIcon;
				if (i == 4) {
					Debug.Log ("Playing cinematic");
					StartCoroutine(GameObject.Find ("CinematicCamera").GetComponentInChildren<Cinematic> ().Play ());
				}
				break;
			}
		}

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

	public void ResetItems(){
		foreach (ItemScript item in items) {
			if (item != null) {
				item.isUsed = false;
			}
		}
	}
}
