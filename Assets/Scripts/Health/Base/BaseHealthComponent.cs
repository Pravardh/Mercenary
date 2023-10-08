using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mercenary.HealthSystem
{
    public class BaseHealthComponent : MonoBehaviour
    {
        protected Health characterHealth;


        //private DataContainerSO dataContainer
        void Start()
        {
            characterHealth = new Health(100.0f, 100.0f);
            //Replace this with data container
            //current and max health will be replaced with dataContainer's values
        }


    }

}
