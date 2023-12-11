using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Create New Item")]
public class Items : ScriptableObject 
{
    public string itemName;
    public Sprite itemImage;
    public string description;
}
