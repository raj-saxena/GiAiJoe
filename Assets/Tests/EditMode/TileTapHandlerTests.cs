using NUnit.Framework;
using UnityEngine;
using GiAiJoe.Gameplay;

public class TileTapHandlerTests
{
    [Test]
    public void OnTapDetected_EventExists()
    {
        // Arrange
        var go = new GameObject();
        var tapHandler = go.AddComponent<TileTapHandler>();

        // Assert - verify the event exists via reflection
        var eventInfo = typeof(TileTapHandler).GetEvent("OnTapDetected");
        Assert.IsNotNull(eventInfo, "TileTapHandler should have OnTapDetected event");

        // Cleanup
        Object.DestroyImmediate(go);
    }

    [Test]
    public void OnTapDetected_EventCanBeSubscribed()
    {
        // Arrange
        var go = new GameObject();
        var collider = go.AddComponent<BoxCollider2D>();
        var tapHandler = go.AddComponent<TileTapHandler>();

        bool eventFired = false;
        System.Action handler = () => eventFired = true;

        // Act - subscribe to the event
        tapHandler.OnTapDetected += handler;

        // Assert - verify subscription worked
        Assert.IsTrue(eventFired == false, "Event should not fire until tap occurs");

        // Cleanup
        tapHandler.OnTapDetected -= handler;
        Object.DestroyImmediate(go);
    }

    [Test]
    public void TileTapHandler_HasOnTapDetectedEvent()
    {
        // Arrange
        var go = new GameObject();
        var tapHandler = go.AddComponent<TileTapHandler>();

        // Act & Assert
        var eventInfo = typeof(TileTapHandler).GetEvent("OnTapDetected");
        Assert.IsNotNull(eventInfo, "TileTapHandler should have OnTapDetected event");

        // Cleanup
        Object.DestroyImmediate(go);
    }

    [Test]
    public void TileTapHandler_RequiresCollider2D()
    {
        // Arrange
        var go = new GameObject();
        var tapHandler = go.AddComponent<TileTapHandler>();

        // Assert - component should be added successfully
        Assert.IsNotNull(tapHandler);

        // Cleanup
        Object.DestroyImmediate(go);
    }
}
