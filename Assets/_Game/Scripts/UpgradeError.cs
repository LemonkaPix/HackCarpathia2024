using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeError : MonoBehaviour
{


    IEnumerator ErrorHandle()
    {
        LeanTween.scale(gameObject, Vector3.one, .3f);
        LeanTween.rotate(gameObject, new Vector3(0f, 0f, 5f), .05f);
        yield return new WaitForSeconds(.05f);
        LeanTween.rotate(gameObject, new Vector3(0f, 0f, -5f), .05f);
        yield return new WaitForSeconds(.05f);
        LeanTween.rotate(gameObject, new Vector3(0f, 0f, 5f), .05f);
        yield return new WaitForSeconds(.05f);
        LeanTween.rotate(gameObject, new Vector3(0f, 0f, -5f), .05f);
        yield return new WaitForSeconds(.05f);
        LeanTween.rotate(gameObject, new Vector3(0f, 0f, 0f), .05f);
        yield return new WaitForSeconds(2f);

        CloseUpgradeError();
    }

    public void CloseUpgradeError()
    {
        LeanTween.scale(gameObject, Vector3.one, .3f);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(ErrorHandle());
    }
}
