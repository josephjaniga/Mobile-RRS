using UnityEngine;
using System.Collections;

public class RevolverController : MonoBehaviour {

	public StateMachine sm;

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
		if ( chamber == -1 ){
			if ( FindFirstEmpty() > -1 ){
				sm.chambers[FindFirstEmpty()] = ChamberStates.LoadedLive;
				// Live Bullet Loaded Event
				LoadedALiveBullet();
			}
		} else {
			if ( sm.chambers[chamber] == ChamberStates.Empty ){
				sm.chambers[chamber] = ChamberStates.LoadedLive;
				// Live Bullet Loaded Event
				LoadedALiveBullet();
			}
		}
	}

	public void EmptyAll(){
		for ( int i=0; i<sm.chambers.Count; i++ ){
			sm.chambers[i] = ChamberStates.Empty;
		}
	}

	public void EmptyChamber(int chamber = -1){
		if ( chamber != -1 ){
			if ( sm.chambers[chamber] == ChamberStates.LoadedLive ){ 
				// Live Bullet Unloaded Event
				UnloadedALiveBullet();
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
		// TODO: play shoot noise
		if ( !sm.openPlay ){
			sm.dead = true;
		}
		sm.chambers[FindActiveChamber()] = ChamberStates.LoadedSpent;
	}

	public void dryFire(){
		// TODO: play click noise
		if ( sm.liveBulletsInCylinder >= sm.liveBulletsInCylinder_objective ){
			sm.triggerPulls++;
		}
	}

	public void cockHammer(){
		// TODO: play hammer cpck noise
		advanceBarrelOneStep();
		sm.hammerState = HammerStates.Cocked;
	}

}
