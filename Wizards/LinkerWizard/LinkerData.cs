using System;
using System.Collections;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HutongGames.PlayMaker.Ecosystem.Utils
{
	public class LinkerData : ScriptableObject
	{
		public bool debug = true;

		public LinkerData self;

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

		static private LinkerData _instance_;


		static public LinkerData instance
		{
			get{
				if (_instance_==null)
				{
					return null;
				}
				return _instance_.self;
			}

			set{
				_instance_ = value;
			}

		}

		void OnEnable() {
			LinkerData._instance_ = this;
			self = this;
		}


	#if UNITY_EDITOR

		[MenuItem("PlayMaker/Addons/Tools/Linker Wizard")]
		[MenuItem("Assets/Create/PlayMaker/Linker Wizard")]
		public static void CreateAsset ()
		{


			if (LinkerData.instance!=null)
			{
				string path = AssetDatabase.GetAssetPath(LinkerData.instance);
				if (string.IsNullOrEmpty(path))
				{
					LinkerData.instance = null;
				}else{

					Selection.activeObject = LinkerData.instance;
					EditorGUIUtility.PingObject(Selection.activeObject);
					Debug.Log("Linker Wizard already exists at "+path);
					return;
				}
			}

			// search in the assets:
		 	UnityEngine.Object[] _assets =	PlayMakerUtils.GetAssetsOfType(typeof(LinkerData),".asset");

			if (_assets!=null && _assets.Length>0)
			{
				LinkerData.instance = _assets[0] as LinkerData;

				Selection.activeObject = LinkerData.instance;
				EditorGUIUtility.PingObject(Selection.activeObject);
				Debug.Log("Linker Wizard already exists at "+AssetDatabase.GetAssetPath(LinkerData.instance));
				return;
			}

			PlayMakerUtils.CreateAsset<LinkerData>("Linker Wizard");
		}

	#endif

	}
}
