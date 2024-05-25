using UnityEditor;
using UnityEngine;

using Ed = UnityEditor.Editor;

namespace SimpleBootstrap.Editor
{
    [CustomEditor(typeof(BootstrapScript), true)]
    public class BootstrapScriptEditor : Ed
    {
        private static readonly int s_onOffPrefixWidth = 30;

        private SerializedProperty throwExceptionIfPreviousScriptReturnExceptionProperty;

        private void OnEnable()
        {
            throwExceptionIfPreviousScriptReturnExceptionProperty = serializedObject.FindProperty("_throwExceptionIfPreviousScriptReturnException");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            bool currentValue = throwExceptionIfPreviousScriptReturnExceptionProperty.boolValue;
            string labelPrefix = currentValue ? "ON: " : "OFF: ";
            string buttonText = "Throw exception if previous script return exception";

            EditorGUILayout.BeginHorizontal();

            GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.normal.textColor = currentValue ? new Color(0.2f, 0.75f, 0.2f) : new Color(0.9f, 0.4f, 0.4f);

            EditorGUILayout.LabelField(labelPrefix, labelStyle, GUILayout.Width(s_onOffPrefixWidth));

            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.wordWrap = true;

            float buttonHeight = buttonStyle.CalcHeight(new GUIContent(buttonText), EditorGUIUtility.currentViewWidth - s_onOffPrefixWidth - 25);

            if (GUILayout.Button(buttonText, buttonStyle, GUILayout.Height(buttonHeight)))
            {
                throwExceptionIfPreviousScriptReturnExceptionProperty.boolValue = !currentValue;
            }
            EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();

            DrawDefaultInspector();
        }
    }
}
