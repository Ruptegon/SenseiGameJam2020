using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitFire : MonoBehaviour
{
    [SerializeField] GameObject fireball;

    private List<GameObject> childs;

    private int secondDelay = 4;
    int secondTimer;

    private void Start()
    {
        secondTimer = Mathf.CeilToInt(GameManager.SyncTimer);
    }

    void Update()
    {
        if(GameManager.SyncTimer > secondTimer + secondDelay)
        {
            secondTimer += secondDelay;
            Run();
        }
    }

    private void Run()
    {
        var obj = Instantiate<GameObject>(fireball, this.transform.position + this.transform.forward, Quaternion.identity);
        childs.Add(obj);
    }

    private void OnDestroy()
    {
        childs.ForEach(x => Destroy(x.gameObject));
    }
}
