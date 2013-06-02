using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for all kinds of Pickups.
/// </summary>

public abstract class Pickup : MonoBehaviour {
	
	public Transform graphicsTransform;
	
	public abstract ItemType ItemType { get; }
	public abstract Item CreateItem();
	
	public float spinY;
	
	void Start() {
		var prefab = ItemType.pickupGraphics;
		if (prefab) {
			var pos = graphicsTransform.position;
			var rot = graphicsTransform.rotation;
			var o = Instantiate(prefab, pos, rot);
			var t = (Transform) o;
			t.parent = graphicsTransform;
		}
	}
	
	void Update() {
		if (graphicsTransform) {
			graphicsTransform.Rotate(Vector3.up, spinY * Time.deltaTime);
		}
	}
	
}
