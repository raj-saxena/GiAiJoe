using UnityEngine;
using GiAiJoe.Audio;

namespace GiAiJoe.Gameplay.AnimalHabitat
{
    /// <summary>
    /// Manages audio cues for Animal Habitat levels.
    /// Subscribes to LevelController and DropZone events to play appropriate audio feedback.
    /// </summary>
    public class LevelAudioCues : MonoBehaviour
    {
        [SerializeField]
        private AudioSource correctChimeSource;

        [SerializeField]
        private AudioSource wrongDropSource;

        [SerializeField]
        private AudioSource ambientLoopSource;

        [SerializeField]
        private AudioSource levelCompleteSource;

        [SerializeField]
        private LevelController levelController;

        private void Start()
        {
            // Generate placeholder audio clips (all use procedurally generated tones at different pitches)
            // Correct chime: higher pitch (523 Hz = C5)
            if (correctChimeSource != null && correctChimeSource.clip == null)
            {
                correctChimeSource.clip = AudioGeneratorUtility.GenerateToneClip(523f);
            }

            // Wrong drop reaction: descending pitch (349 Hz = F4)
            if (wrongDropSource != null && wrongDropSource.clip == null)
            {
                wrongDropSource.clip = AudioGeneratorUtility.GenerateToneClip(349f);
            }

            // Ambient loop: low, gentle pitch (220 Hz = A3)
            if (ambientLoopSource != null && ambientLoopSource.clip == null)
            {
                ambientLoopSource.clip = AudioGeneratorUtility.GenerateToneClip(220f);
                ambientLoopSource.loop = true;
                ambientLoopSource.volume = 0.3f; // Keep ambient low
                ambientLoopSource.Play();
            }

            // Level complete fanfare: triumphant high pitch (659 Hz = E5)
            if (levelCompleteSource != null && levelCompleteSource.clip == null)
            {
                levelCompleteSource.clip = AudioGeneratorUtility.GenerateToneClip(659f);
            }

            // Wire up event listeners if LevelController is set
            if (levelController != null)
            {
                levelController.OnLevelComplete += PlayLevelCompleteFanfare;
            }

            // Find and subscribe to all DropZones in the scene
            DropZone[] dropZones = FindObjectsOfType<DropZone>();
            foreach (var dropZone in dropZones)
            {
                dropZone.OnCorrectMatch += PlayCorrectChime;
                dropZone.OnWrongMatch += PlayWrongDropReaction;
            }
        }

        private void PlayCorrectChime()
        {
            if (correctChimeSource != null)
            {
                correctChimeSource.PlayOneShot(correctChimeSource.clip);
            }
        }

        private void PlayWrongDropReaction()
        {
            if (wrongDropSource != null)
            {
                wrongDropSource.PlayOneShot(wrongDropSource.clip);
            }
        }

        private void PlayLevelCompleteFanfare()
        {
            if (levelCompleteSource != null)
            {
                levelCompleteSource.PlayOneShot(levelCompleteSource.clip);
            }
        }

        private void OnDestroy()
        {
            // Unsubscribe from events
            if (levelController != null)
            {
                levelController.OnLevelComplete -= PlayLevelCompleteFanfare;
            }

            DropZone[] dropZones = FindObjectsOfType<DropZone>();
            foreach (var dropZone in dropZones)
            {
                dropZone.OnCorrectMatch -= PlayCorrectChime;
                dropZone.OnWrongMatch -= PlayWrongDropReaction;
            }
        }
    }
}
