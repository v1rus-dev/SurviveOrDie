using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tags : MonoBehaviour
{
    [SerializeField] private List<Tag> tags;

    public List<Tag> GetTags()
    {
        return tags;
    }
        
    public void AddTag(Tag tag)
    {
        tags.Add(tag);
    }
        
    public void RemoveTag(Tag tag)
    {
        tags.Remove(tag);
    }
        
    public bool HasTag(Tag tag)
    {
        return tags.Contains(tag);
    }
        
    public bool HasTagByName(string name)
    {
        return tags.Any(tag => tag.name == name);
    }
}

[Serializable]
public class Tag
{
    public string name;
}