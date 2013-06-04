using UnityEngine;
using System.Collections;

[AddComponentMenu("Items/Weapon Type")]
public class WeaponType : ItemType {
	public float cooldownTime;
	public Projectile projectilePrefab;
	
	public bool overrideProjectileVelocity;
	public float projectileVelocity;
	
	public bool overrideProjectileDamage;
	public float projectileDamage;
}
