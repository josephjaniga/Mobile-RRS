using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum HammerStates { Rest, Cocked }
public enum CylinderStates { Closed, Open }
public enum TriggerStates { Reset, Pulled }
public enum ChamberStates { Empty, LoadedLive, LoadedSpent }

public class StateMachine : MonoBehaviour {

	public OrientationManager om;
	public CylinderStates cylinderState = CylinderStates.Open;
	public HammerStates hammerState = HammerStates.Rest;
	public TriggerStates triggerState = TriggerStates.Reset;
	
	public List<ChamberStates> chambers = new List<ChamberStates>();
	public List<GameObject> liveBullets = new List<GameObject>();
	public List<GameObject> spentBullets = new List<GameObject>();

	void Start(){
		// register the chambers and bullets
		for ( int i=0; i < 8; i++ ){
			chambers.Add(new ChamberStates());
			Transform chamber = GameObject.Find("Chamber"+i).transform;
			GameObject tempLive = chamber.FindChild("LiveBullet").gameObject;
			liveBullets.Add(tempLive);
			GameObject tempSpent = chamber.FindChild("SpentBullet").gameObject;
			spentBullets.Add(tempSpent);
		}

		om = gameObject.GetComponent<OrientationManager>();
	}

	void OnEnable(){
		OrientationManager.OrientationChange += OrientationChanged;
	}

	void OnDisable(){
		OrientationManager.OrientationChange -= OrientationChanged;
	}

	public void OrientationChanged(){
		DeviceOrientation DO = om.current;
		if (DO != DeviceOrientation.Unknown &&
		    DO != DeviceOrientation.FaceDown &&
		    DO != DeviceOrientation.FaceUp){
			if ( DO.ToString().Contains("Portrait") ){
				cylinderState = CylinderStates.Open;
			} else {
				cylinderState = CylinderStates.Closed;			
			}
		}
	}

	void Update(){
		// Hammer
		if ( Input.GetKeyDown(KeyCode.E) ){
			if ( hammerState == HammerStates.Rest ){
				hammerState = HammerStates.Cocked;
			}
//			else {
//				hammerState = HammerStates.Rest;
//			}
		}

		// Trigger
		if ( Input.GetKeyDown(KeyCode.F) && hammerState == HammerStates.Cocked ){
			if ( triggerState == TriggerStates.Reset ){
				triggerState = TriggerStates.Pulled;
			} else {
				triggerState = TriggerStates.Reset;
			}
		}

		for( int i=0; i<chambers.Count; i++ ){
			if ( chambers[i] == ChamberStates.Empty ){
				liveBullets[i].SetActive(false);
				spentBullets[i].SetActive(false);
			}
			if ( chambers[i] == ChamberStates.LoadedLive ){
				liveBullets[i].SetActive(true);
				spentBullets[i].SetActive(false);
			}
			if ( chambers[i] == ChamberStates.LoadedSpent ){
				liveBullets[i].SetActive(false);
				spentBullets[i].SetActive(true);
			}
		}
	}

}