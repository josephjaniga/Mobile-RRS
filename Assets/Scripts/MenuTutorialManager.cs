using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuTutorialManager : MonoBehaviour {

	public NewsManager newsManager;

	public int TutorialTipsEnabled;
	
	public int CurrentStep = 0;

	public GameObject step1;
	public GameObject step2;
	public GameObject step3;

	public List<GameObject> steps = new List<GameObject>();

	// Use this for initialization
	void Start () {
		steps.Add (step1);
		steps.Add (step2);
		steps.Add (step3);
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
			gameObject.SetActive(false);
			DisableAllSteps();
			break;
		case 1:
		case 2:
		case 3:
			gameObject.SetActive(true);
			EnableStep(CurrentStep-1);
			break;
		case 4:
			CurrentStep = 0;
			if ( PlayerPrefs.GetInt("NewsDisplayCount", 0) == 0 ){
				newsManager.ToggleNewsWindow();
			}
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

