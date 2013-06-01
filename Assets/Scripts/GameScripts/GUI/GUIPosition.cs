using UnityEngine;
using System.Collections;

public class GUIPosition : MonoBehaviour {
	
	public Transform player;
	
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
		transform.position =  new Vector3(player.position.x, transform.position.y, player.position.z);
		transform.rotation = player.rotation;
	}
}
