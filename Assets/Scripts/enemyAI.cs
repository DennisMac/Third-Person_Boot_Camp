using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class enemyAI : MonoBehaviour
{
    public Animator creatureAnim;
    public Animator playerAnim;
    public NavMeshAgent enemy;
    public Transform[] waypoints;
    public Image playerLife;
    int waypointIndex;
    Transform player;
    bool playerIdentified;
    bool playerLost;
    bool Attacking;
    bool timerStarted;
    float coolTimer;
    float idleTimer;
    float idleTimerLength;
    public float enemyLife;

    private void Start()
    {
        playerIdentified = false;
        enemyLife = 1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            playerIdentified = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerLost = true;
        }
    }
    private void Update()
    {
        if (playerIdentified)
        {
            enemy.stoppingDistance = 2f;
            enemy.SetDestination(player.position);

            if (!creatureAnim.GetBool("Running") && !Attacking)
            {
                enemy.speed = 3f;
                creatureAnim.SetBool("Running", true);
                creatureAnim.SetBool("Walking", false);
            }
            if (!creatureAnim.GetBool("Attacking") && Attacking)
            {
                enemy.speed = 0f;
                creatureAnim.SetBool("Attacking",true);
                playerLife.fillAmount -= 0.1f;
                playerAnim.SetTrigger("GettingHit");
            }
            if (Vector3.Distance(transform.position, player.position) < enemy.stoppingDistance)
            {
                Attacking = true;
                creatureAnim.SetBool("Running", false);
            }
            else
            {
                Attacking = false;
                creatureAnim.SetBool("Attacking", false);
            }

        }
        else
        {
            enemy.stoppingDistance = 0.5f;
            if (!creatureAnim.GetBool("Walking") && !timerStarted)
            {
                enemy.speed = 1f;
                creatureAnim.SetBool("Walking", true);
                creatureAnim.SetBool("Running", false);
            }
            else
            {
                if (waypointIndex < waypoints.Length)
                {
                    enemy.SetDestination(waypoints[waypointIndex].position);
                }
                else
                {
                    waypointIndex = 0;
                }
                
            }
            if (Vector3.Distance(transform.position, waypoints[waypointIndex].position) <= enemy.stoppingDistance && !creatureAnim.GetBool("Idle"))
            {
                waypointIndex += 1;
                enemy.speed = 0f;
                idleTimerLength = Random.Range(2f, 5f);
                idleTimer = 0f;
                timerStarted = true;
                creatureAnim.SetBool("Idle", true);
                creatureAnim.SetBool("Walking", false);
            }
            if (timerStarted)
            {
                idleTimer += 1f * Time.deltaTime;
                if (idleTimer > idleTimerLength)
                {
                    creatureAnim.SetBool("Idle", false);
                    timerStarted = false;
                }
            }
        }
        if (playerLost)
        {
            coolTimer += 1f * Time.deltaTime;
            if (coolTimer >= 5f)
            {
                playerIdentified = false;
                playerLost = false;

            }
        }
    }
}
