using UnityEngine;
using System.Collections;

public class HalthBarFiller : MonoBehaviour {
	
	public float health;
	
	private Material bar;
	private float tiling;
	
	// Use this for initialization
	void Start () {
		bar = renderer.material;
		tiling = bar.GetTextureScale("_MainTex").x;
	}
	
	// Update is called once per frame
	void Update () {
		if (health > 1)
			health = 1;
		else if (health < 0)
			health = 0;
		bar.SetTextureOffset("_MainTex", new Vector2(tiling - tiling * health, 1));
	}
}
