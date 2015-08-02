using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OpenPlayTutorialManager : MonoBehaviour {

	public int TutorialTipsEnabled;
	
	public int CurrentStep = 0;

	public GameObject step1;
	public GameObject step2;
	public GameObject step3;
	public GameObject step4;
	public GameObject step5;
	public GameObject step6;

	public List<GameObject> steps = new List<GameObject>();

	// Use this for initialization
	void Start () {
		steps.Add (step1);
		steps.Add (step2);
		steps.Add (step3);
		steps.Add (step4);
		steps.Add (step5);
		steps.Add (step6);
		TutorialTipsEnabled = PlayerPrefs.GetInt("TutorialTipsEnabled", 1);

		if ( TutorialTipsEnabled == 1 ){
			CurrentStep = 1;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		switch(CurrentStep){
		default:
		case 0:
			DisableAllSteps();
			break;
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
			EnableStep(CurrentStep-1);
			break;
		case 7:
			PlayerPrefs.SetInt("TutorialTipsEnabled", 0);
			TutorialTipsEnabled = PlayerPrefs.GetInt("TutorialTipsEnabled", 1);
			CurrentStep = 0;
			break;
		}

	}

	public void DisableAllSteps(){
		foreach( GameObject step in steps ){
			step.SetActive(false);
		}
	}

	public void EnableStep(int x){
		DisableAllSteps();
		steps[x].SetActive(true);
	}

	public void AdvanceStep(){
		CurrentStep++;
	}
}

