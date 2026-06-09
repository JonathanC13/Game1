using UnityEngine;

public static class EvidenceGameObjectGenerator
{
    public static void Create(Evidence evidence, GameObject prefab)
    {

        GameObject cube = GameObject.Instantiate(prefab);


        cube.name = evidence.Type.ToString();


        EvidenceObject obj = cube.AddComponent<EvidenceObject>();


        obj.Evidence = evidence;
    }
}