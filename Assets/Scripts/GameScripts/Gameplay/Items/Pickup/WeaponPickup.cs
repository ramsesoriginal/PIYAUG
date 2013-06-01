using UnityEngine;
using System.Collections;

public class WeaponPickup : Pickup {
	
	public WeaponType weaponType;
	public int ammo;
	
	public override Item CreateItem () {
		return new WeaponItem(weaponType) {
			AmmoLeft = ammo,
		};
	}
	
}
