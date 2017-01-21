using System;
using UnityEngine;

public class PenItemScript : ItemScript{

	public SkinnedMeshRenderer penMeshRenderer;

	public override void OnItemPickup(){
		penMeshRenderer.enabled = true;
	}
}

