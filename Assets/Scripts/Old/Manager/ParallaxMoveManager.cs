using System.Collections.Generic;
using UnityEngine;
using ZiercCode.Old.Basic;
using ZiercCode.Old.Environment;

namespace ZiercCode.Old.Manager
{

    public class ParallaxMoveManager : USingletonComponentDestroy<ParallaxMoveManager>
    {
        [SerializeField] private List<ParallaxMoveFeedBack> moveBackgrounds;
        [SerializeField] private GameObject optionTutorialTemp;
        [SerializeField] private Camera mainCamera;

        public void BackGroundMove()
        {
            foreach (var feedback in moveBackgrounds)
            {
                feedback.OnMove(mainCamera);
            }
        }

        public void Add()
        {
            if (moveBackgrounds == null) moveBackgrounds = new List<ParallaxMoveFeedBack>();
            ParallaxMoveFeedBack newParallaxMoveFeedBack = new ParallaxMoveFeedBack();
            moveBackgrounds.Add(newParallaxMoveFeedBack);
        }

        public void Delete()
        {
            if (moveBackgrounds != null && moveBackgrounds.Count > 0)
            {
                int last = moveBackgrounds.Count - 1;
                moveBackgrounds.RemoveAt(last);
            }
        }
    }
}