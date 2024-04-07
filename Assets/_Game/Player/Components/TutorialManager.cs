using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] CameraMovement cameraMovement;
    [SerializeField] Transform[] BuildingTransforms;
    [SerializeField] GameObject hubUpgrade;

    [SerializeField] DialogTrigger trigger1;
    [SerializeField] DialogTrigger trigger2;

    public static TutorialManager instance;
    public bool duringTutorial;


    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -10f);
    }
    private void Start()
    {
        DialogSystem.instance.OnDialog += OnNewDialog;
        cameraMovement.enabled = false;
        trigger1.StartDialog();
        duringTutorial= true;
        StartCoroutine(WaitForHubLevel());
    }

    IEnumerator WaitForHubLevel()
    {
        duringTutorial = false;
        yield return new WaitUntil(() => PlayerUpgrades.instance.hubLevel >= 1);
        hubUpgrade.GetComponent<UpgradeUIBehaviour>().CloseWindow();
        duringTutorial = true;
        trigger2.StartDialog();
        yield break;
    }

    public void OnNewDialog(int dialogID)
    {
        if( dialogID > 4 && dialogID <= 11)
        {
            Camera camera = Camera.main;
            LeanTween.moveX(camera.gameObject, BuildingTransforms[dialogID - 5].position.x, 0.5f);
            LeanTween.moveY(camera.gameObject, BuildingTransforms[dialogID - 5].position.y, 0.5f);
        }
        if(dialogID >= 13)
        {
            LeanTween.move(Camera.main.gameObject, new Vector3(0, 0, -10), 0.5f);
            Invoke(nameof(EnableMovement), 0.5f);
        }
    }

    void EnableMovement()
    {
        cameraMovement.enabled = true;
        duringTutorial = false;
    }
}
