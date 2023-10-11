using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Mercenary.Animations
{
    public class EnemyAnimationEvents : MonoBehaviour
    {
        public static event Action OnEnemyAttack;

        public void AttackPlayer()
        {
            OnEnemyAttack?.Invoke();
        }

    }

}
