using UnityEngine;
using System.Collections;

public class CamPos : PIYAUGBehaviourBase {
	
	public float smooth = 0.3f;
	public Transform player;
	public float cameraHeight = 6;
	public bool rotateWithPlayer = false;
	
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
		if (rotateWithPlayer)
		{
			var velocity = player.rigidbody.velocity.magnitude;
			if (velocity < 0.5f)
			{
				velocity = 0.5f;
			}
			transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x, player.position.y + cameraHeight, player.position.z),  Time.deltaTime * smooth * velocity);
		    transform.LookAt(player.position);
		
		}
		else
		{
			transform.position =  new Vector3(player.position.x, player.position.y + cameraHeight, player.position.z);
			transform.rotation = new Quaternion(1f,0f,0f,1f);
		}
	}
}
