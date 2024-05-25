using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleBootstrap
{
    /// <summary>
    /// This is the base class for all bootstrappers. <br/>
    /// A "Bootstrapper" is an entity that executes bootstrap scripts one by one.
    /// </summary>
    public abstract class BootstrapperBase : MonoBehaviour
	{
        [SerializeField] private bool _runOnAwake;

        [HideInInspector]
        [SerializeField] private bool _isNextBootstrapperSet;
        [HideInInspector]
        [SerializeField] private BootstrapperBase _nextBootstrapper;

        private bool _isScriptsRunProcessStarted;
        private Dictionary<Type, BootstrapScript> _uniqueCompletedBootstrapScripts;
        private List<BootstrapScript> _completedBootstrapScripts;

        public bool RunOnAwake { get => _runOnAwake; }
        public bool IsScriptsRunProcessStarted { get => _isScriptsRunProcessStarted; }
        public BootstrapperBase NextBootstrapper { get => _nextBootstrapper; }
        public IReadOnlyList<BootstrapScript> CompletedBootstrapScripts { get => _completedBootstrapScripts; }
        public bool IsAllScriptsCompleted 
        { 
            get => _completedBootstrapScripts.Count == BootstrapScriptsCount; 
        }

        public abstract int BootstrapScriptsCount { get; }

        protected void Awake()
        {
            if (_runOnAwake)
            {
                RunScripts();
            }
        }

        /// <summary>
        /// Looking for first bootstrap script with type "<typeparamref name="T"/>" from completed scripts list. <br/>
        /// If there are no completed script with type "<typeparamref name="T"/>" then throw exception.
        /// </summary>
        /// <typeparam name="T">Target bootstrap script type.</typeparam>
        /// <returns>Completed bootstrap script.</returns>
        public T GetCompletedScript<T>()
            where T : BootstrapScript
        {
            if (_uniqueCompletedBootstrapScripts.ContainsKey(typeof(T)))
            {
                return _uniqueCompletedBootstrapScripts[typeof(T)] as T;
            }
            else
            {
                throw new InvalidOperationException($"Bootstrapper \"{name}\" don't contains " +
                    $"completed bootstrap script with type \"{typeof(T).Name}\"!");
            }
        }

        /// <summary>
        /// Looking for first bootstrap script with type "<typeparamref name="T"/>" from completed scripts list. <br/>
        /// Return true if there are completed script with type "<typeparamref name="T"/>".
        /// </summary>
        /// <typeparam name="T">Target bootstrap script type.</typeparam>
        public bool IsScriptCompleted<T>()
             where T : BootstrapScript
        {
            return _uniqueCompletedBootstrapScripts.ContainsKey(typeof(T));
        }

        public void RunScripts(Action allScriptsCompletedCallback = null)
        {
            if (_isScriptsRunProcessStarted)
            {
                throw new InvalidOperationException(
                    $"Bootstrapper \"{name}\" was already running scripts. " +
                    $"You can run scripts only once!");
            }

            _isScriptsRunProcessStarted = true;
            _uniqueCompletedBootstrapScripts = new Dictionary<Type, BootstrapScript>(BootstrapScriptsCount);
            _completedBootstrapScripts = new List<BootstrapScript>(BootstrapScriptsCount);

            OnRunScripts(() => OnScriptsCompleted(allScriptsCompletedCallback));
        }

        protected void RegisterBootstrapScriptAsCompleted(BootstrapScript bootstrapScript)
        {
            _completedBootstrapScripts.Add(bootstrapScript);

            if (!_uniqueCompletedBootstrapScripts.ContainsKey(bootstrapScript.GetType()))
            {
                _uniqueCompletedBootstrapScripts.Add(bootstrapScript.GetType(), bootstrapScript);
            }
        }

        protected abstract void OnRunScripts(Action allScriptsCompletedCallback = null);

        private void OnScriptsCompleted(Action allScriptsCompletedCallback = null)
        {
            allScriptsCompletedCallback?.Invoke();
            _nextBootstrapper?.RunScripts();
        }
    }
 
}
