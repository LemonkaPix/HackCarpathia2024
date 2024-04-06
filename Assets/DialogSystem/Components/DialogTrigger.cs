using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] List<DialogProperties> Dialogs = new List<DialogProperties>();

    [Button]
    public void StartDialog()
    {
        DialogSystem.instance.StopDialog();
        DialogSystem.instance.DialogQueue.AddRange(Dialogs);
        DialogSystem.instance.NextDialog();
    }
}
