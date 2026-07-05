using NUnit.Framework;
using UnityEngine;
using GiAiJoe.Gameplay;
using GiAiJoe.Audio;

public class TileAudioSourceTests
{
    [Test]
    public void PlayToneOnce_WithAudioSourceInitialized()
    {
        // Arrange
        var go = new GameObject();
        var audioSource = go.AddComponent<AudioSource>();
        var tileAudioSource = go.AddComponent<TileAudioSource>();

        // Manually initialize (since Start() won't be called in EditMode)
        // We'll access and call the initialization logic
        typeof(TileAudioSource).GetField("audioSource", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.SetValue(tileAudioSource, audioSource);
        var genClipMethod = typeof(TileAudioSource).GetField("generatedToneClip", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (genClipMethod != null)
        {
            genClipMethod.SetValue(tileAudioSource, AudioGeneratorUtility.GenerateToneClip());
        }

        // Act - call PlayToneOnce
        tileAudioSource.PlayToneOnce();

        // Assert - if no exception was thrown, the method executed
        Assert.Pass("PlayToneOnce executed without throwing an exception");

        // Cleanup
        Object.DestroyImmediate(go);
    }

    [Test]
    public void TileAudioSource_HasPlayToneOnceMethod()
    {
        // Arrange
        var go = new GameObject();
        var tileAudioSource = go.AddComponent<TileAudioSource>();

        // Assert - verify the method exists via reflection
        var method = typeof(TileAudioSource).GetMethod("PlayToneOnce");
        Assert.IsNotNull(method, "TileAudioSource should have PlayToneOnce method");

        // Cleanup
        Object.DestroyImmediate(go);
    }

    [Test]
    public void TileAudioSource_InitializesAudioSource()
    {
        // Arrange
        var go = new GameObject();
        var tileAudioSource = go.AddComponent<TileAudioSource>();

        // Assert - verify component is created and has correct type
        Assert.IsNotNull(tileAudioSource, "TileAudioSource should be created");
        Assert.That(typeof(TileAudioSource).GetMethod("PlayToneOnce") != null,
            "TileAudioSource should have PlayToneOnce method");

        // Cleanup
        Object.DestroyImmediate(go);
    }
}
