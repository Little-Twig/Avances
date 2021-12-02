using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    [SerializeField] private GameObject originOne;

    //[SerializeField] protected float speedEnemy = 50f;
    //[SerializeField] private float distanceRay = 50f;

    [SerializeField] protected MinionData myData;

    protected Animator animMinion;
    protected Rigidbody rbMinion;

    private bool isAttack = false;
    protected bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        rbMinion = GetComponent<Rigidbody>();
        animMinion = gameObject.transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(!isAttack)
        {
            FindEnemy();
            Move();

        }
    }

    public virtual void Move()
    {
        Vector3 direction = Vector3.left;
        rbMinion.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        rbMinion.AddForce(direction * myData.speed, ForceMode.Impulse);
    }

    public void FindEnemy()
    {
        BroadcastRaycast(originOne.transform);
    }
    private void BroadcastRaycast(Transform origin)
    {
        RaycastHit hit;
        if(Physics.Raycast(origin.position, origin.TransformDirection(Vector3.forward), out hit, myData.distanceRay))
        {
            if(hit.transform.CompareTag("Player"))
            {
                Debug.Log(" Hit ");
                isAttack = true;
                rbMinion.velocity = Vector3.zero;
                animMinion.SetTrigger("Attack");
            }
        }
    }
    public void DrawRay(Transform origin)
    {
        Gizmos.color = Color.green;
        Vector3 direction = origin.TransformDirection(Vector3.left) * myData.distanceRay;
        Gizmos.DrawRay(origin.position, direction);
    }
    private void OnDrawGizmos()
    {
        DrawRay(originOne.transform);
    }
}
