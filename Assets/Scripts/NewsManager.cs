using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SimpleJSON;

public class NewsManager : MonoBehaviour {

	// Game Objects
	public GameObject NewsWindow;
	public Text newsText;
	public Image newsImage;

	// API REQUEST DATA
	string host = "http://api.firehazard.us:3334";
	string text = "";
	string targetURL = "http://firehazard.us/";
	Texture pic;

	// Use this for initialization
	IEnumerator Start () {

		// if we havent yet opened the news window and
		// tutorial tips are off
		// else well open this at the end of the menu tutorial
		if ( PlayerPrefs.GetInt("NewsDisplayCount", 0) == 0 && PlayerPrefs.GetInt("TutorialTipsEnabled", 1) == 0 ){
			ToggleNewsWindow();
		}
		
		WWW newsReq = new WWW(host + "/news");
		yield return newsReq;
		
		if ( string.IsNullOrEmpty(newsReq.error) ){
			var newsObj = JSON.Parse(newsReq.text);
			text = newsObj["items"][0]["text"];
			string imageURL = host + newsObj["items"][0]["image"];
			targetURL = newsObj["items"][0]["url"];
			
			WWW imageReq = new WWW(imageURL);
			yield return imageReq;
			if ( string.IsNullOrEmpty(newsReq.error) ){
				pic = imageReq.texture;
				Texture2D texture = pic as Texture2D;
				Sprite sprite = Sprite.Create(texture, new Rect(0,0,texture.width, texture.height), new Vector2(0.5f,0.5f), 1.0f);
				newsImage.sprite = sprite;
				newsText.text = text;
			} else {
				// default values
				newsText.text = "Check out our games @ FireHazard.us";
				targetURL = "http://firehazard.us/";
				newsImage.sprite = null;
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ToggleNewsWindow(){
		PlayerPrefs.SetInt("NewsDisplayCount", (PlayerPrefs.GetInt("NewsDisplayCount", 0)+1) );
		NewsWindow.SetActive(!NewsWindow.activeSelf);
	}

	public void LoadURL(){
		Application.OpenURL(targetURL);
	}


}
