using System.Collections;
using UnityEngine;

namespace sdk_feed.Common.Extensions
{
    public static class AudioSourceExtensions
    {
        public static IEnumerator FadeOut(this AudioSource a, float duration)
        {
            var startVolume = a.volume;
 
            while (a.volume > 0)
            {
                a.volume -= startVolume * Time.deltaTime / duration;
                yield return new WaitForEndOfFrame();
            }
 
            a.Stop();
            a.volume = startVolume;
        }
        
        public static IEnumerator FadeIn(this AudioSource a, float duration)
        {
            var startVolume = a.volume;
            a.volume = 0;
            a.Play();
            
            var startTime = Time.realtimeSinceStartup;
            var fraction = 0.0f;
            while(fraction < 1f)
            {
                fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / duration);
                a.volume = startVolume * fraction;
                yield return new WaitForEndOfFrame();
            }
 
            a.volume = startVolume;
        }
    }
}