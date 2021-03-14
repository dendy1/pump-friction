using UnityEditor;
using UnityEngine;

namespace sdk_feed.Common.Extensions
{
     public static class  EditorHeader
    {
        public static void Header(this Editor property)
        {
            var script = MonoScript.FromMonoBehaviour((MonoBehaviour)property.target);
            EditorGUI.BeginDisabledGroup(true);
            // ReSharper disable once RedundantAssignment
            script = EditorGUILayout.ObjectField("Script:", script, typeof(MonoScript), false) as MonoScript;
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.Space();
        }
        
    }
}