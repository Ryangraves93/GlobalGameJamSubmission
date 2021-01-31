using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public GameObject Player = GameObject.Find("Player");
    public bool attacking = false;
    private void LateUpdate()
    {
      // transform.position = new Vector3(Player.transform.position.x + 1, Player.transform.position.y, Player.transform.position.z);
       //transform.position = new Vector3(1, 0, 0);
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "breakable")
        {
            Debug.Log("Player is trying to break!");
            if (GameObject.Find("Player").GetComponent<Player>().attacking && attacking)
            {
                col.collider.GetComponent<Breakable>().breakMe();
            }
        }

    }
}
