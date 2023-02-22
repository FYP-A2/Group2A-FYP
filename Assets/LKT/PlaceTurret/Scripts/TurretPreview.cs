using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace FYP2A.VR.PlaceTurrent
{
    public class TurretPreview : MonoBehaviour
    {
        public LayerMask layerNotCheckCollision;


        [SerializeField]
        private GameObject colorCanPlace;
        [SerializeField]
        private GameObject colorCannotPlace;


        public float offsetY;
        [SerializeField]
        private bool autoSetOffsetY = true;


        float rotateCDMin = 0.8f;
        float rotateCDMax = 1f;
        float rotateAngleMin = 50;
        float rotateAngleMax = 90;


        public TurretPlaceHint hint;



        [HideInInspector]
        public bool canPlace = false;
        [HideInInspector]
        public bool canPlaceUpgrade = false;

        [HideInInspector]
        public GameObject previewCreator;


        [SerializeField]
        List<GameObject> overlappedGameobject = new List<GameObject>();


        public TowerScriptableObject towerSO;
        public int tier { get => towerSO.level; }
        public enum PlaceType { baseT, upgradeT }
        [HideInInspector]
        public PlaceType placeType { get => tier == 0? PlaceType.baseT: PlaceType.upgradeT; }

        // Start is called before the first frame update
        void Start()
        {
            if (autoSetOffsetY)
                offsetY = colorCanPlace.transform.localPosition.y;

            colorCannotPlace.GetComponent<MeshRenderer>().enabled = true;
            colorCanPlace.GetComponent<MeshRenderer>().enabled = false;

            if (placeType == PlaceType.upgradeT)
            {
                StartCoroutine(AutoRotate());
            }
        }

        // Update is called once per frame
        void Update()
        {
            CheckCanPlace();

            DisplayHint(!canPlace);
        }

        public void Initialize(GameObject previewCreator,TowerScriptableObject towerScriptableObject)
        {
            this.previewCreator = previewCreator;
            this.towerSO = towerScriptableObject;
        }

        IEnumerator AutoRotate()
        {
            float nextRotateCDFull = UnityEngine.Random.Range(rotateCDMin, rotateCDMax);
            float nextRotateCD = nextRotateCDFull;
            float nextRotateAngle = UnityEngine.Random.Range(rotateAngleMin, rotateAngleMax);
            float startRotY = 0;

            while (true)
            {
                if (nextRotateCD <= 0)
                {
                    nextRotateCDFull = UnityEngine.Random.Range(rotateCDMin, rotateCDMax);
                    nextRotateCD = nextRotateCDFull;

                    startRotY = transform.localEulerAngles.y;
                    nextRotateAngle = UnityEngine.Random.Range(rotateAngleMin, rotateAngleMax);
                    if (UnityEngine.Random.Range(0, 2) == 0)
                        nextRotateAngle = -nextRotateAngle;
                    nextRotateAngle += startRotY;
                }
               

                float nowRotY = Mathf.Lerp(startRotY, nextRotateAngle, 1 - nextRotateCD / nextRotateCDFull);
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, nowRotY, transform.localEulerAngles.z);

                nextRotateCD -= Time.deltaTime;

                yield return null;
            }

        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        void CheckCanPlace()
        {
            bool result = false;
            if (placeType == PlaceType.baseT)
            {
                if (overlappedGameobject.Count == 0)
                    result = true;
                else
                {
                    result = false;
                    SetDisplayHint("Too close to other buildings");
                }
            }
            else
            {
                result = canPlaceUpgrade;
            }

            if (canPlace != result)
            {
                if (result)
                {
                    colorCanPlace.GetComponent<MeshRenderer>().enabled = true;
                    colorCannotPlace.GetComponent<MeshRenderer>().enabled = false;
                }

                else 
                {
                    colorCanPlace.GetComponent<MeshRenderer>().enabled = false;
                    colorCannotPlace.GetComponent<MeshRenderer>().enabled = true;
                }
                    
                canPlace = result;
            }
        }

        public void SetCanUpgrade()
        {
            canPlaceUpgrade = true;
        }

        public void SetCannotUpgrade(string reason)
        {
            canPlaceUpgrade = false;
            SetDisplayHint(reason);
        }

        private void SetDisplayHint(string msg)
        {
            hint.SetDisplayHint(msg);
        }

        private void DisplayHint(bool condition)
        {
            hint.DisplayHint(condition, previewCreator.transform.position);
        }

        private void OnTriggerEnter(Collider other)
        {
            GameObject go = other.gameObject;
            if (layerNotCheckCollision != (layerNotCheckCollision | (1 << go.layer)))
            if (go.layer != 10 &&
                go.layer != 12 &&
                !go.Equals(colorCanPlace) &&
                !go.Equals(colorCannotPlace))
                overlappedGameobject.Add(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if (overlappedGameobject.Contains(other.gameObject))
                overlappedGameobject.Remove(other.gameObject);
        }
    }
}