using NUnit.Framework;
using UnityEngine;
using GiAiJoe.Gameplay.AnimalHabitat;
using System.Collections.Generic;

public class AnimalHabitatMatchingTests
{
    [Test]
    public void DropZone_CorrectMatchFiresOnCorrectMatch()
    {
        // Arrange
        var dropZoneGo = new GameObject("DropZone");
        var dropZone = dropZoneGo.AddComponent<DropZone>();
        dropZone.SetExpectedAnimalId("Lion");

        bool correctEventFired = false;
        dropZone.OnCorrectMatch += () => correctEventFired = true;

        // Act
        dropZone.HandleDrop("Lion");

        // Assert
        Assert.IsTrue(correctEventFired, "OnCorrectMatch event should fire for matching AnimalId");

        // Cleanup
        Object.DestroyImmediate(dropZoneGo);
    }

    [Test]
    public void DropZone_WrongMatchFiresOnWrongMatch()
    {
        // Arrange
        var dropZoneGo = new GameObject("DropZone");
        var dropZone = dropZoneGo.AddComponent<DropZone>();
        dropZone.SetExpectedAnimalId("Lion");

        bool wrongEventFired = false;
        dropZone.OnWrongMatch += () => wrongEventFired = true;

        // Act
        dropZone.HandleDrop("Fish");

        // Assert
        Assert.IsTrue(wrongEventFired, "OnWrongMatch event should fire for non-matching AnimalId");

        // Cleanup
        Object.DestroyImmediate(dropZoneGo);
    }

    [Test]
    public void DropZone_OnlyOneEventFiresPerDrop()
    {
        // Arrange
        var dropZoneGo = new GameObject("DropZone");
        var dropZone = dropZoneGo.AddComponent<DropZone>();
        dropZone.SetExpectedAnimalId("Lion");

        int correctCount = 0;
        int wrongCount = 0;
        dropZone.OnCorrectMatch += () => correctCount++;
        dropZone.OnWrongMatch += () => wrongCount++;

        // Act
        dropZone.HandleDrop("Lion");

        // Assert
        Assert.AreEqual(1, correctCount, "OnCorrectMatch should fire exactly once");
        Assert.AreEqual(0, wrongCount, "OnWrongMatch should not fire for correct match");

        // Cleanup
        Object.DestroyImmediate(dropZoneGo);
    }

    [Test]
    public void DropZone_OnlyOneEventFiresPerDrop_WrongMatch()
    {
        // Arrange
        var dropZoneGo = new GameObject("DropZone");
        var dropZone = dropZoneGo.AddComponent<DropZone>();
        dropZone.SetExpectedAnimalId("Lion");

        int correctCount = 0;
        int wrongCount = 0;
        dropZone.OnCorrectMatch += () => correctCount++;
        dropZone.OnWrongMatch += () => wrongCount++;

        // Act
        dropZone.HandleDrop("Fish");

        // Assert
        Assert.AreEqual(0, correctCount, "OnCorrectMatch should not fire for wrong match");
        Assert.AreEqual(1, wrongCount, "OnWrongMatch should fire exactly once");

        // Cleanup
        Object.DestroyImmediate(dropZoneGo);
    }

    [Test]
    public void LevelController_TracksCorrectMatches()
    {
        // Arrange
        var levelControllerGo = new GameObject("LevelController");
        var levelController = levelControllerGo.AddComponent<LevelController>();

        // Create 3 drop zones with expected animal IDs
        var dropZones = new List<DropZone>();
        var animalIds = new[] { "Lion", "Fish", "Cow" };
        for (int i = 0; i < 3; i++)
        {
            var dzGo = new GameObject($"DropZone_{i}");
            var dz = dzGo.AddComponent<DropZone>();
            dz.SetExpectedAnimalId(animalIds[i]);
            dropZones.Add(dz);
        }

        levelController.Initialize(dropZones);

        // Act - match 2 animals correctly
        dropZones[0].HandleDrop("Lion");
        dropZones[1].HandleDrop("Fish");

        // Assert
        Assert.AreEqual(2, levelController.GetCorrectMatchCount(), "CorrectMatchCount should be 2 after 2 correct matches");

        // Cleanup
        Object.DestroyImmediate(levelControllerGo);
        foreach (var dz in dropZones)
        {
            Object.DestroyImmediate(dz.gameObject);
        }
    }

