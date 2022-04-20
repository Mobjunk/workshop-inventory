using UnityEngine;

namespace Items.Tools
{
    [CreateAssetMenu(fileName = "New Tool", menuName = "Items/New Tool")]
    public class AbstractTool : AbstractItemData
    {
        [Header("Tool Type")] public ToolType toolType;
        [Header("Tool damage")] public int damage;
    }

    public enum ToolType
    {
        NONE,
        AXE,
        PICKAXE,
        SCYTHE,
        HOE,
        WATERING_CAN,
        SWORD
    }
}