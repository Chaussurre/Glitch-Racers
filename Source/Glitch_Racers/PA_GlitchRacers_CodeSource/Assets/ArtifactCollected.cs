using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArtifactCollected : MonoBehaviour
{
    private IEnumerator OnTriggerEnter(Collider other)
    {
        Destroy(GetComponent<CapsuleCollider>());
        GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(3f);
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }
}
