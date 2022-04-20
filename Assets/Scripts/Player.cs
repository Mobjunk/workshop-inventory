using Inventory.Player;
using UnityEngine;

namespace DefaultNamespace
{
    public class Player : MonoBehaviour
    {
        private ItemManager _itemManager => ItemManager.Instance();
        
        [SerializeField] private PlayerInventory _playerInventory;

        private void Start()
        {
            _playerInventory = GetComponent<PlayerInventory>();
            
            _playerInventory.AddItem(_itemManager.ForName("Wood"), 100);
            _playerInventory.AddItem(_itemManager.ForName("Pickaxe"), 2);
            
            _playerInventory.RemoveItem(_itemManager.ForName("Wood"), 2);
        }
    }
}