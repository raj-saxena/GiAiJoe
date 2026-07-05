using UnityEngine;
using System;

namespace GiAiJoe.Gameplay
{
    /// <summary>
    /// Detects tap/click input on a tile via its Collider2D.
    /// Exposes OnTapDetected event that fires when the tile is tapped.
    /// Works with mouse input (editor) and touch-simulates-mouse on device.
    /// </summary>
    public class TileTapHandler : MonoBehaviour
    {
        /// <summary>
        /// Fired when the tile is tapped/clicked.
        /// </summary>
        public event Action OnTapDetected;

        private Collider2D tileCollider;
        private Camera mainCamera;

        private void Start()
        {
            tileCollider = GetComponent<Collider2D>();
            mainCamera = Camera.main;

            if (tileCollider == null)
            {
                Debug.LogError($"TileTapHandler on {gameObject.name} requires a Collider2D component", this);
            }

            if (mainCamera == null)
            {
                Debug.LogError("No main camera found in scene", this);
            }
        }

        private void Update()
        {
            // Handle mouse input (works as touch-simulates-mouse on device for this spike)
            if (Input.GetMouseButtonDown(0))
            {
                HandleTapInput();
            }
        }

        private void HandleTapInput()
        {
            if (tileCollider == null || mainCamera == null)
                return;

            // Convert mouse position to world point
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f; // Arbitrary distance from camera
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);

            // Check if the tap hit this tile's collider
            Collider2D hit = Physics2D.OverlapPoint(new Vector2(worldPos.x, worldPos.y));

            if (hit != null && hit == tileCollider)
            {
                OnTapDetected?.Invoke();
            }
        }

        /// <summary>
        /// Raises OnTapDetected directly, bypassing real input. Events can't be
        /// invoked from outside their declaring class, so tests use this instead.
        /// </summary>
        public void SimulateTap()
        {
            OnTapDetected?.Invoke();
        }
    }
}
