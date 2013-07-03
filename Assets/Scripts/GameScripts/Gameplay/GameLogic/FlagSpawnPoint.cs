using UnityEngine;
using System.Collections.Generic;

[AddComponentMenu("Game Logic/Flag Spawn Point")]
public class FlagSpawnPoint : MonoBehaviour {
	
	void OnDrawGizmos() {
		var pos = transform.position;
		
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(pos, 0.5f);
		Gizmos.color = Color.gray;
		Gizmos.DrawSphere(pos, 0.35f);
	}

}
