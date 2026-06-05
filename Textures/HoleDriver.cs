using UnityEngine;

public class HoleDriver : MonoBehaviour
{
    void Update()
    {
        Shader.SetGlobalVector("_HolePosition", transform.position);
    }
}