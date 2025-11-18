using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using R3;
using VContainer;

namespace RinaBullet.Direction.State {

    public interface IDirectionStateMachine {
        
        Vector3 Direction { get; }
    }
    
    public class DirectionStateMachine : SerializedMonoBehaviour, IDirectionStateMachine {

        private List<IDirectionState> _states = new();

        private IDirectionState _currentState;

        private int _currentIndex = -1;

        private IObjectResolver _resolver;
        
        public Vector3 Direction => _currentState.Direction;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            _resolver = resolver;
        }

        private void Start() {
            foreach (var state in _states) {
                state?.Initialize(gameObject.transform.root.gameObject, _resolver);
            }
        }

        private void TransitionNextState() {
            
        }

        protected void TransitionState(int index) {
            
        }
        

        protected void RegisterTransitionRequest(IDirectionState state) {
            state
                .OnNextStateRequest
                .Subscribe(_ => TransitionNextState())
                .AddTo(this);
        }
    }
    
}