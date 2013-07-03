using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[AddComponentMenu("Game Logic/Assault Objective")]
public class AssaultObjective : MonoBehaviour {
	
	public string displayText;
	public AssaultObjective next;
	
	public Transform overrideSpawnPointsParent;
	
	public FlagSpawnPoint FlagSpawnPoint { get; private set; }
	public CapturePoint CapturePoint { get; private set; }
	public IEnumerable<SpawnPoint> SpawnPointsAttackers { get; private set; }
	public IEnumerable<SpawnPoint> SpawnPointsDefenders { get; private set; }
	
	void Awake() {
		FlagSpawnPoint = GetComponentInChildren<FlagSpawnPoint>();
		
		CapturePoint = GetComponentInChildren<CapturePoint>();
		
		var tf = overrideSpawnPointsParent;
		if (!tf) tf = transform;
		var spawnPoints = tf.GetComponentsInChildren<SpawnPoint>();
		SpawnPointsAttackers = spawnPoints.Where(q => q.team == Team.Attackers);
		SpawnPointsDefenders = spawnPoints.Where(q => q.team == Team.Defenders);
	}
	
}
