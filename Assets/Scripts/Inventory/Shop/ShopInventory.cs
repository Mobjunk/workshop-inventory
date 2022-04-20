using Items;
using UnityEngine;

namespace Inventory.Shop
{
    public class ShopInventory : AbstractItemInventory
    {
        [SerializeField] private ShopStock _shopStock;
        
        public override void Awake()
        {
            if (_shopStock == null) return;

            foreach (GameItem item in _shopStock.items)
                AddItem(item.Item, item.Amount);
        }
        
        public void PurchaseItem(AbstractItemData pItem, int pAmount = 1)
        {
            AddItem(pItem, pAmount);
        }

        public void SellItem(AbstractItemData pItem, int pAmount = 1)
        {
            RemoveItem(pItem, pAmount);
        }

        public bool CanPurchase(AbstractItemData pItem)
        {
            if (!_shopStock.GeneralStore && !HasItem(pItem, 0)) return false;
            if (!HasRoom()) return false;
            return true;
        }

        public bool HasStock(int pSlot)
        {
            return _inventoryItems[pSlot].Amount > 0;
        }

        public int GetSellPrice(AbstractItemData pItem)
        {
            return Mathf.FloorToInt(pItem.basePrice * _shopStock.SellRatio);
        }

        public int GetBuyPrice(AbstractItemData pItem)
        {
            return Mathf.FloorToInt(pItem.basePrice * _shopStock.BuyRatio);
        }
    }
}