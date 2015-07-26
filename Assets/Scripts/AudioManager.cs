using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public bool isMusic = false;

	// SFX
	public AudioClip bullet_load;
	public AudioClip chamber_empty;
	public AudioClip cylinder_close;
	public AudioClip cylinder_open_close;
	public AudioClip cylinder_spin_one;
	public AudioClip cylinder_spin_two;
	public AudioClip guitar_riff_one;
	public AudioClip gun_cock;
	public AudioClip gun_live_fire;
	public AudioClip gun_dry_fire;
	public AudioClip pickup;
	public AudioClip e_major_blues;

	// the audio Sourçe
	public AudioSource src;

	// Use this for initialization
	void Start () {
		src = gameObject.GetComponent<AudioSource>();
		if ( isMusic ){
			src.clip = e_major_blues;
			src.Play();
		}
	}

	public void PlayClip(AudioClip clippy){
		src.clip = clippy;
		src.Play();
	}

}
