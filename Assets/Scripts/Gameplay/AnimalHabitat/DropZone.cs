using UnityEngine;
using UnityEngine.Events;
using System;

namespace GiAiJoe.Gameplay.AnimalHabitat
{
    /// <summary>
    /// A drop zone for a specific animal-habitat pair.
    /// Listens for drops and fires OnCorrectMatch or OnWrongMatch depending on whether
    /// the dropped animal's ID matches the expected ID.
    /// </summary>
    public class DropZone : MonoBehaviour
    {
        [SerializeField]
        private string expectedAnimalId;

        /// <summary>
        /// Fired when a Draggable with the correct AnimalId is dropped here.
        /// </summary>
        public event Action OnCorrectMatch;

        /// <summary>
        /// Fired when a Draggable with the wrong AnimalId is dropped here.
        /// </summary>
        public event Action OnWrongMatch;

        public void SetExpectedAnimalId(string animalId)
        {
            expectedAnimalId = animalId;
        }

        public string GetExpectedAnimalId()
        {
            return expectedAnimalId;
        }

        /// <summary>
        /// Called when a Draggable is dropped on this zone.
        /// Compares the dropped animal's ID with the expected ID and fires appropriate event.
        /// </summary>
        public void HandleDrop(string droppedAnimalId)
        {
            if (droppedAnimalId == expectedAnimalId)
            {
                OnCorrectMatch?.Invoke();
            }
            else
            {
                OnWrongMatch?.Invoke();
            }
        }
    }
}
