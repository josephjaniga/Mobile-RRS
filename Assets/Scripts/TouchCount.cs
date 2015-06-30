using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TouchCount : MonoBehaviour {

	public GameObject counter;
	
	// Update is called once per frame
	void Update () {
		counter.GetComponent<Text>().text = Input.touches.Length + "";
	}
}
