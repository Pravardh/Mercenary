using Mercenary.HealthSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Mercenary.StateMachine
{
    public class EnemyDeathState : BaseEnemyState
    {
        public EnemyDeathState(GameObject _characterReference, Transform _characterEyes, Animator _characterAnimator, IHealthSystem _characterHealthSystem, Transform _waypoint1, Transform _waypoint2) : base(_characterReference, _characterEyes, _characterAnimator, _characterHealthSystem, _waypoint1, _waypoint2)
        {
            characterState = EnemyStates.ATTACK;   
        }

        public override void OnBegin()
        {
            characterAnimator.SetTrigger("isDead");
            shouldEnemyTick = false;
            Debug.Log("Is dead");
            GameObject.Destroy(characterReference, 1f);
            base.OnBegin();
        }
        public override void OnTick()
        {
            base.OnTick();


        }

        public override void OnEnd()
        {

            base.OnEnd();
        }


        protected override void OnTargetReached()
        {
            base.OnTargetReached();

        }
    }
}
