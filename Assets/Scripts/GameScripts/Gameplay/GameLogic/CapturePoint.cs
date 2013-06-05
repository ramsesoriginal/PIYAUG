using UnityEngine;
using System.Collections;

[AddComponentMenu("Game Logic/Capture Point")]
public class CapturePoint : MonoBehaviour {
	
	void OnTriggerEnter(Collider c) {
		var it = c.GetComponent<InventoryTrigger>();
		if (!it || !it.inventory) return;
		
		var inv = it.inventory;
		
		if (inv.SelectedItem != null && inv.SelectedItem is FlagItem && inv.SelectedItemReady) {
			inv.RemoveItem(inv.SelectedItem);
			GameLogic.Instance.OnCaptured(this);
		}
	}
	
	void OnDrawGizmos() {
		var pos = transform.position;
		
		Gizmos.color = Color.blue;
		Gizmos.DrawSphere(pos, 0.5f);
		Gizmos.color = Color.gray;
		Gizmos.DrawSphere(pos, 0.35f);
	}

}
