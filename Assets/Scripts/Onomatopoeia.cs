using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Onomatopoeia : MonoBehaviour {

	public Transform WorldSpace;

	// text styles
	public GameObject BasicComicStyle;
	public GameObject ElectricStyle;
	public GameObject RoboticStyle;
	public GameObject SlayerStyle;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void createTextFX(string text, TextStyles ts = TextStyles.BasicComic, TextMoods tm = TextMoods.Positive){

		// POSITION
		GameObject prefab;

		switch (ts){
		default:
		case TextStyles.BasicComic:
			prefab = BasicComicStyle;
			break;
		}

		GameObject temp = GameObject.Instantiate(prefab, Vector3.zero,Quaternion.identity) as GameObject;
		temp.GetComponent<Text>().text = text;
		temp.transform.SetParent(WorldSpace);
		RectTransform rt = temp.GetComponent<RectTransform>();

		rt.offsetMin = new Vector2(0f, 0f);
		rt.offsetMax = new Vector2(0f, 0f);
		rt.anchoredPosition3D = new Vector3(Random.Range (-0.3f,0.3f), Random.Range (-0.3f,0.3f), 1f);

		temp.GetComponent<RectTransform>().localRotation = Quaternion.Euler(Vector3.zero);

		// COLOR
		Color32 top;
		Color32 bottom;
		switch (tm){
		default:
		case TextMoods.Positive:
			float topRandom = Random.Range(0f,1f);
			if (topRandom > .9f) {
				top = Color.blue;
				bottom = Color.green;
			} else if (topRandom > 0.6f){
				top = Color.green;
				bottom = Color.yellow;
			} else if (topRandom > 0.3f) {
				top = new Color(.5f, 0f, .7f); // purple
				bottom = new Color(1f, 0f, 1f); // pink
			} else {
				top = new Color(0f, .7f, .2f);
				bottom = Color.white;
			}
			break;
		case TextMoods.Negative:
			top = Color.red;
			bottom = Color.yellow;
			break;
		}

		temp.GetComponent<Gradient>().topColor = top;
		temp.GetComponent<Gradient>().bottomColor = bottom;

		// ANIMATION
		MoveTowardCamera mtc = temp.GetComponent<MoveTowardCamera>();
		mtc.target = new Vector3(0f,0f,0f);

	}
}

public enum TextStyles{
	BasicComic = 0,
	ElectricStyle,
	RoboticStyle,
	SlayerStyle
}

public enum TextMoods{
	Positive = 0,
	Negative
}