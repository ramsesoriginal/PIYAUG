using UnityEngine;
using System.Collections;

public class WeaponItem : Item {
	
	public WeaponItem(WeaponType type) : base(type) {
	}
	
	public int AmmoLeft { get; set; }
	public float ReadyTime { get; set; }
	
	WeaponType WeaponType {
		get { return (WeaponType) ItemType; }
	}
	
	/// <summary>
	/// Attempt to fire the weapon, if possible.
	/// Call this whenever the fire key is down.
	/// </summary>
	public bool Fire(Transform origin) {
		// Can only fire if ready and ammo available
		if (Time.time < ReadyTime || AmmoLeft <= 0) return false;
		
		var p = (Projectile) Object.Instantiate(WeaponType.projectilePrefab, origin.position, origin.rotation);
		if (WeaponType.overrideProjectileVelocity) {
			p.velocity = WeaponType.projectileVelocity;
		}
		if (WeaponType.overrideProjectileDamage) {
			p.damage = WeaponType.projectileDamage;
		}
		
		AmmoLeft -= 1;
		ReadyTime = Time.time + WeaponType.cooldownTime;
		
		return true;
	}
	
	protected override void CopyStateToPickup (Pickup pickup) {
		var w = pickup as WeaponPickup;
		if (w == null) return;
		
		w.weaponType = WeaponType;
		w.ammo = AmmoLeft;
	}
	
	public override bool CombineWithItem (Item item) {
		var w = item as WeaponItem;
		AmmoLeft += w.AmmoLeft;
		return true;
	}
	
}
