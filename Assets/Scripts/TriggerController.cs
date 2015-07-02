using UnityEngine;
using System.Collections;

public class TriggerController : MonoBehaviour {
	
	// positions
	public Transform triggerReset;
	public Transform triggerPulled;
	
	public Transform target;
	
	public StateMachine sm;
	
	// Use this for initialization
	void Start () {
		target = triggerReset;
	}
	
	// Update is called once per frame
	void Update () {
		
		if ( sm.triggerState == TriggerStates.Reset ){
			target = triggerReset;
		} else if ( sm.triggerState == TriggerStates.Pulled ){
			target = triggerPulled;
		}
		
		if ( transform.position != target.position ){
			transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime*10f);
		}
		
		if ( transform.rotation != target.rotation ){
			transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime*10f);
		}

		// after pull complete reset
		if ( Vector3.Distance(transform.position, triggerPulled.position) < 0.05f ){
			sm.triggerState = TriggerStates.Reset;
			if ( sm.hammerState != HammerStates.Rest ){
				sm.hammerState = HammerStates.Rest;
			}
		}
		
	}
}
