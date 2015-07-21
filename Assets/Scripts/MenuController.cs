using UnityEngine;

using System.Collections;

public class MenuController : MonoBehaviour {
	
	public void Play(){
		Application.LoadLevel("LevelOne");
	}

	public void Open(){
		Application.LoadLevel("OpenPlay");
	}

	public void Tutorial(){
		Application.LoadLevel("Tutorial");
	}

	public void Quit(){
//		# if UNITY_EDITOR
//			console.log ("quit button press");
//			UnityEditor.EditorApplication.isPlaying = false;
//		# endif
		Application.Quit();
	}
}
