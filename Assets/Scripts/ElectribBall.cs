using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElectribBall : MonoBehaviour
{
    [SerializeField] private float proyectileSpeed = 50f;
    [SerializeField] private UnityEvent onPlyrAttack;
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

            Debug.Log("Fireball activó daño a MainCharControllerr");
            onPlyrAttack?.Invoke();
        }
    }
}
