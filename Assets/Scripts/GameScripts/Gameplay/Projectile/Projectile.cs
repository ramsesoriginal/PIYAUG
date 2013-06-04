using UnityEngine;
using System.Collections;

/// <summary>
/// Simple projectile using raycasts to detect a hit.
/// </summary>

public class Projectile : MonoBehaviour {
	
	/// <summary>
	/// Will be instantiated on impact, with position/rotation of this projectile.
	/// </summary>
	public Transform impactPrefab;
	
	public float velocity;
	public float damage;
	
	void FixedUpdate() {
		var origin = transform.position;
		var direction = transform.forward;
		var distance = velocity * Time.deltaTime;
		RaycastHit hitInfo;
		if (Physics.Raycast(origin, direction, out hitInfo, distance)) {
			// hit something
			print ("hit");
			
			// TODO get some component from the collider to apply damage ...
			
			
			if (impactPrefab) {
				var pos = origin + direction * distance;
				var rot = transform.rotation;
				/*var t = (Transform)*/ Instantiate(impactPrefab, pos, rot);
			}
			Destroy(gameObject);
		} else {
			// hit nothing
			transform.position += direction * distance;
		}
	}
	
}
