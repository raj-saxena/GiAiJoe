using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace GiAiJoe.Gameplay.AnimalHabitat
{
    /// <summary>
    /// Serializable data pair for an animal-habitat matching entry.
    /// </summary>
    [System.Serializable]
    public class AnimalHabitatPair
    {
        public string animalId;
        public Sprite animalSprite;
        public Sprite habitatSprite;
        public Vector2 animalStartPosition;
        public Vector2 habitatPosition;
    }

    /// <summary>
    /// ScriptableObject that defines the layout and sprite references for a level.
    /// Contains a list of animal-habitat pairs with their positions and sprites.
    /// </summary>
    [CreateAssetMenu(fileName = "LevelDefinition", menuName = "GiAiJoe/AnimalHabitat/Level Definition")]
    public class LevelDefinition : ScriptableObject
    {
        [SerializeField]
        public string levelName;

        [SerializeField]
        public int levelIndex;

        [SerializeField]
        public List<AnimalHabitatPair> pairs = new List<AnimalHabitatPair>();

        [SerializeField]
        public Sprite backgroundSprite;

        /// <summary>
        /// Validates the level definition for common issues.
        /// Returns a list of validation error messages. Empty list means valid.
        /// </summary>
        public List<string> Validate()
        {
            var errors = new List<string>();

            if (pairs == null || pairs.Count == 0)
            {
                errors.Add("Level must have at least one animal-habitat pair");
                return errors;
            }

            var seenAnimalIds = new HashSet<string>();

            for (int i = 0; i < pairs.Count; i++)
            {
                var pair = pairs[i];

                if (string.IsNullOrEmpty(pair.animalId))
                {
                    errors.Add($"Pair {i}: animalId cannot be null or empty");
                }

                if (pair.animalSprite == null)
                {
                    errors.Add($"Pair {i} ({pair.animalId}): animalSprite is null");
                }

                if (pair.habitatSprite == null)
                {
                    errors.Add($"Pair {i} ({pair.animalId}): habitatSprite is null");
                }

                // Check for duplicate animalIds
                if (!string.IsNullOrEmpty(pair.animalId))
                {
                    if (seenAnimalIds.Contains(pair.animalId))
                    {
                        errors.Add($"Duplicate animalId: {pair.animalId}");
                    }
                    else
                    {
                        seenAnimalIds.Add(pair.animalId);
                    }
                }
            }

            return errors;
        }

        /// <summary>
        /// Gets a pair by its animalId, or null if not found.
        /// </summary>
        public AnimalHabitatPair GetPairByAnimalId(string animalId)
        {
            return pairs.FirstOrDefault(p => p.animalId == animalId);
        }
    }
}
