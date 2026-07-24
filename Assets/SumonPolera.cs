using UnityEngine;

public class SumonPolera : MonoBehaviour
{
    public GameObject objetoPrefab;

    void Update()
    {
        // Sumonear al presionar la tecla Espacio
        if (Input.GetKeyDown(KeyCode.L))
        {
            Vector3 posicionDeseada = new Vector3(0, 10, 0);

            Instantiate(objetoPrefab, posicionDeseada, Quaternion.identity);
        }
    }
}
