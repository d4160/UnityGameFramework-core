using d4160.Collections;
using System.Collections.Generic;
using UMA;
using UnityEngine;
using UMA.CharacterSystem;

namespace d4160.UMA
{
    [CreateAssetMenu(menuName = "d4160/UMA/Collections/Refs Group")]
    public class UmaRefsGroupLibrarySO : LibrarySOBase<UMARefGroup>
    {
        public void SetDNA(int groupIdx, Dictionary<string, DnaSetter> dna, float value, DynamicCharacterAvatar avatar = null)
        {
            var group = this[groupIdx];
            if (group == null) return;

            group.SetDNA(dna, value, avatar);
        }

        public void SetColor(int groupIdx, DynamicCharacterAvatar avatar, Color color)
        {
            var group = this[groupIdx];
            if (group == null) return;

            group.SetColor(avatar, color);
        }
    }

    [System.Serializable]
    public class UMARefGroup
    {
        [SerializeField] private string _groupName;
        [SerializeField] private string[] _refs;

        public void SetDNA(Dictionary<string, DnaSetter> dna, float value, DynamicCharacterAvatar avatar = null)
        {
            for (int i = 0; i < _refs.Length; i++)
            {
                dna[_refs[i]].Set(value);
            }

            if (avatar) avatar.BuildCharacter();
        }

        public void SetColor(DynamicCharacterAvatar avatar, Color color)
        {
            for (int i = 0; i < _refs.Length; i++)
            {
                avatar.SetColor(_refs[i], color);
            }

            avatar.UpdateColors(true);
        }

        public void SetSlot(DynamicCharacterAvatar avatar, UMATextRecipe recipe)
        {
            if (_refs.Length > 1) 
            {
                Debug.LogWarning("[UMARefGroup>SetSlot] Only one slot name is allowed at the time");
                return; 
            }

            for (int i = 0; i < _refs.Length; i++)
            {
                avatar.SetSlot(recipe);
            }

            avatar.UpdateColors(true);
        }
    }
}