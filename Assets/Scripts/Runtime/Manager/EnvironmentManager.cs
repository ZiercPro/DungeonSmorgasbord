using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : SingletonIns<EnvironmentManager>
{
    [SerializeField] private List<BackMoveFeedBack> moveBackgrounds;
    [SerializeField] private GameObject optionTutorialTemp;
    [SerializeField] private Camera mainCamera;
    public Transform environmentRoot;

    public void BackGroundMove()
    {
        foreach (var feedback in moveBackgrounds)
        {
            feedback.OnMove(mainCamera);
        }
    }

    public void BackGroundStop()
    {
        foreach (var feedback in moveBackgrounds)
        {
            feedback.OnStop();
        }
    }
}