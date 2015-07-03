using UnityEngine;
using System.Collections;
using System.Timers;

public class TouchPainter : MonoBehaviour {

	public GameObject p;
	public Transform debug;

	// Use this for initialization
	void Start () {
		debug = GameObject.Find("Debug").transform;
	}
	
	// Update is called once per frame
	void Update () {
	
		if ( Input.touches.Length > 0 ){
			foreach ( Touch touch in Input.touches ){
	
				// create a touch debug
				if ( touch.phase == TouchPhase.Began ){
					GameObject temp = Instantiate(
							p,
							Camera.main.ScreenToWorldPoint(
								new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane+1f)
							),
							Quaternion.identity
						) as GameObject;
					temp.name = "touch"+touch.fingerId;
					temp.transform.SetParent(debug);
					temp.GetComponent<TouchAttributes>().start = touch.position;
				}

				// position it
				if ( touch.phase == TouchPhase.Moved /* || touch.phase == TouchPhase.Stationary */ ){
					Transform t = debug.Find("touch"+touch.fingerId);
					if ( t != null ){
						Vector3 modifiedScreenPoint = new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane+1f);
						t.gameObject.transform.position = Camera.main.ScreenToWorldPoint(modifiedScreenPoint);
					}
				}

				// destroy it
				if ( touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled ){
					Transform t = debug.Find("touch"+touch.fingerId);
					t.GetComponent<TouchAttributes>().end = touch.position;
					if ( t != null ){
						StartCoroutine(WaitAndDestroy(0.25f, t.gameObject));
					}
				}

			}
		}

	}

	IEnumerator WaitAndDestroy(float waitTime, GameObject obj) {
		TouchAttributes ta = obj.GetComponent<TouchAttributes>();
		//obj.name = "temp_"+obj.name;
		//Debug.Log ("Total "+obj.name+" Delta: " + ta.deltaX + ", " + ta.deltaY);
		yield return new WaitForSeconds(waitTime);
		Destroy(obj);
	}


}
