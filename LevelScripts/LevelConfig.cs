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

    [Header("Position")]
    public bool overridePosition;
    public Vector3 position;

    [Header("Rotation")]
    public bool overrideRotation;
    public Vector3 rotation;

    [Header("Scale")]
    public bool overrideScale;
    public Vector3 scale = Vector3.one;

    [Header("Visual")]
    public bool overrideMaterial;
    public Material material;
}