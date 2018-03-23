using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    //animal walking radius and the timer for time walking a certain direction
    public float radius;
    public float timer;

    //switching between idle and walking
    private Transform target;
    private NavMeshAgent agent;
    private float currentTimer;

    //how long the animal stays idle
    private bool idle;
    public float idleTimer;
    private float currentIdleTimer;

    //call our other animations for the animal (tiger)
    private Animation anim;

    //health for the animal
    public float health;
    private bool dead;


    //amount of items dropped, and what items dropped
    public int amountOfItems;
    public GameObject[] item;

    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animation>();

        currentTimer = timer;
        currentIdleTimer = idleTimer;
    }

    void Update()
    {
        currentTimer += Time.deltaTime;
        currentIdleTimer += Time.deltaTime;
        if(currentTimer >= timer)
        {
            Vector3 newPostion = RandomNavSphere(transform.position, radius, -1);
            agent.SetDestination(newPostion);
            currentTimer = 0; //resets the timer, so that movement will continue
        }

        if (idle)
        {
            anim.CrossFade("idle"); //calls the idle animation

        }
        else
        {
            anim.CrossFade("walk"); //calls the walk animation
        }

        if (health <= 0)
        {
            Die(); //calls the Die method if you punish the defenseless animal enough
        }
    }

    IEnumerator switchIdle()
    {
        idle = true;
        yield return new WaitForSeconds(3);
        currentIdleTimer = 0;
        idle = false;
    }

    public void DropItems()
    {
        for (int i = 0; i < amountOfItems; i++)
        {
            GameObject droppedItem = Instantiate(item[i], transform.position, Quaternion.identity);
            break;
        }
    }
    public void Die()
    {
        DropItems();
        Destroy(this.gameObject);
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layerMask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, layerMask);

        return navHit.position;
    }

}
