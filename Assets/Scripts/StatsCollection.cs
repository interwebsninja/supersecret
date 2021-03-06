﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class StatsCollection
{
    [Range(0, 1)]
    public float maxHealth;
    public float defaultMaxHealth;

    public float currentHealth;

    [Range(0, 1)]
    public float speed;
    public float defaultSpeed;

    [Range(0, 1)]
    public float agility;
    public float defaultAgility;

    [Range(0, 1)]
    public float power;
    public float defaultPower;

    [Range(0, 1)]
    public float luck;
    public float defaultLuck;

    [Range(0, 1)]
    public float weight;
    public float defaultWeight;

    public StatsCollection(StatsCollection collection)
    {
        maxHealth = collection.maxHealth;
        speed = collection.speed;
        agility = collection.agility;
        power = collection.power;
        luck = collection.luck;
        weight = collection.weight;

        currentHealth = maxHealth;
        defaultMaxHealth = maxHealth;
        defaultSpeed = speed;
        defaultAgility = agility;
        defaultPower = power;
        defaultLuck = luck;
        defaultWeight = weight;
    }

    public StatsCollection(float healthValue, float speedValue, float agilityValue, float powerValue, float luckValue, float weightValue)
    {
        maxHealth = healthValue;
        speed = speedValue;
        agility = agilityValue;
        power = powerValue;
        luck = luckValue;
        weight = weightValue;

        currentHealth = maxHealth;
        defaultMaxHealth = maxHealth;
        defaultSpeed = speed;
        defaultAgility = agility;
        defaultPower = power;
        defaultLuck = luck;
        defaultWeight = weight;
    }
}
