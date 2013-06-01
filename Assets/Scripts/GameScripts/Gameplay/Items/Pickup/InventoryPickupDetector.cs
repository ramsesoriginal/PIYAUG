using UnityEngine;
using System.Collections;


/// <summary>
/// Inventory pickup detector.
/// Allows detection of Pickups that use a trigger.
/// Notifies the linked inventory about detected Pickups.
/// </summary>

[RequireComponent(typeof(Collider))]
public class InventoryPickupDetector : MonoBehaviour {
	
	public Inventory inventory;
	
	void OnTriggerStay(Collider c) {
		if (!inventory) return;
		
		var pickup = c.GetComponent<Pickup>();
		if (!pickup) return;
		
		inventory.OnDetectedPickup(pickup);
	}
	
	void OnTriggerExit(Collider c) {
		if (!inventory) return;
		
		var pickup = c.GetComponent<Pickup>();
		if (!pickup) return;
		
		inventory.OnUndetectedPickup(pickup);
	}
	
}
