using Mercenary.HealthSystem;
using Mercenary.Utilities;
using UnityEngine;

namespace Mercenary.StateMachine
{
    public abstract class BaseStateMachine : MonoBehaviour
    {
        [SerializeField]
        protected Transform characterEyes;

        [SerializeField]
        protected Animator characterAnimator;

        protected GameObject characterReference;
        protected EnemyDetector enemyDetector;

        protected IHealthSystem characterHealthSystem;

        protected void InitBase()
        {
            //Init base values

            characterReference = gameObject;

            enemyDetector = GetComponent<EnemyDetector>();

            characterHealthSystem = GetComponent<IHealthSystem>();

        }

    }

}
