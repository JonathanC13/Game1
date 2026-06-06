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
        // to also locate disabled objects.
        var objects = Resources.FindObjectsOfTypeAll<LevelObject>() 
            .Where(o => o.gameObject.scene.IsValid())
            .ToArray();

        foreach (var obj in objects)
        {
            var match = config.overrides
                .FirstOrDefault(o => o.objectId == obj.ObjectId);

            if (match == null)
                continue;

            Undo.RecordObject(obj.transform, "Apply Level");

            if (match.overridePosition)
            {
                obj.transform.localPosition = match.localPosition;
            }

            if (match.overrideRotation)
            {
                obj.transform.localEulerAngles = match.localRotation;
            }

            if (match.overrideScale)
            {
                obj.transform.localScale = match.localScale;
            }

            if (obj.TryGetComponent<Light>(out Light light))
            {
                if (match.overrideLightEnabled)
                    light.enabled = match.lightEnabled;

                if (match.overrideLightIntensity)
                    light.intensity = match.lightIntensity;

                if (match.overrideLightTemperature)
                {
                    light.useColorTemperature = true;
                    light.colorTemperature = match.lightTemperature;
                }

                if (match.overrideLightColor)
                    light.color = match.lightColor;
            }

            var renderer = obj.GetComponent<Renderer>();
            if (renderer != null && match.overrideMaterial && match.material != null)
                renderer.sharedMaterial = match.material;

            // Set active state after changes because could cause error when trying to apply to disabled object.
            if (match.overrideActive)
            {
                obj.gameObject.SetActive(match.active);
            }
        }
    }

    void Capture(LevelConfig config)
    {
        var objects = Resources.FindObjectsOfTypeAll<LevelObject>()
            .Where(o => o.gameObject.scene.IsValid())
            .ToArray();

        config.overrides = objects.Select(o =>
        {
            var renderer = o.GetComponent<Renderer>();
            Light light = o.GetComponent<Light>();

            return new ObjectOverride
            {
                objectId = o.ObjectId,

                overrideActive = true,
                active = o.gameObject.activeSelf,

                overridePosition = true,
                localPosition = o.transform.localPosition,

                overrideRotation = true,
                localRotation = o.transform.localEulerAngles,

                overrideScale = true,
                localScale = o.transform.localScale,

                overrideMaterial = renderer != null,
                material = renderer ? renderer.sharedMaterial : null,

                overrideLightEnabled = light != null,
                lightEnabled = light ? light.enabled : true,

                overrideLightIntensity = light != null,
                lightIntensity = light ? light.intensity : 1f,

                overrideLightTemperature = light != null,
                lightTemperature = light ? light.colorTemperature : 5000f,

                overrideLightColor = light != null,
                lightColor = light ? light.color : Color.white
            };
        }).ToArray();

        EditorUtility.SetDirty(config);
        AssetDatabase.SaveAssets();
    }

    void BuildMissing(LevelConfig config)
    {
        var objects = Resources.FindObjectsOfTypeAll<LevelObject>()
            .Where(o => o.gameObject.scene.IsValid())
            .ToArray();

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
                overrideMaterial = false,
                overrideLightEnabled = false,
                overrideLightIntensity = false,
                overrideLightTemperature = false,
                overrideLightColor = false
            });
        }

        config.overrides = list.ToArray();

        EditorUtility.SetDirty(config);
        AssetDatabase.SaveAssets();
    }
}