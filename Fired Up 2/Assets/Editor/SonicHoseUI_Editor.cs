using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SonicHose_UI)), CanEditMultipleObjects]
public class SonicHoseUI_Editor : Editor {

	public SerializedProperty
		sonicBeamTransform;
	
	void OnEnable(){
		sonicBeamTransform = serializedObject.FindProperty("sonicBeamTransform");
	}
	
	public override void OnInspectorGUI (){
		serializedObject.Update();
		EditorGUILayout.PropertyField(sonicBeamTransform);
	    serializedObject.ApplyModifiedProperties();
	}
}
