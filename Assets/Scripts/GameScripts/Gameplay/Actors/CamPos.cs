using UnityEngine;
using System.Collections;

public class CamPos : PIYAUGBehaviourBase {
	
	public float smooth = 0.3f;
	public Transform player;
	public float cameraHeight = 6;
	public float cameraBack = 2;
	public bool rotateWithPlayer = false;
	private bool moved = false;
	
	// Use this for initialization
	void Start () {
		if (this.player == null)
		{
			var player = GameObject.FindWithTag("Player");
			if (player != null)
				this.player = player.transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		var cameraBackOffset = player.forward * -cameraBack;
		var targetCameraPos = new Vector3(
			player.position.x,
			player.position.y + cameraHeight,
			player.position.z) + cameraBackOffset;
		if (rotateWithPlayer)
		{
			var velocity = player.rigidbody.velocity.magnitude;
			if (velocity < 0.5f)
			{
				velocity = 0.5f;
				moved = true;
			}
			if (moved) {
				velocity = velocity * 2;
			}
			transform.position = Vector3.Lerp(
				transform.position, 
				targetCameraPos,
				Time.deltaTime * smooth * velocity
			);
		    transform.LookAt(player.position);
		
		}
		else
		{
			transform.position =  targetCameraPos;
			transform.rotation = new Quaternion(1f,0f,0f,1f);
		}
	}
}
