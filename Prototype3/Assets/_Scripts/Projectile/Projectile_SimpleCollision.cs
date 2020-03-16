using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_SimpleCollision : MonoBehaviour
{
    private Projectile_Master projectileMaster;
    private Projectile_Data projectileData;

    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        projectileMaster = GetComponent<Projectile_Master>();
        projectileData = GetComponent<Projectile_Data>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Projectile " + projectileData.collidesWith.value + " collides with " + (1 << other.transform.root.gameObject.layer));

        if ((projectileData.collidesWith.value & (1 << other.transform.root.gameObject.layer)) == 0) return;

        //Debug.Log("Collision Successful");

        if (other.transform.root.tag == "Enemy")
            other.transform.root.gameObject.GetComponent<Enemy_Master>().CallHealthDeductedEvent(projectileData.damage);
        else if (other.transform.root.tag == "Player")
            other.transform.root.gameObject.GetComponent<Player_Master>().CallHealthDeductEvent(projectileData.damage);

        /*if (other.gameObject.tag == projectileData.sourceTag) return;
        
        if(other.tag == projectileData.targetTag)
        {
            if (other.transform.root.tag == "Enemy")
                other.transform.root.gameObject.GetComponent<Enemy_Master>().CallHealthDeductedEvent(projectileData.damage);
            else if(other.transform.root.tag == "Player")
                other.transform.root.gameObject.GetComponent<Player_Master>().CallHealthDeductEvent(projectileData.damage);
        }*/

        projectileMaster.CallTargetHitEvent(other.transform);
    }

    private void OnDisable()
    {
        
    }
}