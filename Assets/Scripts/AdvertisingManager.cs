using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class AdvertisingManager : MonoBehaviour {

	void Awake() {
		if (Advertisement.isSupported) {
			Advertisement.Initialize("56858", true);
		} else {
			Debug.Log("Platform not supported");
		}
	}

//	void OnGUI() {
//		if(GUI.Button(new Rect(25, 25, 300, 175), Advertisement.IsReady() ? "Show Ad" : "Waiting...")) {
//			// Show with default zone, pause engine and print result to debug log
//			Advertisement.Show(null, new ShowOptions {
//				resultCallback = result => {
//					//Debug.Log(result.ToString());
//				}
//			});
//		}
//	}


// Update is called once per frame
//	void Update () {
//		if ( Advertisement.IsReady() ){
//			Advertisement.Show();
//		}
//	}


}
