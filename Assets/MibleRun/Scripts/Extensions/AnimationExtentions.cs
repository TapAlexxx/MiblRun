using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Extensions
{
    public class AnimationExtensions : MonoBehaviour
    {
        public static IEnumerator Ripple(Transform transform, MonoBehaviour mono, float duration, float newSize, float oldSize = 1f)
        {
            transform.localScale = new Vector3(oldSize, oldSize, 1f);
            
            var targetScaleValue = transform.localScale.x * newSize;

            mono.StartCoroutine(ScalingUp(transform, targetScaleValue, duration));
            yield return new WaitForSeconds(0.15f);

            duration = transform.localScale.x / -2;
            targetScaleValue = oldSize;
            
            mono.StartCoroutine(ScalingDown(transform, targetScaleValue, duration));
        }

        public static IEnumerator ScalingUp(Transform transform, float targetScaleValue = 2f, float duration = 1)
        {
            Vector3 targetScale = new Vector3(targetScaleValue, targetScaleValue, targetScaleValue);
            
            while (transform.localScale.x < targetScale.x)
            {
                Scaling(transform, targetScale, duration);
                yield return null;
            }
        }

        public static IEnumerator ScalingDown(Transform transform, float targetScaleValue = 0.5f,  float duration = -1)
        {
            Vector3 targetScale = new Vector3(targetScaleValue, targetScaleValue, targetScaleValue);
            
            while (transform.localScale.x > targetScale.x)
            {
                Scaling(transform, targetScale, duration);
                yield return null;
            }
        }
    
        private static void Scaling(Transform transform, Vector3 targetScale, float duration)
        {
            transform.localScale = new Vector3(
                Mathf.SmoothDamp(transform.localScale.x, targetScale.x, ref duration, Time.deltaTime),
                Mathf.SmoothDamp(transform.localScale.y, targetScale.y, ref duration, Time.deltaTime), 
                1);
        }
    
        public static IEnumerator Motion(Transform transform, Vector3 targetPosition, float duration = 40f)
        {
            Vector3 currentPosition = transform.localPosition;
            double tolerance = 0.001f;
        
            while(Math.Abs(transform.localPosition.x - targetPosition.x) > tolerance ||
                  Math.Abs(transform.localPosition.y - targetPosition.y) > tolerance)
            {
                transform.localPosition = Vector3.MoveTowards(currentPosition, targetPosition, duration);
                currentPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
                yield return null;
            }
        }
    }
}
