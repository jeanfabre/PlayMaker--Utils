using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

using HutongGames.PlayMakerEditor;



/// <summary>
/// Adds Playmaker Utils defines to project
/// Other tools can now use #if PLAYMAKER_UTILS or PLAYMAKER_UTILS_X_X_OR_NEWER
/// </summary>
[InitializeOnLoad]
public class PlayMakerUtilsDefines
{
	static PlayMakerUtilsDefines()
	{

		#if ! PLAYMAKER_UTILS
			#if PLAYMAKER_1_9_OR_NEWER
				Defines.AddSymbolToAllTargets("PLAYMAKER_UTILS");
			#else
				PlayMakerDefines.AddScriptingDefineSymbolToAllTargets("PLAYMAKER_UTILS");
			#endif
		#endif

		#if ! PLAYMAKER_UTILS_1_4_OR_NEWER
			#if PLAYMAKER_1_9_OR_NEWER
				Defines.AddSymbolToAllTargets("PLAYMAKER_UTILS_1_4_OR_NEWER");
			#else
				PlayMakerDefines.AddScriptingDefineSymbolToAllTargets("PLAYMAKER_UTILS_1_4_OR_NEWER");
			#endif
		#endif

	}

}