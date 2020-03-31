using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryController : MonoBehaviour
{
    [Header("Stats Reference")]
    public StatsManager statsManager;

    [Header("Aim Adjust Settings")]
    public float heightFromBottom;
    private Vector2 mousePos;
    private float angle;
    private Vector2 bottomOfScreen;
    private Vector2 relativePos;

    // Attacking (VALUES SET BY STAT MANAGER)
    [HideInInspector] public float attackSpeed;
    [HideInInspector] public float damage;
    [HideInInspector] public float attackCd;
    private float currentAttackCd;

    [Header("Object References")]
    public GameObject bullet;
    public GameObject muzzleFlash;

    [Header("Sentry Tiers")]
    public GameObject[] sentries;
    public int currentTier;

    [Header("Aim Positions (Runtime)")]
    public int currentAimPosIndex = 0;
    public List<Transform> aimPositions = new List<Transform>();

    void Start()
    {
        currentTier = 0;
        // Rotation
        bottomOfScreen = new Vector2(Screen.width / 2, heightFromBottom);
        UpdateAimPositions(transform.GetChild(0).gameObject);
    }

    void Update()
    {
        if (Input.mousePosition.y > heightFromBottom) RotateSentry();

        if (Input.mousePosition.y > Screen.height / 5) MouseInput();

        if (currentAttackCd > 0) currentAttackCd -= Time.deltaTime;
    }

    public void Evolve()
    {
        // Disable current sentry model
        transform.GetChild(currentTier).gameObject.SetActive(false);
        // Increase tier
        currentTier++;
        // Enable next sentry model (currentTier has already been incremented)
        GameObject newSentry = transform.GetChild(currentTier).gameObject;
        newSentry.SetActive(true);
        // Update aim positions
        UpdateAimPositions(newSentry);
        // Set stats
        statsManager.IncreaseAttackSpeed(statsManager.attackSpeed * (aimPositions.Count - 1));
    }


    void UpdateAimPositions(GameObject sentry)
    {
        aimPositions.Clear();
        var barrels = sentry.transform.GetChild(0);
        // Loop through all barrels
        for (int i = 0; i < barrels.childCount; i++)
        {
            aimPositions.Add(barrels.GetChild(i).GetChild(0));
        }
    }

    void Shoot()
    {
        if (currentAttackCd <= 0)
        {
            // If the current aim position is the final one in the list, then go to 0
            if (currentAimPosIndex == aimPositions.Count - 1) currentAimPosIndex = 0;
            else currentAimPosIndex++;

            GameObject bulletClone = Instantiate(bullet, aimPositions[currentAimPosIndex].position, Quaternion.Euler(transform.eulerAngles));
            bulletClone.GetComponent<Bullet>().damage = damage;
            Instantiate(muzzleFlash, aimPositions[currentAimPosIndex].position, Quaternion.Euler(transform.eulerAngles));

            /*
            foreach (Transform aimPos in aimPositions)
            {
                GameObject bulletClone = Instantiate(bullet, aimPos.position, Quaternion.Euler(transform.eulerAngles));
                bulletClone.GetComponent<Bullet>().damage = damage;
                Instantiate(muzzleFlash, aimPos.position, Quaternion.Euler(transform.eulerAngles));
            }
            */

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
