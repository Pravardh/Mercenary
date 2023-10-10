using Mercenary.HealthSystem;
using Mercenary.User;
using Mercenary.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Mercenary.StateMachine
{
    public abstract class BaseEnemyState : BaseState
    {
        protected Transform waypoint1;
        protected Transform waypoint2;

        protected Transform moveToTarget;

        protected EnemyStates characterState;
        protected EnemyMovementState characterMovementState;

        protected float characterReachTolerance = 1f;

        protected bool shouldEnemyTick = true;

        public BaseEnemyState(GameObject _characterReference, Transform _characterEyes, Animator _characterAnimator, IHealthSystem _characterHealthSystem, Transform _waypoint1, Transform _waypoint2) : base(_characterReference, _characterEyes, _characterAnimator, _characterHealthSystem)
        {
            waypoint1 = _waypoint1;
            waypoint2 = _waypoint2;
        }

        protected void SetMoveToTarget(Transform moveToTarget)
        {
            this.moveToTarget = moveToTarget;
        }

        protected EnemyMovementState TryMoveToTarget(float speedMultiplier = 1) //Change this to sprint data container
        {
            CharacterOrientation _shouldFaceDirection;

            if (moveToTarget == null)
            {
                SetMoveToTarget(waypoint1);

                //Helper this should be removed
            }

            _shouldFaceDirection = FaceTarget(moveToTarget);

            if (!HasReachedTarget())
            {
                characterMovementState = EnemyMovementState.ONGOING;
                characterReference.transform.Translate(
                    (_shouldFaceDirection == CharacterOrientation.RIGHT ? characterReference.transform.right : -characterReference.transform.right) 
                    * Time.deltaTime * speedMultiplier
                    );
            }
            else
            {
                characterMovementState = EnemyMovementState.REACHED;
                OnTargetReached();
            }

            return characterMovementState;
        }
        protected override GameObject TryDetectEnemyInRange()
        {
            //Use data container for range
            //Returns null if enemy is not in range

            //Check to see if I contain 

            if (PlayerPrefs.HasKey("Invisibility"))
            {
                Debug.Log("invisibility is still active");
                return null;
            }


            Vector2 _startPosition = characterEyes.position;

            RaycastHit2D hitInfo = Physics2D.Raycast(
                _startPosition,
                characterEyes.right,
                characterVisionRange
                );


            if (hitInfo)
            {
                if (hitInfo.transform.gameObject.TryGetComponent(out PlayerHealthComponent _healthComponent))
                {
                    Debug.Log(hitInfo.transform.gameObject.name);
                    return hitInfo.transform.gameObject;
                }
            }

            return null;
        }

        virtual protected void OnTargetReached()
        {

        }

        protected CharacterOrientation FaceTarget(Transform _targetToFace)
        {
            CharacterOrientation _faceDirection = CalculateFaceDirection(moveToTarget);
            SetCharacterOrientation(_faceDirection);
            return _faceDirection;
        }

        protected bool HasReachedTarget()
        {
            return Mathf.Abs(Vector2.Distance(characterReference.transform.position, moveToTarget.position)) <= characterReachTolerance;
        }

        public override void OnTick()
        {
            if (!shouldEnemyTick) return;

            base.OnTick();

            if (characterHealthSystem.IsDead() && shouldEnemyTick)
            {

                SwitchState(new EnemyDeathState(characterReference, characterEyes, characterAnimator, characterHealthSystem, waypoint1, waypoint2));
            }
        }
    }
}

