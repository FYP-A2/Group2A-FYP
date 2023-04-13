using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using static ResourceGroupType;

namespace FYP2A.VR.PlaceTurret
{
    //This component is needed to set on player prefab
    public class PlaceTurret : MonoBehaviour, IPlaceTurret
    {
        public Player player;

        TurretPreview nowPreview;
        TowerBuildSO nowBuild;
        public bool isPreviewing;

        [Header("Place Turret")]
        [SerializeField]
        XRRayInteractor xrRayInteractor;

        public TurretPrefabIndex turretPrefabIndex;

        [SerializeField]
        UIBook book;

        [SerializeField]
        InputActionProperty inputConfirm;
        [SerializeField]
        InputActionProperty inputCancel;

        float placeAnimationHeight = 10f;
        float placeAnimationduration = 0.3f;

        [Header("Test Input")]
        public InputActionProperty testSelect;
        public int test1TowerID;
        public InputActionProperty testSelect2;
        public int test2TowerID;
        public InputActionProperty testSelect3;
        public int test3TowerID;
        public InputActionProperty testSelect4;
        public int test4TowerID;


        float placeCD = 0;


        protected void OnEnable()
        {
            inputConfirm.EnableDirectAction();
            inputCancel.EnableDirectAction();
            testSelect.EnableDirectAction();
            testSelect2.EnableDirectAction();
            testSelect3.EnableDirectAction();
            testSelect4.EnableDirectAction();
        }
        protected void OnDisable()
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

            if (placeCD > 0)
                placeCD -= Time.deltaTime;
        }

        private void InputConfirm_Action_performed(InputAction.CallbackContext obj)
        {
            if (inputConfirm.action.ReadValue<float>() > 0.3f && isPreviewing && nowPreview.canPlace)
            {
                PlaceDownTurret();
            }
        }

        private void InputCancel_Action_performed(InputAction.CallbackContext obj)
        {
            if (isPreviewing)
            {
                DeletePreview();
                book.SelectExitAllButton();
            }
        }

        public void SetTurretPrefabIndex(TurretPrefabIndex tpi)
        {
            turretPrefabIndex = tpi;
        }

        public void SetPreviewTurret(int towerID)
        {
            nowBuild = turretPrefabIndex.towerBuildSOs[towerID];
            SetPreviewTurret(nowBuild);
        }


        bool SetPreviewTurret(TowerBuildSO turretType)
        {
            SetPlaceCD(0.3f);

            Debug.Log("Turret:  Set preview :" + turretType.ToString());
            if (CheckResources(turretType.resourceGroup))
            {

                CreatePreview(turretType.towerPreview, turretType.Tower.level);

                return true;
            }
            else
            {
                DeletePreview();
                book.SelectExitAllButton();
            }

            return false;
        }

        public void CreatePreview(GameObject previewPrefab, int tier)
        {
            DeletePreview();

            nowPreview = Instantiate(previewPrefab).GetComponent<TurretPreview>();
            nowPreview.Initialize(gameObject, nowBuild.Tower);
            isPreviewing = true;
        }

        public void DeletePreview()
        {
            if (nowPreview != null)
                Destroy(nowPreview.gameObject);

            nowPreview = null;
            isPreviewing = false;

        }

        protected virtual Ray GetRay()
        {
            return new Ray(xrRayInteractor.rayOriginTransform.position, xrRayInteractor.rayOriginTransform.rotation * Vector3.forward);
            //important
        }

