using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

[System.Serializable]
public class Craft
{
    public string craftName;
    public GameObject prefab;
    public GameObject previewPrefab;
}

public class CraftManual : MonoBehaviour
{
    private bool isActivated = false;
    private bool isPreviewActivated = false;

    [SerializeField]
    private GameObject baseUIObj;

    [SerializeField]
    private Craft[] craftFire;

    private GameObject previewPrefab;
    private GameObject prefab;

    [SerializeField]
    private Transform playerTransform;

    private RaycastHit hitInfo;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float range;

    public void SlotClick(int slotNumber)
    {
        GameManager.isOpenCraftManual = false;

        previewPrefab = Instantiate(craftFire[slotNumber].previewPrefab, playerTransform.position + playerTransform.forward, Quaternion.identity);
        prefab = craftFire[slotNumber].prefab;
        isPreviewActivated = true;
        baseUIObj.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !isPreviewActivated)
            CheckWindow();

        if (Input.GetKeyUp(KeyCode.Escape))
            Cancel();

        if (Input.GetButtonDown("Fire1"))
            Build();

        if (isPreviewActivated)
            PreviewPositionUpdate();
    }

    private void Build()
    {
        if (isPreviewActivated && previewPrefab.GetComponent<PreviewObject>().IsBuildable())
        {
            Instantiate(prefab, hitInfo.point, Quaternion.identity);
            Destroy(previewPrefab);
            isActivated = false;
            isPreviewActivated = false;
            previewPrefab = null;
            prefab = null;
        }
    }

    private void PreviewPositionUpdate()
    {
        if (Physics.Raycast(playerTransform.position, playerTransform.forward, out hitInfo, range, layerMask))
        {
            if (hitInfo.transform != null)
            {
                Vector3 location = hitInfo.point;
                previewPrefab.transform.position = location;
            }
        }
    }

    private void Cancel()
    {
        if (isPreviewActivated)
            Destroy(previewPrefab);

        isPreviewActivated = false;
        previewPrefab = null;
        prefab = null;
        CloseWindow();
    }

    private void CheckWindow()
    {
        if (!isActivated)
            OpenWindow();
        else
            CloseWindow();
    }

    private void CloseWindow()
    {
        GameManager.isOpenCraftManual = false;
        isActivated = false;
        baseUIObj.SetActive(false);
    }

    private void OpenWindow()
    {
        GameManager.isOpenCraftManual = true;
        isActivated = true;
        baseUIObj.SetActive(true);
    }
}
