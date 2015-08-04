using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SplashScreenDelayed : MonoBehaviour {

	public float delayTime = 5f;
	public bool done = false;
	public float timer;
	public AsyncOperation asyncOp;

	public ImageFader imgFader;

	void Start(){
		timer = delayTime;
		asyncOp = Application.LoadLevelAsync("Menu");
		asyncOp.allowSceneActivation = false;
		StartCoroutine(AsyncLoading());
	}

	void Update(){
		if (timer < 2f && imgFader != null && imgFader.fadeIn){
			imgFader.fadeIn = false;
			imgFader.fadeOut = true;
		}
		if (timer > 0) {
			timer -= Time.deltaTime;
			return;
		}
		if (done){
			asyncOp.allowSceneActivation = true;
		}
	}

	IEnumerator AsyncLoading(){
		while (asyncOp.progress < 0.9f){
			yield return null;
		}
		done = true;
	}
	
}
