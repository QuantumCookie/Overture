using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private GameManager_Master gameManagerMaster;

    //Movement
    public float moveSpeed = 15f;
    private Vector3 speedSmoothVelocity;
    public float movementSmoothingTime = 0.01f;
    private Vector3 currentVelocity;

    //Turning
    public float turnSpeed = 20f;
    public LayerMask floorMask;
    private float maxRaycastDistance = 100;
    private Quaternion newRotation;

    //Swoosh
    public float swooshDistance = 5f;
    public float swooshDuration = 1f;
    public float swooshCooldown = 1f;
    private bool isSwooshing;
    private float lastSwoosh;

    private void OnEnable()
    {
        Initialize();
        gameManagerMaster.OnGameOver += DisableThis;
    }

    private void Initialize()
    {
        gameManagerMaster = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Master>();
    }

    void Update()
    {
        Swoosh();
        Move();
        Turn();
    }

    private void Swoosh()
    {
        if (Time.time - lastSwoosh < swooshCooldown) return;

        if (Input.GetKeyDown(KeyCode.Space) && !isSwooshing)
        {
            Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f,  Input.GetAxisRaw("Vertical")).normalized;

            if (direction == Vector3.zero) return;

            StartCoroutine(SwooshCoroutine(direction));
        }
    }

    private IEnumerator SwooshCoroutine(Vector3 direction)
    {
        isSwooshing = true;

        float initialVelocity = 1.85f * swooshDistance;
        float velocity = initialVelocity;
        float elapsedTime = 0f;

        float dx = 0.45f * Mathf.PI;
        float x = 0.05f * Mathf.PI;

        while((elapsedTime / swooshDuration) < 0.75f)
        {
            transform.position += velocity * direction * Time.deltaTime;
            yield return null;

            elapsedTime += Time.deltaTime;

            velocity = initialVelocity / Mathf.Tan(x + dx * (elapsedTime / swooshDuration));
        }

        lastSwoosh = Time.time;
        isSwooshing = false;
    }

    private void Move()
    {
        //if (isSwooshing) return;

        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;

        transform.position += direction * moveSpeed * Time.deltaTime;
        //Vector3 targetPos = transform.position + direction * moveSpeed * Time.fixedDeltaTime;
        //transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref speedSmoothVelocity, movementSmoothingTime);
    }

    private void Turn()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxRaycastDistance, floorMask))
        {
            Vector3 targetDir = hit.point - transform.position;

            newRotation = Quaternion.Euler(0f, Mathf.Atan2(targetDir.x, targetDir.z) * Mathf.Rad2Deg, 0f);
            transform.rotation = newRotation;
        }
    }

    private void DisableThis()
    {
        this.enabled = false;
    }

    private void OnDisable()
    {
        gameManagerMaster.OnGameOver -= DisableThis;
    }
}