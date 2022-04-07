using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void OnItemChanged(); //triggers all asigned methods if certain event is triggered
    public OnItemChanged onItemChangedCallback;

    public int space = 20;

    public List<Item> items = new List<Item>();
    public static Inventory instance;

    // Singleton
    private void Awake()
    {
       
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        GameObject GameManager = GameObject.Find("GameManager");
        instance = GameManager.GetComponent<Inventory>();

        //instance = this;
    }

    public bool Add(Item item)
    {
        // add if-statement to check what to add and what not
        if (items.Count >= space)
        {
            Debug.Log("Not enough inventory space!");
            return false;
        }

        items.Add(item);

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }

        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
