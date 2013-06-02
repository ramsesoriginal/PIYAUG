using UnityEngine;
using System.Collections;

public abstract class PickupSpawn : MonoBehaviour {
	
	public Pickup pickupPrefab;
	public float initialDelay;
	public float spawnInterval;
	
	Pickup spawnedPickup;
	bool spawned;
	float nextSpawnTime;
	
	void Reset() {
		spawnInterval = 10.0f;
	}
	
	void Start() {
		nextSpawnTime = Time.fixedTime + initialDelay;
	}
	
	void Update () {
		if (spawned) {
			if (!spawnedPickup) {
				spawned = false;
				nextSpawnTime = Time.fixedTime + spawnInterval;
			}
		} else {
			if (Time.fixedTime >= nextSpawnTime && pickupPrefab) {
				var pos = transform.position;
				var rot = transform.rotation;
				spawnedPickup = (Pickup) Instantiate(pickupPrefab, pos, rot);
				CopyStateToPickup(spawnedPickup);
				spawned = true;
			}
		}
	}
	
	void OnDrawGizmos() {
		var pos = transform.position;
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(pos, 0.3f);
	}
	
	/// <summary>
	/// Copies additional state to the created pickup.
	/// </summary>
	protected virtual void CopyStateToPickup(Pickup p) {
	}
	
}
