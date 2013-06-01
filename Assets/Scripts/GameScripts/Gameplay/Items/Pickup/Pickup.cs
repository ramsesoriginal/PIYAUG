using UnityEngine;
using System.Collections;

public abstract class Pickup : MonoBehaviour {

	public Transform transformOverride;
	
	public Transform GetTransform() {
		if (transformOverride) {
			return transformOverride;
		}
		return transform;
	}
	
	/// <summary>
	/// Creates the item for the Inventory can take.
	/// </summary>
	public abstract Item CreateItem();
	
}
