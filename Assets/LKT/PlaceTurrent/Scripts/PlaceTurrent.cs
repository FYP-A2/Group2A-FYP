using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

namespace FYP2A.VR.PlaceTurrent
{
    public class PlaceTurrent : MonoBehaviour
    {
        TurrentPreview nowPreview;
        bool isPreviewing;
        [SerializeField]
        XRRayInteractor xrRayInteractor;

        [SerializeField]
        TurrentPrefabIndex turrentPrefabIndex;

        [SerializeField]
        InputActionProperty inputConfirm;
        [SerializeField]
        InputActionProperty inputCancel;

        [SerializeField]
        InputActionProperty testSelect;

        [SerializeField]
        InputActionProperty testSelect2;




        private void OnEnable()
        {
            inputConfirm.EnableDirectAction();
            inputCancel.EnableDirectAction();
            testSelect.EnableDirectAction();
            testSelect2.EnableDirectAction();
        }
        private void OnDisable()
        {
            inputConfirm.DisableDirectAction();
            inputCancel.DisableDirectAction();
            testSelect.DisableDirectAction();
            testSelect2.DisableDirectAction();
        }

        // Start is called before the first frame update
        void Start()
        {
            inputConfirm.action.performed += InputConfirm_Action_performed;
            inputCancel.action.performed += InputCancel_Action_performed;
            testSelect.action.performed += Test_Action_performed;
            testSelect2.action.performed += Test2_Action_performed;
        }

        private void Test_Action_performed(InputAction.CallbackContext obj)
        {
            SetPreviewTurrent(turrentPrefabIndex.turrentsBase[0]);
        }
        private void Test2_Action_performed(InputAction.CallbackContext obj)
        {
            SetPreviewTurrent(turrentPrefabIndex.turrentsUpgrade[0]);
        }

        // Update is called once per frame
        void Update()
        {
            if (isPreviewing)
                SetPreviewPosition();
        }

        private void InputConfirm_Action_performed(InputAction.CallbackContext obj)
        {
            if (inputConfirm.action.ReadValue<float>() > 0.3f && isPreviewing && nowPreview.canPlace)
            {
                PlaceDownTurrent();
            }
        }

        private void InputCancel_Action_performed(InputAction.CallbackContext obj)
        {
            if (isPreviewing)
            {
                DeletePreview();
            }
        }

        bool SetPreviewTurrent(TurrentPrefabIndex.Turrent turrentType)
        {
            Debug.Log("Set preview");
            if (CheckEnoughResources(turrentType.neededResources))
            {

                CreatePreview(turrentType.preview);

                return true;
            }

            return false;
        }

        void CreatePreview(GameObject previewPrefab)
        {
            DeletePreview();

            nowPreview = Instantiate(previewPrefab).GetComponent<TurrentPreview>();
            isPreviewing = true;

            Debug.Log("Create");

        }

        void DeletePreview()
        {
            if (nowPreview != null)
                Destroy(nowPreview.gameObject);

            nowPreview = null;
            isPreviewing = false;

            Debug.Log("Delete");
        }

        void SetPreviewPosition()
        {
            RaycastHit hit;
            Ray ray = new Ray(xrRayInteractor.rayOriginTransform.position, xrRayInteractor.rayOriginTransform.rotation * Vector3.forward);
 
            if (nowPreview.placeType == TurrentPreview.PlaceType.baseT)
            {
                Physics.Raycast(ray, out hit, 100, 1 << 10);
                nowPreview.SetPosition(hit.point);
            }
            else
            {
                LayerMask lm = 1 << 10;
                lm |= (1 << 12);
                Physics.Raycast(ray, out hit, 100, lm);
                TurretUpgradeConnector2 tupc;
                if (hit.transform != null && hit.transform.TryGetComponent(out tupc))
                {
                    nowPreview.canPlaceUpgrade = true;
                    nowPreview.SetPosition(tupc.transform.position);
                }
                else
                {
                    nowPreview.canPlaceUpgrade = false;
                    nowPreview.SetPosition(hit.point);
                }
            }

            
        }

        void PlaceDownTurrent()
        {
            if (nowPreview.canPlace)
            {
                //if (nowPreview.placeType==TurrentPreview.PlaceType.baseT)
                Instantiate(nowPreview.TurrentPrefab, nowPreview.gameObject.transform.position + new Vector3(0,nowPreview.offsetY,0), nowPreview.gameObject.transform.rotation);
                DeletePreview();
            }
        }

        bool CheckEnoughResources(TurrentPrefabIndex.Resources neededResources)
        {
            return true;
            //Check player has enough resources
        }
    }
}


