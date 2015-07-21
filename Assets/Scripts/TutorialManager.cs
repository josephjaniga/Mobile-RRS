using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialManager : MonoBehaviour {
	
	// tutorial steps
	public int currentTutorialStep = 1;

	// COMPLETED STEPS
	public bool tutorialStep1 = false;
		public bool step1_portraitComplete = false;
		public bool step1_landscapeComplete = false;
		public bool step1_landscapeLastComplete = false;
		public int step1_cylinderCycleCount = 0;
		public int step1_opensGoal = 3;
	public bool tutorialStep2 = false;
		public int step2_bulletsCount = 0;
		public int step2_bulletsGoal = 8;
	public bool tutorialStep3 = false;
		public int step3_cylinderSpinsCount = 0;
		public int step3_cylinderSpinsGoal = 6;
	public bool tutorialStep4 = false;
	public bool tutorialStep5 = false;
		public int step5_triggerPullsCount = 0;
		public int step5_triggerPullsGoal = 6;
	

	public int displayCount = 0;
	public int displayGoal = 0;
	public Text displayText;

	void OnEnable(){
		OrientationManager.OrientationChange += OrientationChanged;
	}
	
	void OnDisable(){
		OrientationManager.OrientationChange -= OrientationChanged;
	}

	// Use this for initialization
	void Start () {
		if (_.stateMachine.isTutorial) {
			_.stateMachine.orientationChanges = 0;
			showTutorialStep1();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		// if is tutorial
		if (_.stateMachine.isTutorial){
			switch(currentTutorialStep){
			default:
				break;
			case 1:

				displayCount = step1_cylinderCycleCount;
				displayGoal = step1_opensGoal;

				if ( tutorialStep1 ) {
					currentTutorialStep++;
					showTutorialStep2();
				}
				if ( step1_portraitComplete && step1_landscapeComplete && step1_landscapeLastComplete ){
					step1_cylinderCycleCount++;
					step1_portraitComplete = step1_landscapeComplete = step1_landscapeLastComplete = false;
				}
				if ( step1_cylinderCycleCount >= step1_opensGoal ){
					tutorialStep1 = true;				
				}
				break;
			case 2:

				step2_bulletsCount = _.stateMachine.liveBulletsInCylinder;
				displayCount = step2_bulletsCount;
				displayGoal = step2_bulletsGoal;

				if ( tutorialStep2 ) {
					currentTutorialStep++;
					_.stateMachine.cylinderSpins = 0;
					showTutorialStep3();
				}
				if ( step2_bulletsCount >= step2_bulletsGoal ){
					tutorialStep2 = true;
				}
				break;
			case 3:

				step3_cylinderSpinsCount = _.stateMachine.cylinderSpins;
				displayCount = step3_cylinderSpinsCount;
				displayGoal = step3_cylinderSpinsGoal;

				if ( tutorialStep3 ) {
					currentTutorialStep++;
					_.stateMachine.hammerState = HammerStates.Rest;
					showTutorialStep4();
				}
				if ( step3_cylinderSpinsCount >= step3_cylinderSpinsGoal ){
					tutorialStep3 = true;
				}
				break;
			case 4:
				displayCount = 0;
				displayGoal = 1;
				if ( tutorialStep4 ) {
					currentTutorialStep++;
					showTutorialStep5();
				}
				if ( _.stateMachine.hammerState == HammerStates.Cocked ){
					tutorialStep4 = true;
				}
				break;
			case 5:
				displayCount = 0;
				displayGoal = 1;
				_.stateMachine.isTutorial = false;
				break;
			}
		}

		displayText.text = displayCount + " / " + displayGoal;

	}

	// tutorial step 1
	// ROTATION
	public void showTutorialStep1(){
		string descriptiontext = "Rotate Your Device to Open and Close the Revolver Cylinder.  Open and Close the Cylinder a Few times for practice.";
		string buttonText = "CHECK  IT  OUT!";
		Sprite img = Resources.LoadAll<Sprite>("Art/Source/rss_tutorial")[0];
		ModalLayouts layout = ModalLayouts.TwoColumnModal;
		_.stateMachine.guiManager.customModal(
			descriptiontext,
			buttonText,
			img,
			layout
			);
	}

	// tutorial step 2
	// LOADING BULLETS
	public void showTutorialStep2(){
		string descriptiontext = "GREAT! With an open cylinder, tap chambers to load bullets, Or tap the bullet to load the next empty chamber!";
		string buttonText = "Load 8 Bullets!";
		Sprite img = null;
		ModalLayouts layout = ModalLayouts.OneColumnModal;
		_.stateMachine.guiManager.customModal(
			descriptiontext,
			buttonText,
			img,
			layout
			);
	}

	// tutorial step 3
	// SPIN THE CYLINDER
	public void showTutorialStep3(){
		string descriptiontext = "SWEET! Now that we loaded a few bullets we need to spin the barrel, close the cylinder and swipe up or down to spin the cylinder.";
		string buttonText = "Spin A Few Times!";
		Sprite img = null;
		ModalLayouts layout = ModalLayouts.OneColumnModal;
		_.stateMachine.guiManager.customModal(
			descriptiontext,
			buttonText,
			img,
			layout
			);
	}

	// tutorial step 4
	// COCK THE HAMMER
	public void showTutorialStep4(){
		string descriptiontext = "NICE! Now that we're loaded and ready, lets cock the hammer by swiping right while the cylinder is closed";
		string buttonText = "Cock the Hammer!";
		Sprite img = null;
		ModalLayouts layout = ModalLayouts.OneColumnModal;
		_.stateMachine.guiManager.customModal(
			descriptiontext,
			buttonText,
			img,
			layout
			);
	}

	// tutorial step 5
	// COCK THE HAMMER
	public void showTutorialStep5(){
		string descriptiontext = "Last but not least! To pull the trigger swipe right once more after the hammer is cocked!";
		string buttonText = "GOOD LUCK!";
		Sprite img = null;
		ModalLayouts layout = ModalLayouts.OneColumnModal;
		_.stateMachine.guiManager.customModal(
			descriptiontext,
			buttonText,
			img,
			layout
			);
	}


	public void OrientationChanged(){
		if ( currentTutorialStep == 1 ){
			if ( !_.stateMachine.locked && _.stateMachine.om.last != _.stateMachine.om.current ){
				switch(_.stateMachine.om.current){
				default:
					break;
				case DeviceOrientation.Portrait:
					step1_portraitComplete = true;
					break;
				case DeviceOrientation.LandscapeRight:
					step1_landscapeComplete = true;
					if ( step1_portraitComplete && step1_landscapeComplete ){
						step1_landscapeLastComplete = true;
					}
					break;
				}
			}
		}
	}
}
