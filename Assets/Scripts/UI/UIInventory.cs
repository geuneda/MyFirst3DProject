using UnityEngine;
using UnityEngine.InputSystem;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots; // 인벤토리 슬롯 배열

    private PlayerCondition condition;

    void Start()
    {
        condition = CharacterManager.Instance.Player.condition;

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].index = i;
            slots[i].Clear();
        }

        CharacterManager.Instance.Player.addItem += AddItem;
        UpdateUI();
    }

    public void OnInventorySelect(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            string inputName = context.control.name;
            int slotIndex = int.Parse(inputName) - 1;
            if (slotIndex >= 0 && slotIndex < slots.Length)
            {
                UseItem(slotIndex);
            }
        }
    }

    public void UseItem(int slotIndex)
    {
        if (slots[slotIndex].IsEmpty()) return;

        // Consumable 아이템일 경우 사용
        if (slots[slotIndex].item.type == ItemType.Consumable)
        {
            for (int i = 0; i < slots[slotIndex].item.consumables.Length; i++)
            {
                switch (slots[slotIndex].item.consumables[i].type)
                {
                    case ConsumableType.Health:
                        condition.Heal(slots[slotIndex].item.consumables[i].value);
                        break;
                    case ConsumableType.Hunger:
                        condition.Eat(slots[slotIndex].item.consumables[i].value);
                        break;
                }
            }
            RemoveItem(slotIndex);
        }
    }

    void RemoveItem(int slotIndex)
    {
        slots[slotIndex].quantity--;

        if (slots[slotIndex].quantity <= 0)
        {
            slots[slotIndex].Clear();
        }

        UpdateUI();
    }

    public void AddItem()
    {
        ItemData data = CharacterManager.Instance.Player.itemData;
        if (data == null) return;

        if (data.canStack)
        {
            ItemSlot stackSlot = GetItemStack(data);
            if (stackSlot != null)
            {
                stackSlot.quantity++;
                UpdateUI();
                CharacterManager.Instance.Player.itemData = null;
                return;
            }
        }

        ItemSlot emptySlot = GetEmptySlot();
        if (emptySlot != null)
        {
            emptySlot.item = data;
            emptySlot.quantity = 1;
            UpdateUI();
            CharacterManager.Instance.Player.itemData = null;
            return;
        }

        // 인벤토리가 가득 찬 경우
        ThrowItem(data);
        CharacterManager.Instance.Player.itemData = null;
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].IsEmpty())
            {
                slots[i].Set();
            }
            else
            {
                slots[i].Clear();
            }
        }
    }

    ItemSlot GetItemStack(ItemData data)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == data && slots[i].quantity < data.maxStackAmount)
            {
                return slots[i];
            }
        }
        return null;
    }

    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].IsEmpty())
            {
                return slots[i];
            }
        }
        return null;
    }

    public void ThrowItem(ItemData data)
    {
        Instantiate(data.dropPrefab, CharacterManager.Instance.Player.dropPosition.position, Quaternion.identity);
    }
}
