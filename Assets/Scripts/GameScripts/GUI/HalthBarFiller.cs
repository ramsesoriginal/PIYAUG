using UnityEngine;
using System.Collections;

public class HalthBarFiller : PIYAUGBehaviourBase {
	
	private GameObject PlayerController;
	private Life life;
	private float health;
	
	private Material bar;
	private float tiling;
	
	// Use this for initialization
	void Start () {
		bar = renderer.material;
		tiling = bar.GetTextureScale("_MainTex").x;
		PlayerController = GameObject.FindGameObjectWithTag("PlayerController");
		if(PlayerController!=null)
			life = PlayerController.GetComponent<Life>();
		health = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (life != null)
			health = life.ComputedHealth;
		else
			health = 1;
		if (health > 1)
			health = 1;
		else if (health < 0)
			health = 0;
		bar.SetTextureOffset("_MainTex", new Vector2(tiling - tiling * health, 1));
	}
}
