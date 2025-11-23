using System;
using System.Collections.Generic;
using GeneralModule.Symbol;
using RinaBullet.Context;
using RinaBullet.Context.Container;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using VContainer;

namespace RinaBullet.Symbol {
    public class Bullet : ASerializedSymbol {
        
        [Title("参照")]
        
        [OdinSerialize] 
        [ReadOnly] 
        private IReadOnlyList<IBulletContext> m_contexts;

        protected IObjectResolver m_resolver;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver ?? throw new ArgumentNullException();
        }
        
        public void Start() {
            OnPreStart();
            
            ApplyContexts();
            
            OnPostStart();
        }

        public void InitializeContext(IReadOnlyList<IBulletContext> contexts) {

            if (contexts.Count is 0) {
                return;
            }
            
            var copiedContexts = new List<IBulletContext>();

            foreach (var context in contexts) {

                if (context is null) {
                    continue;
                }
                
                if (context is ABulletContext record) {
                    var copiedContext = record with { };
                    copiedContexts.Add(copiedContext);
                    return;
                }
                
                copiedContexts.Add(context);
            }
            
            m_contexts = copiedContexts;
        }

        protected virtual void ApplyContexts() {
            if (m_contexts.Count is 0) {
                return;
            }

            foreach (var context in m_contexts) {
                context.Apply(gameObject.transform.root.gameObject);
            }
        }
        
        protected virtual void OnPreStart() {}
        
        protected virtual void OnPostStart() {}
    }
}