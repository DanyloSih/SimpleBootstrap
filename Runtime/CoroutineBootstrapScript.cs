using System;
using System.Collections;
using UnityEngine;

namespace SimpleBootstrap
{
    /// <summary>
    /// You can inherit from this class if you need a bootstrap script that will be executed inside coroutine.
    /// </summary>
    public abstract class CoroutineBootstrapScript : BootstrapScript
    {
        private Coroutine _coroutine;

        protected sealed override void RunScript(BootstrapContext bootstrapContext, Action<BootstrapScript> scriptCompletedCallback)
        {           
            if (_coroutine != null)
            {
                throw new InvalidOperationException($"The \"{GetType().Name}\" coroutine bootstrap script is already running!");
            }
            _coroutine = StartCoroutine(
                RunProcess(bootstrapContext, scriptCompletedCallback));
        }

        private IEnumerator RunProcess(BootstrapContext bootstrapContext, Action<BootstrapScript> scriptCompletedCallback)
        {
            yield return OnRunCoroutine(bootstrapContext);
            scriptCompletedCallback?.Invoke(this);  
            _coroutine = null;
        }

        protected abstract IEnumerator OnRunCoroutine(BootstrapContext bootstrapContext);
    }
}
