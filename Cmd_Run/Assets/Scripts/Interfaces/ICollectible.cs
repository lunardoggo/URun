﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectible {
    void Start();
    void OnTriggerEnter2D(Collider2D other);
}