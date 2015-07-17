using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour {
	
	// tutorial steps
	public int currentTutorialStep = 1;

	// COMPLETED STEPS
	public bool tutorialStep1 = false;
		public bool step1_portraitComplete = false;
		public bool step1_landscapeComplete = false;
		public bool step1_landscapeLastComplete = false;
	public bool tutorialStep2 = false;
	public bool tutorialStep3 = false;
	public bool tutorialStep4 = false;
	public bool tutorialStep5 = false;


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
				if ( tutorialStep1 ) {
					currentTutorialStep++;
					showTutorialStep2();
				}
				if ( step1_portraitComplete && step1_landscapeComplete && step1_landscapeLastComplete ){
					tutorialStep1 = true;
				}
				break;
			case 2:
				if ( tutorialStep2 ) {
					currentTutorialStep++;
					_.stateMachine.cylinderSpins = 0;
					showTutorialStep3();
				}
				if ( _.stateMachine.liveBulletsInCylinder >= 8 ){
					tutorialStep2 = true;
				}
				break;
			case 3:
				if ( tutorialStep3 ) {
					currentTutorialStep++;
					_.stateMachine.hammerState = HammerStates.Rest;
					showTutorialStep4();
				}
				if ( _.stateMachine.cylinderSpins > 5 ){
					tutorialStep3 = true;
				}
				break;
			case 4:
				if ( tutorialStep4 ) {
					currentTutorialStep++;
					showTutorialStep5();
				}
				if ( _.stateMachine.hammerState == HammerStates.Cocked ){
					tutorialStep4 = true;
				}
				break;
			case 5:
				_.stateMachine.isTutorial = false;
				break;
			}
		}

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
