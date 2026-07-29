using UnityEngine;

public class SumonPolera : MonoBehaviour
{
    public GameObject objetoPrefab;

    void Update()
    {
        // Sumonear al presionar la tecla Espacio
        if (Input.GetKeyDown(KeyCode.L))
        {
            Vector3 posicionDeseada = new Vector3(23.456f, 1.5f, -7.523f);

            Instantiate(objetoPrefab, posicionDeseada, Quaternion.identity);
        }
    }
}
