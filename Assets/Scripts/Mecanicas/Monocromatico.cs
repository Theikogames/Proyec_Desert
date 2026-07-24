using UnityEngine;

public class Monocromatico : MonoBehaviour
{
    public float _RadioColor = 0f;
    public Material[] materiales;

    void Update()
    {
        for (int i = 0; i < materiales.Length; i++)
        {
            materiales[i].SetFloat("_RadioColor", _RadioColor);
        }
    }
}