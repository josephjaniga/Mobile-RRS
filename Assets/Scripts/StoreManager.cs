using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StoreManager : MonoBehaviour {

	public PlayerManager pm;
	public Text bulletCount;
	public Text moneyCount;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		bulletCount.text = ""+pm.bullet;
		moneyCount.text = ""+pm.bulletCoins;
	}

	public void BackToMenu(){
		Application.LoadLevel("Menu");
	}

	public void FreeBullet(){
		if ( pm.canAffordPurchasePrice(0) ){
			pm.addBullets(1);
			pm.setCoins(pm.bulletCoins - 0);
		}
	}

	public void PurchaseBullet(){
		if ( pm.canAffordPurchasePrice(10) ){
			pm.addBullets(1);
			pm.setCoins(pm.bulletCoins - 10);
		}
	}

	public void PurchaseBox(){
		if ( pm.canAffordPurchasePrice(75) ){
			pm.addBullets(8);
			pm.setCoins(pm.bulletCoins - 75);
		}
	}
}
