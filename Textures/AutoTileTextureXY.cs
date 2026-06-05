using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class AutoTileTextureXY : MonoBehaviour
{
    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        Material mat = rend.material;

        Vector3 scale = transform.localScale;
        Vector2 tiling = new Vector2(scale.x, scale.y);

        if (mat.HasProperty("_BaseMap"))
            mat.SetTextureScale("_BaseMap", tiling);

        if (mat.HasProperty("_MainTex"))
            mat.SetTextureScale("_MainTex", tiling);
    }
}