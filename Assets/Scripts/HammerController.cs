using UnityEngine;
using System.Collections;

public class HammerController : MonoBehaviour {

	// positions
	public Transform hammerRest;
	public Transform hammerCocked;
	
	public Transform target;
	
	public StateMachine sm;
	
	public float animationSpeed = 5f;
	
	// Use this for initialization
	void Start () {
		if ( hammerRest == null ){
			hammerRest = _.hammerPositions.transform.FindChild("HammerRest").transform;
		}
		if ( hammerCocked == null ){
			hammerCocked = _.hammerPositions.transform.FindChild("HammerCocked").transform;
		}
		target = hammerRest;
	}
	
	// Update is called once per frame
	void Update () {
		
		if ( sm.hammerState == HammerStates.Rest ){
			animationSpeed = 200f;
			target = hammerRest;
		} else if ( sm.hammerState == HammerStates.Cocked ){
			animationSpeed = 5f;
			target = hammerCocked;
		}

		if ( transform.position != target.position ){
			transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime*animationSpeed);
		}

		if ( transform.rotation != target.rotation ){
			transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime*animationSpeed);
		}
		
	}
}
