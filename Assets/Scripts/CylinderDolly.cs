using UnityEngine;
using System.Collections;



public class CylinderDolly : MonoBehaviour {

	// positions
	public Transform cylinderOpen;
	public Transform cylinderClosed;
	
	public Transform target;
	
	public StateMachine sm;
	
	// Use this for initialization
	void Start () {
		if ( cylinderOpen == null ){
			cylinderOpen = _.cylinderPositions.transform.FindChild("CylinderOpen").transform;
		}
		if ( cylinderClosed == null ){
			cylinderClosed = _.cylinderPositions.transform.FindChild("CylinderClosed").transform;
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
		
	}

}
