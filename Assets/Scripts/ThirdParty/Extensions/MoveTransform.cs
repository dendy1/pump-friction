using System.Collections;
using UnityEngine;

namespace sdk_feed.Common.Extensions
{
	internal delegate void SetPosition(float t);
	
    public static class MoveTransform
    {
        public static IEnumerator Move(this UnityEngine.Transform transform, Vector3 position, float duration)
        {
	        yield return transform.Move(position, duration, null, false);
        }
        
        public static IEnumerator Move(this UnityEngine.Transform transform, Vector3 position, AnimationCurve curve, float duration)
        {
	        yield return transform.Move(position, duration, curve, false);
        }
        
        public static IEnumerator Move(this UnityEngine.Transform transform, Vector3 position, float duration, bool isOnTop)
        {
	        yield return transform.Move(position, duration, null, isOnTop);
        }
        
        public static IEnumerator Move(this UnityEngine.Transform transform, Vector3 position, float duration, AnimationCurve curve, bool isOnTop)
        {
	        SetPosition setPos = t => transform.position = Vector3.Lerp(transform.position, position, curve?.Evaluate(t) ?? t);
	        yield return transform.Move(setPos, duration, isOnTop);
        }
        
        public static IEnumerator MoveByLocalCoordinates(this UnityEngine.Transform transform, Vector3 position, float duration, AnimationCurve curve, bool isOnTop)
        {
	        SetPosition setPos = t => transform.localPosition = Vector3.Lerp(transform.localPosition, position, curve?.Evaluate(t) ?? t);
	        yield return transform.Move(setPos, duration, isOnTop);
        }

        public static IEnumerator MoveBezier(this UnityEngine.Transform transform, Vector3 position, Vector3 delta, float duration)
        {
	        yield return transform.MoveBezier(position, delta, duration, null, false);
        }

        public static IEnumerator MoveBezier(this UnityEngine.Transform transform, Vector3 position, Vector3 delta, AnimationCurve curve, float duration)
        {
	        yield return transform.MoveBezier(position, delta, duration, curve, false);
        }

        public static IEnumerator MoveBezier(this UnityEngine.Transform transform, Vector3 position, Vector3 delta, float duration, bool isOnTop)
        {
	        yield return transform.MoveBezier(position, delta, duration, null, isOnTop);
        }
        
        public static IEnumerator MoveBezier(this UnityEngine.Transform transform, Vector3 position, Vector3 delta, float duration, AnimationCurve curve, bool isOnTop)
        {
	        SetPosition setPos = t => transform.position = GetQuadBezierPoint(transform.position, delta, position, curve?.Evaluate(t) ?? t);
	        yield return transform.Move(setPos, duration, isOnTop);
        }
        
        public static IEnumerator MoveBezierByLocalCoordinates(this UnityEngine.Transform transform, Vector3 position, Vector3 delta, float duration, AnimationCurve curve, bool isOnTop)
        {
	        SetPosition setPos = t => transform.localPosition = GetQuadBezierPoint(transform.localPosition, delta, position, curve?.Evaluate(t) ?? t);
	        yield return transform.Move(setPos, duration, isOnTop);
        }
        
        public static IEnumerator MoveByAnchoredPosition(this RectTransform transform, Vector3 position, float duration, AnimationCurve curve, bool isOnTop)
        {
	        SetPosition setPos = t => transform.anchoredPosition = Vector3.Lerp(transform.anchoredPosition, position, curve?.Evaluate(t) ?? t);
	        yield return transform.Move(setPos, duration, isOnTop);
        }
        
        public static IEnumerator MoveBezierByAnchoredPosition(this RectTransform transform, Vector3 position, Vector3 delta, float duration, AnimationCurve curve, bool isOnTop)
        {
	        SetPosition setPos = t => transform.anchoredPosition = GetQuadBezierPoint(transform.anchoredPosition, delta, position, curve?.Evaluate(t) ?? t);
	        yield return transform.Move(setPos, duration, isOnTop);
        }
        
		private static Vector3 GetQuadBezierPoint(Vector3 startPoint, Vector3 deltaPoint, Vector3 endPoint, float t)
        {
            var invT = 1 - t;
            return (invT * invT * startPoint) + (2 * invT * t * deltaPoint) + (t * t * endPoint);
        }

        private static IEnumerator Move(this UnityEngine.Transform transform, SetPosition setPosition, float duration, bool isOnTop)
        {
	        var siblingIndex = transform.GetSiblingIndex();
	        if (isOnTop)
	        {
		        transform.SetAsLastSibling();
	        }
	        yield return Move(setPosition, duration);
	        transform.SetSiblingIndex(siblingIndex);
        }
        
        private static IEnumerator Move(SetPosition setPosition, float duration)
        {
			var startTime = Time.realtimeSinceStartup;
			var fraction = 0.0f;
			while(fraction < 1f)
			{
				fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / duration);
				setPosition(fraction);
	            yield return new WaitForEndOfFrame();
			}
        }
    }
}