    [Test]
    public void LevelController_FiresLevelCompleteOnlyWhenAllMatched()
    {
        // Arrange
        var levelControllerGo = new GameObject("LevelController");
        var levelController = levelControllerGo.AddComponent<LevelController>();

        var dropZones = new List<DropZone>();
        for (int i = 0; i < 3; i++)
        {
            var dzGo = new GameObject($"DropZone_{i}");
            var dz = dzGo.AddComponent<DropZone>();
            dz.SetExpectedAnimalId(new[] { "Lion", "Fish", "Cow" }[i]);
            dropZones.Add(dz);
        }

        levelController.Initialize(dropZones);

        bool levelCompleteFired = false;
        levelController.OnLevelComplete += () => levelCompleteFired = true;

        // Act - match only 2 animals
        dropZones[0].HandleDrop("Lion");
        dropZones[1].HandleDrop("Fish");

        // Assert - level should not be complete yet
        Assert.IsFalse(levelCompleteFired, "LevelComplete should not fire when not all animals matched");

        // Act - match the third animal
        dropZones[2].HandleDrop("Cow");

        // Assert - level should now be complete
        Assert.IsTrue(levelCompleteFired, "LevelComplete should fire when all animals matched");

        // Cleanup
        Object.DestroyImmediate(levelControllerGo);
        foreach (var dz in dropZones)
        {
            Object.DestroyImmediate(dz.gameObject);
        }
    }

    [Test]
    public void LevelController_DoesNotFireLevelCompleteMultipleTimes()
    {
        // Arrange
        var levelControllerGo = new GameObject("LevelController");
        var levelController = levelControllerGo.AddComponent<LevelController>();

        var dropZones = new List<DropZone>();
        for (int i = 0; i < 3; i++)
        {
            var dzGo = new GameObject($"DropZone_{i}");
            var dz = dzGo.AddComponent<DropZone>();
            dz.SetExpectedAnimalId(new[] { "Lion", "Fish", "Cow" }[i]);
            dropZones.Add(dz);
        }

        levelController.Initialize(dropZones);

        int levelCompleteCount = 0;
        levelController.OnLevelComplete += () => levelCompleteCount++;

        // Act - match all 3 animals
        dropZones[0].HandleDrop("Lion");
        dropZones[1].HandleDrop("Fish");
        dropZones[2].HandleDrop("Cow");

        // Assert
        Assert.AreEqual(1, levelCompleteCount, "OnLevelComplete should fire exactly once");

        // Cleanup
        Object.DestroyImmediate(levelControllerGo);
        foreach (var dz in dropZones)
        {
            Object.DestroyImmediate(dz.gameObject);
        }
    }

    [Test]
    public void LevelDefinition_ValidatesAnimalIdRequired()
    {
        // Arrange
        var definition = ScriptableObject.CreateInstance<LevelDefinition>();
        var pair = new AnimalHabitatPair
        {
            animalId = null, // null animal ID
            animalSprite = null,
            habitatSprite = null,
            animalStartPosition = Vector2.zero,
            habitatPosition = Vector2.one
        };

        definition.pairs = new List<AnimalHabitatPair> { pair };

        // Act
        var validationErrors = definition.Validate();

        // Assert - should have error about null animalId
        Assert.That(validationErrors.Count > 0, "Validation should find errors for null animalId");
        Assert.That(validationErrors, Has.Some.Matches((string s) => s.Contains("animalId")),
            "Should have error mentioning animalId");

        // Cleanup
        Object.DestroyImmediate(definition);
    }

