using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	
	public virtual void SetVelocity(float velocity) {
		if (rigidbody) {
			rigidbody.AddForce(Vector3.forward * velocity, ForceMode.VelocityChange);
		}
	}
	
}
