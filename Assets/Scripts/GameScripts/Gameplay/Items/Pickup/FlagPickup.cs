using UnityEngine;
using System.Collections;

[AddComponentMenu("Items/Flag Pickup")]
public class FlagPickup : Pickup {

	public override ItemType ItemType {
		get { return GameLogic.Instance.flagItemType; }
	}
	
	public override Item CreateItem () {
		return new FlagItem(ItemType);
	}
}
