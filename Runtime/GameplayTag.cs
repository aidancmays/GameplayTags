using UnityEngine;

namespace GameplayTags
{
    /// <summary>
    /// A label for things and actions: a faction, a damage type, a kind of cost.
    /// Tags form a tree through Parent, so "Fire" (parent "Magic", parent "Damage") counts as all three.
    /// New tags are new assets.
    /// </summary>
    [CreateAssetMenu(menuName = "Gameplay Tags/Gameplay Tag")]
    public class GameplayTag : ScriptableObject
    {
        [Tooltip("Optional. This tag also counts as its parent, and its parent's parent, and so on.")]
        [SerializeField] GameplayTag parent;

        public GameplayTag Parent => parent;

        /// <summary>True if this tag is <paramref name="other"/> or one of its descendants.</summary>
        public bool Is(GameplayTag other)
        {
            if (other == null) return false;

            GameplayTag tag = this;
            for (int guard = 0; tag != null && guard < 32; guard++) // guard: a parent loop made by mistake
            {
                if (tag == other) return true;
                tag = tag.parent;
            }

            return false;
        }
    }
}
