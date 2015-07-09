// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.

// http://wiki.unity3d.com/index.php?title=CreateScriptableObjectAsset

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public partial class PlayMakerUtils {

	#if UNITY_EDITOR
	/// <summary>
	//	This makes it easy to create, name and place unique new ScriptableObject asset files.
	/// </summary>
	public static void CreateAsset<T> (string name = "") where T : ScriptableObject
	{
		T asset = ScriptableObject.CreateInstance<T> ();
		
		string path = AssetDatabase.GetAssetPath (Selection.activeObject);
		if (path == "") 
		{
			path = "Assets";
		} 
		else if (Path.GetExtension (path) != "") 
		{
			path = path.Replace (Path.GetFileName (AssetDatabase.GetAssetPath (Selection.activeObject)), "");
		}

		string _name = string.IsNullOrEmpty(name)? "New " + typeof(T).ToString():name;
		Debug.Log("Will CreateAsset with name: "+_name+" in path:"+path);
		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath (path + "/" + _name + ".asset");

		Debug.Log("CreateAsset:"+assetPathAndName);
		AssetDatabase.CreateAsset (asset, assetPathAndName);
		
		AssetDatabase.SaveAssets ();
		AssetDatabase.Refresh();
		EditorUtility.FocusProjectWindow ();
		Selection.activeObject = asset;
	}


	/// <summary>
	/// Used to get assets of a certain type and file extension from entire project
	/// </summary>
	/// <param name="type">The type to retrieve. eg typeof(GameObject).</param>
	/// <param name="fileExtension">The file extention the type uses eg ".prefab".</param>
	/// <returns>An Object array of assets.</returns>
	public static UnityEngine.Object[] GetAssetsOfType(System.Type type, string fileExtension)
	{
		List<UnityEngine.Object> tempObjects = new List<UnityEngine.Object>();
		DirectoryInfo directory = new DirectoryInfo(Application.dataPath);
		FileInfo[] goFileInfo = directory.GetFiles("*" + fileExtension, SearchOption.AllDirectories);
		
		int i = 0; int goFileInfoLength = goFileInfo.Length;
		FileInfo tempGoFileInfo; string tempFilePath;
		UnityEngine.Object tempGO;
		for (; i < goFileInfoLength; i++)
		{
			tempGoFileInfo = goFileInfo[i];
			if (tempGoFileInfo == null)
				continue;
			
			tempFilePath = tempGoFileInfo.FullName;
			tempFilePath = tempFilePath.Replace(@"\", "/").Replace(Application.dataPath, "Assets");
			
			//Debug.Log(tempFilePath + "\n" + Application.dataPath);
			
			tempGO = AssetDatabase.LoadAssetAtPath(tempFilePath, typeof(UnityEngine.Object)) as UnityEngine.Object;
			if (tempGO == null)
			{
				Debug.LogWarning("Skipping Null");
				continue;
			}
			else if (tempGO.GetType() != type)
			{
				Debug.LogWarning("Skipping " + tempGO.GetType().ToString());
				continue;
			}
			
			tempObjects.Add(tempGO);
		}
		
		return tempObjects.ToArray();
	}
	#endif
}
