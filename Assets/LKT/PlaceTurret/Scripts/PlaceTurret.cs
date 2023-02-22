using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

namespace FYP2A.VR.PlaceTurrent
{
    public class PlaceTurret : MonoBehaviour
    {
        [Header("Place Turret")]
        public GameObject towerManagerPrefab;

        float placeAnimationHeight = 10f;
        float placeAnimationduration = 0.3f;

        TurretPreview nowPreview;
        TowerBuildSO nowBuild;
        bool isPreviewing;
        [SerializeField]
        XRRayInteractor xrRayInteractor;

        [SerializeField]
        TurretPrefabIndex turrentPrefabIndex;

        [SerializeField]
        InputActionProperty inputConfirm;
        [SerializeField]
        InputActionProperty inputCancel;


        [Header("Test Input")]
        public InputActionProperty testSelect;
        public int test1TowerID;
        public InputActionProperty testSelect2;
        public int test2TowerID;
        public InputActionProperty testSelect3;
        public int test3TowerID;
        public InputActionProperty testSelect4;
        public int test4TowerID;


        private void OnEnable()
        {
            inputConfirm.EnableDirectAction();
            inputCancel.EnableDirectAction();
            testSelect.EnableDirectAction();
            testSelect2.EnableDirectAction();
            testSelect3.EnableDirectAction();
            testSelect4.EnableDirectAction();
        }
        private void OnDisable()
        {
            inputConfirm.DisableDirectAction();
            inputCancel.DisableDirectAction();
            testSelect.DisableDirectAction();
            testSelect2.DisableDirectAction();
            testSelect3.DisableDirectAction();
            testSelect4.DisableDirectAction();
        }

        // Start is called before the first frame update
        protected void Start()
        {
            inputConfirm.action.performed += InputConfirm_Action_performed;
            inputCancel.action.performed += InputCancel_Action_performed;
            testSelect.action.performed += Test_Action_performed;
            testSelect2.action.performed += Test2_Action_performed;
            testSelect3.action.performed += Test3_Action_performed;
            testSelect4.action.performed += Test4_Action_performed;
        }

        private void Test_Action_performed(InputAction.CallbackContext obj)
        {
            Debug.Log(test1TowerID);
            SetPreviewTurret(test1TowerID);
        }
        private void Test2_Action_performed(InputAction.CallbackContext obj)
        {
            SetPreviewTurret(test2TowerID);
        }

        private void Test3_Action_performed(InputAction.CallbackContext obj)
        {
            SetPreviewTurret(test3TowerID);
        }

        private void Test4_Action_performed(InputAction.CallbackContext obj)
        {
            SetPreviewTurret(test4TowerID);
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

        public void SetPreviewTurret(int towerID)
        {
            nowBuild = turrentPrefabIndex.towerBuildSOs[towerID];
            SetPreviewTurret(nowBuild);
        }


        bool SetPreviewTurret(TowerBuildSO turretType)
        {
            Debug.Log("Turret:  Set preview :" + turretType.ToString());
            if (CheckEnoughResources(turretType.neededResources))
            {

                CreatePreview(turretType.towerPreview, turretType.Tower.level);

                return true;
            }

            return false;
        }

        void CreatePreview(GameObject previewPrefab, int tier)
        {
            DeletePreview();

            nowPreview = Instantiate(previewPrefab).GetComponent<TurretPreview>();
            nowPreview.Initialize(gameObject, nowBuild.Tower);
            isPreviewing = true;
        }

        void DeletePreview()
        {
            if (nowPreview != null)
                Destroy(nowPreview.gameObject);

            nowPreview = null;
            isPreviewing = false;

        }

        protected virtual Ray GetRay()
        {
            return new Ray(xrRayInteractor.rayOriginTransform.position, xrRayInteractor.rayOriginTransform.rotation * Vector3.forward); ;
        }

        void SetPreviewPosition()
        {
            RaycastHit hit;
            Ray ray = GetRay();


            if (nowPreview.placeType == TurretPreview.PlaceType.baseT)
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
                if (hit.transform != null && hit.transform.TryGetComponent(out tupc) && tupc.confirmedConnection == false)
                {
                    if (nowPreview.tier == tupc.parentConnector.DistanceToBase() + 1)
                    {
                        nowPreview.SetCanUpgrade();
                    }
                    else
                        nowPreview.SetCannotUpgrade("Place this upgrade on floor " + (nowPreview.tier+1));

                    nowPreview.SetPosition(tupc.transform.position);
                }
                else
                {
                    nowPreview.SetCannotUpgrade("Place this upgrade on a building");
                    nowPreview.SetPosition(hit.point);
                }
            }

            
        }

        void PlaceDownTurrent()
        {
            if (nowPreview.canPlace)
            {
                GameObject tm = Instantiate(towerManagerPrefab,nowPreview.gameObject.transform.position + new Vector3(0, nowPreview.offsetY + placeAnimationHeight, 0), nowPreview.gameObject.transform.rotation);
                tm.GetComponent<TowerManager>().BuildTower(nowBuild.Tower);

                StartCoroutine(PlaceDownTurrentAnimation(tm.transform, placeAnimationHeight, placeAnimationduration));
                DeletePreview();
            }
        }

        IEnumerator PlaceDownTurrentAnimation(Transform turret,float height,float duration)
        {
            float t = 0;
            float originHeight = turret.position.y - height;

            turret.position = new Vector3(turret.position.x, originHeight, turret.position.z);

            yield return null;
            yield return null;
            yield return null;
            yield return null;
            yield return null;
            yield return null;
            yield return null;
            yield return null;
            yield return null;

            TurretUpgradeConnector1 tuc1;
            if (turret.GetChild(0).TryGetComponent(out tuc1) && tuc1.connectorDown != null)
                tuc1.connectorDown.ConfirmConnection();

            yield return null;
            yield return null;

            float heightNow;
            while (t < 1)
            {
                heightNow = Mathf.Lerp(originHeight + height, originHeight, t);
                turret.position = new Vector3(turret.position.x, heightNow, turret.position.z);

                t += Time.deltaTime / duration;
                yield return null;
            }
            turret.position = new Vector3(turret.position.x, originHeight, turret.position.z);
            yield return null;
            yield return null;
            yield return null;
            yield return null;
            if (tuc1.nexus[0].canPlaceOre && tuc1.nexus[0].inConnector != null)
            {
                tuc1.nexus[0].inConnector.GetComponent<Renderer>().material.color = new Color(1, 1, 0, 0.5f);
                tuc1.nexus[0].inConnector.connectedConnector3.GetComponent<Renderer>().material.color = new Color(1, 1, 0, 0.5f);
            }
        }

        bool CheckEnoughResources(TowerBuildSO.Resources neededResources)
        {
            return true;
            //Check player has enough resources
        }
    }
}


