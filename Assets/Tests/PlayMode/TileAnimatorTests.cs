using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using System.Collections.Generic;
using GiAiJoe.Gameplay;

public class TileAnimatorTests
{
    [UnityTest]
    public IEnumerator PlayBounceAnimation_ScaleReturnsToBaseline()
    {
        // Arrange
        var go = new GameObject();
        var animator = go.AddComponent<TileAnimator>();
        var initialScale = go.transform.localScale;

        // Act - start the bounce animation
        animator.PlayBounceAnimation();

        // Wait for animation to complete (slightly more than 0.2s)
        yield return new WaitForSeconds(0.25f);

        // Assert - scale should return to baseline
        Assert.That(go.transform.localScale, Is.EqualTo(initialScale).Using<Vector3>(new Vector3EqualityComparer(0.01f)),
            "Scale should return to baseline after bounce animation");

        // Cleanup
        Object.DestroyImmediate(go);
    }

    [UnityTest]
    public IEnumerator PlayBounceAnimation_ScaleExceedsBaselineDuringAnimation()
    {
        // Arrange
        var go = new GameObject();
        go.transform.localScale = Vector3.one;
        var animator = go.AddComponent<TileAnimator>();

        // Act - start the bounce animation and check midway through
        animator.PlayBounceAnimation();
        yield return new WaitForSeconds(0.1f); // Midway through 0.2s animation

        // Assert - scale should be larger than 1.0
        float currentScale = go.transform.localScale.x;
        Assert.Greater(currentScale, 1.0f, "Scale should exceed 1.0 during bounce animation");

        // Cleanup
        yield return new WaitForSeconds(0.15f); // Let animation finish
        Object.DestroyImmediate(go);
    }

    [UnityTest]
    public IEnumerator PlayBounceAnimation_CanBeCalledAgainBeforeCompletion()
    {
        // Arrange
        var go = new GameObject();
        go.transform.localScale = Vector3.one;
        var animator = go.AddComponent<TileAnimator>();

        // Act - start animation, then call again before it finishes
        animator.PlayBounceAnimation();
        yield return new WaitForSeconds(0.05f);
        animator.PlayBounceAnimation(); // Should restart gracefully

        // Wait for new animation to complete
        yield return new WaitForSeconds(0.25f);

        // Assert - scale should still return to baseline
        Assert.That(go.transform.localScale, Is.EqualTo(Vector3.one).Using<Vector3>(new Vector3EqualityComparer(0.01f)),
            "Scale should return to 1.0 even after restarting animation mid-play");

        // Cleanup
        Object.DestroyImmediate(go);
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
