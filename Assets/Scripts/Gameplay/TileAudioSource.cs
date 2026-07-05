using UnityEngine;
using GiAiJoe.Audio;

namespace GiAiJoe.Gameplay
{
    /// <summary>
    /// Wraps an AudioSource and plays procedurally generated tones.
    /// </summary>
    public class TileAudioSource : MonoBehaviour
    {
        private AudioSource audioSource;
        private AudioClip generatedToneClip;

        private void Start()
        {
            // Ensure an AudioSource component exists
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            // Generate the tone clip once
            generatedToneClip = AudioGeneratorUtility.GenerateToneClip();
        }

        /// <summary>
        /// Plays the generated tone once via PlayOneShot.
        /// </summary>
        public void PlayToneOnce()
        {
            if (audioSource == null)
            {
                Debug.LogError("AudioSource is not initialized on " + gameObject.name);
                return;
            }

            if (generatedToneClip == null)
            {
                generatedToneClip = AudioGeneratorUtility.GenerateToneClip();
            }

            audioSource.PlayOneShot(generatedToneClip);
        }
    }
}
