using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public int bulletCoins = 0;
	public int bullet = 0;
	public float lastPlayedAd = 0f;
	public float adCooldown = 300f;

	// Use this for initialization
	void Start () {
		bulletCoins = PlayerPrefs.GetInt("bulletCoins", 0);
		bullet = PlayerPrefs.GetInt("bullet", 0);
		lastPlayedAd = PlayerPrefs.GetFloat("lastPlayedAd", 0f);
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	public void addBullets(int numberBullets){
		bullet += numberBullets;
		PlayerPrefs.SetInt("bullet", bullet);
	}

	public void removeBullets(int numberBullets){
		bullet -= numberBullets;
		if ( bullet < 0 ){
			bullet = 0;
		}
		PlayerPrefs.SetInt("bullet", bullet);
	}

	public void addCoins(int numberCoins){
		bulletCoins += numberCoins;
		PlayerPrefs.SetInt("bulletCoins", bulletCoins);
	}

	public void removeCoins(int numberCoins){
		bulletCoins -= numberCoins;
		if ( bulletCoins < 0 ){
			bulletCoins = 0;
		}
		PlayerPrefs.SetInt("bulletCoins", bulletCoins);
	}

	public void setCoins(int newAmount){
		bulletCoins = newAmount;
		PlayerPrefs.SetInt("bulletCoins", bulletCoins);
	}


	public bool canAffordPurchasePrice(int purchasePrice){
		bool result = false;
		if ( bulletCoins >= purchasePrice ){
			result = true;
		}
		return result;
	}

}
