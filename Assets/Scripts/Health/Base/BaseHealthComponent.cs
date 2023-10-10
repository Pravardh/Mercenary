using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mercenary.Managers;

namespace Mercenary.HealthSystem
{
    public class BaseHealthComponent : MonoBehaviour
    {

        //Base health component for both player and enemy. It constructs the health object.

        [SerializeField]
        protected GameManager gameManager;

        protected Health characterHealth;

        void Awake()
        {
            characterHealth = new Health(100.0f, 100.0f);
        }


    }

}
