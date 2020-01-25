using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClientUIController : MonoBehaviour
{
    
    [SerializeField] private Button UpArrow; 
    [SerializeField] private Button DownArrow; 
    [SerializeField] private Button RightArrow; 
    [SerializeField] private Button LeftArrow;

    public static float HorizontalInput;
    public static float VertiaclInput;

    private void Awake()
    {
        gameObject.SetActive(Net.IsClient);

        UpArrow.onClick.AddListener(() => IncreaseValue(ref VertiaclInput));
        DownArrow.onClick.AddListener(() => DecreaseValue(ref VertiaclInput));
        RightArrow.onClick.AddListener(() => IncreaseValue(ref HorizontalInput));
        LeftArrow.onClick.AddListener(() => DecreaseValue(ref HorizontalInput));
    }

    private void IncreaseValue(ref float value)
    {
        value += 0.1f;
    }

    private void DecreaseValue(ref float value)
    {
        value -= 0.1f;
    }

    private void Update()
    {
        Debug.Log($"Horizontal: {HorizontalInput}, Vertical: {VertiaclInput}");
    }
}
