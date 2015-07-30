using UnityEngine;
using System.Collections;

public class NewsManager : MonoBehaviour {

	public GameObject NewsWindow;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ToggleNewsWindow(){
		NewsWindow.SetActive(!NewsWindow.activeSelf);
	}
}
