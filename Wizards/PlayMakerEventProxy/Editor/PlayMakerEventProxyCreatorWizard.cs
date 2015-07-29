// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
#if FALSE
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using UnityEditor;
using UnityEngine;

namespace HutongGames.PlayMakerEditor
{
	public class PlayMakerEventProxyCreatorWizard : EditorWindow
	{ 

		
		#region UI

		
		public void OnGUI()
		{
			FsmEditorStyles.Init();

			
			FsmEditorGUILayout.ToolWindowLargeTitle(this, "Event Proxy Creator");

			GUILayout.Label(" ");
			GUILayout.Label("This Proxy lets you create a Component with a public method.\n" +
				"That method will send a PlayMaker event.\n" +
				"Use this when you expect Unity or third party assets to fire messages\n" +
				"and you want to catch that message as a PlayMaker Event");
			OnGUI_DoEnumDefinitionForm();
		}

		string ClassName = "MyMessageProxy";
		string MethodName= "MyMessage";

		void OnGUI_DoEnumDefinitionForm()
		{

			// NAME
			GUILayout.Label("Class Name");
			ClassName = GUILayout.TextField(ClassName);
			
			// Method Name
			GUILayout.Label("Method/Message Name");
			ClassName = GUILayout.TextField(ClassName);

			FsmEditorGUILayout.Divider();

			if (GUILayout.Button("Create")) // Label "Save Changes" when we detected that we are editing an existing enum
			{
				//enumCreator.CreateEnum(currentEnum);
			}

		}

		#endregion
		

		#region Window Management
		
		public static PlayMakerEventProxyCreatorWizard Instance;


		[MenuItem ( "PlayMaker/Tools/Addons/PlayMaker Event Proxy Creator Wizard")]
		static public void Init () {
			
			// Get existing open window or if none, make a new one:
			Instance = (PlayMakerEventProxyCreatorWizard)EditorWindow.GetWindow (typeof (PlayMakerEventProxyCreatorWizard));
			
			Instance.Initialize();
		}
		
		public void Initialize()
		{
			//Debug.Log("Init");
			Instance = this;
			
			InitWindowTitle();
			position =  new Rect(120,120,400,292);
			// initial fixed size
			minSize = new Vector2(400, 292);
			
			
		}
		
		public void InitWindowTitle()
		{
			title = "Proxy Creator";
		}
		
		
		protected virtual void OnEnable()
		{
			// Debug.Log("OnEnable");
	
		}

		#endregion Window Management
	}
}
#endif