using UnityEngine;

public class AumentarColor : MonoBehaviour
{
    public Monocromatico _color;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Polera")) 
        {
            _color._RadioColor += 1f;
            Destroy(other.gameObject);
        }
    }
}
