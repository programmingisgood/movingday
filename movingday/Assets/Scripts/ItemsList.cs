using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ItemsList", menuName = "movingday/ItemsList")]
public class ItemsList : ScriptableObject
{
    [SerializeField]
    private List<GameObject> itemPrefabs = null;

    public List<GameObject> GetItemPrefabs()
    {
        return itemPrefabs;
    }
}
