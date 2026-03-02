using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{
    public string MenuName = "Menu";

    public float MapSize = 5;

    public float CameraSize = 2.5f;
}
