using UnityEngine;

namespace GiAiJoe.Gameplay
{
    /// <summary>
    /// Wires together the tile tap handler, animator, and audio source.
    /// When the tile is tapped, it bounces and plays a beep sound.
    /// </summary>
    public class TileController : MonoBehaviour
    {
        private TileTapHandler tapHandler;
        private TileAnimator animator;
        private TileAudioSource audioSource;

        private void Start()
        {
            // Get references to all components
            tapHandler = GetComponent<TileTapHandler>();
            animator = GetComponent<TileAnimator>();
            audioSource = GetComponent<TileAudioSource>();

            // Wire the tap handler event to trigger animation and audio
            if (tapHandler != null)
            {
                tapHandler.OnTapDetected += OnTileTapped;
            }

            if (tapHandler == null)
                Debug.LogError("TileTapHandler not found on " + gameObject.name, this);
            if (animator == null)
                Debug.LogError("TileAnimator not found on " + gameObject.name, this);
            if (audioSource == null)
                Debug.LogError("TileAudioSource not found on " + gameObject.name, this);
        }

        private void OnTileTapped()
        {
            if (animator != null)
            {
                animator.PlayBounceAnimation();
            }

            if (audioSource != null)
            {
                audioSource.PlayToneOnce();
            }
        }

        private void OnDestroy()
        {
            // Unsubscribe from event to avoid memory leaks
            if (tapHandler != null)
            {
                tapHandler.OnTapDetected -= OnTileTapped;
            }
        }
    }
}
