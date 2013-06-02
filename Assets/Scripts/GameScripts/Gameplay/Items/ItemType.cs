using UnityEngine;
using System.Collections;

public abstract class ItemType : MonoBehaviour {
	public string displayName;
	public float weight;
	public Pickup droppedPrefab;
	public Transform pickupGraphics;
}
