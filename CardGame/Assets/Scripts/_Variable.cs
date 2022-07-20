using UnityEngine;

namespace Variable
{
    public abstract class BaseVariable : ScriptableObject
    {
        public abstract void SaveToInitialValue();
        public abstract void ToggleRuntimePersistance();
        public abstract void ToggleRuntimeMode();
    }
    public class GenericVariable<T> : BaseVariable, ISerializationCallbackReceiver
    {
        public enum RuntimeMode { ReadOnly = 0, ReadWrite = 1}
        public enum PersistenceMode { None = 0, Persist = 1}
        private bool persistent => persistenceMode == PersistenceMode.Persist;
        [Header("Value Setting")]
        [SerializeField] private T initialValue;
        [SerializeField] private T runtimeValue;
        [SerializeField] private RuntimeMode runtimeMode;
        [SerializeField] private PersistenceMode persistenceMode;

        public T Value
        {
             // if persistent then run initialValue, else run runtimeValue
            get => (persistent) ? initialValue : runtimeValue;
            set
            {
                switch (runtimeMode)
                {
                    case RuntimeMode.ReadOnly:
                        { Debug.LogWarning("Attempted to set read only Variable"); break; }
                    case RuntimeMode.ReadWrite:
                        {
                            if (persistent)
                            { initialValue = value; }
                            else
                            { runtimeValue = value; }
                            break;
                        }
                    default:
                        { Debug.LogWarning("Runtime mode Switched to defaulted"); break; }
                }
            }
        }

        public static implicit operator T(GenericVariable<T> variable) { return variable.Value; }

        public void OnAfterDeserialize()
        {
            if (!persistent)
            { runtimeValue = initialValue; }
        }

        public void OnBeforeSerialize()
        {
            // ehh useless atm
        }

        public override void SaveToInitialValue()
        {
            initialValue = runtimeValue;
        }

        public override void ToggleRuntimeMode()
        {
            runtimeMode = (runtimeMode == 0) ? (RuntimeMode)1 : 0;
        }

        public override void ToggleRuntimePersistance()
        {
            persistenceMode = (persistenceMode == 0) ? (PersistenceMode)1 : 0;
        }
    }
}
