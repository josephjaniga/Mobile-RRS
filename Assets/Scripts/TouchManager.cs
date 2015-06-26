using UnityEngine;
using System.Collections;
using System.Linq;

public class TouchManager : MonoBehaviour {

	[SerializeField]
	public float rotationSpeedScale = 0.1f;
	
	// Update is called once per frame
	void Update () {
	
		if ( Input.touches.Length > 0 ){
			foreach ( Touch touch in Input.touches ){

				if ( touch.phase == TouchPhase.Began ){
					// began event
				}

				if ( touch.phase == TouchPhase.Moved ){
					transform.Rotate(0f, 0f, touch.deltaPosition.y * rotationSpeedScale);
				}

				TouchPhase[] endPhases = {TouchPhase.Canceled, TouchPhase.Ended};
				if ( endPhases.Contains(touch.phase) ){

				}

			}
		}

	}
}
