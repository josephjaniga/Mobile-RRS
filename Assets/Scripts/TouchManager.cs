using UnityEngine;
using System.Collections;
using System.Linq;

public class TouchManager : MonoBehaviour {

	[SerializeField]
	public float rotationSpeedScale = 1f;
	public Quaternion targetRotation;

	void Start(){
		targetRotation = Quaternion.identity;
	}

	// Update is called once per frame
	void FixedUpdate () {
	
		if (Input.touchSupported) {

			if (Input.touches.Length > 0) {
				foreach (Touch touch in Input.touches) {

					if (touch.phase == TouchPhase.Began) {
						// began event
					}

					if (touch.phase == TouchPhase.Moved) {
						//					Vector3 r = targetRotation.eulerAngles;
						//					r = new Vector3(r.x+0f, r.y+0f, r.z + touch.deltaPosition.y );
						//					targetRotation = Quaternion.Euler(r);
						//					transform.GetComponent<Rigidbody>().AddTorque(new Vector3(0f, 0f, touch.deltaPosition.y / Time.deltaTime ));
					}

					TouchPhase[] endPhases = {TouchPhase.Canceled, TouchPhase.Ended};
					if (endPhases.Contains (touch.phase)) {
						//					Vector3 r = targetRotation.eulerAngles;
						//					r = new Vector3(r.x+0f, r.y+0f, (r.z + touch.deltaPosition.y / Time.deltaTime )%360f);
						//					targetRotation = Quaternion.Euler(r);
						if (touch.phase == TouchPhase.Ended) {
							transform.GetComponent<Rigidbody> ().AddTorque (new Vector3 (0f, 0f, touch.deltaPosition.y ));
						}
					}

				}
				
				//transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
			}
		} else {
			// touch not supported
			if ( Input.GetMouseButtonDown(0) ){
				transform.GetComponent<Rigidbody> ().AddTorque (new Vector3 (0f, 0f, 1080f));
			}
		}
	}
}
