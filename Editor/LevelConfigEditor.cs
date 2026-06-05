using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(LevelConfig))]
public class LevelConfigEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var config = (LevelConfig)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Apply Level"))
            Apply(config);

        if (GUILayout.Button("Capture Scene → Level"))
            Capture(config);

        if (GUILayout.Button("Build Missing Overrides"))
            BuildMissing(config);
    }

    void Apply(LevelConfig config)
    {
        var objects = FindObjectsByType<LevelObject>();

        foreach (var obj in objects)
        {
            var match = config.overrides
                .FirstOrDefault(o => o.objectId == obj.ObjectId);

            if (match == null)
                continue;

            Undo.RecordObject(obj.transform, "Apply Level");

            if (match.overridePosition)
                obj.transform.position = match.position;

            if (match.overrideRotation)
                obj.transform.eulerAngles = match.rotation;

            if (match.overrideScale)
                obj.transform.localScale = match.scale;

            var renderer = obj.GetComponent<Renderer>();
            if (renderer != null && match.overrideMaterial && match.material != null)
                renderer.sharedMaterial = match.material;
        }
    }

    void Capture(LevelConfig config)
    {
        var objects = FindObjectsByType<LevelObject>();

        config.overrides = objects.Select(o =>
        {
            var renderer = o.GetComponent<Renderer>();

            return new ObjectOverride
            {
                objectId = o.ObjectId,

                overridePosition = true,
                position = o.transform.position,

                overrideRotation = true,
                rotation = o.transform.eulerAngles,

                overrideScale = true,
                scale = o.transform.localScale,

                overrideMaterial = renderer != null,
                material = renderer ? renderer.sharedMaterial : null
            };
        }).ToArray();

        EditorUtility.SetDirty(config);
        AssetDatabase.SaveAssets();
    }

    void BuildMissing(LevelConfig config)
    {
        var objects = FindObjectsByType<LevelObject>();

        var list = config.overrides.ToList();

        foreach (var obj in objects)
        {
            bool exists = list.Any(o => o.objectId == obj.ObjectId);

            if (exists)
                continue;

            var renderer = obj.GetComponent<Renderer>();

            list.Add(new ObjectOverride
            {
                objectId = obj.ObjectId,

                overridePosition = false,
                overrideRotation = false,
                overrideScale = false,
                overrideMaterial = false
            });
        }

        config.overrides = list.ToArray();

        EditorUtility.SetDirty(config);
        AssetDatabase.SaveAssets();
    }
}