using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mercenary.HealthSystem
{ 
    public sealed class Health
    {
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
            return currentHealth <= 0;
        }

        public void TakeDamage(float damageMagnitude)
        {
            currentHealth = Mathf.Clamp(currentHealth - damageMagnitude, 0.0f, maxHealth);
        }

        public void TakeHealth(float healthMagnitude)
        {
            currentHealth = Mathf.Clamp(currentHealth + healthMagnitude, currentHealth, maxHealth);
        }
    }
}
