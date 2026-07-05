using UnityEngine;
using System.Collections;

namespace GiAiJoe.Gameplay
{
    /// <summary>
    /// Handles tile animation effects, particularly the bounce animation.
    /// </summary>
    public class TileAnimator : MonoBehaviour
    {
        private const float BounceAnimationDuration = 0.2f;
        private const float BounceScaleMax = 1.2f;

        private Coroutine bounceCoroutine;
        private Vector3? baseScale;

        /// <summary>
        /// Plays a bounce animation: scales 1.0 → 1.2 → 1.0 over ~0.2 seconds.
        /// If called while a bounce is in progress, restarts the animation.
        /// </summary>
        public void PlayBounceAnimation()
        {
            // Capture the resting scale once, before any bounce has moved it -
            // otherwise a restart mid-animation would treat the current
            // (still-animating) scale as the new baseline and never settle back.
            if (baseScale == null)
            {
                baseScale = transform.localScale;
            }

            // Stop any existing bounce coroutine
            if (bounceCoroutine != null)
            {
                StopCoroutine(bounceCoroutine);
            }

            // Start the new bounce animation
            bounceCoroutine = StartCoroutine(BounceAnimationCoroutine());
        }

        private IEnumerator BounceAnimationCoroutine()
        {
            Vector3 startScale = baseScale.Value;
            float elapsedTime = 0f;

            // Scale up phase: 0.0 → 0.5 * duration
            while (elapsedTime < BounceAnimationDuration * 0.5f)
            {
                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / (BounceAnimationDuration * 0.5f);

                // Interpolate from start scale to max scale
                float currentScale = Mathf.Lerp(startScale.x, BounceScaleMax, progress);
                transform.localScale = new Vector3(currentScale, currentScale, startScale.z);

                yield return null;
            }

            // Scale down phase: 0.5 * duration → 1.0 * duration
            elapsedTime = 0f;
            while (elapsedTime < BounceAnimationDuration * 0.5f)
            {
                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / (BounceAnimationDuration * 0.5f);

                // Interpolate from max scale back to start scale
                float currentScale = Mathf.Lerp(BounceScaleMax, startScale.x, progress);
                transform.localScale = new Vector3(currentScale, currentScale, startScale.z);

                yield return null;
            }

            // Ensure we end exactly at the start scale
            transform.localScale = startScale;

            bounceCoroutine = null;
        }
    }
}
