using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mercenary.UI
{
    public class PlayFabMenuAnimator : MonoBehaviour
    {
        [SerializeField]
        private GameObject parentMenuElement;

        void Start()
        {
            LeanTween.scale(parentMenuElement, Vector3.one, 1f);    
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }

}
