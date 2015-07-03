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

	public Transform target;

	public StateMachine sm;

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

		if ( sm.cylinderState == CylinderStates.Open ){
			target = cylinderOpen;
		} else if ( sm.cylinderState == CylinderStates.Closed ){
			target = cylinderClosed;
		}

		if ( transform.position != target.position ){
			transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime*3f);
		}

		if ( transform.rotation != target.rotation ){
			transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime*3f);
		}

	}
}
