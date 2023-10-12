using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Mercenary.Animations
{
    public class AnimationEvents : MonoBehaviour
    {
        public event Action OnAttack;
        public event Action OnAttackEnd;

        public void Attack()
        {
            OnAttack?.Invoke();
        }

        public void EndAttack()
        {
            OnAttackEnd?.Invoke();
        }
    }

}
