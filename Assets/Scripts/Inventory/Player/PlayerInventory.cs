using Items;

namespace Inventory.Player
{
    public class PlayerInventory : AbstractItemInventory
    {
        public int Coins;
        

        public void PurchaseItem(AbstractItemData pItem, int pItemPrice, int pAmount = 1)
        {
            UpdateCoins(-pItemPrice);
            AddItem(pItem, pAmount);
        }

        public void SellItem(AbstractItemData pItem, int pItemPrice, int pAmount = 1)
        {
            UpdateCoins(pItemPrice);
            RemoveItem(pItem, pAmount);
        }

        public bool HasEnoughCoins(int pPrice)
        {
            return Coins > pPrice;
        }

        public void UpdateCoins(int pAmount)
        {
            Coins += pAmount;
        }
        
    }
}