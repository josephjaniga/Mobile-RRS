using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsManager : MonoBehaviour {

	public GameObject SettingsWindow;
	public Slider SoundSlider;
	public Slider MusicSlider;
	public float SoundVolume = 1.0f;
	public float MusicVolume = 1.0f;

	// Use this for initialization
	void Start () {
		SoundVolume = PlayerPrefs.GetFloat("SoundVolume", 1f);
		MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
		SoundSlider.value = SoundVolume;
		MusicSlider.value = MusicVolume;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetVolume(){
		SoundVolume = SoundSlider.value;
		MusicVolume = MusicSlider.value;
		PlayerPrefs.SetFloat("SoundVolume", SoundVolume);
		PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
	}

	public void ToggleSettingsWindow(){
		SettingsWindow.SetActive(!SettingsWindow.activeSelf);
		SoundSlider.value = SoundVolume;
		MusicSlider.value = MusicVolume;
	}
}
