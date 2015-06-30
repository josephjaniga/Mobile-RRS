using UnityEngine;
using System.Collections;

public enum CylinderStates {
	Closed,
	Open
}

public class StateMachine : MonoBehaviour {

	public OrientationManager om;

	public CylinderStates cylinderState = CylinderStates.Open;

	void Start(){
		om = gameObject.GetComponent<OrientationManager);
	}

	void OnEnable(){
		OrientationManager.OrientationChange += OrientationChanged;
	}

	void OnDisable(){
		OrientationManager.OrientationChange -= OrientationChanged;
	}

	public void OrientationChanged(){
		DeviceOrientation DO = Input.deviceOrientation;
		if (DO != DeviceOrientation.Unknown &&
		    DO != DeviceOrientation.FaceDown &&
		    DO != DeviceOrientation.FaceUp){
			if ( DO.ToString().Contains("Portrait") ){
				cylinderState = CylinderStates.Open;
			} else {
				cylinderState = CylinderStates.Closed;			
			}
		}
	}

}