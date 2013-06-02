using UnityEngine;
using System.Collections;

[AddComponentMenu("Items/Weapon Pickup")]
public class WeaponPickup : Pickup {
	
	public WeaponType weaponType;
	public int ammo;
	
	public override ItemType ItemType {
		get { return weaponType; }
	}
	
	public override Item CreateItem () {
		return new WeaponItem(weaponType) {
			AmmoLeft = ammo,
		};
	}
	
}
