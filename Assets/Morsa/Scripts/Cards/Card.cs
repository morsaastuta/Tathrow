using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Scriptable Objects/Card")]
public class Card : ScriptableObject
{
    [SerializeField] public string title;
    [SerializeField] public int id;
    [SerializeField] public string description;
    [SerializeField] public List<Property> properties;
    [SerializeField] public Sprite image;
}
