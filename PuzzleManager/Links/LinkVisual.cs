using System.Collections.Generic;
using UnityEngine;

public class LinkVisual
{
    public List<LinkLine> segments = new();


    public void SetNewSegments(List<LinkLine> linkLines)
    {
        Destroy();

        segments = linkLines;
    }

    public void Destroy()
    {
        foreach (var segment in segments)
        {
            segment.RemoveLine();
        }
        segments.Clear();
    }
}