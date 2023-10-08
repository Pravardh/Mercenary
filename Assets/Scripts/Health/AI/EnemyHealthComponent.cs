using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mercenary.HealthSystem
{
    public class EnemyHealthComponent : BaseHealthComponent, IHealthSystem
    {

        public bool IsDead()
        {
            return characterHealth.IsDead();
        }

        public void TakeDamage(float damageMagnitude)
        {
            characterHealth.TakeDamage(damageMagnitude);
            Debug.Log("Took damage of " + damageMagnitude);
        }

        public void TakeHealth(float healthMagnitude)
        {
            characterHealth.TakeHealth(healthMagnitude);
        }

        public void Kill()
        {
            TakeDamage(100);

        }
    }
}
