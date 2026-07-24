using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    [Header("Movimiento")]
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    [Header("deteccion de suelo")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [Header("Objeto vacio para agarrar objetos")]
    public Transform grabPoint; // Punto donde se agarrará el objeto
    
    [Header("Detección de objetos")]
    public float grabDistance = 5f; // Distancia máxima para agarrar objetos
    
    // Variables privadas para el movimiento
    private Vector3 velocity;
    private bool isGrounded;
    private Vector2 moveInput;
    [Header("Objeto agarrado")]
    private GameObject heldObject; // Objeto actualmente agarrado

    public Transform origenRayo; // Origen del rayo para detectar objetos

    public float Lanzamiento; //numero de lanzamiento

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 1. Verificar si estamos tocando el suelo
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Mantiene al personaje pegado al suelo
        }

        // 2. Procesar el movimiento (WASD / Flechas)
        if (Keyboard.current != null)
        {
            // Leemos los ejes horizontal (A/D) y vertical (W/S)
            float x = 0;
            float z = 0;
            // Verificamos si las teclas están presionadas y asignamos valores a x y z
            if (Keyboard.current.wKey.isPressed) z = 1f;
            if (Keyboard.current.sKey.isPressed) z = -1f;
            if (Keyboard.current.aKey.isPressed) x = -1f;
            if (Keyboard.current.dKey.isPressed) x = 1f;
            // Normalizamos el vector de entrada para que la velocidad sea consistente en todas las direcciones
            moveInput = new Vector2(x, z).normalized;
        }

        // Movemos en base a la dirección a la que mira el personaje (transform.right y transform.forward)
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * speed * Time.deltaTime);

        // 3. Procesar el Salto (Espacio)
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            // Ecuación física clásica para calcular la fuerza del salto: v = sqrt(h * -2 * g)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // 4. Aplicar Gravedad
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //si aprietas E, agarras un objeto cercano. Si ya tienes uno agarrado y aprietas E, lo sueltas
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null)
            {
                grabObject();
            }
            else
            {
                releaseObject();
            }
        }
    }

    // Detectar y agarrar el objeto más cercano con layer "Grabbable"
    public void grabObject()
    {
        if (heldObject != null) return; // Ya tiene un objeto agarrado

        // Raycast hacia adelante desde la cámara
        Ray ray = new Ray(origenRayo.position, origenRayo.forward);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * grabDistance, Color.green, 1f); // Dibuja el rayo en la escena para depuración

        if (Physics.Raycast(ray, out hit, grabDistance))
        {
            GameObject targetObj = hit.collider.gameObject;
            if (targetObj.layer == LayerMask.NameToLayer("Grabbable"))
            {
                // Agarrar el objeto
                heldObject = targetObj;
                Rigidbody rb = heldObject.GetComponent<Rigidbody>();
                
                if (rb != null)
                {
                    rb.isKinematic = true; // Hacer el objeto cinemático para que se mueva con el jugador
                }
                
                heldObject.transform.SetParent(grabPoint);
                heldObject.transform.localPosition = Vector3.zero;
                heldObject.transform.localRotation = Quaternion.identity;
            }
        }
    }

    // Soltar el objeto agarrado
    public void releaseObject()
    {
        if (heldObject == null) return; // No tiene nada agarrado

        // Soltar el objeto
        heldObject.transform.SetParent(null);
        
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false; // Restaurar física

            rb.AddForce(transform.forward * Lanzamiento, ForceMode.Impulse); // Aplicar una pequeña fuerza hacia adelante al soltar
        }
        
        heldObject = null;
    }
}