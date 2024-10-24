using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Book", menuName = "Book Data", order = 51)]
public class BookData : ScriptableObject
{
    public string title;
    public int yearOfRelease;
    public List<Sprite> pages;
}
