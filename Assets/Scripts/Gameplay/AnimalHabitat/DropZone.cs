using UnityEngine;
using UnityEngine.Events;
using System;

namespace GiAiJoe.Gameplay.AnimalHabitat
{
    /// <summary>
    /// A drop zone for a specific animal-habitat pair.
    /// Listens for drops and fires OnCorrectMatch or OnWrongMatch depending on whether
    /// the dropped animal's ID matches the expected ID.
    /// On correct match, moves the draggable to this zone's position.
    /// On wrong match, the draggable will be returned to its start position by the Draggable itself.
    /// </summary>
    public class DropZone : MonoBehaviour
    {
        [SerializeField]
        private string expectedAnimalId;

        /// <summary>
        /// Fired when a Draggable with the correct AnimalId is dropped here.
        /// The Draggable will lock to this position after this event fires.
        /// </summary>
        public event Action OnCorrectMatch;

        /// <summary>
        /// Fired when a Draggable with the wrong AnimalId is dropped here.
        /// The Draggable will return to its start position after this event fires.
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
        /// <param name="droppedAnimalId">The ID of the animal being dropped</param>
        /// <param name="draggable">The Draggable component that was dropped (optional, for future extensions)</param>
        public void HandleDrop(string droppedAnimalId, Draggable draggable = null)
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
