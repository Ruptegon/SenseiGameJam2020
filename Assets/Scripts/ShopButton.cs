using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Texture image;
    [SerializeField] int cost;

    Button button;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClickButton);
        GetComponent<RawImage>().texture = image;
    }

    private void OnClickButton()
    {
        DungeonKeeper.Instance.SetSelectedPrefab(prefab);
        DungeonKeeper.Instance.SetCostOfNext(cost);
    }

}
