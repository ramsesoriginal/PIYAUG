using UnityEngine;
using System.Collections;

public class Item {
	
	ItemType type;
	
	public Item(ItemType type) {
		this.type = type;
	}
	
	public ItemType ItemType {
		get { return type; }
	}
	
	public Pickup Drop(Transform tf) {
		// Drop only if there is a prefab for the pickup
		var prefab = type.droppedPrefab;
		if (!prefab) return null;
		
		// Instantiate object and transfer all relevant state to the new pickup.
		var pickup = (Pickup) Object.Instantiate(prefab, tf.position, tf.rotation);
		CopyStateToPickup(pickup);
		
		return pickup;
	}
	
	protected virtual void CopyStateToPickup(Pickup pickup) {
	}
	
	public virtual bool CombineWithItem(Item item) {
		return false;
	}

}
