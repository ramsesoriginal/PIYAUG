using UnityEngine;
using System.Collections;

public abstract class ItemType : MonoBehaviour {
	public string displayName;
	public float weight;
	public Pickup droppedPrefab;
	public Transform pickupGraphics;
	/// <summary>Time it takes for the item to become usable after selecting it from the inventory</summary>
	public float delayAfterSelect;
}