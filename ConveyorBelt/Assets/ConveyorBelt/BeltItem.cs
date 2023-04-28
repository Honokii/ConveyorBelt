using System;
using UnityEngine;

namespace ConveyorBelt {
    public class BeltItem : MonoBehaviour {
        private GameObject _item;

        public GameObject Item => _item;

        private void Awake() {
            _item = gameObject;
        }
    }
}