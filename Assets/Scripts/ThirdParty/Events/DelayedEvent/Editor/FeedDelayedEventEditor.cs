using System;
using sdk_feed.Common.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace sdk_feed.Common.Events
{
    [CustomEditor(typeof(FeedDelayedEvent))]
    [CanEditMultipleObjects]
    public class FeedDelayedEventEditor : Editor
    {
        private SerializedProperty _typeInitialization;
        private SerializedProperty _delayInitialization;
        private SerializedProperty _minSwipeLengthByX;
        private SerializedProperty _minSwipeLengthByY;
        private SerializedProperty _isUseTag;
        private SerializedProperty _tag;
        private SerializedProperty _event;


        private void OnEnable()
        {
            _typeInitialization = serializedObject.FindProperty(nameof(_typeInitialization));
            _delayInitialization = serializedObject.FindProperty(nameof(_delayInitialization));
            _minSwipeLengthByX = serializedObject.FindProperty(nameof(_minSwipeLengthByX));
            _minSwipeLengthByY = serializedObject.FindProperty(nameof(_minSwipeLengthByY));
            _isUseTag = serializedObject.FindProperty(nameof(_isUseTag));
            _tag = serializedObject.FindProperty(nameof(_tag));
            _event = serializedObject.FindProperty(nameof(_event));
        }

        public override void OnInspectorGUI()
        {
            this.Header();
            DrawWarning();
            serializedObject.Update();
            EditorGUILayout.PropertyField(_typeInitialization);
            EditorGUILayout.PropertyField(_delayInitialization);
            DrawAdditionalParam();
            EditorGUILayout.PropertyField(_event);
            if (serializedObject.hasModifiedProperties) serializedObject.ApplyModifiedProperties();
        }

        private void DrawWarning()
        {
            var obj = (AbstractEvent) serializedObject.targetObject;
            var isObjectContainCollider = obj.gameObject.GetComponent<Collider>() != null || obj.gameObject.GetComponent<Collider2D>() != null;
            var isObjectContainImage = obj.gameObject.GetComponent<Image>() != null;
            
            if (!(isObjectContainImage || isObjectContainCollider) && IsTypeShouldHaveCollider()) 
                EditorGUILayout.HelpBox("The object must have a collider or image", MessageType.Error);
        }

        private bool IsTypeShouldHaveCollider()
        {
            return IsTypeNameContain("Swipe") || IsTypeNameContain("Trigger") || IsTypeNameContain("Mouse");
        }

        private void DrawAdditionalParam()
        {
            if (IsTypeNameContain("Swipe"))
            {
                DrawSwipeParams();
            }
            else if (IsTypeNameContain("OnTriggerEnter") || IsTypeNameContain("OnTriggerExit"))
            {
                DrawTriggerParams();
            }
        }

        private bool IsTypeNameContain(string part)
        {
            return _typeInitialization.enumNames[_typeInitialization.enumValueIndex].IndexOf(part, StringComparison.Ordinal) != -1;
        }

        private void DrawSwipeParams()
        {
            EditorGUILayout.PropertyField(_minSwipeLengthByX);
            EditorGUILayout.PropertyField(_minSwipeLengthByY);
        }
		private void DrawTriggerParams()
		{
            if (!_isUseTag.boolValue)
            {
                _isUseTag.boolValue = EditorGUILayout.ToggleLeft("Set specific tag", _isUseTag.boolValue);
            }
            else
            {
                EditorGUILayout.BeginHorizontal();
                _isUseTag.boolValue = EditorGUILayout.ToggleLeft("Tag", _isUseTag.boolValue);
                _tag.stringValue = EditorGUILayout.TagField(_tag.stringValue);
                EditorGUILayout.EndHorizontal();
            }
		}
    }
}