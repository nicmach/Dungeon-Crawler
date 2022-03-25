using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    /**private Inventory inventory;
    private Transform ItemSlotContainer;
    private Transform Inventory;

    private void Awake()
    {
        ItemSlotContainer = transform.Find("ItemSlotContainer");
        Inventory = Inventory.Find("Inventory");
    }

    public void RefreshInventory()
    {
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 30f;
        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(Inventory, ItemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            x++;
            if (x > 4)
            {
                x = 0;
                y++;
            }
        }
    }**/


}
