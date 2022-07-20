using UnityEngine;
using UnityEditor;

namespace Variable
{
    [CustomEditor(typeof(BaseVariable),true)]
    [CanEditMultipleObjects]
    public class VariableEditor : Editor
    {
        private SerializedProperty initialValue;
        private SerializedProperty runtimeValue;
        private SerializedProperty runtimeMode;
        private SerializedProperty persistentMode;


        private void OnEnable()
        {
            initialValue    = serializedObject.FindProperty("initialValue");
            runtimeValue    = serializedObject.FindProperty("runtimeValue");
            runtimeMode     = serializedObject.FindProperty("runtimeMode");
            persistentMode  = serializedObject.FindProperty("persistentMode");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(initialValue);

            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(runtimeValue);
            EditorGUI.EndDisabledGroup();
        }
    }
}
