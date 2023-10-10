using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mercenary.HealthSystem
{ 
    public sealed class Health
    {
        //This is a class with helper functions like take damage, take health, etc.

        private float currentHealth;
        private float maxHealth;

        public float CurrentHealth { get { return currentHealth; } }
        public float MaxHealth { get { return maxHealth; } }

        public Health(float _currentHealth, float _maxHealth) 
        {
            currentHealth = _currentHealth;
            maxHealth = _maxHealth;
        }
        public bool IsDead()
        {
            //Check to see if character is dead.

            return currentHealth <= 0;
        }

        public void TakeDamage(float damageMagnitude)
        {
            //TakeDamage and then clamp.

            currentHealth = Mathf.Clamp(currentHealth - damageMagnitude, 0.0f, maxHealth);
        }

        public void TakeHealth(float healthMagnitude)
        {
            //TakeHealth and then clamp.

            currentHealth = Mathf.Clamp(currentHealth + healthMagnitude, currentHealth, maxHealth);
        }
    }
}