        //server execute
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
                TurretUpgradeConnector2 hittedTuc2;
                if (hit.transform != null && hit.transform.TryGetComponent(out hittedTuc2) && hittedTuc2.confirmedConnection == false && hittedTuc2.parentConnector != null)
                {
                    if (nowPreview.tier == hittedTuc2.parentConnector.GetDistanceToBase() + 1)
                    {
                        if (nowBuild.requiredBaseType == TowerBuildSO.RequiredBaseType.NoRequired)
                            nowPreview.SetCanUpgrade();
                        //if For required Base type (arrow or magic)
                        if (nowBuild.requiredBaseType == TowerBuildSO.RequiredBaseType.Phy)
                            if (hittedTuc2.parentConnector.GetTowerSO().towerType == TowerScriptableObject.TowerType.Phy)
                                nowPreview.SetCanUpgrade();
                            else
                                nowPreview.SetCannotUpgrade("Place this upgrade on Phy base");

                        if (nowBuild.requiredBaseType == TowerBuildSO.RequiredBaseType.Magic)
                            if (hittedTuc2.parentConnector.GetTowerSO().towerType == TowerScriptableObject.TowerType.Magic)
                                nowPreview.SetCanUpgrade();
                            else
                                nowPreview.SetCannotUpgrade("Place this upgrade on Magic base");


                    }
                    else
                        nowPreview.SetCannotUpgrade("Place this upgrade on floor " + (nowPreview.tier + 1));

                    nowPreview.SetPosition(hittedTuc2.transform.position);
                }
                else
                {
                    nowPreview.SetCannotUpgrade("Place this upgrade on a building");
                    nowPreview.SetPosition(hit.point);
                }
            }
        }

        //server execute
        void PlaceDownTurret()
        {
            if (placeCD > 0)
                return;

            if (nowPreview.canPlace && CheckResourcesAndPay(nowBuild.resourceGroup))
            {
                GameObject go = Instantiate(nowBuild.Tower.towerPrefab, nowPreview.gameObject.transform.position + new Vector3(0, nowPreview.offsetY + placeAnimationHeight, 0), nowPreview.gameObject.transform.rotation);
                go.GetComponent<TurretUpgradeConnector1>().SetTowerSO(nowBuild.Tower);

                StartCoroutine(PlaceDownTurretAnimation(go.transform, placeAnimationHeight, placeAnimationduration));
                DeletePreview();
                book.SelectExitAllButton();
            }
        }

        void SetPlaceCD(float duration)
        {
            placeCD = duration;
        }

        //server boardcast
        IEnumerator PlaceDownTurretAnimation(Transform turret, float height, float duration)
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
            yield return null;
            yield return null;
            yield return null;
            yield return null;
            yield return null;


            TurretUpgradeConnector1 tuc1;
            if (turret.TryGetComponent(out tuc1) && tuc1.connectorDown != null)
            {
                tuc1.connectorDown.ConfirmConnection();
                tuc1.GetBaseConnector().GetComponent<Tower>().UpdateTowerSO(nowBuild.Tower);
            }



            yield return null;
            yield return null;
            yield return null;
            yield return null;
            yield return null;
            yield return null;
            yield return null;
            yield return null;
            yield return null;
            yield return null;
            yield return null;
            yield return null;
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

            //Debug Start

            foreach (TurretUpgradeConnector1.Nexus tucNex in tuc1.nexus)
            {
                if (tucNex.Active && tucNex.inConnector != null)
                {
                    tucNex.inConnector.GetComponent<Renderer>().material.color = new Color(1, 1, 0, 0.5f);
                    tucNex.inConnector.ConnectedConnector3.GetComponent<Renderer>().material.color = new Color(1, 1, 0, 0.5f);
                }

                //tucNex.towerPearlSlot.SetPearl(TowerPearlSlot.PearlType.Speed);
            }


            //string debugMsg = "tc";
            //List<TurretUpgradeConnector1> tuc1List = tuc1.GetBaseConnector().GetAllConnector();
            //foreach (TurretUpgradeConnector1 a in tuc1List)
            //    if (a!=null)
            //        debugMsg += "Tower" + a.GetTowerSO().ToString() + "\n";
            //Debug.Log(debugMsg);
            //
            //Debug.Log("all pearl: " + tuc1.GetAllActivatedPearl().Count);
        }

        //server execute
        bool CheckResourcesAndPay(ResourceGroupTypeSO neededResource)
        {
            bool result = CheckResources(neededResource);

            if (result)
                Pay(neededResource);


            return result;
        }

        public static bool CheckResources(List<Resource> neededResource, List<Resource> ownedResource)
        {
            bool result = true;
            for (int i = 0; i < neededResource.Count; i++)
                if (ownedResource[i].amount < neededResource[i].amount)
                    result = false;
            return result;
        }

        bool CheckResources(ResourceGroupTypeSO neededResource)
        {
            bool result = true;
            for (int i = 0; i < neededResource.resources.Count; i++)
                if (player.resourceGroup.GetAmount(i) < neededResource.resources[i].amount)
                    result = false;
            return result;
        }

        void Pay(ResourceGroupTypeSO neededResource)
        {
            for (int i = 0; i < neededResource.resources.Count; i++)
                player.resourceGroup.Add(i, -neededResource.resources[i].amount);
        }


    }
}


