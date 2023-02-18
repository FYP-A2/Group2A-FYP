using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FYP2A.VR.PlaceTurrent
{
    public class TurrentPreview : MonoBehaviour
    {
        public GameObject TurrentPrefab;
        [HideInInspector]
        public bool canPlace = false;
        [HideInInspector]
        public bool canPlaceUpgrade = false;
        [SerializeField]
        private GameObject colorCanPlace;
        [SerializeField]
        private GameObject colorCannotPlace;

        public float offsetY;
        [SerializeField]
        private bool autoSetOffsetY = true;


        [SerializeField]
        List<GameObject> overlappedGameobject = new List<GameObject>();

        public enum PlaceType { baseT, upgradeT }
        public PlaceType placeType = PlaceType.baseT;

        // Start is called before the first frame update
        void Start()
        {
            if (autoSetOffsetY)
                offsetY = colorCanPlace.transform.localPosition.y;

            colorCannotPlace.GetComponent<MeshRenderer>().enabled = true;
            colorCanPlace.GetComponent<MeshRenderer>().enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            CheckCanPlace();
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
                result = overlappedGameobject.Count == 0;
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

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != 10 && other.gameObject.layer != 12 && !other.gameObject.Equals(colorCanPlace) && !other.gameObject.Equals(colorCannotPlace))
                overlappedGameobject.Add(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if (overlappedGameobject.Contains(other.gameObject))
                overlappedGameobject.Remove(other.gameObject);
        }
    }
}