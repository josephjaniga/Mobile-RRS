using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomEditor(typeof(PlayerManager))]
public class PlayerManagerEditor : Editor
{
	public override void OnInspectorGUI()
	{

		DrawDefaultInspector();

		PlayerManager myPlayerManager = (PlayerManager)target;

		if(GUILayout.Button("AddCoin")){
			myPlayerManager.addCoins(1);
		}

		if(GUILayout.Button("RemoveCoin")){
			myPlayerManager.removeCoins(1);
		}

		if(GUILayout.Button("AddBullet")){
			myPlayerManager.addBullets(1);
		}

		if(GUILayout.Button("RemoveBullet")){
			myPlayerManager.removeBullets(1);
		}

	}

}
