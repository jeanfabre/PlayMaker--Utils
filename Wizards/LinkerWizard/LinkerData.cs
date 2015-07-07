using System;
using System.Collections;

using UnityEngine;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HutongGames.PlayMaker.Ecosystem.Utils
{
	public class LinkerData : ScriptableObject
	{
		public bool debug;

		static public bool DebugAll
		{
			get{
				if (instance==null)
				{
					return true;
				}
				return instance.debug;
			}
		}


		static public LinkerData instance;
		

		void OnEnable() {
			LinkerData.instance = this;
		}


	#if UNITY_EDITOR

		[MenuItem("PlayMaker/Addons/Tools/Linker Wizard")]
		[MenuItem("Assets/Create/PlayMaker/Linker Wizard")]
		public static void CreateAsset ()
		{
			if (LinkerData.instance!=null)
			{
				Selection.activeObject = LinkerData.instance;
				EditorGUIUtility.PingObject(Selection.activeObject);
				Debug.Log("Linker Wizard already exists");
				return;
			}
			PlayMakerUtils.CreateAsset<LinkerData>();
		}

	#endif

	}
}
