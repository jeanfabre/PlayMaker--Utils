// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
#if FALSE
using UnityEditor;
using UnityEngine;

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using System.CodeDom.Compiler;
using System.Text.RegularExpressions;

namespace HutongGames.PlayMakerEditor.Ecosystem.Utils
{
	
	public class PlayMakerEventProxyCreator {
		
		
		public class ValidationResult
		{
			public bool success = true;
			public string message = "";
			
			public ValidationResult(bool success,string message){
				this.success = success;
				this.message = message;
			}
			
			public static ValidationResult ValidResult()
			{
				return new ValidationResult(true,"");
			}
		}
		
		[Serializable]
		public class PlayMakerEventProxyCreatorDefinition
		{
			
			#region Data
			/// <summary>
			/// The name space.
			/// </summary>
			public string NameSpace = "Net.FabreJean";
			
			/// <summary>
			/// The name.
			/// </summary>
			public string Name = "MyMessageProxy";
			
			/// <summary>
			/// The folder path.
			/// </summary>
			public string FolderPath = "PlayMaker Custom Scripts/";
			
			/// <summary>
			/// The public method name.
			/// </summary>
			public string PublicMethodName = "MyMessage";
			
			/// <summary>
			/// The full filePath of this definition.
			/// deduced property or injected property 
			/// </summary>
			public string filePath = "";
			
			public string directoryPath ="";
			
			/// <summary>
			/// The script literal. It's only use for preview (shorter version, minus comments and all)
			/// </summary>
			public string LiteralPreview = "";
			
			/// <summary>
			/// The script literal.
			/// </summary>
			public string ScriptLiteral = "";
			
			#endregion
			
			
			public void UpdateFilePath()
			{
				
				string fileName = Name+".cs";
				directoryPath = Path.Combine(Application.dataPath, FolderPath);
				
				filePath = Path.Combine(directoryPath,fileName);
			}
			#region Validation
			
			public ValidationResult DefinitionValidation	= ValidationResult.ValidResult();
			public ValidationResult NameSpaceValidation		= ValidationResult.ValidResult();
			public ValidationResult NameValidation			= ValidationResult.ValidResult();
			public ValidationResult FolderPathValidation	= ValidationResult.ValidResult();
			public ValidationResult PublicMethodValidation		= ValidationResult.ValidResult();

			public ValidationResult ValidateDefinition()
			{
				NameSpaceValidation		= ValidateNameSpace();
				NameValidation			= ValidateName();
				FolderPathValidation	= ValidateFolderPath();
				
				if (
					NameSpaceValidation.success
					&&
					NameValidation.success
					&& 
					FolderPathValidation.success
					&&
					PublicMethodValidation.success
					)
				{
					DefinitionValidation =  ValidationResult.ValidResult();
				}else{
					DefinitionValidation = new ValidationResult(false,"Invalid Definition: Please correct fields with errors");
				}
				
				return DefinitionValidation;
			}
			
			private readonly Regex doubleDot = new Regex("\\.\\.");
			private readonly Regex FolderPathRegex =  new Regex("^([a-zA-Z0-9][^*/><?\"|:]*)$");
			//private readonly Regex FolderNameRegex = new Regex("[" + Regex.Escape(Path.GetInvalidPathChars) + "]");
			private readonly CodeDomProvider provider = CodeDomProvider.CreateProvider("C#");
			
			public ValidationResult ValidateNameSpace()
			{
				if ( String.IsNullOrEmpty(NameSpace.Trim()) )
				{
					return new ValidationResult(false, "A Namespace must be provided.");
				}
				
				
				if (doubleDot.IsMatch(NameSpace))
				{
					return new ValidationResult(false, "NameSpace Structure is not valid.");
				}
				
				var inputs = (NameSpace as string).Split('.');
				foreach (var item in inputs)
				{
					if (!provider.IsValidIdentifier(item))
					{
						return new ValidationResult(false, string.Format("NameSpace sub element '{0}' is invalid.", item));
					}
				}
				return ValidationResult.ValidResult();
			}
			
			
			public ValidationResult ValidateName()
			{
				if ( String.IsNullOrEmpty(Name.Trim()))
				{
					return new ValidationResult(false, "An Enum name must be provided.");
				}
				if (!provider.IsValidIdentifier(Name))
				{
					return new ValidationResult(false,"Enum Name is invalid");
				}
				
				return ValidationResult.ValidResult();
			}
			
			
			
			public ValidationResult ValidateFolderPath()
			{
				// we accept not folder path, meaning it will be at the root of the assets.
				if (String.IsNullOrEmpty(FolderPath))
				{
					return ValidationResult.ValidResult();
				}
				string outputPath = Path.Combine(Application.dataPath, FolderPath);
				
				
				var inputs = (FolderPath as string).Split('/');
				foreach (var item in inputs)
				{
					/*
					if (FolderNameRegex.IsMatch(item))
					{
						return new ValidationResult(false, string.Format("Folder '{0}' is invalid.", item));
					}
					*/
				}
				
				return ValidationResult.ValidResult();
			}
			
		}
		
		#endregion Validation
		static string Template_MainStructure =@"[HEADER]

[STRUCTURE]";
		
		static string Template_Header = @"// (c) Copyright HutongGames, LLC 2010-[YEAR]. All rights reserved.
// THIS CONTENT IS AUTOMATICALLY GENERATED.
// [TAG]
// this script was generated by the 'PlayMaker Event Proxy Creator Wizard'. You can perfectly edit this script directly now.";
		
		static string Template_EnumStructure = @"namespace [NAMESPACE]
{
	public class MyEventProxy : PlayMakerEventProxy {

		[Button(""[METHOD_NAME]"",""[METHOD_NAME_NICIFIED]"")] public bool _;
		public void MyOwnEvent()
		{
			if (debug || !Application.isPlaying)
			{
				Debug.Log(""MyEventProxy : MyOwnEvent()"");
			}
			base.SendPlayMakerEvent();
		}
	}
}";
		
		static string Template_EnumEntry = "\t\t{0}{1}";
		
		
		/// <summary>
		/// Create a new script featuring the new enum.
		/// </summary>
		public void CreateProxy(PlayMakerEventProxyCreatorDefinition definition)
		{
			BuildScriptLiteral(definition);
			
			definition.UpdateFilePath();
			
			// Ensure that this path actually exists.
			if (!Directory.Exists(definition.directoryPath))
				Directory.CreateDirectory(definition.directoryPath);
			
			File.WriteAllText(definition.filePath, definition.ScriptLiteral);
			
			AssetDatabase.Refresh();
		}
		
		
		public void BuildScriptLiteral(PlayMakerEventProxyCreatorDefinition definition)
		{
			
			
			// build the header
			string headerLiteral = Template_Header;
			headerLiteral = headerLiteral.Replace("[YEAR]",DateTime.Today.Year.ToString());

			// build the structure
			string structureliteral = Template_EnumStructure;
			structureliteral = structureliteral.Replace("[NAMESPACE]",definition.NameSpace);
			structureliteral = structureliteral.Replace("[CLASS_NAME]",definition.Name);

			structureliteral = structureliteral.Replace("[METHOD_NAME]",definition.PublicMethodName);
			
			// build script literal
			string scriptLiteral = Template_MainStructure;
			scriptLiteral = scriptLiteral.Replace("[HEADER]",headerLiteral);
			scriptLiteral = scriptLiteral.Replace("[STRUCTURE]",structureliteral);
			definition.ScriptLiteral = scriptLiteral;
			
			definition.LiteralPreview = structureliteral;
		}
		
	}
}
#endif