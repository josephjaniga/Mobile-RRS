using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AllowAutorotation : MonoBehaviour {

	// Use this for initialization
	void Start() {
		force();
	}

	void Awake(){
		force();
	}

	void OnEnable(){
		force();
	}

	public void force(){
		#if UNITY_EDITOR
			PlayerSettings.allowedAutorotateToLandscapeLeft = true;
			PlayerSettings.allowedAutorotateToLandscapeLeft = true;
			PlayerSettings.allowedAutorotateToPortrait = true;
			PlayerSettings.allowedAutorotateToPortraitUpsideDown = true;
			PlayerSettings.defaultInterfaceOrientation = UIOrientation.AutoRotation;
		#endif

		Screen.autorotateToLandscapeLeft = true;
		Screen.autorotateToLandscapeRight = true;
		Screen.autorotateToPortrait = true;
		Screen.autorotateToPortraitUpsideDown = true;
		Screen.orientation = ScreenOrientation.AutoRotation;
	}
}
