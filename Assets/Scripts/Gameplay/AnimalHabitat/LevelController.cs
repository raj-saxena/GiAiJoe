using UnityEngine;
using System;
using System.Collections.Generic;

namespace GiAiJoe.Gameplay.AnimalHabitat
{
    /// <summary>
    /// Main controller for an animal-habitat matching level.
    /// Wires together Draggable animals, DropZones, and tracks level completion.
    /// </summary>
    public class LevelController : MonoBehaviour
    {
        [SerializeField]
        private LevelDefinition levelDefinition;

        [SerializeField]
        private Transform animalsContainer;

        [SerializeField]
        private Transform habitatsContainer;

        private List<DropZone> dropZones = new List<DropZone>();
        private List<Draggable> draggables = new List<Draggable>();
        private int correctMatchCount = 0;
        private bool levelCompleted = false;

        /// <summary>
        /// Fired when all animal-habitat pairs are correctly matched.
        /// </summary>
        public event Action OnLevelComplete;

        private void Start()
        {
            // Auto-discover drop zones and draggables in the scene if not already initialized
            if (dropZones.Count == 0)
            {
                AutoDiscoverComponents();
                WireUpEventListeners();
            }
        }

        /// <summary>
        /// Initializes the level controller with a predefined list of DropZones.
        /// Used for testing or when components are pre-placed in the scene.
        /// </summary>
        public void Initialize(List<DropZone> zones)
        {
            dropZones = zones;
            correctMatchCount = 0;
            levelCompleted = false;
            WireUpEventListeners();
        }

        private void AutoDiscoverComponents()
        {
            // Find all DropZones in the scene
            dropZones.AddRange(FindObjectsOfType<DropZone>());

            // Find all Draggables in the scene
            draggables.AddRange(FindObjectsOfType<Draggable>());

            // If we have a level definition, set up the expected animal IDs on drop zones
            if (levelDefinition != null && dropZones.Count == levelDefinition.pairs.Count)
            {
                for (int i = 0; i < dropZones.Count; i++)
                {
                    dropZones[i].SetExpectedAnimalId(levelDefinition.pairs[i].animalId);
                }
            }
        }

        private void WireUpEventListeners()
        {
            foreach (var dropZone in dropZones)
            {
                dropZone.OnCorrectMatch += HandleCorrectMatch;
                dropZone.OnWrongMatch += HandleWrongMatch;
            }
        }

        private void HandleCorrectMatch()
        {
            correctMatchCount++;

            // Check if all pairs are now matched
            if (correctMatchCount >= dropZones.Count && !levelCompleted)
            {
                levelCompleted = true;
                OnLevelComplete?.Invoke();
            }
        }

        private void HandleWrongMatch()
        {
            // Wrong match doesn't advance the counter; just a redirect animation occurs
            // (animation logic is handled elsewhere; this just tracks the logic)
        }

        public int GetCorrectMatchCount()
        {
            return correctMatchCount;
        }

        public bool IsLevelComplete()
        {
            return levelCompleted;
        }

        /// <summary>
        /// Gets the total number of animal-habitat pairs in this level.
        /// </summary>
        public int GetTotalPairs()
        {
            return dropZones.Count;
        }
    }
}
