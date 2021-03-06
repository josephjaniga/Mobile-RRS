﻿using UnityEngine;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;

public class TouchManager : MonoBehaviour {

	public bool spinCounterClockwise;
	public bool spinClockwise;
	public bool touchSpin = false;

	public StateMachine sm;
	public Onomatopoeia o;
	public AudioManager audioManager;

	public Transform debug;
	public Transform cylinder;

	void Start(){
		debug = _.debug.transform;
		cylinder = GameObject.Find ("Cylinder").transform;
		spinCounterClockwise = false;
		spinClockwise = false;
	}

	void Update(){
		if (Input.touches.Length > 0 && !sm.locked) {
			foreach (Touch touch in Input.touches) {

				if ( touch.phase == TouchPhase.Began ){
					TouchBeganAction(touch);
				}

				TouchPhase[] endPhases = {TouchPhase.Canceled, TouchPhase.Ended};
				if (endPhases.Contains (touch.phase)) {

					Transform t = debug.Find("touch"+touch.fingerId);
					TouchAttributes ta = t.GetComponent<TouchAttributes>();
					ta.end = touch.position;

					// y action
					if ( Mathf.Abs(ta.delta.y) > Mathf.Abs(ta.delta.x) ){ 

						/**
						 * TODO: this should fire a touch y event and isolate the logic
						 */
						if ( Mathf.Abs(ta.deltaY) > 10f ){
							if ( sm.hammerState != HammerStates.Cocked ){
								touchSpin = true;
								if ( ta.delta.y < 0 ){ // swipe up
									spinCounterClockwise = true;
									spinClockwise = false;
								} else { // swipe down
									spinCounterClockwise = false;
									spinClockwise = true;
								}
							}
						}

					// x action
					} else {

						/**
						 * TODO: this should call a touch x event and isolate business logic
						 */

						// swipe right at least 10f
						if ( ta.deltaX > 10f && sm.cylinderState == CylinderStates.Closed ){
							if ( sm.hammerState == HammerStates.Rest ){
								// cock the hammer
								sm.rc.cockHammer();
							} else if ( sm.hammerState == HammerStates.Cocked ){
								// trigger pull
								sm.rc.triggerPull();
							}
						}
					}
				}
			}
		}
	}

	void FixedUpdate () {

		Rigidbody rb = cylinder.GetComponent<Rigidbody> ();

		// Cylinder Rotation!
		if ( sm.cylinderState != CylinderStates.Open ){
			if ( spinCounterClockwise ){
				audioManager.PlayClip(audioManager.cylinder_spin_one);
				o.createTextFX("WHZZZTK!", TextStyles.BasicComic, TextMoods.Positive);
				rb.AddTorque (new Vector3 (0f, 0f, 1000f+Random.Range (300f, 777f)));
				if ( touchSpin ){
					_.stateMachine.cylinderSpins++;
					touchSpin = false;
				}
			} else if ( spinClockwise ) {
				o.createTextFX("WURZZCT!", TextStyles.BasicComic, TextMoods.Positive);
				audioManager.PlayClip(audioManager.cylinder_spin_two);
				rb.AddTorque (new Vector3 (0f, 0f, -1000f+Random.Range (300f, 777f)));
				if ( touchSpin ){
					_.stateMachine.cylinderSpins++;
					touchSpin = false;
				}
			}
		}
		spinCounterClockwise = false;
		spinClockwise = false;

		// Cylinder Rotation Snapping
		// 8 barrel
		float[] steps = new float[] {0f, 45f, 90f, 135f, 180f, 225f, 270f, 315f, 360f};
		if ( Mathf.Abs(rb.angularVelocity.z) > 0 ){
			if ( Mathf.Abs(rb.angularVelocity.z) < 1f ){
				// jump to nearest 45*
				float closestDistance = 9999f;
				foreach ( float step in steps ){
					float z = rb.transform.rotation.eulerAngles.z;
					if ( Mathf.Abs(step - z) < closestDistance ){
						closestDistance = Mathf.Abs(step - z);
					}
				}
				rb.constraints = RigidbodyConstraints.FreezeAll;
				rb.transform.Rotate(new Vector3(0f, 0f, rb.transform.rotation.z+closestDistance));
				rb.constraints &= ~RigidbodyConstraints.FreezeRotationZ;
			}
		} 
	}


	public void TouchBeganAction(Touch touch){
		// raycast Rotating Bullet
		
		int layerMask =  1 << 8;
		Ray ray = Camera.main.ScreenPointToRay(touch.position);
		RaycastHit hit;
		if ( Physics.Raycast(ray, out hit, 100f, layerMask) ){
			string hitName = hit.collider.transform.gameObject.name;
			switch(hitName){
			case "RotatingBullet":
				sm.rc.LoadBullet();
				break;
			case "Chamber0":
			case "Chamber1":
			case "Chamber2":
			case "Chamber3":
			case "Chamber4":
			case "Chamber5":
			case "Chamber6":
			case "Chamber7":
				//console.log(hitName);
				if ( sm.cylinderState == CylinderStates.Open ) {
					int chamber = System.Int32.Parse(new Regex(@"\d").Matches(hitName)[0].Value);
					if ( chamber != -1 ){
						switch ( sm.chambers[chamber] ){
						case ChamberStates.Empty:
							sm.rc.LoadBullet(chamber);
							break;
						default:
						case ChamberStates.LoadedLive:
						case ChamberStates.LoadedSpent:
							sm.rc.EmptyChamber(chamber);
							break;
						}
					}
				}
				break;
			default:
				break;
			}
		}
	}
	
}