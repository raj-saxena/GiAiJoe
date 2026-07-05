using UnityEngine;
using System;
using System.Collections.Generic;

namespace GiAiJoe.Gameplay.AnimalHabitat
{
    /// <summary>
    /// A draggable animal sprite. Handles drag input via OnMouseDown/Drag/Up.
    /// Updates sorting layer during drag (Animals -> AnimalsInTransit).
    /// Detects drop zones on release and notifies them of the drop.
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
        private Vector3 startPosition;
        private bool isMatched = false;

        /// <summary>
        /// Fired when dragging starts.
        /// </summary>
        public event Action OnDragStarted;

        /// <summary>
        /// Fired when dragging ends.
        /// </summary>
        public event Action OnDragEnded;

        /// <summary>
        /// Fired when this draggable is matched to the correct drop zone.
        /// </summary>
        public event Action OnMatched;

        /// <summary>
        /// Fired when this draggable is incorrectly dropped (and will be returned to start position).
        /// </summary>
        public event Action OnIncorrectDrop;

        private void Awake()
        {
            // Initialize start position as early as possible (Awake runs before Start)
            InitializeStartPosition();
        }

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

        private void InitializeStartPosition()
        {
            // Only set once, in case this is called multiple times
            if (startPosition == Vector3.zero && transform.position != Vector3.zero)
            {
                startPosition = transform.position;
            }
            else if (startPosition == Vector3.zero)
            {
                startPosition = transform.position;
            }
        }

        public string GetAnimalId()
        {
            return animalId;
        }

        public Vector3 GetStartPosition()
        {
            return startPosition;
        }

        public bool IsMatched()
        {
            return isMatched;
        }

        public void SetMatched(bool matched)
        {
            isMatched = matched;
        }

        private void OnMouseDown()
        {
            // Don't allow dragging if already matched
            if (isMatched || !CanDrag())
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

            // Detect which drop zone (if any) this animal was dropped on
            DropZone dropZoneAtPosition = FindDropZoneAtPosition(transform.position);

            if (dropZoneAtPosition != null)
            {
                // Store reference to drop zone for use in match handlers
                // Notify the drop zone of the drop
                dropZoneAtPosition.HandleDrop(animalId, this);

                // Subscribe to the drop zone's response events
                dropZoneAtPosition.OnCorrectMatch += () => HandleCorrectMatch(dropZoneAtPosition);
                dropZoneAtPosition.OnWrongMatch += HandleWrongMatch;
            }
            else
            {
                // Dropped in empty space: return to start position
                ReturnToStartPosition();
            }

            OnDragEnded?.Invoke();
        }

        /// <summary>
        /// Finds a DropZone at the given world position using Physics2D overlap.
        /// Returns the first DropZone found, or null if none.
        /// </summary>
        private DropZone FindDropZoneAtPosition(Vector3 position)
        {
            // Use Physics2D.OverlapPoint to find colliders at this position
            Collider2D[] colliders = Physics2D.OverlapPointAll(new Vector2(position.x, position.y));

            foreach (Collider2D collider in colliders)
            {
                DropZone dropZone = collider.GetComponent<DropZone>();
                if (dropZone != null)
                {
                    return dropZone;
                }
            }

            return null;
        }

        private void HandleCorrectMatch(DropZone dropZone)
        {
            // Snap to drop zone position and mark as matched
            isMatched = true;
            transform.position = dropZone.transform.position;

            // Switch back to resting sorting layer
            if (spriteRenderer != null)
            {
                spriteRenderer.sortingLayerName = restingSortingLayer;
                spriteRenderer.sortingOrder = restingOrderInLayer;
            }

            OnMatched?.Invoke();
        }

        private void HandleWrongMatch()
        {
            // Return to start position
            ReturnToStartPosition();
        }

        private void ReturnToStartPosition()
        {
            // Instantly snap back to start position
            transform.position = startPosition;

            // Switch back to resting sorting layer
            if (spriteRenderer != null)
            {
                spriteRenderer.sortingLayerName = restingSortingLayer;
                spriteRenderer.sortingOrder = restingOrderInLayer;
            }

            OnIncorrectDrop?.Invoke();
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
