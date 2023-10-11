using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Mercenary.Animations
{
    public class PlayerAnimationEvents : MonoBehaviour
    {
        public static event Action OnPlayerAttack;


        public void AttackEnemy()
        {
            OnPlayerAttack?.Invoke();
        }

    }

}
