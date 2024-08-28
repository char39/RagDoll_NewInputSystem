using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollCtrl : MonoBehaviour
{
    public Rigidbody rb;
    public Rigidbody[] RagDoll_rb;
    public Collider[] RagDoll_cols;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        RagDoll_rb = transform.GetChild(2).GetComponentsInChildren<Rigidbody>();
        RagDoll_cols = transform.GetChild(2).GetComponentsInChildren<Collider>();
        SetRagDoll(true);
        //StartCoroutine(WakeUpRagDoll());
    }

    /// <summary> RagDoll의 IsKinematic을 Off </summary>
    void SetRagDoll(bool isEnable)
    {
        foreach (Rigidbody rb in RagDoll_rb)
            rb.isKinematic = !isEnable;
    }

    IEnumerator WakeUpRagDoll()
    {
        SetRagDoll(false);
        yield return new WaitForSeconds(1.0f);
        GetComponent<Animator>().enabled = false;
        SetRagDoll(true);
    }
}
