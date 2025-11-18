using System.Collections.Generic;
using System.Linq;
using R3;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using VContainer;

namespace RinaBullet.Direction.State {

    public interface IDirectionStateManager : IBulletDirectionHolder {
        
    }
    
    public class DirectionStateManager : SerializedMonoBehaviour {

        [OdinSerialize]
        [LabelText("稼働しているステートマシン")]
        [ReadOnly]
        private List<IDirectionStateMachine> _machines = new();

        private Vector3 _direction = Vector3.zero;


        private void Start() {
            
            var root = gameObject.transform.root.gameObject;

            var machines = root.GetComponentsInChildren<IDirectionStateMachine>().ToList();
            
            _machines = machines;
            
        }
        
        private void Update() {
            _direction = CalculateDirection();
        }

        private Vector3 CalculateDirection() {
            
            if (_machines.Count is 0) {
                return transform.forward;
            }
            
            var result = Vector3.zero;
            
            foreach (var machine in _machines) {

                if (machine is null) {
                    continue;
                }
                
                result += machine.Direction;
            }
            
            return result;
        }
    }
}