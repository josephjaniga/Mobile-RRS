using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TouchCount : MonoBehaviour {

	public GameObject counter;
	public GameObject phaser;
	
	// Update is called once per frame
	void Update () {
		counter.GetComponent<Text>().text = Input.touches.Length + "";

		if (Input.touches.Length > 0)
			phaser.GetComponent<Text>().text = Input.touches[0].phase + "";
		else 
			phaser.GetComponent<Text>().text = "Phase";
	}
}
