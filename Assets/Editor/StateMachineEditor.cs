using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomEditor(typeof(StateMachine))]
public class StateMachineEditor : Editor
{
	public override void OnInspectorGUI()
	{

		DrawDefaultInspector();

		StateMachine myStateMachine = (StateMachine)target;
		if(GUILayout.Button("Advance Barrel"))
		{
			myStateMachine.advanceBarrelOneStep();
		}

	}

}
