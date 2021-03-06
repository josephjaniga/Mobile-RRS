﻿using UnityEngine;

public class ObjectManager : MonoBehaviour {

	public GameObject debug;
	public GameObject positions;
	public GameObject cameraPositions;
	public GameObject triggerPositions;
	public GameObject cylinderPositions;
	public GameObject hammerPositions;
	public GameObject revolver;
	public GameObject stationaryParts;

	public TouchManager touchManager;
	public StateMachine stateMachine;
	
	void Start(){
		debug = _.debug;
		positions = _.positions;
		cameraPositions = _.cameraPositions;
		triggerPositions = _.triggerPositions;
		cylinderPositions = _.cylinderPositions;
		hammerPositions = _.hammerPositions;
		revolver = _.revolver;
		stationaryParts = _.stationaryParts;
		touchManager = _.touchManager;
		stateMachine = _.stateMachine;
	}
}

public static class _ {

	// TODO: This method checks for an object, creates it if its not available
	public static GameObject verify(string goName, ref GameObject _singleton, Vector3 pos, string PrefabsPath = "Prefabs/Setup/", string parent = "")
	{
		GameObject instance = GameObject.Find(goName);
		if ( _singleton == null ) {
			if ( instance == null ){
				instance = GameObject.Instantiate(Resources.Load(PrefabsPath + goName), pos, Quaternion.identity) as GameObject;
				instance.name = goName;
				if ( parent != "" ){
					GameObject tempParent = GameObject.Find (parent);
					if (tempParent != null){
						instance.transform.SetParent(tempParent.transform);
					}
				}
				_singleton = instance;
			}
		}
		return _singleton;
	}

	/**
	 * Debug - object contains the touch event game objects and data
	 */
	private static GameObject _debug;
	public static GameObject debug {
		get { return verify("Debug", ref _debug, Vector3.zero, "Prefabs/Setup/", "_"); }
		set { _debug = value; }
	}

	/**
	 * Canvas - Event System, TouchCount Debugging, TouchPhase Debugging
	 */
//	private static GameObject _canvas;
//	public static GameObject canvas {
//		get { return verify("Canvas", ref _debug, "Prefabs/Setup/", "_", Vector3.zero); }
//		set { _canvas = value; }
//	}

	/**
	 * Positions
	 */
	private static GameObject _positions;
	public static GameObject positions {
		get { return verify("Positions", ref _positions, Vector3.zero, "Prefabs/Setup/", ""); }
		set { _positions = value; }
	}

	private static GameObject _cameraPositions;
	public static GameObject cameraPositions {
		get { return verify("CameraPositions", ref _cameraPositions, Vector3.zero, "Prefabs/Setup/", _.positions.name); }
		set { _cameraPositions = value; }
	}

	private static GameObject _triggerPositions;
	public static GameObject triggerPositions {
		get { return verify("TriggerPositions", ref _triggerPositions, Vector3.zero, "Prefabs/Setup/", _.positions.name); }
		set { _cameraPositions = value; }
	}

	private static GameObject _cylinderPositions;
	public static GameObject cylinderPositions {
		get { return verify("CylinderPositions", ref _cylinderPositions, Vector3.zero, "Prefabs/Setup/", _.positions.name); }
		set { _cameraPositions = value; }
	}

	private static GameObject _hammerPositions;
	public static GameObject hammerPositions {
		get { return verify("HammerPositions", ref _hammerPositions, Vector3.zero, "Prefabs/Setup/", _.positions.name); }
		set { _hammerPositions = value; }
	}

	/**
	 * The Revolver and its peices
	 */
	private static GameObject _revolver;
	public static GameObject revolver {
		get { return verify("Revolver", ref _revolver, Vector3.zero, "Prefabs/Setup/", ""); }
		set { _revolver = value; }
	}

	private static GameObject _stationaryParts;
	public static GameObject stationaryParts {
		get { return verify("StationaryParts", ref _stationaryParts, Vector3.zero, "Prefabs/Setup/", "Revolver"); }
		set { _stationaryParts = value; }
	}

	private static TouchManager _touchManager;
	public static TouchManager touchManager {
		get {
			if ( _touchManager == null ){
				_touchManager = GameObject.Find("_").GetComponent<TouchManager>();
			}
			return _touchManager;
		}
		set { _touchManager = value; }
	}

	private static StateMachine _stateMachine;
	public static StateMachine stateMachine {
		get {
			if ( _stateMachine == null ){
				_stateMachine = GameObject.Find("_").GetComponent<StateMachine>();
			}
			return _stateMachine;
		}
		set { _stateMachine = value; }
	}

	public static void debugpoint(Vector3 pos){
		GameObject debug = _.debug;
		GameObject temp = GameObject.Instantiate(
			Resources.Load ("Prefabs/GUI/DebugPoint") as GameObject,
			pos,
			Quaternion.identity
			) as GameObject;
		temp.name = "Debug";
		temp.transform.SetParent(debug.transform);
	}

}
