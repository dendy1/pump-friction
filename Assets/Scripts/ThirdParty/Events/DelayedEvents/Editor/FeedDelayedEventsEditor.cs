using UnityEditor;
using sdk_feed.Common.Extensions;
using UnityEngine;

namespace sdk_feed.Common.Events.DelayedEvents
{
    [CustomEditor(typeof(FeedDelayedEvents), true)]
    [CanEditMultipleObjects]
    public class FeedDelayedEventsEditor : Editor
    {
        private SerializedProperty _events;


        private void OnEnable()
        {
            _events = serializedObject.FindProperty("_events");
        }


        public override void OnInspectorGUI()
        {
            this.Header();
			serializedObject.Update();
            DrawEvents();
            DrawButtons();
            if (serializedObject.hasModifiedProperties)
            {
                serializedObject.ApplyModifiedProperties();
            }
        }

        private void DrawEvents()
        {
            for (var i = 0; i < _events.arraySize; i++)
            {
                DrawEvent(i, _events.GetArrayElementAtIndex(i));
            }
        }

        private void DrawEvent(in int index, in SerializedProperty evt)
        {
            DrawHeaderEvent(index, evt);
            if (index < _events.arraySize && evt.isExpanded)
            {
                EventsEditor.Draw(evt);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private void DrawHeaderEvent(in int index, in SerializedProperty evt)
        {
            EditorGUILayout.BeginHorizontal();
            
            var elem = _events.GetArrayElementAtIndex(index);
            elem.isExpanded = EditorGUILayout.BeginFoldoutHeaderGroup(elem.isExpanded, GetHeaderEventName(evt));
            
            if (GUILayout.Button("+", GUILayout.Width(18))) _events.InsertArrayElementAtIndex(index);
            if (GUILayout.Button("▲", GUILayout.Width(18))) _events.MoveArrayElement(index, index - 1);
            if (GUILayout.Button("▼", GUILayout.Width(18))) _events.MoveArrayElement(index, index + 1);
            if (GUILayout.Button("x", GUILayout.Width(18))) _events.DeleteArrayElementAtIndex(index);
            
            EditorGUILayout.EndHorizontal();
        }

        private static GUIContent GetHeaderEventName(in SerializedProperty evt)
        {
            var type = evt.FindPropertyRelative("_typeAction");
            var delay = evt.FindPropertyRelative("_typeRun");
            var isTypeAsCustom = type.enumNames[type.enumValueIndex] == TypeInitialization.Custom.ToString();
            var evtTag = evt.FindPropertyRelative("_tag").stringValue;
            var tag = isTypeAsCustom && !string.IsNullOrEmpty(evtTag) ? evtTag  : type.enumNames[type.enumValueIndex];
            return new GUIContent(tag + " :: " + delay.enumNames[delay.enumValueIndex]);
        }

        private void DrawButtons()
        {
            if (_events.arraySize > 0) return;
            if (!GUILayout.Button("Add Events Sequence")) return;
            
            _events.InsertArrayElementAtIndex(_events.arraySize);
        }
    }
}