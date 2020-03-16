using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_SimpleNavPursue : MonoBehaviour
{
    public float speed = 10f;
    public float stoppingDistance = 2f;
    public LayerMask playerLayer;
    
    private Enemy_Master enemyMaster;
    private GameManager_Master gameManagerMaster;
    private GameObject player;

    private NavMeshAgent nav;

    private void OnEnable()
    {
        Initialize();
        enemyMaster.OnEnemyDead += DisableNav;
        gameManagerMaster.OnGameOver += DisableNav;
    }

    private void Initialize()
    {
        nav = GetComponent<NavMeshAgent>();
        enemyMaster = GetComponent<Enemy_Master>();
        player = enemyMaster.GetPlayerGameObject();
        gameManagerMaster = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Master>();

        nav.stoppingDistance = stoppingDistance;
        nav.speed = speed;
        nav.updateRotation = false;
        SetNavTarget();
    }

    private void Update()
    {
        SetNavTarget();
        FixRotation();
        BrakeAtDestination();
    }

    private void FixRotation()
    {
        Vector3 faceTarget;
        Quaternion finalRotation;
        RaycastHit hit;

        Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, 100f);

        if (hit.transform.Equals(player.transform))
        {
            faceTarget = player.transform.position;
        }
        else
        {
            faceTarget = nav.steeringTarget;
        }

        finalRotation = Quaternion.RotateTowards(transform.rotation,
            Quaternion.LookRotation(faceTarget - transform.position),
            nav.angularSpeed * Time.deltaTime);

        transform.rotation = finalRotation;
    }

    private void BrakeAtDestination()
    {
        if(nav.remainingDistance < nav.stoppingDistance)
        {
            nav.velocity = nav.velocity / 1.1f;
        }
    }

    private void SetNavTarget()
    {
        nav.SetDestination(player.transform.position);
    }

    private void DisableNav()
    {
        nav.enabled = false;
        this.enabled = false;
    }

    private void OnDisable()
    {
        enemyMaster.OnEnemyDead -= DisableNav;
        gameManagerMaster.OnGameOver -= DisableNav;
    }
}
