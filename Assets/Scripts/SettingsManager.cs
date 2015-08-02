using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsManager : MonoBehaviour {

	public GameObject SettingsWindow;

	public Slider SoundSlider;
	public float SoundVolume = 1.0f;

	public Slider MusicSlider;
	public float MusicVolume = 1.0f;

	public Toggle TutorialToggle;
	public int TutorialTipsEnabled = 1;  // integer bool 0 - Off   1 - On

	// Use this for initialization
	void Start () {

		SoundVolume = PlayerPrefs.GetFloat("SoundVolume", 1f);
		MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
		TutorialTipsEnabled = PlayerPrefs.GetInt("TutorialTipsEnabled", 1);

		SoundSlider.value = SoundVolume;
		MusicSlider.value = MusicVolume;
		if ( TutorialTipsEnabled == 1 ){
			TutorialToggle.isOn = true;
		} else {
			TutorialToggle.isOn = false;
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateSettings(){
		SoundVolume = SoundSlider.value;
		MusicVolume = MusicSlider.value;
		if ( TutorialToggle.isOn ){
			TutorialTipsEnabled = 1;
		} else {
			TutorialTipsEnabled = 0;
		}

		PlayerPrefs.SetFloat("SoundVolume", SoundVolume);
		PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
		PlayerPrefs.SetInt("TutorialTipsEnabled", TutorialTipsEnabled);
	}

	public void ToggleSettingsWindow(){
		SettingsWindow.SetActive(!SettingsWindow.activeSelf);
		SoundSlider.value = SoundVolume;
		MusicSlider.value = MusicVolume;
	}
}
