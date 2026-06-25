using System.Collections.Generic;
using UnityEngine;

public class LinkVisual
{
    public List<LinkLine> segments = new();


    public void Destroy()
    {
        foreach (var segment in segments)
        {
            segment.RemoveLine();
        }
    }
}