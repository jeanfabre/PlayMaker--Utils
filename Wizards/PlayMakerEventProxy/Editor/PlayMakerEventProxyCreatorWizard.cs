// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using UnityEditor;
using UnityEngine;

using HutongGames.PlayMakerEditor.Ecosystem.Utils;

namespace HutongGames.PlayMakerEditor
{
	public class PlayMakerEventProxyCreatorWizard : EditorWindow
	{ 

		PlayMakerEventProxyCreator eventProxyCreator = new PlayMakerEventProxyCreator();
		
		
		public PlayMakerEventProxyCreator.PlayMakerEventProxyCreatorDefinition currentDefinition = new PlayMakerEventProxyCreator.PlayMakerEventProxyCreatorDefinition();

		bool ReBuildPreview;

		#region UI

		GUIStyle labelStyle;

		public void OnGUI()
		{
			FsmEditorStyles.Init();

			// set style ot use rich text.
			if (labelStyle==null)
			{
				labelStyle = GUI.skin.GetStyle("Label");
				labelStyle.richText = true;
			}

			FsmEditorGUILayout.ToolWindowLargeTitle(this, "Event Proxy Creator");

			GUILayout.Label(" ");
			GUILayout.Label("This Proxy lets you create a Component with a public method.\n" +
				"That method will send a PlayMaker event that you can define in the component proxy Inspector.\n" +
				"Use this when you expect Unity or third party assets to fire messages\n" +
				"and you want to catch that message as a PlayMaker Event");

			FsmEditorGUILayout.Divider();

			OnGUI_DoDefinitionForm();
		}



		void OnGUI_DoDefinitionForm()
		{
			Color _orig = Color.clear;
			ReBuildPreview = false;

			/*
			if (currentFileDetails!=null)
			{
				GUILayout.Label("You are editing an existing enum");
				FsmEditorGUILayout.Divider();
			}
			*/
			// FOLDER
			_orig = GUI.color;
			if (!currentDefinition.FolderPathValidation.success)
			{
				GUI.color = new Color(255,165,0);
			}
			GUILayout.Label("Project Folder: <color=#ffa500><b>"+currentDefinition.FolderPathValidation.message+"</b></color>");
			currentDefinition.FolderPath = GUILayout.TextField(currentDefinition.FolderPath);
			GUI.color = _orig;

			// NAMESPACE
			_orig = GUI.color;
			if (!currentDefinition.NameSpaceValidation.success)
			{
				GUI.color = Color.red;
			}
			GUILayout.Label("NameSpace: <color=#B20000><b>"+currentDefinition.NameSpaceValidation.message+"</b></color>");
			string _nameSpace = GUILayout.TextField(currentDefinition.NameSpace);
			GUI.color = _orig;
			if (!string.Equals(_nameSpace,currentDefinition.NameSpace))
			{
				currentDefinition.NameSpace = _nameSpace;
				ReBuildPreview = true;
			}

			// NAME
			_orig = GUI.color;
			if (!currentDefinition.NameValidation.success)
			{
				GUI.color = Color.red;
			}
			GUILayout.Label("Class Name: <color=#B20000><b>"+currentDefinition.NameValidation.message+"</b></color>");
			string _className = GUILayout.TextField(currentDefinition.Name);
			GUI.color = _orig;
			if (!string.Equals(_className,currentDefinition.Name))
			{
				currentDefinition.Name = _className;
				ReBuildPreview = true;
			}

			
			// Method Name
			_orig = GUI.color;
			if (!currentDefinition.PublicMethodValidation.success)
			{
				GUI.color = Color.red;
			}
			GUILayout.Label("Public Method/Message Name: <color=#B20000><b>"+currentDefinition.PublicMethodValidation.message+"</b></color>");
			string _methodName = GUILayout.TextField(currentDefinition.PublicMethodName);
			GUI.color = _orig;
			if (!string.Equals(_methodName,currentDefinition.PublicMethodName))
			{
				currentDefinition.PublicMethodName = _methodName;
				ReBuildPreview = true;
			}

			
			FsmEditorGUILayout.Divider();

			if (!currentDefinition.DefinitionValidation.success)
			{
				GUILayout.Label("<color=#B20000><b>"+currentDefinition.DefinitionValidation.message+"</b></color>");
			}

			if (currentDefinition.DefinitionValidation.success)
			{
				if (GUILayout.Button("Create")) // Label "Save Changes" when we detected that we are editing an existing enum
				{
					eventProxyCreator.CreateProxy(currentDefinition);
				}
			}else{
				
				Color _color = GUI.color;
				
				_color.a = 0.5f;
				GUI.color = _color;
				GUILayout.Label("Create","Button");
				_color.a = 1f;
				GUI.color =_color;
			}


			if (ReBuildPreview )
			{
				currentDefinition.ValidateDefinition();
				
				//enumCreator.BuildScriptLiteral(currentEnum);
				Repaint();
			}
		}

		#endregion
		

		#region Window Management
		
		public static PlayMakerEventProxyCreatorWizard Instance;


		[MenuItem ( "PlayMaker/Addons/Tools/PlayMaker Event Proxy Wizard")]
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