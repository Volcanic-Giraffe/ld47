using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShopUi : MonoBehaviour
{
    public GameObject[] towerPrefabs;
    
    public GameObject[] itemPrefabs;

    public Transform[] itemSlots;

    public TextMesh[] itemTexts;

    public TextMesh description;

    private int slots = 4; // matches UI

    private Camera _camera;
    private string _emptyDescription;

    private ShopItem _hovered;
    
    private void Start()
    {
        _camera = Camera.main;
        _emptyDescription = description.text;

        RandomizeLots();
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 50, ~LayerMask.NameToLayer("ShopItem")))
        {
            var hitItem = hit.collider.gameObject;
            if (hitItem)
            {
                var shopItem = hitItem.GetComponent<ShopItem>();
                ItemInteraction(shopItem);
            }
            else
            {
                NoItemInteraction();                
            }
        }
        else
        {
            NoItemInteraction();
        }
    }

    private void ItemInteraction(ShopItem item)
    {
        if (item != null)
        {
            _hovered = item;
            _hovered.OnHover();
            
            ShowDescription(item.description);
                    
            if (Input.GetMouseButtonDown(0))
            {
                _hovered.TryPurchase();
            }
        }
        else
        {
            NoItemInteraction();
        }
    }

    private void NoItemInteraction()
    {
        if (_hovered != null)
        {
            _hovered.OnBlur();
            _hovered = null;
        }
        // ShowDescription(_emptyDescription);
        ShowDescription("RES: " + GameState.GameState.GetInstance().Resources);
    }

    public void RandomizeLots()
    {
        var towerItem = towerPrefabs[Random.Range(0, towerPrefabs.Length)];
        AssignItemToSlot(0, towerItem); // first slot is new Tower

        GameObject tempGO;

        for (int i = 0; i < itemPrefabs.Length; i++) {
            int rnd = Random.Range(0, itemPrefabs.Length);
            tempGO = itemPrefabs[rnd];
            itemPrefabs[rnd] = itemPrefabs[i];
            itemPrefabs[i] = tempGO;
        }

        // start from 1, since 0 is tower slot
        for (var i = 1; i < slots; i++)
        {
            var item = itemPrefabs[i];
            AssignItemToSlot(i, item);
        }
    }

    private void ShowDescription(string text)
    {
        description.text = $"{text}";
    }
    
    private void AssignItemToSlot(int slotNum, GameObject itemPrefab)
    {
        var slot = itemSlots[slotNum];
        var price = itemTexts[slotNum];
        
        price.text = "---";
        foreach (Transform child in slot) {
            Destroy(child.gameObject);
        }

        if (itemPrefab != null)
        {
            var lot = Instantiate(itemPrefab, slot.position, slot.rotation);
            lot.transform.SetParent(slot);

            var storeItem = lot.GetComponent<ShopItem>();
            price.text = $"{storeItem.price}";
        }
        
        
    }
    
    
    
    
}
