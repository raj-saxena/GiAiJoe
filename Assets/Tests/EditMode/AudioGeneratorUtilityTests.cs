using NUnit.Framework;
using UnityEngine;
using GiAiJoe.Audio;

public class AudioGeneratorUtilityTests
{
    [Test]
    public void GenerateToneClip_ReturnsValidClip()
    {
        // Act
        AudioClip clip = AudioGeneratorUtility.GenerateToneClip();

        // Assert
        Assert.IsNotNull(clip, "GenerateToneClip should return a valid AudioClip");
    }

    [Test]
    public void GenerateToneClip_ClipHasCorrectLength()
    {
        // Arrange
        float expectedDuration = 0.1f;

        // Act
        AudioClip clip = AudioGeneratorUtility.GenerateToneClip();

        // Assert
        Assert.That(clip.length, Is.EqualTo(expectedDuration).Within(0.01f),
            "Generated clip should have duration of approximately 0.1 seconds");
    }

    [Test]
    public void GenerateToneClip_ClipIsMono()
    {
        // Act
        AudioClip clip = AudioGeneratorUtility.GenerateToneClip();

        // Assert
        Assert.That(clip.channels, Is.EqualTo(1), "Generated clip should be mono (1 channel)");
    }

    [Test]
    public void GenerateToneClip_ClipHasSampleData()
    {
        // Act
        AudioClip clip = AudioGeneratorUtility.GenerateToneClip();

        // Assert
        Assert.Greater(clip.samples, 0, "Generated clip should have audio sample data");
    }

    [Test]
    public void GenerateToneClip_MultipleCallsGenerateIndependentClips()
    {
        // Act
        AudioClip clip1 = AudioGeneratorUtility.GenerateToneClip();
        AudioClip clip2 = AudioGeneratorUtility.GenerateToneClip();

        // Assert
        Assert.AreNotEqual(clip1, clip2, "Multiple calls should generate independent clip instances");
    }
}
