using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
	using UnityEditor;
#endif

public class ForcePortrait : MonoBehaviour {

	// Use this for initialization
	void Start() {
		force();
	}

	void Awake() {
		force();
	}

	void OnEnable() {
		force();
	}
	
	public void force(){

		#if UNITY_EDITOR
			PlayerSettings.allowedAutorotateToLandscapeLeft = false;
			PlayerSettings.allowedAutorotateToLandscapeLeft = false;
			PlayerSettings.allowedAutorotateToPortrait = true;
			PlayerSettings.allowedAutorotateToPortraitUpsideDown = false;
			PlayerSettings.defaultInterfaceOrientation = UIOrientation.Portrait;
		#endif

		Screen.autorotateToLandscapeLeft = false;
		Screen.autorotateToLandscapeRight = false;
		Screen.autorotateToPortrait = true;
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.orientation = ScreenOrientation.Portrait;
	}

}
