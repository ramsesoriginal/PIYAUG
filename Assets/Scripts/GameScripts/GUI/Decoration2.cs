using UnityEngine;
using System.Collections;

public class Decoration2 : PIYAUGBehaviourBase {
	
	public float dampening = 20;
	private float speed = 0;
	private float height;
	private Color color;
	private Vector3 originalscale;
	
	void Start () {
		height = transform.position.y;
		color = GetComponent<MeshRenderer>().material.color;
		originalscale = transform.localScale;
	}
	
	void Update () {
		speed = Mathf.Lerp(speed, InputController.Vertical, dampening * Time.deltaTime);
		transform.SetY(height + speed * 2.5f);
		transform.localScale = new Vector3(
			originalscale.x + (0+speed/2.5f),
			originalscale.y + (0+speed/2.5f),
			originalscale.z + (0+speed/2.5f)
		);
		GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, color.a - speed*2.5f);
	}
}
