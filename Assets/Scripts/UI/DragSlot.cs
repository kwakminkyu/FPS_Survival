using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    public static DragSlot Instance;

    public Slot dragSlot;

    [SerializeField]
    private Image itemImage;

    private void Awake()
    {
        Instance = this;
    }

    public void DragSetImage(Image itemImage)
    {
        this.itemImage.sprite = itemImage.sprite;
        SetColor(1);
    }

    public void SetColor(float alpha)
    {
        Color color = itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }
}
