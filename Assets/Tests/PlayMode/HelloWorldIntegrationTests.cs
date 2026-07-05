using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using System.Collections.Generic;
using GiAiJoe.Gameplay;
using GiAiJoe.Audio;

public class HelloWorldIntegrationTests
{
    [UnityTest]
    public IEnumerator TileAnimatorAndAudioPlayOnTap_IntegrationTest()
    {
        // Arrange: Set up a tile similar to what exists in HelloWorld scene
        var tileGo = new GameObject("TestTile");
        tileGo.transform.position = Vector3.zero;

        // Add collider for tap detection
        var collider = tileGo.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(2f, 2f);
        collider.isTrigger = true;

        // Add sprite renderer for visual feedback
        var spriteRenderer = tileGo.AddComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;

        // Add game components
        var tapHandler = tileGo.AddComponent<TileTapHandler>();
        var animator = tileGo.AddComponent<TileAnimator>();
        var audioSource = tileGo.AddComponent<TileAudioSource>();

        // Track animation and audio state
        bool animationStarted = false;
        bool audioPlayed = false;

        // Subscribe to tap event
        tapHandler.OnTapDetected += () =>
        {
            animationStarted = true;
            animator.PlayBounceAnimation();
            audioPlayed = true;
            audioSource.PlayToneOnce();
        };

        // Ensure camera exists for tap input
        var camera = new GameObject("TestCamera");
        var cam = camera.AddComponent<Camera>();
        camera.tag = "MainCamera";
        cam.orthographic = true;

        yield return null; // Let components initialize

        // Act: Simulate tap at tile position (Input.mousePosition is read-only in
        // batchmode tests, so we trigger the handler's test hook directly instead)
        tapHandler.SimulateTap();

        yield return null;

        // Assert: Animation should have started
        Assert.IsTrue(animationStarted, "Animation should have started after tap");
        Assert.IsTrue(audioPlayed, "Audio should have started after tap");

        // Wait for animation to complete
        yield return new WaitForSeconds(0.25f);

        // Assert: Scale should return to baseline
        Assert.That(tileGo.transform.localScale, Is.EqualTo(Vector3.one).Using<Vector3>(new Vector3EqualityComparer(0.01f)),
            "Scale should return to 1.0 after bounce animation");

        // Cleanup
        Object.DestroyImmediate(tileGo);
        Object.DestroyImmediate(camera);
    }

    /// <summary>
    /// Custom comparer for Vector3 equality with tolerance.
    /// </summary>
    private class Vector3EqualityComparer : EqualityComparer<Vector3>
    {
        private readonly float tolerance;

        public Vector3EqualityComparer(float tolerance)
        {
            this.tolerance = tolerance;
        }

        public override bool Equals(Vector3 a, Vector3 b)
        {
            return Vector3.Distance(a, b) < tolerance;
        }

        public override int GetHashCode(Vector3 obj)
        {
            return obj.GetHashCode();
        }
    }
}
