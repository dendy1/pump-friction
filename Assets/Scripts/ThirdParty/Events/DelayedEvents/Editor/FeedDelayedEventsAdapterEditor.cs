using sdk_feed.Common.Extensions;
using UnityEditor;
using UnityEngine;

namespace sdk_feed.Common.Events.DelayedEvents
{
    [CustomEditor(typeof(FeedDelayedEventsAdapter), true)]
    [CanEditMultipleObjects]
    public class FeedDelayedEventsAdapterEditor  : Editor
    {
        private SerializedProperty _searchType;
        private SerializedProperty _nameToSearch;
        private SerializedProperty _firstSearchDelay;
        private SerializedProperty _isSearchEveryInvoke;
        
        
        private void OnEnable()
        {
            _searchType = serializedObject.FindProperty("_searchType");
            _nameToSearch = serializedObject.FindProperty("_nameToSearch");
            _firstSearchDelay = serializedObject.FindProperty("_firstSearchDelay");
            _isSearchEveryInvoke = serializedObject.FindProperty("_isSearchEveryInvoke");
        }

        public override void OnInspectorGUI()
        {
            this.Header();
            
			serializedObject.Update();
            EditorGUILayout.PropertyField(_searchType);
            DrawNameToSearch();
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(_firstSearchDelay);
            EditorGUILayout.PropertyField(_isSearchEveryInvoke);

            if (serializedObject.hasModifiedProperties) serializedObject.ApplyModifiedProperties();
        }

        private void DrawNameToSearch()
        {
            var type = (SearchType) _searchType.enumValueIndex;
            if (type == SearchType.ManyObjectsByThisParent) return;
            
            var label = type == SearchType.FirstObjectByName || type == SearchType.ManyObjectByName ? "Object name" : "Parent name";
            EditorGUILayout.PropertyField(_nameToSearch, new GUIContent(label));
        }
    }
}