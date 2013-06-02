using UnityEngine;
using System.Collections;


/// <summary>
/// Inventory pickup detector.
/// Allows detection of ItemPickups through a PickupTrigger
/// Notifies the linked inventory about detected ItemPickup.
/// </summary>

[RequireComponent(typeof(Collider))]
public class InventoryPickupDetector : MonoBehaviour {
	
	public Inventory inventory;
	
	void OnTriggerStay(Collider c) {
		if (!inventory) return;
		
		var pt = c.GetComponent<PickupTrigger>();
		if (!pt || !pt.pickup) return;
		
		inventory.OnDetectedPickup(pt.pickup);
	}
	
	void OnTriggerExit(Collider c) {
		if (!inventory) return;
		
		var pt = c.GetComponent<PickupTrigger>();
		if (!pt || !pt.pickup) return;
		
		inventory.OnUndetectedPickup(pt.pickup);
	}
	
}
