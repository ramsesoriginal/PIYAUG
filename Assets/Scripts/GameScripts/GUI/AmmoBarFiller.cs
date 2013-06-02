using UnityEngine;
using System.Collections;

public class AmmoBarFiller : MonoBehaviour {
	
	public float ammo;
	public float maxAmmo;
	
	private Material bar;
	private float tiling;
	
	// Use this for initialization
	void Start () {
		bar = renderer.material;
		tiling = bar.GetTextureScale("_MainTex").x;
	}
	
	// Update is called once per frame
	void Update () {
		if (ammo > maxAmmo)
			ammo = maxAmmo;
		else if (ammo < 0)
			ammo = 0;
		bar.SetTextureOffset("_MainTex", new Vector2(1 - tiling * ((maxAmmo - ammo) / maxAmmo), 1));
	}
}
