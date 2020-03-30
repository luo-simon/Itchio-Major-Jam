using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryController : MonoBehaviour
{
    // Systems
    public StatsManager statsManager;

    // Rotation
    private Vector2 mousePos;
    private float angle;
    private Vector2 bottomOfScreen;
    private Vector2 relativePos;
    public float heightFromBottom;

    // Attacking (VALUES SET BY STAT MANAGER)
    [HideInInspector] public float attackSpeed;
    [HideInInspector] public float damage;
    [HideInInspector] public float attackCd;
    [SerializeField] private float currentAttackCd;

    public GameObject bullet;
    public Transform aimPos;
    public GameObject muzzleFlash;

    void Start()
    {
        // Rotation
        bottomOfScreen = new Vector2(Screen.width / 2, heightFromBottom);
    }

    void Update()
    {
        if (Input.mousePosition.y > heightFromBottom) RotateSentry();

        if (Input.mousePosition.y > Screen.height / 5) MouseInput();

        if (currentAttackCd > 0) currentAttackCd -= Time.deltaTime;
    }

    void Shoot()
    {
        if (currentAttackCd <= 0)
        {
            GameObject bulletClone = Instantiate(bullet, aimPos.position, Quaternion.Euler(transform.eulerAngles));
            bulletClone.GetComponent<Bullet>().damage = damage;
            Instantiate(muzzleFlash, aimPos.position, Quaternion.Euler(transform.eulerAngles));
            currentAttackCd = attackCd;
        }
    }

    void MouseInput()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    void RotateSentry()
    {
        mousePos = Input.mousePosition;
        relativePos = mousePos - bottomOfScreen;

        angle = -CalculateAngleFromVector(relativePos) + 90;
        angle = Mathf.Clamp(angle, -70f, 70f);

        var rotation = Quaternion.Euler(0, angle, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
        //transform.eulerAngles = new Vector3(0, currentAngle, 0);
    }

    float CalculateAngleFromVector(Vector2 vector2)
    {
        var y = vector2.y;
        var x = vector2.x;
        var angle_rad = Mathf.Atan2(y, x);
        return Mathf.Rad2Deg * angle_rad;
    }
}
