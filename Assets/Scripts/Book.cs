using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book
{
    private Dictionary<string, Recipe> book;

    public Book(Dictionary<string, Recipe> b)
    {
        book = b;
    }

    public Dictionary<string, Recipe> GetBook()
    {
        return book;
    }

    public Recipe GetRecipe(string name)
    {
        return book[name];
    }

    public void AddRecipe(string name, Recipe recipe)
    {
        book.Add(name, recipe);
    }

    public void Save()
    {
        SaveSystem.SaveBook(book);
    }

    public void Load()
    {
        BookData data = SaveSystem.LoadBook();
        if (data.book != null)
        {
            book = data.book;
        }
    }
}
