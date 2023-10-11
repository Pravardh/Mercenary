using Mercenary.HealthSystem;
using Mercenary.Utilities;
using UnityEditor;
using UnityEngine;

namespace Mercenary.StateMachine
{
    public abstract class BaseState
    {
        private const float LEFT_ORIENTATION = 1;
        private const float RIGHT_ORIENTATION = 0;

        protected GameObject characterReference;
        protected Transform characterEyes;
        protected Animator characterAnimator;
        protected EnemyDetector characterEnemyDetector;

        //Use data  containers for this
        protected float characterVisionRange = 8f;
        protected float characterAttackRange = 1.5f;

        protected BaseState nextState;

        protected Stage currentStage;

        protected IHealthSystem characterHealthSystem;

        public BaseState(GameObject _characterReference, Transform _characterEyes, Animator _characterAnimator, IHealthSystem _characterHealthSystem)
        {
            characterReference = _characterReference;
            characterEyes = _characterEyes;
            characterAnimator = _characterAnimator;
            characterHealthSystem = _characterHealthSystem;
        }


        protected CharacterOrientation CalculateCharacterOrientation()
        {
            //Get character orientation value based on localRotation.

            if (characterReference.transform.localRotation.y == LEFT_ORIENTATION)
            {
                return CharacterOrientation.LEFT;
            }
            else if(characterReference.transform.localRotation.y == RIGHT_ORIENTATION)
            {
                return CharacterOrientation.RIGHT;
            }

            return CharacterOrientation.NONE;
        }

        protected CharacterOrientation CalculateFaceDirection(Transform _targetToFace)
        {
            //Get orientation character should face for _target.

            if (_targetToFace.position.x < characterReference.transform.position.x)
            {
                return CharacterOrientation.LEFT;
            }
            else if (_targetToFace.position.x > characterReference.transform.position.x)
            {
                return CharacterOrientation.RIGHT;
            }

            return CharacterOrientation.NONE;
        }

        protected void SetCharacterOrientation(CharacterOrientation characterOrientation)
        {
            if (characterOrientation == CharacterOrientation.LEFT)
            {
                characterReference.transform.localRotation = new Quaternion(0.0f, LEFT_ORIENTATION, 0.0f, 0.0f);
            }
            else if (characterOrientation == CharacterOrientation.RIGHT)
            {
                characterReference.transform.localRotation = new Quaternion(0.0f, RIGHT_ORIENTATION, 0.0f, 0.0f);

            }
        }

        protected virtual GameObject TryDetectEnemyInRange()
        {
            //Check to see if a health component is in range. if it is then return the game object. This is 
            //useful for the AI

            Vector2 _startPosition = characterEyes.transform.position;

            RaycastHit2D hitInfo = Physics2D.Raycast(
                _startPosition,
                characterEyes.right,
                characterVisionRange
                );


            if (hitInfo)
            {
                if (hitInfo.transform.gameObject.TryGetComponent(out IHealthSystem _healthSystem))
                {
                    return hitInfo.transform.gameObject;
                }
            }

            return null;
        }

        protected IHealthSystem TryAttackEnemyInRange()
        {

            GameObject enemyRef = TryDetectEnemyInRange();

            if (enemyRef == null) return null;

            if (IsInAttackingRange(characterReference.transform, enemyRef.transform))
            {

                return enemyRef.GetComponent<IHealthSystem>();
            }

            return null;
        }


        public virtual void OnBegin()
        {
            currentStage = Stage.TICK;
        }
        public virtual void OnTick()
        {
            currentStage = Stage.TICK;
        }
        public virtual void OnEnd()
        {
            currentStage = Stage.END;
        }

        protected void SwitchState(BaseState newState)
        {
            //Function to change state, and sets currentStage to END so that  execute() function can call OnEnd() to start the new state
            if (newState == null) return;

            nextState = newState;
            currentStage = Stage.END;
        }

        public BaseState Execute()
        {
            //Based on the currrentStage enum run the particular functions.

            if (currentStage == Stage.ENTER)
            {
                OnBegin();
            }
            if(currentStage == Stage.TICK)
            {
                OnTick();
            }
            if (currentStage == Stage.END)
            {
                OnEnd();
                return nextState;
            }

            return this;    
        }

        protected bool IsInAttackingRange(Transform _controllerPlayer, Transform _enemyPlayer)
        {
            //Check to see if enemy is in characterAttackRange.

            return Mathf.Abs(Vector2.Distance(_controllerPlayer.position, _enemyPlayer.position)) < characterAttackRange;
        }

    }
}
