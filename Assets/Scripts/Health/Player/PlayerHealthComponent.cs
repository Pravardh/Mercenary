using Mercenary.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mercenary.HealthSystem
{
    public class PlayerHealthComponent : BaseHealthComponent, IHealthSystem
    {
        public bool IsDead()
        {
            return characterHealth.IsDead();
        }

        public void TakeDamage(float damageMagnitude)
        { 
            characterHealth.TakeDamage(damageMagnitude);
        }

        public void TakeHealth(float healthMagnitude)
        {
            characterHealth.TakeHealth(healthMagnitude);
        }

        public void Kill()
        {
            TakeDamage(100);

            gameManager.OnPlayerDead();
        }
    }
}
