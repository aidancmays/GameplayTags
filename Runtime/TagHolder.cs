using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayTags
{
    /// <summary>
    /// The tags something currently has (its faction, "Undead", "Burning"...). Put it on anything that needs them.
    /// Tags are counted, so one added by two sources stays until both have removed it.
    /// </summary>
    [DefaultExecutionOrder(-96)] // before StatSheet, so starting tags are in place when stats are first worked out
    public class TagHolder : MonoBehaviour
    {
        [SerializeField] List<GameplayTag> startingTags = new List<GameplayTag>();

        readonly Dictionary<GameplayTag, int> counts = new Dictionary<GameplayTag, int>();

        /// <summary>Raised when a tag is gained or lost (not when only its count changes).</summary>
        public event Action Changed;

        /// <summary>Every tag currently held (each once, however many sources hold it). For debug displays.</summary>
        public IEnumerable<GameplayTag> Held => counts.Keys;

        /// <summary>The TagHolder on this object, adding one if there is none.</summary>
        public static TagHolder On(Component component)
        {
            if (!component.TryGetComponent(out TagHolder holder)) holder = component.gameObject.AddComponent<TagHolder>();
            return holder;
        }

        void Awake()
        {
            foreach (GameplayTag tag in startingTags) Add(tag);
        }

        /// <summary>True if any held tag is <paramref name="tag"/> or a descendant of it.</summary>
        public bool Has(GameplayTag tag)
        {
            if (tag == null) return false;

            foreach (GameplayTag held in counts.Keys)
            {
                if (held.Is(tag)) return true;
            }

            return false;
        }

        public void Add(GameplayTag tag)
        {
            if (tag == null) return;

            counts.TryGetValue(tag, out int count);
            counts[tag] = count + 1;
            if (count == 0) Changed?.Invoke();
        }

        public void Remove(GameplayTag tag)
        {
            if (tag == null || !counts.TryGetValue(tag, out int count)) return;

            if (count > 1)
            {
                counts[tag] = count - 1;
                return;
            }

            counts.Remove(tag);
            Changed?.Invoke();
        }
    }
}
