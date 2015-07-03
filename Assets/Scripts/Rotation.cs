using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

	public float rotationSpeed = 75f;

	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0f, rotationSpeed * Time.deltaTime, 0f));
	}
}
