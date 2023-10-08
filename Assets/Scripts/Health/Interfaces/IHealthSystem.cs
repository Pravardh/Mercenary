using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mercenary.HealthSystem
{
    public interface IHealthSystem
    {
        public void TakeDamage(float damageMagnitude);
        public void TakeHealth(float healthMagnitude);
        public bool IsDead();
        public void Kill();


    }
}