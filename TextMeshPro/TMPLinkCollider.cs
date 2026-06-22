using TMPro;
using UnityEngine;


public class TMPLinkCollider : MonoBehaviour
{
    public TMP_Text text;

    BoxCollider box;

    void Awake()
    {
        box = GetComponent<BoxCollider>();
    }


    void Update()
    {
        box.center =
            text.bounds.center;


        box.size =
            text.bounds.size;
    }
}