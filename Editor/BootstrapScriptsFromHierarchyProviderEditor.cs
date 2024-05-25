using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

using Ed = UnityEditor.Editor;

namespace SimpleBootstrap.Editor
{
    [CustomEditor(typeof(BootstrapScriptsFromHierarchyProvider))]
    public class BootstrapScriptsFromHierarchyProviderEditor : Ed
    {
        private SerializedProperty foldoutProperty;

        private void OnEnable()
        {
            foldoutProperty = serializedObject.FindProperty("_foldout");
        }

        public override void OnInspectorGUI()
        {
            var provider = (BootstrapScriptsFromHierarchyProvider)target;
            provider.UpdateBootstrapScriptsList();
            IReadOnlyList<BootstrapScript> bootstrapScripts = provider.GetBootstrapScripts();

            serializedObject.Update();

            foldoutProperty.boolValue = EditorGUILayout.Foldout(foldoutProperty.boolValue, "Bootstrap Scripts");

            if (foldoutProperty.boolValue)
            {
                GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
                GUIStyle labelStyle = new GUIStyle(GUI.skin.label)
                {
                    fontStyle = FontStyle.Bold,
                    fontSize = 11,
                    normal = { textColor = new Color(0.84f, 0.84f, 0.84f) }
                };
                boxStyle.normal.background = MakeTex(1, 1, new Color(0.2f, 0.2f, 0.2f));

                EditorGUILayout.BeginVertical(boxStyle);

                if (bootstrapScripts != null && bootstrapScripts.Count > 0)
                {
                    foreach (var script in bootstrapScripts)
                    {
                        if (script != null)
                        {
                            EditorGUILayout.LabelField(script.GetType().Name, labelStyle);
                        }
                    }
                }
                else
                {
                    EditorGUILayout.LabelField("No Bootstrap Scripts found.", labelStyle);
                }

                EditorGUILayout.EndVertical();
            }

            serializedObject.ApplyModifiedProperties();
        }

        private Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];

            for (int i = 0; i < pix.Length; i++)
            {
                pix[i] = col;
            }

            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();

            return result;
        }
    }
}