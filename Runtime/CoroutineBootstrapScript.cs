using System;
using System.Collections;
using UnityEngine;

namespace SimpleBootstrap
{
    public abstract class CoroutineBootstrapScript : BootstrapScript
    {
        private Coroutine _coroutine;

        protected sealed override void RunScript(BootstrapContext bootstrapContext, Action<BootstrapScript> scriptCompletedCallback)
        {           
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
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
