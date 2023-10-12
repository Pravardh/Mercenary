using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Mercenary.Animations
{
    public class PlayerAnimationEvents : MonoBehaviour
    {
        public event Action OnPlayerAttack;
        public event Action OnPlayerAttackEnd;

        public void PlayerAttack()
        {
            OnPlayerAttack?.Invoke();
            Debug.Log("Attack Enemy");
        }

        public void StopPlayerAttack()
        {
            OnPlayerAttackEnd?.Invoke();
        }
    }

}
