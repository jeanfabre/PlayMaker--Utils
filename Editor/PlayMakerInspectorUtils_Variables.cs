// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

using HutongGames.PlayMaker;
using HutongGames.PlayMakerEditor;

namespace HutongGames.PlayMaker.Ecosystem.Utils
{
	public partial class PlayMakerInspectorUtils {

		/// <summary>
		/// Display an _selectionIndex the fsm variable from a list of variables ( from an fsm likely).
		/// This is to paliate for the PlayMaker 1.8 that deprecated the api call VariableEditor.FsmVarPopup()
		/// </summary>
		/// <returns>The fsm variable GU.</returns>
		/// <param name="fieldLabel">Field label.</param>
		/// <param name="fsmVariables">Fsm variables.</param>
		/// <param name="selection">Selection.</param>
		/// <param name="GuiChanged">GUI changed flag</param>
		public static FsmVar EditorGUILayout_FsmVarPopup(string fieldLabel,NamedVariable[] namedVariables,FsmVar selection,out bool GuiChanged)
		{
			GuiChanged = false;

			if (namedVariables==null)
			{
				Debug.LogWarning("EditorGUILayout_FsmVarPopup: namedVariables is null");
				return null;
			}



			int _selectionIndex = 0;

			string[] _variableChoices = new string[namedVariables.Length+1];
			_variableChoices[0] = "None";
			for(int i=0;i<namedVariables.Length;i++)
			{
				if (string.Equals(selection.variableName,namedVariables[i].Name))
				{
					_selectionIndex = i+1;
				}
				_variableChoices[i+1] = namedVariables[i].Name;
			}
			
			if (_variableChoices.Length!=0)
			{

				int _choiceIndex =  EditorGUILayout.Popup(fieldLabel,_selectionIndex,_variableChoices);
				if (_choiceIndex != _selectionIndex)
				{
					GuiChanged = true;

					if (_choiceIndex==0)
					{
						return new FsmVar();
					}else{
						FsmVar _newSelection = new FsmVar(namedVariables[_choiceIndex-1]);
						_newSelection.useVariable = true;
						return _newSelection;
					}

				}
			}

			return selection;
		}

	}
}