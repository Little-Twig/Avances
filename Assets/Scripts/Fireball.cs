using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float proyectileSpeed = 10f;
    [SerializeField] private UnityEvent onPlyrCollission;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddRelativeForce(0, 0, proyectileSpeed);
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            
            Debug.Log("Fireball activó daño a MainCharController");
            onPlyrCollission?.Invoke();
        }
    }
}
