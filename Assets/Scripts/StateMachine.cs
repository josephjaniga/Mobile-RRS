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
	public int orientationChanges = 0;
	public int orientationChanges_objective = 0;
	public int cylinderSpins = 0;
	public int cylinderSpins_objective = 0;

	public bool dead = false;
	public bool locked = false;
	public bool isTutorial = false;

	public GUIManager guiManager;

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

		if ( !isTutorial ){
			guiManager.displayLevelModal();
		} else {

			// tutorial step 0
			// ROTATION
			string descriptiontext = "Rotate Your Device Open and Close the Revolver Cylinder";
			string buttonText = "CHECK  IT  OUT!";
			Sprite img = Resources.LoadAll<Sprite>("Art/Source/rss_tutorial")[0];;
			ModalLayouts layout = ModalLayouts.TwoColumnModal;
			guiManager.customModal(
					descriptiontext,
					buttonText,
					img,
					layout
				);
		}

		hammerState = HammerStates.Rest;
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
		CylinderStates old = cylinderState;
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
		// on cylinder State change spin the barrel
		if ( cylinderState != old ){
			if (Random.value > 0.5f){
				_.touchManager.spinCounterClockwise = true;
				_.touchManager.spinClockwise = false;
			} else {
				_.touchManager.spinCounterClockwise = false;
				_.touchManager.spinClockwise = true;
			}
			// reset the hammer on cylinder change
			hammerState = HammerStates.Rest;
		}
	}
	
	void Update(){
		// Hammer
		if ( Input.GetKeyDown(KeyCode.E) ){
			if ( hammerState == HammerStates.Rest ){
				hammerState = HammerStates.Cocked;
			}
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
			guiManager.objectivesPanel.SetActive(false);
			guiManager.restartButton.SetActive(true);
			guiManager.victoryButton.SetActive(false);
		} else {
			if ( triggerPulls >= triggerPulls_objective ){
				locked = true;
				Camera.main.backgroundColor = Color.green;
				cylinderState = CylinderStates.Open;
				// show the restart button
				guiManager.objectivesPanel.SetActive(false);
				guiManager.restartButton.SetActive(false);
				guiManager.victoryButton.SetActive(true);
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

	/**
	 * LEVEL MANAGER?
	 */
	public void begin(){
		guiManager.deactivateAllModals();
	}

	public void restart(){
		Application.LoadLevel("Loading");
	}

	public void loadLevelOne(){
		Application.LoadLevel("LevelOne");
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
		guiManager.restartButton.SetActive(false);
		guiManager.victoryButton.SetActive(false);

		// reset the camera color
		Camera.main.backgroundColor = bgBlack;
		//cylinderState = CylinderStates.Open;

		// unlock the scene
		locked = false;

		// display the new scene modal
		guiManager.displayLevelModal();
	}
	
}