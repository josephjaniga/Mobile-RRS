using UnityEngine;
using System.Collections;

public class SplashScreenDelayed : MonoBehaviour {

	public float delayTime = 5f;
	public bool done = false;

	public string url = "http://i.imgur.com/bUNXwFD.jpg";

	private float timer;

	void Start(){
		timer = delayTime;
		StartCoroutine ("Loading");
		DontDestroyOnLoad (gameObject);
	}

	void Update(){
		if (timer > 0) {
			timer -= Time.deltaTime;
			return;
		}

		if (done) {
			Application.LoadLevel(1);
		}
	}

	IEnumerator Loading(){

		// Do something here
		WWW www = new WWW(url);
		yield return www;
		Renderer renderer = GetComponent<Renderer>();
		renderer.material.mainTexture = www.texture;

		// Yield until something...
		//yield return null;

		// If something completed...
		if (renderer.material.mainTexture != null) {
			done = true;
		}

	}
}
