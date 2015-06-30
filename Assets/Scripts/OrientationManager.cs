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

		if (Input.touchSupported) {
			current = Input.deviceOrientation;
			if (last != current) {
				if (OrientationChange != null) {
					OrientationChange ();
				}
			}
		} else {
			if ( Input.GetKeyDown(KeyCode.Alpha1) ){
				current = DeviceOrientation.LandscapeLeft;
			} else if ( Input.GetKeyDown(KeyCode.Alpha2) ){
				current = DeviceOrientation.Portrait;
			} else if ( Input.GetKeyDown(KeyCode.Alpha3) ){
				current = DeviceOrientation.LandscapeRight;
			}
			if (last != current) {
				if (OrientationChange != null) {
					OrientationChange ();
				}
			}
		}

	}
	
}
