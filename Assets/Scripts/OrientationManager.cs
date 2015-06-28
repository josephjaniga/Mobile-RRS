using UnityEngine;
using System.Collections;

public class OrientationManager : MonoBehaviour {

	public DeviceOrientation last;
	public DeviceOrientation current;

	// create a delegate and event so other classes can subscribe to orientation change
	public delegate void ChangeAction();
	public static event ChangeAction OrientationChange;

	// Use this for initialization
	void Start () {
		last = DeviceOrientation.Unknown;
		current = Input.deviceOrientation;
	}
	
	// Update is called once per frame
	void Update () {
		last = current;
		current = Input.deviceOrientation;
		if (last != current){
			if (OrientationChange != null){
				OrientationChange();
			}
		}
	}
	
}
