using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody; // El objeto padre del jugador (el cuerpo)

    private float xRotation = 0f;

    void Start()
    {
        // Bloquea el cursor en el centro de la pantalla y lo oculta
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Mouse.current == null) return;

        // Obtenemos el movimiento del mouse en este frame
        Vector2 mouseDelta = Mouse.current.delta.ReadValue() * (mouseSensitivity * Time.deltaTime);

        float mouseX = mouseDelta.x;
        float mouseY = mouseDelta.y;

        // Rotación vertical (mirar arriba/abajo) - Rotamos la cámara
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limitamos para no dar la vuelta completa

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotación horizontal (mirar izquierda/derecha) - Rotamos el cuerpo completo del jugador
        if (playerBody != null)
        {
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}