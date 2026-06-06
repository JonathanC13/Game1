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
        //Debug.Log("ApplyLevel CALLED");
        var objects = Resources.FindObjectsOfTypeAll<LevelObject>()
            .Where(o => o.gameObject.scene.IsValid())
            .ToArray();

        foreach (var obj in objects)
        {
            var match = levelConfig.overrides
                .FirstOrDefault(o => o.objectId == obj.ObjectId);

            if (match == null)
            {
                continue;
            }

            Transform t = obj.transform;

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

            if (match.overrideMaterial)
            {
                var renderer = obj.GetComponent<Renderer>();
                if (renderer != null && match.material != null)
                    renderer.sharedMaterial = match.material;
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

            // Set active state after changes because could cause error when trying to apply to disabled object.
            if (match.overrideActive)
            {
                obj.gameObject.SetActive(match.active);
            }
        }
    }
}