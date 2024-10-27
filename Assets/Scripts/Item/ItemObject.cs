using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n\n{data.description}";
        return str;
    }

    public void OnInteract()
    {

        if (data.type == ItemType.Structure)
        {
            var structureScript = GetComponent<IStructure>();
            if (structureScript != null)
            {
                structureScript.Activate();
            }
            return;
        }

        CharacterManager.Instance.Player.itemData = data;
        CharacterManager.Instance.Player.addItem?.Invoke();
        Destroy(gameObject);
        
    }
}