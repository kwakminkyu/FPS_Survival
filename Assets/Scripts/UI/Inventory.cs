using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;

    [SerializeField]
    private GameObject inventoryBase;

    [SerializeField]
    private GameObject slotParent;

    private Slot[] slots;

    public Slot[] GetSlots() { return slots; }

    [SerializeField]
    private Item[] items;

    public void LoadToInven(int arrayNum, string itemName, int itemNum)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemName == itemName)
                slots[arrayNum].AddItem(items[i], itemNum);
        }
    }

    private void Awake()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
    }

    private void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I)) 
        {
            inventoryActivated = !inventoryActivated;

            if (inventoryActivated)
                OpenInventory();
            else
                CloseInventory();
        }
    }

    private void OpenInventory()
    {
        GameManager.isOpenInventory = true;
        inventoryBase.SetActive(true);
    }

    private void CloseInventory()
    {
        GameManager.isOpenInventory = false;
        inventoryBase.SetActive(false);
    }

    public void AcquireItem(Item item, int count = 1)
    {
        if (Item.ItemType.Equipment != item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == item.itemName)
                    {
                        slots[i].SetSlotCount(count);
                        return;
                    }
                }
            }
        }

        for (int i = 0;i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(item, count);
                return;
            }
        }
    }
}
