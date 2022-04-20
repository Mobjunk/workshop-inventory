using Items;

namespace Inventory
{
    [System.Serializable]
    public class GameItem
    {
        public AbstractItemData Item;

        public int Amount;

        public GameItem()
        {
            Item = null;
            Amount = 0;
        }

        public GameItem(AbstractItemData pItem)
        {
            Item = pItem;
        }

        public GameItem(AbstractItemData pItem, int pAmount)
        {
            Item = pItem;
            Amount = pAmount;
        }

        public void SetAmount(int pAmount)
        {
            Amount = pAmount;
        }

        public override string ToString()
        {
            return $"{Item.name}, {Amount}";
        }
    }

}