using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Group", menuName = "Item/Item Group")]
public class ItemGroup : ScriptableObject
{
    public List<Item> items = new List<Item>();
}