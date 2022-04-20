using UnityEngine;

namespace Items.Consumables
{
    [CreateAssetMenu(fileName = "New Consumable", menuName = "Items/New Consumable")]
    public class AbstractConsumable : AbstractItemData
    {
        [Header("Restoration data")]
        public int healthRestoration;
        public int energyRestoration;
    }
}
