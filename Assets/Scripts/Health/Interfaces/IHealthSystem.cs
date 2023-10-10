using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mercenary.HealthSystem
{
    public interface IHealthSystem
    {
        //Interface that defines what functions a health component is supposed to implement
        public void TakeDamage(float damageMagnitude);
        public void TakeHealth(float healthMagnitude);
        public bool IsDead();
        public void Kill();


    }
}