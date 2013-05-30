using UnityEngine;
using System.Collections;

public class CamPos : PIYAUGBehaviourBase {
	
	private Transform player;
	public float cameraHeight = 6;
	public bool rotateWithPlayer = false;
	
	// Use this for initialization
	void Start () {
		var player = GameObject.FindWithTag("Player");
		if (player != null)
			this.player = player.transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(player.position.x, player.position.y + cameraHeight, player.position.z);
		if (rotateWithPlayer)
		{
			transform.rotation = new Quaternion(1f,player.rotation.y,0f,1f); 
		}
		else
		{
			transform.rotation = new Quaternion(1f,0f,0f,1f);
		}
	}
}
