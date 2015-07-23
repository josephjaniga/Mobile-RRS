using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class AdvertisingManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Advertisement.Initialize("56858", true);
	}
	
	// Update is called once per frame
	void Update () {
		if ( Advertisement.IsReady() ){
			Advertisement.Show();
		}
	}
}
