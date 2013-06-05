using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[AddComponentMenu("Game Logic/Main Game Logic")]
public class GameLogic : MonoBehaviour {
	
	static GameLogic _Instance;
	public static GameLogic Instance {
		get {
			if (!_Instance) {
				_Instance = (GameLogic) FindObjectOfType(typeof(GameLogic));
				if (!_Instance) {
					Debug.LogError("GameLogic not found!");
				}
			}
			return _Instance;
		}
	}
	
	public ItemType flagItemType;
	public AssaultObjective firstObjective;
	public Transform attackerPrefab;
	public Transform defenderPrefab;
	
	public AssaultObjective CurrentObjective { get; private set; }
	public GameObject FlagCarrier { get; private set; }
	
	void Awake() {
		// Disable all objectives.
		var obj = firstObjective;
		while (obj) {
			obj.gameObject.SetActive(false);
			obj = obj.next;
		}
	}
	
	void Start() {
		ActivateObjective(firstObjective);
		
		if (CurrentObjective) {
			int i = Random.Range(0, CurrentObjective.SpawnPointsAttackers.Count() - 1);
			var sp = CurrentObjective.SpawnPointsAttackers.ElementAt(i);
			var pos = sp.transform.position;
			var rot = sp.transform.rotation;
			Instantiate(attackerPrefab, pos, rot);
		}
	}
	
	void SpawnFlag() {
		if (CurrentObjective) {
			var sp = CurrentObjective.FlagSpawnPoint;
			var pos = sp.transform.position;
			var rot = sp.transform.rotation;
			Instantiate(flagItemType.droppedPrefab, pos, rot);
		}
	}
	
	void ActivateObjective(AssaultObjective objective) {
		if (objective) {
			CurrentObjective = objective;
			objective.gameObject.SetActive(true);
			SpawnFlag();
			print (objective.displayText);
		} else {
			CurrentObjective = null;
		}
	}
	
	public void OnCaptured(CapturePoint cp) {
		if (CurrentObjective && CurrentObjective.CapturePoint == cp) {
			CurrentObjective.gameObject.SetActive(false);
			if (CurrentObjective.next) {
				ActivateObjective(CurrentObjective.next);
			} else {
				CurrentObjective = null;
				print ("The defenders were overrun!");
			}
		}
	}
	
}
