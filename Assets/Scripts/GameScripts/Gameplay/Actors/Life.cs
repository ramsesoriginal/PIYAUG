using UnityEngine;
using System.Collections;

public class Life : PIYAUGBehaviourBase {
	
	public bool TakeDebugDamage = false;
	
	public int MaxHealth = 100;
	public int StartHealth = 80;
	
	
	private int maxHealth;
	private int health;
	public int Health {
		get {
			return health;
		}
	}
	public float ComputedHealth{
		get {
			return Health/MaxHealth;
		}
	}
	
	// Use this for initialization
	void Start () {
		health = StartHealth;
		maxHealth = MaxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (TakeDebugDamage) {
			health--;
			TakeDebugDamage = false;
		}
	}
}
