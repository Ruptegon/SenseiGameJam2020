using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClientUIController : MonoBehaviour
{

    public static ClientUIController Instance;

    [SerializeField] public Button UpArrow; 
    [SerializeField] public Button DownArrow; 
    [SerializeField] public Button RightArrow; 
    [SerializeField] public Button LeftArrow;

    public static float HorizontalInput;
    public static float VertiaclInput;

    private void Awake()
    {
        Instance = this;
        
        gameObject.SetActive(Net.IsClient);
    }


}
