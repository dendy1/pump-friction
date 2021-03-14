using System;
using sdk_feed.Common.Extensions;
using UnityEditor;
using UnityEngine;

namespace sdk_feed.Common.Events
{
    [CustomEditor(typeof(FeedLoopedEvent))]
    [CanEditMultipleObjects]
    public class FeedLoopedEventEditor : Editor
    {
        private SerializedProperty _typeInitialization;
        private SerializedProperty _delayInitialization;
        private SerializedProperty _minSwipeLengthByX;
        private SerializedProperty _minSwipeLengthByY;
        private SerializedProperty _event;


        private void OnEnable()
        {
            _typeInitialization = serializedObject.FindProperty(nameof(_typeInitialization));
            _delayInitialization = serializedObject.FindProperty(nameof(_delayInitialization));
            _minSwipeLengthByX = serializedObject.FindProperty(nameof(_minSwipeLengthByX));
            _minSwipeLengthByY = serializedObject.FindProperty(nameof(_minSwipeLengthByY));
            _event = serializedObject.FindProperty(nameof(_event));
        }

        public override void OnInspectorGUI()
        {
            this.Header();
            DrawWarning();
            
            serializedObject.Update();
            EditorGUILayout.PropertyField(_typeInitialization);
            EditorGUILayout.PropertyField(_delayInitialization);
            DrawSwipeParam();
            EditorGUILayout.PropertyField(_event);
            if (serializedObject.hasModifiedProperties) serializedObject.ApplyModifiedProperties();
        }

        private void DrawWarning()
        {
            var obj = (AbstractEvent) serializedObject.targetObject;
            var isObjectContainCollider = obj.gameObject.GetComponent<Collider>() != null;
            
            if (!isObjectContainCollider && IsTypeShouldHaveCollider()) 
                EditorGUILayout.HelpBox("The object must have a collider", MessageType.Error);
        }

        private bool IsTypeShouldHaveCollider()
        {
            return IsTypeNameContain("Swipe") || IsTypeNameContain("Trigger") || IsTypeNameContain("Mouse");
        }

        private void DrawSwipeParam()
        {
            if (!IsTypeNameContain("Swipe")) return;
            
            EditorGUILayout.PropertyField(_minSwipeLengthByX);
            EditorGUILayout.PropertyField(_minSwipeLengthByY);
        }

        private bool IsTypeNameContain(string part)
        {
            return _typeInitialization.enumNames[_typeInitialization.enumValueIndex].IndexOf(part, StringComparison.Ordinal) != -1;
        }
    }
}