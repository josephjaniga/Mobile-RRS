using UnityEngine;
using System.Collections;
using System.Linq;

public class TouchManager : MonoBehaviour {

	[SerializeField]
	public float rotationSpeedScale = 1f;
	public Quaternion targetRotation;

	public float torqueValue = 0f;

	void Start(){
		targetRotation = Quaternion.identity;
	}

	void Update(){

		if (Input.touches.Length > 0) {
			foreach (Touch touch in Input.touches) {
			
				Debug.Log (touch.fingerId + " " + touch.phase);

				if (touch.phase == TouchPhase.Began) {}
				if (touch.phase == TouchPhase.Moved) {}
				
				TouchPhase[] endPhases = {TouchPhase.Canceled, TouchPhase.Ended};
				if (endPhases.Contains (touch.phase)) {
					if (touch.phase == TouchPhase.Ended) {
						torqueValue = touch.deltaPosition.y;
						Debug.Log (torqueValue);
					}
				}
			}
		}
		
//		if (SystemInfo.deviceType == DeviceType.Desktop ) {
//			// touch not supported
//			if ( Input.GetMouseButtonDown(0) ){
//				torqueValue = 1080f;
//				Debug.Log (torqueValue);
//			}
//		}

	}

	// Update is called once per frame
	void FixedUpdate () {
		if (torqueValue > 0f) {
			transform.GetComponent<Rigidbody> ().AddTorque (new Vector3 (0f, 0f, torqueValue));
			torqueValue = 0f;
		}
	}
}
