using SimpleBootstrap;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BootstrapperBase), true)]
public class BootstrapperBaseEditor : Editor
{
    private SerializedProperty isNextBootstrapperSetProperty;
    private SerializedProperty nextBootstrapperProperty;

    private void OnEnable()
    {
        isNextBootstrapperSetProperty = serializedObject.FindProperty("_isNextBootstrapperSet");
        nextBootstrapperProperty = serializedObject.FindProperty("_nextBootstrapper");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawDefaultInspector();

        if (!isNextBootstrapperSetProperty.boolValue)
        {
            if (GUILayout.Button("Set next bootstrapper"))
            {
                isNextBootstrapperSetProperty.boolValue = true;
            }
        }
        else
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(nextBootstrapperProperty, GUIContent.none);

            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                nextBootstrapperProperty.objectReferenceValue = null;
                isNextBootstrapperSetProperty.boolValue = false;
            }

            EditorGUILayout.EndHorizontal();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
