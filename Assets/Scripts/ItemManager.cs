using System.Collections.Generic;
using System.Linq;
using Items;
using UnityEngine;

namespace DefaultNamespace
{
    public class ItemManager : MonoBehaviour
    {
        private static ItemManager _instance;
        
        public static ItemManager Instance() => _instance;
        
        public List<AbstractItemData> Items = new List<AbstractItemData>();
        
        private void Awake()
        {
            _instance = this;
        }
        
        public AbstractItemData ForName(string pItemName)
        {
            foreach (AbstractItemData item in Items)
            {
                if (item.name.ToLower().Equals(pItemName.ToLower()))
                    return item;
            }
            return null;
        }
    }
}