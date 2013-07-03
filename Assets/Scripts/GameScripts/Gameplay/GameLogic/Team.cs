using UnityEngine;
using System.Collections;

public enum Team {
	Undefined = 0,
	Attackers = 1,
	Defenders = 2,
}

public static class TeamExtensions {
	
	public static Color GetColor(this Team team) {
		if (team == Team.Attackers) return Color.red;
		if (team == Team.Defenders) return Color.blue;
		return Color.yellow;
	}
	
}
