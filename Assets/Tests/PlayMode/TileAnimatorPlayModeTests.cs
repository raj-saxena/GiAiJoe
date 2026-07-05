using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using GiAiJoe.Gameplay;

[TestFixture]
public class TileAnimatorPlayModeTests
{
    [UnityTest]
    public IEnumerator PlayBounceAnimation_ScaleReturnsToBaselineInPlayMode()
    {
        // Arrange
        var go = new GameObject("TestTile");
        go.transform.localScale = Vector3.one;
        var animator = go.AddComponent<TileAnimator>();

        // Act - start the bounce animation
        animator.PlayBounceAnimation();

        // Wait for animation to complete
        yield return new WaitForSeconds(0.25f);

        // Assert - scale should return to baseline
        Assert.That(go.transform.localScale.x, Is.EqualTo(1.0f).Within(0.01f),
            "Scale X should return to 1.0 after bounce animation");
        Assert.That(go.transform.localScale.y, Is.EqualTo(1.0f).Within(0.01f),
            "Scale Y should return to 1.0 after bounce animation");

        // Cleanup
        Object.DestroyImmediate(go);
    }

    [UnityTest]
    public IEnumerator TileTapHandlerAndAnimatorIntegration()
    {
        // Arrange: Set up a minimal scene for tap testing
        var tileGo = new GameObject("IntegrationTestTile");
        tileGo.transform.position = Vector3.zero;

        // Add necessary components
        var collider = tileGo.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(2f, 2f);
        collider.isTrigger = true;

        var tapHandler = tileGo.AddComponent<TileTapHandler>();
        var animator = tileGo.AddComponent<TileAnimator>();
        var audioSource = tileGo.AddComponent<TileAudioSource>();

        bool tapDetected = false;
        tapHandler.OnTapDetected += () => { tapDetected = true; animator.PlayBounceAnimation(); };

        // Setup minimal camera
        var cameraGo = new GameObject("TestCamera");
        var camera = cameraGo.AddComponent<Camera>();
        cameraGo.tag = "MainCamera";
        camera.orthographic = true;

        yield return null;

        // Act: Trigger tap event
        tapHandler.SimulateTap();

        yield return null;

        // Assert: Tap should have been detected
        Assert.IsTrue(tapDetected, "Tap event should have been triggered");

        // Wait for animation
        yield return new WaitForSeconds(0.25f);

        // Assert: Scale should return to baseline
        Assert.That(tileGo.transform.localScale.x, Is.EqualTo(1.0f).Within(0.01f),
            "Scale should return to 1.0 after animation from tap");

        // Cleanup
        Object.DestroyImmediate(tileGo);
        Object.DestroyImmediate(cameraGo);
    }
}
