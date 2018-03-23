using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Player stuff
    public float maxHealth, maxThirst, maxHunger;
    private float health, thirst, hunger;
    public float thirstIncreaseRate, hungerIncreaseRate;
    public bool dead;


    //animal AI stuff
    public float damage;
    public static bool triggeringWithAI;
    public static GameObject triggeringAI;

    //functions
    public void Start()
    {
        health = maxHealth;
    }

    public void Update()
    {
        //hunger and thirst increase logic is here.
        if (!dead)
        {
            thirst += thirstIncreaseRate * Time.deltaTime;
            hunger += hungerIncreaseRate * Time.deltaTime;
        }

        if (thirst >= maxThirst)
            Die();
        if (hunger >= maxHunger)
            Die();

        //detecting and killing the AI
        if(triggeringWithAI == true && triggeringAI) //if the object/AI is existing, then we can attack it through the below iF statement
        {
            if(Input.GetMouseButtonDown(0)) //code for what button is your Attack
            {
                Attack(triggeringAI);
            }
        }
    }

    public void Attack(GameObject target)
    {
        //check for if the target is tagged for Animal
        if(target.tag == "Animal")
        {
            Animal animal = target.GetComponent<Animal>();
            animal.health -= damage;

            
        }
    }

    public void Die()
    {
        dead = true;
        print("You are Dead!");
    }

    public void Drink(float decreaseRate)
    {
        thirst -= decreaseRate;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Animal")
        {
            triggeringAI = other.gameObject;
            triggeringWithAI = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Animal")
        {
            triggeringAI = null;
            triggeringWithAI = false; //stops detecting animal since we are not colliding with it
        }
    }

}
