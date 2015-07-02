using UnityEngine;
using System.Collections;

public enum HammerStates { Rest, Cocked }
public enum CylinderStates { Closed, Open }
public enum TriggerStates { Reset, Pulled }

public class StateMachine : MonoBehaviour {

	public OrientationManager om;
	public CylinderStates cylinderState = CylinderStates.Open;
	public HammerStates hammerState = HammerStates.Rest;
	public TriggerStates triggerState = TriggerStates.Reset;

	void Start(){
		om = gameObject.GetComponent<OrientationManager>();
	}

	void OnEnable(){
		OrientationManager.OrientationChange += OrientationChanged;
	}

	void OnDisable(){
		OrientationManager.OrientationChange -= OrientationChanged;
	}

	public void OrientationChanged(){
		DeviceOrientation DO = om.current;
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

	void Update(){
		// Hammer
		if ( Input.GetKeyDown(KeyCode.E) ){
			if ( hammerState == HammerStates.Rest ){
				hammerState = HammerStates.Cocked;
			}
//			else {
//				hammerState = HammerStates.Rest;
//			}
		}

		// Trigger
		if ( Input.GetKeyDown(KeyCode.F) && hammerState == HammerStates.Cocked ){
			if ( triggerState == TriggerStates.Reset ){
				triggerState = TriggerStates.Pulled;
			} else {
				triggerState = TriggerStates.Reset;
			}
		}
	}

}