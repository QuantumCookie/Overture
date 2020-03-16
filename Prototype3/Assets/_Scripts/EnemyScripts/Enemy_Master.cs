using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Master : MonoBehaviour
{
    public int id;
    public string enemyName;

    private GameObject player;
    //private GameManager_Master gameManagerMaster;

    public delegate void GeneralEvent();

    public event GeneralEvent OnEnemyDead;

    public delegate void HealthEvent(float delta);

    public event HealthEvent OnHealthGained;
    public event HealthEvent OnHealthDeducted;

    public delegate void NavEvent(Transform target);

    public event NavEvent OnNavTargetSet;
    public event NavEvent OnNavTargetReached;

    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        //gameManagerMaster = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Master>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnDisable()
    {
        
    }

    public void CallHealthDeductedEvent(float delta)
    {
        if(OnHealthDeducted != null)
        {
            OnHealthDeducted(delta);
        }
    }

    public void CallHealthGainedEvent(float delta)
    {
        if (OnHealthGained != null)
        {
            OnHealthGained(delta);
        }
    }

    public void CallEnemyDeadEvent()
    {
        if (OnEnemyDead != null)
        {
            OnEnemyDead();
        }
    }

    public void CallNavTargetSetEvent(Transform target)
    {
        if(OnNavTargetSet != null)
        {
            OnNavTargetSet(target);
        }
    }

    public void CallNavTargetReachedEvent(Transform target)
    {
        if (OnNavTargetReached != null)
        {
            OnNavTargetReached(target);
        }
    }

    public GameObject GetPlayerGameObject()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
        return player;
    }
}
