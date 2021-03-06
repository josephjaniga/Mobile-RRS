﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections;

public class AdButtonController : MonoBehaviour {
	
	public PlayerManager pm;
	public Button button;
	public Text text;
	public GameObject timer;
	public Text timerText;

	public Color activeText = Color.white;
	public Color inactiveText = Color.black;

	public int seconds;

	void Awake() {
		if (Advertisement.isSupported) {
			Advertisement.Initialize("56858", true);
		} else {
			Debug.Log("Platform not supported");
		}
	}

	// Use this for initialization
	void Start () {
		text = gameObject.transform.FindChild("Text").GetComponent<Text>();
		timer = gameObject.transform.FindChild("TimerText").gameObject;
		timerText = timer.GetComponent<Text>();
		button = gameObject.GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.LogWarning( "Cooldown time: " + ((int)pm.lastPlayedAd + (int)pm.adCooldown) + " / DateTimeUnix: " + (int)pm.toUnixTime(System.DateTime.Now));
		if ( (int)pm.lastPlayedAd + (int)pm.adCooldown <= (int)pm.toUnixTime(System.DateTime.Now) ){
			button.interactable = true;
			text.color = activeText;
			timer.SetActive(false);
		} else {
			button.interactable = false;
			text.color = inactiveText;
			timer.SetActive(true);
			seconds = (int)((int)pm.adCooldown + (int)pm.lastPlayedAd - (int)pm.toUnixTime(System.DateTime.Now));
			System.TimeSpan span = new System.TimeSpan(0,0,seconds);
			timerText.text = string.Format("{0:0}:{1:00}",span.Minutes,span.Seconds);
		}
	}

	public void ShowAd(){
		Advertisement.Show(null, new ShowOptions {
			resultCallback = result => {
				Debug.Log(result.ToString());
				pm.addCoins(75);
				pm.lastPlayedAd = pm.toUnixTime(System.DateTime.Now);
				PlayerPrefs.SetInt("lastPlayedAd", pm.lastPlayedAd);
				button.interactable = false;

				// make a popup message to alert the player they just have been awarded coins here
			}
		});
	}

}
