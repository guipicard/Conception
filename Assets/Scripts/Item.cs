using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string name;
    public Sprite image;
    public GameObject model;
    public bool found = false;
}
