using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageFader : MonoBehaviour {

	public bool fadeIn = false;
	public bool fadeOut = false;

	public bool destroyOnFadeOutCompletion = false;

	public Image img;

	public Color transparent = new Color(0f, 0f, 0f, 0f);

	// Use this for initialization
	void Start () {
		img = gameObject.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		if ( fadeIn ){
			img.color = Color.Lerp(img.color, Color.white, Time.deltaTime);
		}
		if ( fadeOut ){
			img.color = Color.Lerp(img.color, transparent, Time.deltaTime);
		}
		if ( destroyOnFadeOutCompletion ){
			if ( img.color.a < 0.1f ){
				Destroy (gameObject);
			}
		}
	}
}
