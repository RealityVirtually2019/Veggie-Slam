﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsSpawner : MonoBehaviour {

    [SerializeField] private Transform[] spawn;
    [SerializeField] private GameObject[] fruit;
    // Use this for initialization
    void Start()
    {
        InvokeRepeating("SpawnFruit", 2f, 1f);
    }

    void SpawnFruit()
    {
        int index = Random.Range(0, spawn.Length);
        int fruitIndex = Random.Range(0, fruit.Length);
        GameObject fru = Instantiate(fruit[fruitIndex], spawn[index].position, Quaternion.identity, gameObject.transform);
        fru.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
