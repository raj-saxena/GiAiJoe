using UnityEngine;

namespace GiAiJoe.Audio
{
    /// <summary>
    /// Utility for procedurally generating audio clips without external asset dependencies.
    /// </summary>
    public static class AudioGeneratorUtility
    {
        private const float ToneFrequency = 440f; // A4 note in Hz
        private const float ToneDuration = 0.1f; // 100ms
        private const int SampleRate = 44100;

        /// <summary>
        /// Generates a sine-wave AudioClip with a tone at the specified frequency lasting ~0.1 seconds.
        /// </summary>
        /// <param name="frequency">Frequency in Hz (default 440 Hz = A4 note). Pass null to use default.</param>
        /// <returns>A new AudioClip containing the procedurally generated tone.</returns>
        public static AudioClip GenerateToneClip(float? frequency = null)
        {
            float freq = frequency ?? ToneFrequency;
            int sampleCount = (int)(SampleRate * ToneDuration);

            // Create the audio clip
            AudioClip clip = AudioClip.Create(
                "GeneratedTone",
                sampleCount,
                1, // Mono
                SampleRate,
                false // Not a stream
            );

            // Generate sine-wave sample data
            float[] samples = new float[sampleCount];
            for (int i = 0; i < sampleCount; i++)
            {
                float time = (float)i / SampleRate;
                samples[i] = Mathf.Sin(2f * Mathf.PI * freq * time);
            }

            // Apply a simple fade-out to avoid clicks
            int fadeOutStart = sampleCount - (int)(0.02f * SampleRate); // Last 20ms
            for (int i = fadeOutStart; i < sampleCount; i++)
            {
                float fadeProgress = (float)(i - fadeOutStart) / (sampleCount - fadeOutStart);
                samples[i] *= (1f - fadeProgress);
            }

            // Set the sample data
            clip.SetData(samples, 0);

            return clip;
        }
    }
}
