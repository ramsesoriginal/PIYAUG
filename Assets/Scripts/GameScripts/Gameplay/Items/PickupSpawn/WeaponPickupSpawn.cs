using UnityEngine;
using System.Collections;

[AddComponentMenu("Items/Weapon Pickup Spawn")]
public class WeaponPickupSpawn : PickupSpawn {
	
	public WeaponType weaponType;
	public int ammo;
	
	protected override void CopyStateToPickup(Pickup p) {
		base.CopyStateToPickup(p);
		
		var wp = p as WeaponPickup;
		if (wp != null) {
			wp.weaponType = weaponType;
			wp.ammo = ammo;
		}
	}
	
}
