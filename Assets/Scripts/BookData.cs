using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BookData
{
    public Dictionary<string, Recipe> book;

    public BookData(Dictionary<string, Recipe> b)
    {
        book = b;
    }

}
