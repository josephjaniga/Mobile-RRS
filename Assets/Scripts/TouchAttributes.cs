using UnityEngine;
using System.Collections;

public class TouchAttributes : MonoBehaviour {

	public Vector2 _start;
	public Vector2 _end;

	public Vector2 delta = Vector2.zero;
	public float deltaX = 0f;
	public float deltaY = 0f;

	public Vector2 start {
		get {
			return _start;
		}
		set {
			_start = value;
		}
	}

	public Vector2 end {
		get {
			return _end;
		}
		set {
			_end = value;
			deltaX = _end.x - _start.x;
			deltaY = _end.y - _start.y;
			delta = _end - _start;
		}
	}

}
