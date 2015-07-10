using UnityEngine;

using System.Collections;

public class MenuController : MonoBehaviour {
	
	public void Play(){
		Application.LoadLevel(1);
	}

	public void Quit(){
		# if UNITY_EDITOR
			console.log ("quit button press");
			UnityEditor.EditorApplication.isPlaying = false;
		# endif
		Application.Quit();
	}
}
