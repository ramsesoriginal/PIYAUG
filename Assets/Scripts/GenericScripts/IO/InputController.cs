using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class InputController : PIYAUGBehaviourBase {
	
	public interface Method {
		void fireAllCallbacks();
		bool Active {
			get;
		}
	}
	
	public abstract class Method<T> : Method {
		protected List<Action<T>> callbacks;
		
		public Method() {
			callbacks = new List<Action<T>>();
		}
		
		public void registerCallback(Action<T> callback){
			callbacks.Add(callback);
		}
		
		public abstract bool Active {
			get;
		}
		
		public abstract T Value {
			get;
		}
		
		public static implicit operator bool(Method<T> current) 
		{
			return current.Active;
		}
		
		public void fireAllCallbacks() {
			if (Active)
				foreach(var callback in callbacks) {
					callback(this.Value);
				}
		}
	}
	
	public class Axis : Method<float> {
		private string name; 
		
		public override float Value {
			get {
				return Input.GetAxis(name);
			}
		}
		
		public bool Positive {
			get {
				return (Value > 0.1);
			}
		}
		public bool Negative {
			get {
				return (Value < -0.1);
			}
		}
		
		public Axis(string name) : base() {
			this.name = name;
		}
		
		public static implicit operator float(Axis current) 
		{
			return current == null ? 0 : current.Value;
		}
		
		public override bool Active {
			get{
				return Positive || Negative;
			}
		}
	}
	
	public class Button : Method<bool> {
		
		private string name; 
		
		public bool Pressed {
			get {
				return Input.GetButtonDown(name);
			}
		}
		
		public bool Held {
			get {
				return Input.GetButton(name);
			}
		}
		public bool Released {
			get {
				return Input.GetButtonUp(name);
			}
		}
		
		public Button(string name) : base() {
			this.name = name;
		}
		
		public override bool Active {
			get{
				return Held;
			}
		}
		
		public override bool Value {
			get {
				return Held;
			}
		}
	}
	
	private static Axis vertical;
	public static Axis Vertical {
		get {
			return vertical;
		}
	}
	
	private static Axis horizontal;
	public static Axis Horizontal {
		get {
			return horizontal;
		}
	}
	
	private static Axis rotation;
	public static Axis Rotation {
		get {
			return rotation;
		}
	}
	
	private static Button jump;
	public static Button Jump {
		get {
			return jump;
		}
	}
	
	private static Button action;
	public static Button Action {
		get {
			return action;
		}
	}
		
	private static Button fire;
	public static Button Fire {
		get {
			return fire;
		}
	}
		
	private List<Method> inputs;
	
	// Use this for initialization
	void Start () {
		inputs = new List<Method>();
		if (vertical == null)
			vertical = new Axis("Vertical");
		inputs.Add(vertical);
		if (horizontal == null)
			horizontal = new Axis("Horizontal");
		inputs.Add(horizontal);
		if (rotation == null)
			rotation = new Axis("Rotation");
		inputs.Add(rotation);
		if (jump == null)
			jump = new Button("Jump");
		inputs.Add(jump);
		if (action == null)
			action = new Button("Action");
		inputs.Add(action);
		if (fire == null)
			fire = new Button("Fire");
		inputs.Add(fire);
	}
	
	// Update is called once per frame
	void Update () {
		foreach(var input in inputs)
		{
			input.fireAllCallbacks();
		}
	}
}
