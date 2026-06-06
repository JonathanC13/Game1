using System;
using UnityEngine;

// Array for ObjectOverrides for the level
[CreateAssetMenu(menuName = "Levels/Level Config")] // allows you to automatically list a custom ScriptableObject class in the Assets > Create context menu. 
public class LevelConfig : ScriptableObject
{
    public ObjectOverride[] overrides;
}

// ObjectOverride class for attributes that can be set
[Serializable]
public class ObjectOverride
{
    public string objectId;

    [Header("GameObject")]
    public bool overrideActive;
    public bool active = true;

    [Header("Transform")]
    public bool overridePosition;
    public Vector3 localPosition;

    public bool overrideRotation;
    public Vector3 localRotation;

    public bool overrideScale;
    public Vector3 localScale = Vector3.one;

    [Header("Visual")]
    public bool overrideMaterial;
    public Material material;

    [Header("Light")]
    public bool overrideLightEnabled;
    public bool lightEnabled;

    public bool overrideLightIntensity;
    public float lightIntensity = 1f;

    public bool overrideLightTemperature;
    public float lightTemperature = 6570f;

    public bool overrideLightColor;
    public Color lightColor = Color.white;
}