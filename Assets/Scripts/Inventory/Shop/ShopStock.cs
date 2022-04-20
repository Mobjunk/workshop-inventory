using UnityEngine;

namespace Inventory.Shop
{
    public class ShopStock : ScriptableObject
    {
        public bool GeneralStore;
        public float BuyRatio;
        public float SellRatio;
        public GameItem[] items;
    }
}