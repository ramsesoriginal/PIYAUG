using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour {
	
	[System.Serializable]
	public class MapListEntry {
		public string displayName;
		public string levelName;
	}
	
	public int guiWidth, guiHeight;
	public string emptyLevel;
	public string playerName;
	public int maxMessageCount;
	public List<MapListEntry> mapList;
	
	System.Action guiFunc;
	string joinHostName;
	int lastLevelPrefix;
	bool isConnectingToServer;
	
	
	List<string> messages;
	public void AddMessage(string msg) {
		messages.Add(msg);
		if (messages.Count > 5) {
			messages.RemoveAt(0);
		}
	}
	
	
	
	
	void Reset() {
		guiWidth = 500;
		guiHeight = 400;
		playerName = "Player";
		maxMessageCount = 5;
	}
	
	void Awake() {
		DontDestroyOnLoad(this);
		networkView.group = 1;
		lastLevelPrefix = 0;
		
		guiFunc = GUI_Main;
		joinHostName = "localhost";
		messages = new List<string>();
		for (int i = 0; i < maxMessageCount; i++) {
			messages.Add("");
		}
	}
	
	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (guiFunc == null) {
				guiFunc = GUI_Main;
			} else if (guiFunc == GUI_Main) {
				guiFunc = null;
			}
		}
	}
	
	void OnGUI() {
		if (guiFunc != null) guiFunc();
	}
	
	
	
	Rect GUIRect {
		get {
			var cx = Screen.width / 2;
			var cy = Screen.height / 2;
			return new Rect(cx - guiWidth/2, cy - guiHeight/2, guiWidth, guiHeight);
		}
	}
	
	void GUIStart() {
		GUILayout.BeginArea(GUIRect);
		GUILayout.BeginVertical("box");
	}
	void GUIEnd() {
		GUILayout.EndVertical();
		GUILayout.BeginVertical("box");
		foreach (var msg in messages) {
			GUILayout.Label(msg);
		}
		GUILayout.EndVertical();
		GUILayout.EndArea();
	}
	
	void GUI_Main() {
		GUIStart();
		
		if (Network.isClient || Network.isServer) {
			if (GUILayout.Button("Disconnect")) {
				DisconnectFromGame();
			}
		} else {
			if (GUILayout.Button("Host Game")) {
				guiFunc = GUI_HostGame;
			}
			if (GUILayout.Button("Join Game")) {
				guiFunc = GUI_JoinGame;
			}
			if (GUILayout.Button("Settings")) {
				guiFunc = GUI_Settings;
			}
		}
		
		GUILayout.FlexibleSpace();
		
		if (GUILayout.Button("Quit")) {
			Application.Quit();
		}
		
		GUIEnd();
	}
	
	void GUI_HostGame() {
		GUIStart();
		
		foreach (var m in mapList) {
			if (GUILayout.Button(m.displayName)) {
				guiFunc = null;
				HostGame(m);
			}
		}
		
		GUILayout.FlexibleSpace();
		
		if (GUILayout.Button("Back")) {
			guiFunc = GUI_Main;
		}
		
		GUIEnd();
	}
	
	void GUI_Hosting() {
		GUIStart();
		
		//GUILayout.Label("peertype : " + Network.peerType.ToString());
		
		GUILayout.FlexibleSpace();
		
		if (GUILayout.Button("Disconnect")) {
			guiFunc = GUI_Main;
			DisconnectFromGame();
		}
		
		GUIEnd();
	}
	
	void GUI_JoinGame() {
		GUIStart();
		
		GUILayout.Label("Host:");
		joinHostName = GUILayout.TextField(joinHostName);
		
		if (GUILayout.Button("Join")) {
			guiFunc = GUI_Joining;
			isConnectingToServer = true;
			JoinGame(joinHostName);
		}
		
		GUILayout.FlexibleSpace();
		
		if (GUILayout.Button("Back")) {
			guiFunc = GUI_Main;
		}
		
		GUIEnd();
	}
	
	void GUI_Joining() {
		GUIStart();
		
		{
			var str = "Connecting ";
			int nDots = Mathf.FloorToInt(Time.realtimeSinceStartup) % 5;
			for (int i = 0; i < nDots; i++) {
				str += ".";
			}
			GUILayout.Label(str);
		}
		
		GUILayout.FlexibleSpace();
		
		if (GUILayout.Button("Cancel")) {
			guiFunc = GUI_Main;
			DisconnectFromGame();
		}
		if (!isConnectingToServer) {
			if (!Network.isClient) {
				AddMessage("Could not connect to server");
				guiFunc = GUI_Main;
			} else {
				AddMessage("Connected to server");
				guiFunc = null;
			}
		}
		
		GUIEnd();
	}
	
	void GUI_Settings() {
		GUIStart();
		
		GUILayout.Label("Player Name:");
		playerName = GUILayout.TextField(playerName);
		
		GUILayout.FlexibleSpace();
		
		if (GUILayout.Button("Back")) {
			guiFunc = GUI_Main;
		}
		
		GUIEnd();
	}
	
	
	
	void HostGame(MapListEntry map) {
		Network.InitializeServer(32, 25000, false);
		networkView.RPC("LoadLevel", RPCMode.AllBuffered, map.levelName, lastLevelPrefix + 1);
	}
	
	void JoinGame(string host) {
		var e = Network.Connect(host, 25000);
		AddMessage ("e = " + e);
	}
	
	void DisconnectFromGame() {
		Network.Disconnect();
	}
	
	[RPC]
	IEnumerator LoadLevel(string levelName, int levelPrefix) {
		AddMessage ("LoadLevel(" + levelName + ", " + levelPrefix + ")");
		lastLevelPrefix = levelPrefix;
		
		// There is no reason to send any more data over the network on the default channel,
		// because we are about to load the level, thus all those objects will get deleted anyway
		Network.SetSendingEnabled(0, false);
		
		// We need to stop receiving because first the level must be loaded first.
		// Once the level is loaded, rpc's and other state update attached to objects in the level are allowed to fire
		Network.isMessageQueueRunning = false;
		
		// All network views loaded from a level will get a prefix into their NetworkViewID.
		// This will prevent old updates from clients leaking into a newly created scene.
		Network.SetLevelPrefix(levelPrefix);
		
		Application.LoadLevel(levelName);
		yield return null;
		yield return null;

		// Allow receiving data again
		Network.isMessageQueueRunning = true;
		
		// Now the level has been loaded and we can start sending out data to clients
		Network.SetSendingEnabled(0, true);
		
		//for (var go in FindObjectsOfType(GameObject))
		//	go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);		
	}
	
	
	
	
	// Called on the server whenever a new player has successfully connected.
	void OnPlayerConnected() {
		AddMessage("OnPlayerConnected");
	}
	
	// Called on the server whenever a Network.InitializeServer was invoked and has completed.
	void OnServerInitialized() {
		AddMessage("OnServerInitialized");
	}
	
	// Called on the client when you have successfully connected to a server.
	void OnConnectedToServer() {
		AddMessage("OnConnectedToServer" + Network.isClient);
		isConnectingToServer = false;
	}
	
	// Called on the server whenever a player is disconnected from the server.
	void OnPlayerDisconnected() {
		AddMessage("OnPlayerDisconnected");
	}
	
	// Called on client during disconnection from server, but also on the server when the connection has disconnected.
	void OnDisconnectedFromServer() {
		//AddMessage("OnDisconnectedFromServer");
		AddMessage("Disconnected from server");
		Application.LoadLevel(emptyLevel);
	}
	
	// Called on the client when a connection attempt fails for some reason.
	void OnFailedToConnect() {
		AddMessage("OnFailedToConnect" + Network.isClient);
		isConnectingToServer = false;
	}
	
	// Called on objects which have been network instantiated with Network.Instantiate.
	void OnNetworkInstantiate(NetworkMessageInfo info) {
		AddMessage("OnNetworkInstantiate: " + info);
	}
	
	// Used to customize synchronization of variables in a script watched by a network view.
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
		AddMessage("OnSerializeNetworkView: " + info);
	}
	


}
