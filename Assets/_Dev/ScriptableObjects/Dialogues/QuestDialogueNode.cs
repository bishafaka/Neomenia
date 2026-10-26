using UnityEngine;

[CreateAssetMenu(menuName="Dialogue/Quest")]
public class QuestDialogueNode : DialogueNode
{
    public Item requiredItem;
    public int requiredAmount=1;
    public void GiveItemEvent()
    {
        UIManager.Instance.inventoryManager.RemoveItem(requiredItem, requiredAmount);
    }
}
