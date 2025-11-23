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
        
        private CompositeDisposable _requestDisposables = new();

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
            
            TransitionNextState();
        }

        private void TransitionNextState() {
            for (int i = _currentIndex; i < _states.Count; i++) {
                var state = _states[i];
                
                if (state is null) {
                    continue;
                }

                TransitionState(i);
            }
        }

        protected bool TransitionState(int index) {
            var state = _states[index];
            
            if (state is null) {
                return false;
            }
            
            _currentState?.Exit();
            _requestDisposables.Dispose();
            _currentState = state;
            _currentIndex = index;
            _currentState.Enter();
            
            RegisterTransitionRequest(state);
            
            return true;
        }
        

        protected void RegisterTransitionRequest(IDirectionState state) {
            
            _requestDisposables.Clear();
            
            state
                .OnNextStateRequest
                .Subscribe(_ => TransitionNextState())
                .AddTo(_requestDisposables);
        }
    }
    
}