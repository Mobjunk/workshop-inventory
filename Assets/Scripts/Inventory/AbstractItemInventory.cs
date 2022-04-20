using Items;
using UnityEngine;

namespace Inventory
{
    public class AbstractItemInventory : MonoBehaviour
    {
        /// <summary>
        /// Max amount of slots within this inventory
        /// </summary>
        [SerializeField] private int _maxInventorySize;
        /// <summary>
        /// Array of all the items
        /// </summary>
        [SerializeField] protected GameItem[] _inventoryItems;
        /// <summary>
        /// Stack type of the inventory
        /// </summary>
        [SerializeField] private StackType _stackType;
        
        /// <summary>
        /// Handles setting up the inventory size and initializing the array
        /// </summary>
        public virtual void Awake()
        {
            _inventoryItems = new GameItem[_maxInventorySize];

            for (int index = 0; index < _maxInventorySize; index++)
                _inventoryItems[index] = new GameItem();
        }

        /// <summary>
        /// Handles setting a slot
        /// </summary>
        /// <param name="pSlot">The slot being set</param>
        /// <param name="pItem">The item being set on the slot</param>
        public void Set(int pSlot, GameItem pItem)
        {
            _inventoryItems[pSlot] = pItem.Amount == 0 ? new GameItem() : pItem;
        }

        /// <summary>
        /// Handles swapping items between slots
        /// </summary>
        /// <param name="pFromSlot">The from slot</param>
        /// <param name="pToSlot">The to slot</param>
        public void Swap(int pFromSlot, int pToSlot)
        {
            var temp = _inventoryItems[pFromSlot];

            _inventoryItems[pFromSlot] = _inventoryItems[pToSlot];
            _inventoryItems[pToSlot] = temp;
        }
        
        /// <summary>
        /// Handles adding a item to a inventory
        /// </summary>
        /// <param name="pItem">The item being added</param>
        /// <param name="pAmount">The amount of this item being added</param>
        public void AddItem(AbstractItemData pItem, int pAmount = 1)
        {
            if (!HasRoom())
            {
                Debug.Log("No room in the inventory.");
                return;
            }

            int nextAvailableSlot = GetFreeSlot();
            if ((pItem.stackable || _stackType.Equals(StackType.ALWAYS_STACK)) && HasItem(pItem, 0)) nextAvailableSlot = GetSlot(pItem);
            //if (pItem.stackable || _stackType.Equals(StackType.ALWAYS_STACK) && HasItem(pItem, 0))
            //    nextAvailableSlot = GetSlot(pItem);

            if (nextAvailableSlot == -1)
            {
                Debug.Log("No available slot.");
                return;
            }

            if (pItem.stackable || _stackType.Equals(StackType.ALWAYS_STACK))
            {
                var currentItem = _inventoryItems[nextAvailableSlot];
                if (currentItem.Item == null) currentItem.Item = pItem;

                var totalAmount = currentItem.Amount + pAmount;
                if (totalAmount >= int.MaxValue || totalAmount < 1)
                {
                    Debug.LogError("Total amount has a problem.");
                    return;
                }

                currentItem.Amount = totalAmount;
            }
            else
            {
                for (int index = 0; index < pAmount; index++)
                {
                    int freeSlot = GetFreeSlot();
                    if (freeSlot == -1)
                    {
                        Debug.Log("No free inventory space.");
                        return;
                    }

                    _inventoryItems[freeSlot] = new GameItem(pItem);
                }
            }
        }

        /// <summary>
        /// Handles removing a item from a inventory
        /// </summary>
        /// <param name="pItem">The item being removed</param>
        /// <param name="pAmount">The amount of that item being removed</param>
        public void RemoveItem(AbstractItemData pItem, int pAmount = 1)
        {
            int slot = GetSlot(pItem);
            if (slot == -1)
            {
                Debug.Log("No item was found for: " + pItem.name);
                return;
            }

            GameItem currentItem = _inventoryItems[slot];
            if (currentItem == null)
            {
                Debug.Log("No item found.");
                return;
            }

            if (pItem.stackable || _stackType.Equals(StackType.ALWAYS_STACK))
            {
                if (currentItem.Amount > pAmount)
                    currentItem.Amount -= pAmount;
                else
                    currentItem.Amount = 0;
            }
            else
            {
                for (int index = 0; index < _maxInventorySize; index++)
                {
                    slot = GetSlot(pItem);
                    if (slot != -1)
                    {
                        currentItem = _inventoryItems[slot];
                        currentItem.Item = null;
                        currentItem.Amount = 0;
                    } else Debug.Log("There is no item on slot["+slot+"] to remove.");
                }
            }
        }

        /// <summary>
        /// Get the slot for a certain item
        /// </summary>
        /// <param name="pItem">The item to check</param>
        /// <returns></returns>
        private int GetSlot(AbstractItemData pItem)
        {
            for (int index = 0; index < _maxInventorySize; index++)
            {
                if (_inventoryItems[index].Item == pItem)
                    return index;
            }
            return -1;
        }

        /// <summary>
        /// Checks if the player has a certain item
        /// </summary>
        /// <param name="pItem">The item to check with</param>
        /// <param name="pAmount">The amount to check with</param>
        /// <returns></returns>
        public bool HasItem(AbstractItemData pItem, int pAmount = 1)
        {
            foreach (GameItem item in _inventoryItems)
                if (item.Item == pItem && item.Amount >= pAmount)
                    return true;
            return false;
        }

        /// <summary>
        /// Get the next free available slot
        /// </summary>
        /// <returns></returns>
        private int GetFreeSlot()
        {
            for (int index = 0; index < _maxInventorySize; index++)
            {
                if (_inventoryItems[index].Item == null)
                    return index;
            }
            return -1;
        }

        /// <summary>
        /// Checks if there is room in the inventory
        /// </summary>
        /// <returns></returns>
        public bool HasRoom()
        {
            foreach (GameItem item in _inventoryItems)
                if (item.Item == null)
                    return true;
            return false;
        }
    }
}
