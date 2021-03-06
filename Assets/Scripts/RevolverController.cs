﻿using UnityEngine;
using System.Collections;

public class RevolverController : MonoBehaviour {

	public StateMachine sm;
	public PlayerManager pm;
	public Onomatopoeia o;
	public AudioManager audioManager;
	public AudioManager musicManager;

	// create a delegate and event so other classes can subscribe to orientation change
	public delegate void LiveBulletLoaded();
	public static event LiveBulletLoaded LoadedALiveBullet;

	public delegate void LiveBulletUnloaded();
	public static event LiveBulletUnloaded UnloadedALiveBullet;
	
//	// Use this for initialization
//	void Start () {
//	
//	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}

	public void LoadBullet(int chamber = -1){

		if ( pm.bullet > 0 ){
			if ( chamber == -1 ){
				if ( FindFirstEmpty() > -1 ){
					sm.chambers[FindFirstEmpty()] = ChamberStates.LoadedLive;
					// Live Bullet Loaded Event
					LoadedBullet();
				}
			} else {
				if ( sm.chambers[chamber] == ChamberStates.Empty ){
					sm.chambers[chamber] = ChamberStates.LoadedLive;
					// Live Bullet Loaded Event
					LoadedBullet();
				}
			}
		} else {
			// out of bullets
			Debug.Log ("Out of bullets");
		}

	}

	public void EmptyAll(){
		for ( int i=0; i<sm.chambers.Count; i++ ){
			if ( sm.chambers[i] == ChamberStates.LoadedLive ){
				UnloadedBullet();
			}
			sm.chambers[i] = ChamberStates.Empty;
		}
	}

	public void EmptyChamber(int chamber = -1){
		if ( chamber != -1 ){
			if ( sm.chambers[chamber] == ChamberStates.LoadedLive ){ 
				// Live Bullet Unloaded Event
				UnloadedBullet();
			}
			sm.chambers[chamber] = ChamberStates.Empty;
		}
	}
	
	public int FindFirstEmpty(){
		return sm.chambers.FindIndex(x => x == ChamberStates.Empty);
	}
	
	public int FindActiveChamber(){
		Transform apex = GameObject.Find("Apex").transform;
		int closestIndex = 0;
		Transform closestChamber = GameObject.Find("Chamber0").transform;
		for ( int i=1; i<sm.chambers.Count; i++ ){
			float currentBest = Vector3.Distance(apex.position, closestChamber.position);
			float pass = Vector3.Distance(apex.position, GameObject.Find("Chamber"+i).transform.position);
			if ( pass <= currentBest ) {
				closestIndex = i;
				closestChamber = GameObject.Find("Chamber"+i).transform;
			}
		}
		return closestIndex;
	}
	
	public void advanceBarrelOneStep(){
		// play noise
		Rigidbody rb = GameObject.Find("Cylinder").GetComponent<Rigidbody>();
		rb.constraints = RigidbodyConstraints.FreezeAll;
		float z = rb.transform.rotation.eulerAngles.z;
		float rotationTarget = 45f - (z%45f);
		rb.transform.Rotate(new Vector3(0f, 0f, rotationTarget));
		rb.constraints &= ~RigidbodyConstraints.FreezeRotationZ;
	}

	public void triggerPull(){
		if ( sm.triggerState == TriggerStates.Reset ){
			sm.triggerState = TriggerStates.Pulled;
			if ( sm.chambers[FindActiveChamber()] == ChamberStates.LoadedLive ){
				liveFire();
			} else {
				dryFire();
			}
		} else {
			sm.triggerState = TriggerStates.Reset;
		}
	}

	public void liveFire(){
		o.createTextFX("BOOM!", TextStyles.BasicComic, TextMoods.Negative);
		audioManager.PlayClip(audioManager.gun_live_fire);
		if ( !sm.openPlay ){
			sm.dead = true;
		}
		sm.chambers[FindActiveChamber()] = ChamberStates.LoadedSpent;
	}

	public void dryFire(){
		audioManager.PlayClip(audioManager.gun_dry_fire);
		o.createTextFX("KLIK!", TextStyles.BasicComic, TextMoods.Positive);
		if ( sm.liveBulletsInCylinder >= sm.liveBulletsInCylinder_objective ){
			sm.triggerPulls++;
		}
	}

	public void cockHammer(){
		audioManager.PlayClip(audioManager.gun_cock);
		o.createTextFX("KLA-CLICT!", TextStyles.BasicComic, TextMoods.Positive);
		advanceBarrelOneStep();
		sm.hammerState = HammerStates.Cocked;
	}

	public void LoadedBullet(){
		audioManager.PlayClip(audioManager.bullet_load);
		o.createTextFX("CLINK!", TextStyles.BasicComic, TextMoods.Positive);
		LoadedALiveBullet();
		pm.removeBullets(1);
	}

	public void UnloadedBullet(){
		audioManager.PlayClip(audioManager.chamber_empty);
		o.createTextFX("CLANK!", TextStyles.BasicComic, TextMoods.Positive);
		UnloadedALiveBullet();
		pm.addBullets(1);
	}

}
