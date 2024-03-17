using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : SingletonIns<EnvironmentManager>
{
    [SerializeField] private List<BackMoveFeedBack> moveBackgrounds;
    [SerializeField] private GameObject optionTutorialTemp;
    public Transform environmentRoot;
    public void BackGroundMove()
    {
        foreach (var feedback in moveBackgrounds)
        {
            feedback.OnMove();
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