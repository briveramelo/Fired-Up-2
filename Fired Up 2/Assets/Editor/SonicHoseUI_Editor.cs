﻿using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SonicHose_UI)), CanEditMultipleObjects]
public class SonicHoseUI_Editor : Editor {

	public SerializedProperty
		sonicEffectSettings,
		textureAnimatorScripts,
		sonicBeamTransform;
	
	void OnEnable(){
		sonicEffectSettings = serializedObject.FindProperty("sonicEffectSettings");
		textureAnimatorScripts = serializedObject.FindProperty("textureAnimatorScripts");
		sonicBeamTransform = serializedObject.FindProperty("sonicBeamTransform");
	}
	
	public override void OnInspectorGUI (){
		serializedObject.Update();
		EditorGUILayout.PropertyField(sonicEffectSettings);
		EditorGUILayout.PropertyField(sonicBeamTransform);

		EditorGUI.BeginChangeCheck();
		EditorGUILayout.PropertyField(textureAnimatorScripts, true);
		if (EditorGUI.EndChangeCheck())
			serializedObject.ApplyModifiedProperties();
	}
}
