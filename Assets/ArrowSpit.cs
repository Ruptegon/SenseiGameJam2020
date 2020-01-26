using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpit : MonoBehaviour
{
    [SerializeField] GameObject arrow;

    private List<GameObject> childs;

    private int secondDelay = 4;
    int secondTimer;

    private void Start()
    {
        secondTimer = Mathf.CeilToInt(GameManager.SyncTimer);
    }

    void Update()
    {
        if (GameManager.SyncTimer > secondTimer + secondDelay)
        {
            secondTimer += secondDelay;
            Run();
        }
    }

    private void Run()
    {
        var obj = Instantiate<GameObject>(arrow, this.transform.position + this.transform.forward, Quaternion.identity);
        childs.Add(obj);
    }

    private void OnDestroy()
    {
        childs.ForEach(x => Destroy(x.gameObject));
    }
}
