using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    [SerializeField, Min(1)] private int requiredItemsCount;

    [Header("DEBUG")]
    [SerializeField] private bool log;

    private Transform _transform;
    private List<Item> _items;
    
    private bool IsFull => _items.Count >= requiredItemsCount;

    private void Reset()
    {
        requiredItemsCount = 1;
    }

    private void Awake()
    {
        _transform = transform;
        _items = new List<Item>();
    }

    private void AddItem(Item item)
    {
        if (!CanAddItem(item))
            return;
        
        _items.Add(item);
        Attach(item);
        
        if (log)
            Debug.Log($"Item \"{item.name}\" added!");
        
        if (IsFull && log)
            Debug.Log($"Bag is full! {_items.Count}/{requiredItemsCount}");
    }

    private bool CanAddItem(Item item)
    {
        if (item.IsHeld)
        {
            // if (log)
            //     Debug.Log($"Item: {item.name} is currently being hold");

            return false;
        }
        
        if (IsFull)
        {
            // if (log)
            //     Debug.LogWarning($"Bag is full! {_items.Count}/{requiredItemsCount}");
            
            return false;
        }
        
        if (_items.Contains(item))
        {
            // if (log)
            //     Debug.LogWarning($"Item \"{item.name}\" does already exist in the Bag.");
            
            return false;
        }

        return true;
    }

    private void Attach(Item item)
    {
        item.Attach(_transform, Vector3.zero);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log($"{other.name} {nameof(OnTriggerEnter)}");
        
        if (other.TryGetComponent(out Item item))
            AddItem(item);
    }
    
    private void OnTriggerStay(Collider other)
    {
        // Debug.Log($"{other.name} {nameof(OnTriggerStay)}");
        
        if (other.TryGetComponent(out Item item))
            AddItem(item);
    }
}
