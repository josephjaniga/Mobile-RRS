using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BulletCounter : MonoBehaviour {

	public PlayerManager pm;
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<Text>().text = "Bullets: " + pm.bullet;
	}
}
