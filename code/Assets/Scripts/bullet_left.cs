﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_left : MonoBehaviour
{
    public float Speed;
    
    // Update is called once per frame
    void Update()
    {
        transform.position += -transform.right * Time.deltaTime * Speed;
    }
} 