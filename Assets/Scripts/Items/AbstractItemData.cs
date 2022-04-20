using System;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Items/New Item")]
    [Serializable]
    public class AbstractItemData : ScriptableObject
    {
        public string itemDescription;
        public Sprite uiSprite;
        public bool stackable;
        public int basePrice;
    }
}