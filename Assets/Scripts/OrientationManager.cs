using UnityEngine;
using System.Collections;

public class OrientationManager : MonoBehaviour {

	public DeviceOrientation last;
	public DeviceOrientation current;

	// create a delegate and event so other classes can subscribe to orientation change
	public delegate void ChangeAction();
	public static event ChangeAction OrientationChange;

	public bool lockOrientation = false;

	// Use this for initialization
	void Start () {
		last = DeviceOrientation.Unknown;
		current = Input.deviceOrientation;
		// Debug.Log (SystemInfo.deviceType);
	}
	
	// Update is called once per frame
	void Update () {

		last = current;

		if ( !lockOrientation ){
			current = Input.deviceOrientation;
		}

		if (SystemInfo.deviceType == DeviceType.Desktop ) {
			if ( lockOrientation ){
				if ( Input.GetKeyDown(KeyCode.Alpha1) ){
					current = DeviceOrientation.LandscapeLeft;
				} else if ( Input.GetKeyDown(KeyCode.Alpha2) ){
					current = DeviceOrientation.Portrait;
				} else if ( Input.GetKeyDown(KeyCode.Alpha3) ){
					current = DeviceOrientation.LandscapeRight;
				}
			}

			if ( Input.GetKeyDown(KeyCode.Tab) ){
				lockOrientation = !lockOrientation;
			}
		}

		if (last != current) {
			if (OrientationChange != null) {
				OrientationChange ();
			}
		}

	}
	
}
