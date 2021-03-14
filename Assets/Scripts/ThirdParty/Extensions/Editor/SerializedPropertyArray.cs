using System;
using UnityEditor;
using UnityEngine;

namespace sdk_feed.Common.Extensions
{
    public static class SerializedPropertyArray
    {
        public static void DrawArrayElemAsFoldout(this SerializedProperty property, int index, Action<int> contentDrawer)
        {
            DrawArrayElemAsFoldout(property, index, null, contentDrawer);
        }
        public static void DrawArrayElemAsFoldout(this SerializedProperty property, int index, string header = null, Action<int> contentDrawer = null)
        {
            var elem = property.GetArrayElementAtIndex(index);
            EditorGUILayout.BeginHorizontal();
            elem.isExpanded = EditorGUILayout.Foldout(elem.isExpanded, header ?? elem.displayName, true);
            DrawArrayButtons(property, index);
            EditorGUILayout.EndHorizontal();

            if (!elem.isExpanded) return;
            if (contentDrawer != null)
            {
                contentDrawer.Invoke(index);
            }
            else
            {
                EditorGUILayout.PropertyField(elem, true);
            }
        }
        
        private static void DrawArrayButtons(SerializedProperty property, int index)
        {
            if (GUILayout.Button("+", GUILayout.Width(24))) InsertArrayElementAtIndex(property, index);
            if (index != 0 && GUILayout.Button("▲", GUILayout.Width(24))) MoveArrayElement(property, index, index - 1);
            if (index + 1 < property.arraySize && GUILayout.Button("▼", GUILayout.Width(24))) MoveArrayElement(property, index, index + 1);
            if (GUILayout.Button ("x", GUILayout.Width(24))) DeleteArrayElementAtIndex(property, index);
        }
        
        private static void InsertArrayElementAtIndex(SerializedProperty property, int index)
        {
            if (index < 0 || index > property.arraySize) return;
            property.InsertArrayElementAtIndex(index);
        }
        
        private static void DeleteArrayElementAtIndex(SerializedProperty property, int index)
        {
            if (CheckIndexOutOfRange(property, index)) return;
            property.DeleteArrayElementAtIndex(index);
        }

        private static void MoveArrayElement(SerializedProperty property, int srcIndex, int dstIndex)
        {
            if (CheckIndexOutOfRange(property, srcIndex) || CheckIndexOutOfRange(property, dstIndex)) return;
            property.MoveArrayElement(srcIndex, dstIndex);
        }

        private static bool CheckIndexOutOfRange(SerializedProperty property, int index) => index < 0 || index >= property.arraySize;
    }
}