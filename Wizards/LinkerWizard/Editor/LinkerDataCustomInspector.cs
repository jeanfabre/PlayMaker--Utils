using System;
using UnityEngine;
using UnityEditor;

using HutongGames.PlayMakerEditor;

using HutongGames.PlayMaker.Ecosystem.Utils;

[CustomEditor(typeof(LinkerData))]
public class LinkerDataCustomInspector : Editor
{
	static string ActionsPackagePath = "PlayMaker Utils/Wizards/LinkerWizard/LinkerWizardActions.unitypackage";

	public override void OnInspectorGUI()
	{
		LinkerData _target = target as LinkerData;

		FsmEditorStyles.Init();

		//GUILayout.Box("Hello",FsmEditorStyles.LargeTitleWithLogo,GUILayout.Height(42f));
		GUI.Box (new Rect (0f, 0f, Screen.width, 42f), "Linker Wizard", FsmEditorStyles.LargeTitleWithLogo);


		GUILayout.Label("1: Make sure you installed them actions");

		if (GUILayout.Button("Install Actions"))
		{
			//Debug.Log("importing package "+Application.dataPath+"/"+ActionsPackagePath);
			
			AssetDatabase.ImportPackage(Application.dataPath+"/"+ActionsPackagePath,true);
		}


		GUILayout.Label("2: Check 'debug' for tracking all reflections");

		EditorGUI.indentLevel++;
		bool _debug = EditorGUILayout.Toggle("Debug",_target.debug);
		EditorGUI.indentLevel--;
		if (_debug!=_target.debug)
		{
			_target.debug = _debug;
			EditorUtility.SetDirty(_target);
		}

		GUILayout.Label("3: Run your scenes from start to finish");
		GUILayout.Label("      Check the Unity Console for usages ");


	}

}
