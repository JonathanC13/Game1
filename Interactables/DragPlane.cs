using UnityEngine;

public class DragPlane : MonoBehaviour
{
    public Vector2 bounds = new Vector2(2f, 1f);

    public Vector3 ClampPosition(Vector3 position)
    {
        Vector3 localPos = transform.InverseTransformPoint(position);


        localPos.x = Mathf.Clamp(
            localPos.x,
            -bounds.x,
            bounds.x
        );

        localPos.z = Mathf.Clamp(
            localPos.z,
            -bounds.y,
            bounds.y
        );


        return transform.TransformPoint(localPos);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(
            transform.position,
            new Vector3(
                bounds.x * 2,
                0,
                bounds.y * 2
            )
        );
    }
}