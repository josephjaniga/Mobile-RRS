using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum HammerStates { Rest, Cocked }
public enum CylinderStates { Closed, Open }
public enum TriggerStates { Reset, Pulled }
public enum ChamberStates { Empty, LoadedLive, LoadedSpent }

public class StateMachine : MonoBehaviour {

	public RevolverController rc;

	public OrientationManager om;
	public CylinderStates cylinderState = CylinderStates.Closed;
	public HammerStates hammerState = HammerStates.Rest;
	public TriggerStates triggerState = TriggerStates.Reset;
	
	public List<ChamberStates> chambers = new List<ChamberStates>();
	public List<GameObject> liveBullets = new List<GameObject>();
	public List<GameObject> spentBullets = new List<GameObject>();

	// objectives???
	// number live bullets in cylinder
	public int liveBulletsInCylinder = 0;
	public int liveBulletsInCylinder_objective = 1;
	public int triggerPulls = 0;
	public int triggerPulls_objective = 1;

	public bool dead = false;
	public bool locked = false;

	public GameObject restartButton;
	public GameObject victoryButton;
	public GameObject objectivesPanel;

	public int currentLevel;

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
		RevolverController.LoadedALiveBullet += LiveRoundsChange;
		RevolverController.UnloadedALiveBullet += LiveRoundsChange;
	}

	void OnDisable(){
		OrientationManager.OrientationChange -= OrientationChanged;
		RevolverController.LoadedALiveBullet -= LiveRoundsChange;
		RevolverController.UnloadedALiveBullet -= LiveRoundsChange;
	}

	public void OrientationChanged(){
		DeviceOrientation DO = om.current;
		if (DO != DeviceOrientation.Unknown &&
		    DO != DeviceOrientation.FaceDown &&
		    DO != DeviceOrientation.FaceUp && 
		    !dead ){
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

		// Handle Bullet Chamber Status
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

		// if you died
		if ( dead ){
			locked = true;
			Camera.main.backgroundColor = Color.red;
			cylinderState = CylinderStates.Open;
			// show the restart button
			objectivesPanel.SetActive(false);
			restartButton.SetActive(true);
			victoryButton.SetActive(false);
		} else {
			if ( triggerPulls >= triggerPulls_objective ){
				locked = true;
				Camera.main.backgroundColor = Color.green;
				cylinderState = CylinderStates.Open;
				// show the restart button
				objectivesPanel.SetActive(false);
				restartButton.SetActive(false);
				victoryButton.SetActive(true);
			}
		}

	}

	public void LiveRoundsChange(){
		int liveCount = 0;
		foreach ( ChamberStates c in chambers ){
			if ( c == ChamberStates.LoadedLive ){
				liveCount++;
			}
		}
		liveBulletsInCylinder = liveCount;
	}

	public void restart(){
		Application.LoadLevel("Loading");
	}

	public void victory(){

		Color bgBlack = new Color(28f/255f, 28f/255f, 28f/255f, 5f/255f);

		// reset the level
		rc.EmptyAll();
		LiveRoundsChange ();

		// increment the goal
		liveBulletsInCylinder_objective++;
		triggerPulls = 0;

		// change the objective text and reset the buttons
		objectivesPanel.SetActive(true);
		objectivesPanel.transform.FindChild("Text").gameObject.GetComponent<Text>().text = "Survive 1 Trigger Pull with " +liveBulletsInCylinder_objective+ " Live Bullets";
		restartButton.SetActive(false);
		victoryButton.SetActive(false);

		// reset the camera color
		Camera.main.backgroundColor = bgBlack;
		cylinderState = CylinderStates.Open;

		// unlock the scene
		locked = false;

	}
	
}