// Attach this script to the LevelManager GameObject.
using UnityEngine;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public LevelConfig levelConfig;

    void Start()
    {
        ApplyLevel();
    }

    public void ApplyLevel()
    {
        Debug.Log("ApplyLevel CALLED");
        var objects = FindObjectsByType<LevelObject>();

        foreach (var obj in objects)
        {
            var match = levelConfig.overrides
                .FirstOrDefault(o => o.objectId == obj.ObjectId);

            if (match == null)
            {
                Debug.Log("Cont");
                continue;
            }

            Transform t = obj.transform;

            if (match.overridePosition)
                t.position = match.position;

            if (match.overrideRotation)
                t.eulerAngles = match.rotation;

            if (match.overrideScale)
                t.localScale = match.scale;

            if (match.overrideMaterial)
            {
                var renderer = obj.GetComponent<Renderer>();
                if (renderer != null && match.material != null)
                    renderer.sharedMaterial = match.material;
            }
        }
    }
}