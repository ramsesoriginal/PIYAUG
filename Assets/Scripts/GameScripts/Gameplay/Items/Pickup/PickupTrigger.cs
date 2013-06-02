using UnityEngine;
using System.Collections;

/// <summary>
/// Should be attached to a trigger collider,
/// so that the InventoryPickupDetector can detect the Pickup.
/// Simply links to an ItemPickup.
/// </summary>

[AddComponentMenu("Items/Pickup Trigger")]
public class PickupTrigger : MonoBehaviour {
	
	public Pickup pickup;

}
