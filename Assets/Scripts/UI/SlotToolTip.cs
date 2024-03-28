using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlotToolTip : MonoBehaviour
{
    [SerializeField]
    private GameObject baseObj;

    [SerializeField]
    private TextMeshProUGUI itemName;
    [SerializeField]
    private TextMeshProUGUI itemDescription;
    [SerializeField]
    private TextMeshProUGUI itemHowToUse;

    public void ShowToolTip(Item item, Vector3 pos)
    {
        baseObj.SetActive(true);
        pos += new Vector3(baseObj.GetComponent<RectTransform>().rect.width * 0.5f, -baseObj.GetComponent<RectTransform>().rect.height * 0.5f, 0f) ;
        baseObj.transform.position = pos;

        itemName.text = item.itemName;
        itemDescription.text = item.itemDescription;

        if (item.itemType == Item.ItemType.Equipment)
            itemHowToUse.text = "Right Button - Equip";
        else if (item.itemType == Item.ItemType.Used)
            itemHowToUse.text = "Right Button - Eat";
        else
            itemHowToUse.text = "";
    }

    public void HideToolTip()
    {
        baseObj.SetActive(false);
    }
}
