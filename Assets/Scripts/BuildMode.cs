using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildMode", menuName = "ScriptableObjects/BuildMode")]
public class BuildMode : ScriptableObject
{
    public enum Mode { Server, Client}

    public Mode GameMode; 
}
