using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public int bulletCoins;
	public int bullet;
	public int lastPlayedAd;
	public float adCooldown = 300f;

	// Use this for initialization
	void Start () {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		bulletCoins = PlayerPrefs.GetInt("bulletCoins", 0);
		bullet = PlayerPrefs.GetInt("bullet", 0);
		lastPlayedAd = PlayerPrefs.GetInt("lastPlayedAd", 0);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnApplicationPause(bool paused) {
		if ( paused ){
		
		} else {
		
		}
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

	public int toUnixTime(System.DateTime date)
	{
		var epoch = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
		return (int)((date - epoch).TotalSeconds);
	}
	
}
