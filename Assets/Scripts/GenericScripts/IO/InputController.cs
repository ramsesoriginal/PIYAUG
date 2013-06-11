using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class InputController : PIYAUGBehaviourBase {
	
	private static string verticalAxisName = "Vertical";
	private static string horizontalAxisName = "Horizontal";
	private static string rotationAxisName = "Rotation";
	private static string jumpButtonName = "Jump";
	private static string actionButtonName = "Action";
	private static string fireButtonName = "Fire";
	
	public float Smoothing;
	
	private static float smoothing;
	
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
		
		private float oldValue;
		public override float Value {
			get {
				return Mathf.Lerp(oldValue,Input.GetAxis(name),smoothing);
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
				return oldValue != Value;
			}
		}
		
		public void saveOldValue() {
			oldValue = Value;
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
			if (vertical == null)
				vertical = new Axis(verticalAxisName);
			return vertical;
		}
	}
	
	private static Axis horizontal;
	public static Axis Horizontal {
		get {
			if (horizontal == null)
				horizontal = new Axis(horizontalAxisName);
			return horizontal;
		}
	}
	
	private static Axis rotation;
	public static Axis Rotation {
		get {
			if (rotation == null)
				rotation = new Axis(rotationAxisName);
			return rotation;
		}
	}
	
	private static Button jump;
	public static Button Jump {
		get {
			if (jump == null)
				jump = new Button(jumpButtonName);
			return jump;
		}
	}
	
	private static Button action;
	public static Button Action {
		get {
			if (action == null)
				action = new Button(actionButtonName);
			return action;
		}
	}
		
	private static Button fire;
	public static Button Fire {
		get {
			if (fire == null)
				fire = new Button(fireButtonName);
			return fire;
		}
	}
		
	private List<Method> inputs;
	
	// Use this for initialization
	void Start () {
		inputs = new List<Method>();
		if (vertical == null)
			vertical = new Axis(verticalAxisName);
		inputs.Add(vertical);
		if (horizontal == null)
			horizontal = new Axis(horizontalAxisName);
		inputs.Add(horizontal);
		if (rotation == null)
			rotation = new Axis(rotationAxisName);
		inputs.Add(rotation);
		if (jump == null)
			jump = new Button(jumpButtonName);
		inputs.Add(jump);
		if (action == null)
			action = new Button(actionButtonName);
		inputs.Add(action);
		if (fire == null)
			fire = new Button(fireButtonName);
		inputs.Add(fire);
        Screen.lockCursor = true;
		smoothing = Smoothing;
	}
	
	// Update is called once per frame
	void Update () {
		foreach(var input in inputs)
		{
			input.fireAllCallbacks();
			if (input is Axis)
				((Axis)input).saveOldValue();
		}
	}
}
