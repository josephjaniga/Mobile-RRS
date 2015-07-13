using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SplashScreenDelayed : MonoBehaviour {

	public float delayTime = 5f;
	public bool done = false;
	public float timer;
	public AsyncOperation asyncOp;

	public GameObject pg;

	void Start(){
		timer = delayTime;
		asyncOp = Application.LoadLevelAsync("LevelOne");
		asyncOp.allowSceneActivation = false;
		StartCoroutine(AsyncLoading());
	}

	void Update(){

		pg.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(
				pg.GetComponent<RectTransform>().sizeDelta,
				new Vector2(asyncOp.progress, 5f),
				Time.deltaTime
			);

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

	IEnumerator WebRequest(WWW request){	
		request = new WWW("http://i.imgur.com/bUNXwFD.jpg");
		yield return request;
		Renderer renderer = GetComponent<Renderer>();
		renderer.material.mainTexture = request.texture;
		if (request.isDone || request.error != null) {
			done = true;
		}
	}

	IEnumerator JSONRequest(WWW request){
		request = new WWW("http://bad.management:3333/api/google.com");
		yield return request;
		Debug.Log(request.text);
		if (request.isDone || request.error != null) {
			done = true;
		}
	}
}
