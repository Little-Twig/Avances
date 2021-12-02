using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mimic : Minion
{
    [SerializeField] private GameObject player;

    //[SerializeField] private float timer = 5f;

    private bool Active = false;

    public override void Move()
    {
        if(myData.timer<0 && !isDead)
        {
            
            animMinion.SetBool("Active", true);
            Vector3 playerDirection = GetPLayerDirection();
            rbMinion.rotation = Quaternion.LookRotation(new Vector3(playerDirection.x, 0, playerDirection.z));
            rbMinion.AddForce(playerDirection.normalized * myData.speed, ForceMode.Impulse);
        }
        myData.timer -= Time.deltaTime;

    }
   
    public Vector3 GetPLayerDirection()
    {
        return player.transform.position - transform.position;
    }
}
