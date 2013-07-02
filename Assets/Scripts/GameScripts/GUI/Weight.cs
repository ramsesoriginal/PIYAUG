using UnityEngine;
using System.Collections;

public class Weight : PIYAUGBehaviourBase {
	
	public float TEMP = 20; //should be read from player in future
	public float dampening = 20;
	private Quaternion originalsRotation;
	
	
	public float x;
	public float y;
	public float z;
	public float w;
	
	// Use this for initialization
	void Start () {
		originalsRotation = transform.parent.rotation;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Quaternion.Lerp
		var rotate = Mathf.Lerp(
				transform.rotation.y, 
				originalsRotation.y - TEMP*180, 
				dampening * Time.deltaTime
		);
		transform.rotation = new Quaternion(
			originalsRotation.x + x,
			originalsRotation.y + y,
			originalsRotation.z + z,
			originalsRotation.w + z
		);
	}
}
