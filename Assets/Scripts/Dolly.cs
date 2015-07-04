using UnityEngine;
using System.Collections;

/**
 * Dolly for the Camera Position and Rotation
 * Effected by the position of the Cylinder
 */

public class Dolly : MonoBehaviour {

	// positions
	public Transform cylinderOpen;
	public Transform cylinderClosed;

	public float targetOrthographicSize = 1f;
	public float targetFieldOfView = 60f;

	public Transform target;

	public StateMachine sm;

	public CylinderStates lastState = CylinderStates.Open;

	// Use this for initialization
	void Start () {
		if ( cylinderOpen == null ){
			cylinderOpen = _.cameraPositions.transform.FindChild("CylinderOpen").transform;
		}
		if ( cylinderClosed == null ){
			cylinderClosed = _.cameraPositions.transform.FindChild("CylinderClosed").transform;
		}
		target = cylinderOpen;
	}
	
	// Update is called once per frame
	void Update () {
		
		if ( Input.GetKeyDown(KeyCode.Alpha4) ){
			Camera.main.fieldOfView = 100f;
		}

		if ( Input.GetKeyDown(KeyCode.Alpha5) ){
			Camera.main.fieldOfView = 1f;
		}

		// set camera targets for current state
		if ( sm.cylinderState == CylinderStates.Open ){
			target = cylinderOpen;
			Camera.main.orthographic = true;
		} else if ( sm.cylinderState == CylinderStates.Closed ){
			target = cylinderClosed;
			Camera.main.orthographic = false;
		}

		if ( lastState != sm.cylinderState ){
			Camera.main.orthographicSize = 2f;
			Camera.main.fieldOfView = 1f;
			lastState = sm.cylinderState; 
		}

		if ( transform.position != target.position ){
			transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime*3f);
		}

		if ( transform.rotation != target.rotation ){
			transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime*3f);
		}

		if ( Camera.main.fieldOfView != targetFieldOfView ){
			Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, targetFieldOfView, Time.deltaTime/10f);
		}

		if ( Camera.main.orthographicSize != targetOrthographicSize ){
			Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetOrthographicSize, Time.deltaTime/10f);
		}

	}
}
