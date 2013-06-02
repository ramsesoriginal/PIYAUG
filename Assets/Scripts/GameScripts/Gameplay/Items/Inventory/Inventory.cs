using UnityEngine;
using System.Collections.Generic;

[AddComponentMenu("Items/Inventory")]
public class Inventory : MonoBehaviour {
	
	public Transform itemDropLocation;
	
	Pickup closestPickup;
	List<Item> items;
	float totalWeight;
	Item selectedItem;
	float itemReadyTime;
	
	
	public Pickup ClosestPickup { get { return closestPickup; } }
	public float TotalWeight { get { return totalWeight; } }
	public IEnumerable<Item> Items { get { return items; } }
	/// <summary>The currently selected item, might also be null.</summary>
	public Item SelectedItem { get { return selectedItem; } }
	public bool SelectedItemReady { get { return Time.fixedTime >= itemReadyTime; } }
	
	
	
	
	
	void Awake() {
		items = new List<Item>();
	}
	
	void Update() {
		
		// TODO remove this dummy testing code
		
		if (Input.GetKeyDown(KeyCode.Q)) {
			print("PickupClosestItem() -> " + PickupClosestItem());
		}
		
		if (SelectedItemReady && Input.GetKey(KeyCode.W) && selectedItem != null) {
			if (selectedItem is WeaponItem) {
				var wi = (WeaponItem) selectedItem;
				wi.Fire(transform);
			}
		}
		
		if (Input.GetKeyDown(KeyCode.E)) {
			print("DropItem() -> " + DropSelectedItem());
		}
		
		if (Input.GetKeyDown(KeyCode.R) && items.Count > 0) {
			var i = items.IndexOf(selectedItem);
			i = i < 0 ? 0 : (i + 1) % items.Count;
			print ("SelectItem(" + i + ") -> " + SelectItem(items[i]));
		}
		
		if (Input.GetKeyDown(KeyCode.T) && items.Count > 0) {
			var i = items.IndexOf(selectedItem);
			i = i < 0 ? items.Count - 1 : (i - 1 + items.Count) % items.Count;
			print ("SelectItem(" + i + ") -> " + SelectItem(items[i]));
		}
		
		if (Input.GetKeyDown(KeyCode.Space)) {
			var msg = "";
			foreach (var i in items) {
				msg += "<" + i.ItemType.displayName + "> ";
			}
			print (msg);
		}
		
	}
	
	
	
	public bool PickupClosestItem() {
		if (!closestPickup) return false;
		
		// Create the item and add it to the inventory
		
		var item = closestPickup.CreateItem();
		if (item == null) return false;
		
		// If we already have an item of the same type, check if they can combine.
		// Example: picking up a weapon type that's already present in the inventory
		// should not give you the weapon twice, but will only add additional ammo.
		var prevItem = GetItem(item.ItemType);
		if (prevItem != null) {
			if (!prevItem.CombineWithItem(item)) {
				items.Add(item);
			}
		} else {
			items.Add(item);
		}
		
		// Successfully added to inventory
		// - destroy pickup
		// - select new item if none is currently selected
		// - update weight
		
		Destroy(closestPickup.gameObject);
		closestPickup = null;
		if (selectedItem == null) {
			SelectItem(item);
		}
		UpdateTotalWeight();
		
		return true;
	}
	
	public Pickup DropSelectedItem() {
		// We can only drop our own items.
		if (selectedItem == null || !items.Contains(selectedItem)) return null;
		
		// Get drop location ...
		var tf = itemDropLocation;
		if (!tf) tf = transform;
		
		// Try to drop, and remove from inventory if successful
		var p = selectedItem.Drop(tf);
		if (p) {
			items.Remove(selectedItem);
			selectedItem = null;
			UpdateTotalWeight();
		}
		
		return p;
	}
	
	public bool SelectItem(Item item) {
		// We can only select our own items.
		if (!items.Contains(item)) return false;
		
		if (selectedItem == item) return true;
		
		selectedItem = item;
		
		itemReadyTime = Time.fixedTime + selectedItem.ItemType.delayAfterSelect;
		
		return true;
	}
	
	public Item GetItem(ItemType type) {
		foreach (var item in items) {
			if (item.ItemType == type) {
				return item;
			}
		}
		return null;
	}
	
	
	
	void UpdateTotalWeight() {
		totalWeight = 0.0f;
		foreach (var item in items) {
			totalWeight += item.ItemType.weight;
		}
	}
	
	
	
	#region Called by InventoryPickupDetector
	
	public void OnDetectedPickup(Pickup detectedPickup) {
		var pos = transform.position;
		
		if (!closestPickup) {
			// No need to check distances
			closestPickup = detectedPickup;
		} else {
			// Calculate distance to current closest Pickup to detected Pickup
			var pickupPos = closestPickup.transform.position;
			var currentDistance = Vector3.Distance(pos, pickupPos);
			var newDistance = Vector3.Distance(pos, pickupPos);
			
			// Choose new Pickup if it's closer
			if (newDistance < currentDistance) {
				closestPickup = detectedPickup;
			}
		}
	}
	
	public void OnUndetectedPickup(Pickup p) {
		if (closestPickup == p) closestPickup = null;
	}
	
	#endregion
	
}
