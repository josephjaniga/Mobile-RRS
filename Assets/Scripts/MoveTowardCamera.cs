using UnityEngine;
using System.Collections;

public class MoveTowardCamera : MonoBehaviour {

	public Vector3 target;
	public RectTransform rt;
	public CanvasRenderer cr;

	public float destinationOffset = 0.75f;
	public bool destination = false;

	// Use this for initialization
	void Start () {
		rt = gameObject.GetComponent<RectTransform>();
		cr = gameObject.GetComponent<CanvasRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if ( rt.anchoredPosition3D.z <= target.z + destinationOffset ){
			destination = true;
		}

		if ( destination ){
			cr.SetAlpha(cr.GetAlpha()*0.9f);
		}

		if ( cr.GetAlpha() <= 0f ){
			Destroy (gameObject);
		}

		rt.anchoredPosition3D = Vector3.Lerp(rt.anchoredPosition3D, target, Time.deltaTime*0.25f);
	}

}
