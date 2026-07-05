using UnityEngine;
using System;

namespace GiAiJoe.Gameplay.AnimalHabitat
{
    /// <summary>
    /// A draggable animal sprite. Handles drag input via OnMouseDown/Drag/Up.
    /// Updates sorting layer during drag (Animals -> AnimalsInTransit).
    /// </summary>
    public class Draggable : MonoBehaviour
    {
        [SerializeField]
        private string animalId;

        [SerializeField]
        private string restingSortingLayer = "Animals";

        [SerializeField]
        private int restingOrderInLayer = 40;

        [SerializeField]
        private string dragSortingLayer = "AnimalsInTransit";

        [SerializeField]
        private int dragOrderInLayer = 100;

        private SpriteRenderer spriteRenderer;
        private Collider2D draggableCollider;
        private Camera mainCamera;
        private bool isDragging = false;
        private Vector3 dragOffset;

        /// <summary>
        /// Fired when dragging starts.
        /// </summary>
        public event Action OnDragStarted;

        /// <summary>
        /// Fired when dragging ends.
        /// </summary>
        public event Action OnDragEnded;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            draggableCollider = GetComponent<Collider2D>();
            mainCamera = Camera.main;

            if (spriteRenderer == null)
            {
                Debug.LogError($"Draggable on {gameObject.name} requires a SpriteRenderer component", this);
            }

            if (draggableCollider == null)
            {
                Debug.LogError($"Draggable on {gameObject.name} requires a Collider2D component", this);
            }
        }

        public string GetAnimalId()
        {
            return animalId;
        }

        private void OnMouseDown()
        {
            if (!CanDrag())
                return;

            isDragging = true;

            // Switch to dragging sorting layer
            if (spriteRenderer != null)
            {
                spriteRenderer.sortingLayerName = dragSortingLayer;
                spriteRenderer.sortingOrder = dragOrderInLayer;
            }

            // Calculate offset from mouse position to sprite center
            Vector3 mouseWorldPos = GetMouseWorldPosition();
            dragOffset = transform.position - mouseWorldPos;

            OnDragStarted?.Invoke();
        }

        private void OnMouseDrag()
        {
            if (!isDragging)
                return;

            // Move with the mouse
            Vector3 mouseWorldPos = GetMouseWorldPosition();
            Vector3 newPosition = mouseWorldPos + dragOffset;
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }

        private void OnMouseUp()
        {
            if (!isDragging)
                return;

            isDragging = false;

            // Switch back to resting sorting layer
            if (spriteRenderer != null)
            {
                spriteRenderer.sortingLayerName = restingSortingLayer;
                spriteRenderer.sortingOrder = restingOrderInLayer;
            }

            OnDragEnded?.Invoke();
        }

        private bool CanDrag()
        {
            return mainCamera != null && draggableCollider != null;
        }

        private Vector3 GetMouseWorldPosition()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f;
            return mainCamera.ScreenToWorldPoint(mousePos);
        }
    }
}
