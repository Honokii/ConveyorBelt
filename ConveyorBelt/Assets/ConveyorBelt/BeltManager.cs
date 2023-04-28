using UnityEngine;

namespace ConveyorBelt {
    public class BeltManager : MonoBehaviour {
        
        [SerializeField] private float speed = 2f;
        
        public float BeltSpeed => speed;
    }
}