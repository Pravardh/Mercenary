using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mercenary.Utilities
{
    public class EnemyDetector : MonoBehaviour
    {

        private bool canAttackEnemy = false;

        public bool CanAttackEnemy { get { return canAttackEnemy; } }

        public void StartAttacking()
        {
            canAttackEnemy = true;
        }

        public void StopAttacking()
        {
            canAttackEnemy = false;
        }
    }

}
