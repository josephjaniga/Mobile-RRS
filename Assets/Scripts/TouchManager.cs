using UnityEngine;
using System.Collections;
using System.Linq;

public class TouchManager : MonoBehaviour {

	public bool spinCounterClockwise;
	public bool spinClockwise;

	public StateMachine sm;

	public Transform debug;

	void Start(){
		debug = GameObject.Find ("Debug").transform;
		spinCounterClockwise = false;
		spinClockwise = false;
	}

	void Update(){
		if (Input.touches.Length > 0) {
			foreach (Touch touch in Input.touches) {
				TouchPhase[] endPhases = {TouchPhase.Canceled, TouchPhase.Ended};
				if (endPhases.Contains (touch.phase)) {

					Transform t = debug.Find("touch"+touch.fingerId);
					TouchAttributes ta = t.GetComponent<TouchAttributes>();
					ta.end = touch.position;

					// y action
					if ( Mathf.Abs(ta.delta.y) > Mathf.Abs(ta.delta.x) ){ 
						// swipe up
						if ( ta.delta.y < 0 ){
							spinCounterClockwise = true;
							spinClockwise = false;
						// swipe down
						} else {
							spinCounterClockwise = false;
							spinClockwise = true;
						}
					// x action
					} else {
						// swipe right at least 1F
						if ( ta.deltaX > 1f ){
							if ( sm.hammerState == HammerStates.Rest ){
								sm.hammerState = HammerStates.Cocked;
							} else if ( sm.hammerState == HammerStates.Cocked ){
								if ( sm.triggerState == TriggerStates.Reset ){
									sm.triggerState = TriggerStates.Pulled;
								} else {
									sm.triggerState = TriggerStates.Reset;
								}
							}
						}
					}
				}
			}
		}
	}

	void FixedUpdate () {
		if ( spinCounterClockwise ){
			transform.GetComponent<Rigidbody> ().AddTorque (new Vector3 (0f, 0f, 1000f));
			spinCounterClockwise = false;
			spinClockwise = false;
		} else if ( spinClockwise ) {
			transform.GetComponent<Rigidbody> ().AddTorque (new Vector3 (0f, 0f, -1000f));
			spinCounterClockwise = false;
			spinClockwise = false;
		}
	}

}