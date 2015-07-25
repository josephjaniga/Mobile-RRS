using UnityEngine;

# if UNITY_EDITOR
using UnityEditor;
# endif

using System.Collections;

public class MenuController : MonoBehaviour {
	
	public void GuidedPlay(){
		Application.LoadLevel("LevelOne");
	}

	public void OpenPlay(){
		Application.LoadLevel("OpenPlay");
	}

	public void Tutorial(){
		Application.LoadLevel("Tutorial");
	}

	public void Store(){
		Application.LoadLevel("GunStore");
	}

	public void Quit(){
		# if UNITY_EDITOR
			console.log ("quit button press");
			UnityEditor.EditorApplication.isPlaying = false;
		# endif
		Application.Quit();
	}
}
