using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Sub_Ui : MonoBehaviour

{
    public GameObject uiParent;
    private bool scaledUp = false;



    void Start()
    {
        scaleDown();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            discUiManager();
        }
    }


    public void discUiManager ()
    {
        if( scaledUp == true)
        {
            scaleDown();
        }
       else
        {
            scaleUp();
        }

    }
        


    private void scaleUp()
    {
        uiParent.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.InOutQuad);
        scaledUp = true;
    }

    private void scaleDown()
    {
        uiParent.transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.InOutQuad);
        scaledUp = false;
    }
}
