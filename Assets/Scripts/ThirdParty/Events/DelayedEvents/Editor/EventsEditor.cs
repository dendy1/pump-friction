using UnityEditor;
using UnityEngine;

namespace sdk_feed.Common.Events.DelayedEvents
{
    public static class EventsEditor
    {
        private static SerializedProperty _events;

        public static void Draw(SerializedProperty events)
        {
            if (events == null) return;
            
            _events = events.FindPropertyRelative("_events");
            
            var type = (TypeInitialization) events.FindPropertyRelative("_typeAction").enumValueIndex;
            if (type == TypeInitialization.Custom) EditorGUILayout.PropertyField(events.FindPropertyRelative("_tag"));
            EditorGUILayout.PropertyField(events.FindPropertyRelative("_typeAction"));
            EditorGUILayout.PropertyField(events.FindPropertyRelative("_typeRun"));
            DrawAdditionalParams(events, type);
            EditorGUILayout.Space();

            DrawEventsList();
        }

        private static void DrawAdditionalParams(SerializedProperty events, in TypeInitialization type)
        {
            switch (type)
            {
                case TypeInitialization.OnTriggerEnter:
                case TypeInitialization.OnTriggerExit:
                    DrawAdditionalParamsByTriggers(events);
                    break;
                case TypeInitialization.OnSwipeLeft:
                case TypeInitialization.OnSwipeRight:
                case TypeInitialization.OnSwipeUp:
                case TypeInitialization.OnSwipeDown:
                    DrawAdditionalParamsBySwipes(events);
                    break;
            }
        }

        private static void DrawAdditionalParamsByTriggers(SerializedProperty events)
        {
            var isUseParamTag = events.FindPropertyRelative("_isUseParamTag");
            var paramTag = events.FindPropertyRelative("_paramTag");
            
            if (!isUseParamTag.boolValue)
            {
                isUseParamTag.boolValue = EditorGUILayout.ToggleLeft("Set specific tag", isUseParamTag.boolValue);
            }
            else
            {
                EditorGUILayout.BeginHorizontal();
                isUseParamTag.boolValue = EditorGUILayout.ToggleLeft("Tag", isUseParamTag.boolValue);
                paramTag.stringValue = EditorGUILayout.TagField(paramTag.stringValue);
                EditorGUILayout.EndHorizontal();
            }
        }

        private static void DrawAdditionalParamsBySwipes(SerializedProperty events)
        {
            EditorGUILayout.PropertyField(events.FindPropertyRelative("_minSwipeLength"));
        }

        private static void DrawEventsList()
        {
            if (_events.arraySize == 0)
            {
                if (GUILayout.Button("Add event")) _events.InsertArrayElementAtIndex(0);
                return;
            }
            
            EditorGUILayout.LabelField("Events");
		    EditorGUI.indentLevel += 1;
            for (var i = 0; i < _events.arraySize; i++)
            {
                var evt = _events.GetArrayElementAtIndex(i);
                DrawEvent(evt, i);
            }
            EditorGUI.indentLevel -= 1;
        }

        private static void DrawEvent(SerializedProperty evt, int index)
        {
            DrawEventHeader(evt, index);
            if (evt == null || !evt.isExpanded) return;
            
            EditorGUILayout.PropertyField(evt.FindPropertyRelative("_delayInitialization"), true);
            EditorGUILayout.PropertyField(evt.FindPropertyRelative("_event"), true);
        }

        private static void DrawEventHeader(SerializedProperty evt, int index)
        {
            var header = "Delay: " + evt.FindPropertyRelative("_delayInitialization").floatValue;
            
            EditorGUILayout.BeginHorizontal();
            evt.isExpanded = EditorGUILayout.Foldout(evt.isExpanded, header);
            EditorGUILayout.Space();
            if (GUILayout.Button("+", GUILayout.Width(24))) _events.InsertArrayElementAtIndex(index);
            if (index != 0 && GUILayout.Button("▲", GUILayout.Width(24))) _events.MoveArrayElement(index, index - 1);
            if (index < _events.arraySize && GUILayout.Button("▼", GUILayout.Width(24))) _events.MoveArrayElement(index, index + 1);
            if (GUILayout.Button("x", GUILayout.Width(24))) _events.DeleteArrayElementAtIndex(index);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }
    }
}