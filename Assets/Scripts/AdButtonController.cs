using UnityEngine;
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
		if ( pm.lastPlayedAd + pm.adCooldown <= Time.time ){
			button.interactable = true;
			text.color = activeText;
			timer.SetActive(false);
		} else {
			button.interactable = false;
			text.color = inactiveText;
			timer.SetActive(true);
			timerText.text = (int)(pm.adCooldown + pm.lastPlayedAd - Time.time) + "s";
		}
	}

	public void ShowAd(){
		Advertisement.Show(null, new ShowOptions {
			resultCallback = result => {
				Debug.Log(result.ToString());
				pm.addCoins(10);
				pm.lastPlayedAd = Time.time;
				button.interactable = false;
			}
		});
	}

}
