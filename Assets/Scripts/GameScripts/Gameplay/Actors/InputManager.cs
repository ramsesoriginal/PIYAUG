using UnityEngine;
using System.Collections;

public class InputManager : PIYAUGBehaviourBase {
	
	public CharacterControlScript character;
	public bool initializeLocalInput = true;
	
	public class InputData {
		public float? Accelerate {get; set;}
		public float? Strafe {get; set;}
		public float? Rotate {get; set;}
		public bool? Jump {get; set;}
		public bool? Action {get; set;}
		public bool? Fire {get; set;}
		public Vector3? Position {get; set;}
	}
	
	void Awake () {
		if (character==null) {
			var player = GameObject.FindGameObjectWithTag("Player");
			if (player != null)
				character = player.GetComponent<CharacterControlScript>();
		}
		if (initializeLocalInput)
		{
			InputController.Vertical.registerCallback(character.Accelerate);
			InputController.Horizontal.registerCallback(character.Strafe);
			InputController.Rotation.registerCallback(character.Rotate);
			InputController.Jump.registerCallback(character.Jump);
		}
	}
	
	void setRemoteInput(InputData input) {
		if (input.Accelerate!=null)
			character.Accelerate((float)input.Accelerate);
		if (input.Strafe!=null)
			character.Strafe((float)input.Strafe);
		if (input.Rotate!=null)
			character.Rotate((float)input.Rotate);
		if (input.Jump!=null)
			character.Jump((bool)input.Jump);
		if (input.Position!=null)
			character.gameObject.transform.position = (Vector3)input.Position;
	}
	
	InputData getLocalInput() {
		return new InputData {
			Accelerate = InputController.Vertical,
			Strafe = InputController.Vertical,
			Rotate = InputController.Rotation,
			Jump = InputController.Jump,
			Action = InputController.Action,
			Fire = InputController.Fire,
			Position = character.gameObject.transform.position
		};
	}
	
}
