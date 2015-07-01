using UnityEngine;
using System.Collections;
using System.Linq;

public class TouchManager : MonoBehaviour {

	public bool spinNext;

	void Start(){ spinNext = false; }

	void Update(){
		if (Input.touches.Length > 0) {
			foreach (Touch touch in Input.touches) {
				TouchPhase[] endPhases = {TouchPhase.Canceled, TouchPhase.Ended};
				if (endPhases.Contains (touch.phase)) {
					spinNext = true;
				}
			}
		}
	}

	void FixedUpdate () {
		if ( spinNext ){
			transform.GetComponent<Rigidbody> ().AddTorque (new Vector3 (0f, 0f, 1000f));
			spinNext = false;
		}
	}

}