    [Test]
    public void LevelDefinition_DetectsDuplicateAnimalIds()
    {
        // Arrange
        var definition = ScriptableObject.CreateInstance<LevelDefinition>();
        var pair1 = new AnimalHabitatPair
        {
            animalId = "Lion",
            animalSprite = null,
            habitatSprite = null,
            animalStartPosition = Vector2.zero,
            habitatPosition = Vector2.one
        };

        var pair2 = new AnimalHabitatPair
        {
            animalId = "Lion", // duplicate!
            animalSprite = null,
            habitatSprite = null,
            animalStartPosition = Vector2.one,
            habitatPosition = Vector2.zero
        };

        definition.pairs = new List<AnimalHabitatPair> { pair1, pair2 };

        // Act
        var validationErrors = definition.Validate();

        // Assert
        Assert.That(validationErrors, Has.Some.Matches((string s) => s.Contains("Duplicate")),
            "Validation should detect duplicate animalIds");

        // Cleanup
        Object.DestroyImmediate(definition);
    }

    [Test]
    public void LevelDefinition_RequiresPairs()
    {
        // Arrange
        var definition = ScriptableObject.CreateInstance<LevelDefinition>();
        definition.pairs = new List<AnimalHabitatPair>(); // empty

        // Act
        var validationErrors = definition.Validate();

        // Assert
        Assert.That(validationErrors.Count > 0, "Empty pair list should fail validation");

        // Cleanup
        Object.DestroyImmediate(definition);
    }


    [Test]
    public void Draggable_MarksAsMatchedWhenCorrectDropHandled()
    {
        // Arrange
        var animalGo = new GameObject("TestAnimal");
        animalGo.transform.position = Vector3.zero;
        var spriteRenderer = animalGo.AddComponent<SpriteRenderer>();
        var collider = animalGo.AddComponent<BoxCollider2D>();
        var draggable = animalGo.AddComponent<Draggable>();

        // Act: Mark as matched
        draggable.SetMatched(true);

        // Assert
        Assert.IsTrue(draggable.IsMatched(),
            "Draggable should report as matched after SetMatched(true)");

        // Cleanup
        Object.DestroyImmediate(animalGo);
    }

    [Test]
    public void DropZone_HandleDrop_WithDraggableParameter()
    {
        // Arrange
        var dropZoneGo = new GameObject("DropZone");
        var dropZone = dropZoneGo.AddComponent<DropZone>();
        dropZone.SetExpectedAnimalId("Lion");

        var animalGo = new GameObject("Animal");
        var draggable = animalGo.AddComponent<Draggable>();

        bool correctEventFired = false;
        dropZone.OnCorrectMatch += () => correctEventFired = true;

        // Act: Call HandleDrop with Draggable parameter (backward-compatible signature)
        dropZone.HandleDrop("Lion", draggable);

        // Assert
        Assert.IsTrue(correctEventFired,
            "OnCorrectMatch should fire even with new Draggable parameter");

        // Cleanup
        Object.DestroyImmediate(dropZoneGo);
        Object.DestroyImmediate(animalGo);
    }

    [Test]
    public void DropZone_HandleDrop_BackwardCompatible()
    {
        // Arrange: Verify that old code calling HandleDrop(string) still works
        var dropZoneGo = new GameObject("DropZone");
        var dropZone = dropZoneGo.AddComponent<DropZone>();
        dropZone.SetExpectedAnimalId("Fish");

        bool wrongEventFired = false;
        dropZone.OnWrongMatch += () => wrongEventFired = true;

        // Act: Call old signature (no Draggable parameter)
        dropZone.HandleDrop("Cow");

        // Assert
        Assert.IsTrue(wrongEventFired,
            "Old HandleDrop(string) signature should still work");

        // Cleanup
        Object.DestroyImmediate(dropZoneGo);
    }
}
