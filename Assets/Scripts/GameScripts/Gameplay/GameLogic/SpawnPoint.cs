using UnityEngine;
using System.Collections.Generic;

[AddComponentMenu("Game Logic/Spawn Point")]
public class SpawnPoint : MonoBehaviour {

	public Team team;
	
	void OnDrawGizmos() {
		var pos = transform.position;
		
		Gizmos.color = Color.black;
		Gizmos.DrawSphere(pos, 0.5f);
		Gizmos.color = team.GetColor();
		Gizmos.DrawSphere(pos, 0.35f);
		Gizmos.color = Color.green;
		Gizmos.DrawLine(pos + Vector3.zero, pos + transform.forward);
		Gizmos.DrawLine(pos + transform.forward, pos + Vector3.Lerp(transform.forward, -transform.right, 0.2f));
		Gizmos.DrawLine(pos + transform.forward, pos + Vector3.Lerp(transform.forward, transform.right, 0.2f));
	}
	
	void OnDrawGizmosSelected() {
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, 0.75f);
	}

}
