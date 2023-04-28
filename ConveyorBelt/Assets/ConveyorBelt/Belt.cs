using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConveyorBelt {
    public class Belt : MonoBehaviour {
        public static int BeltID = 0;

        [SerializeField] private float positionPadding = 0.3f;
        [SerializeField] private Belt beltInSequence;
        [SerializeField] private BeltItem beltItem;
        [SerializeField] private bool isSpaceTaken;

        private BeltManager _beltManager;

        private void Start() {
            _beltManager = FindObjectOfType<BeltManager>(); //change this so the belt manager spawns this and set its self to this.
            beltInSequence = null;
            beltInSequence = FindNextBelt();
            gameObject.name = $"Belt: {BeltID++}";
        }

        private void Update() {
            if (beltInSequence == null) beltInSequence = FindNextBelt();

            if (beltItem != null && beltItem.Item != null) StartCoroutine(StartBeltMove());
        }

        public Vector3 GetBeltPosition() {
            var beltPosition = transform.position;
            return new Vector3(beltPosition.x, beltPosition.y + positionPadding, beltPosition.z);
        }

        private IEnumerator StartBeltMove() {
            isSpaceTaken = true;

            if (beltItem.Item != null && beltInSequence != null && beltInSequence.isSpaceTaken == false) {
                var toPosition = beltInSequence.GetBeltPosition();
                beltInSequence.isSpaceTaken = true;
                var step = _beltManager.BeltSpeed * Time.deltaTime;

                while (beltItem.Item.transform.position != toPosition) {
                    beltItem.Item.transform.position =
                        Vector3.MoveTowards(beltItem.transform.position, toPosition, step);

                    yield return null;
                }

                isSpaceTaken = false;
                beltInSequence.beltItem = beltItem;
                beltItem = null;
            }
        }

        private Belt FindNextBelt() {
            var currentBeltTransform = transform;
            RaycastHit hit;
            var forward = transform.forward;
            var ray = new Ray(currentBeltTransform.position, forward);

            if (Physics.Raycast(ray, out hit, 1f)) {
                var belt = hit.collider.GetComponent<Belt>();
                if (belt != null) return belt;
            }

            return null;
        }
    }
}