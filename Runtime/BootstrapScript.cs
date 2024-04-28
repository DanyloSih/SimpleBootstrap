using System;
using UnityEngine;

namespace SimpleBootstrap
{
    public abstract class BootstrapScript : MonoBehaviour
	{
		[SerializeField] private bool _throwExceptionIfPreviousScriptReturnException = false;

        public Exception RunException { get; private set; }

		public void Run(BootstrapContext bootstrapContext, Action<BootstrapScript> scriptCompletedCallback)
        {
            try
            {
                if (bootstrapContext.IsPreviousScriptReturnException
                 && _throwExceptionIfPreviousScriptReturnException)
                {
                    throw new InvalidOperationException(bootstrapContext.GetCurrentScriptInfo +
                        "expects the previous script to complete without exception.");
                }
                else
                {
                    RunScript(bootstrapContext, scriptCompletedCallback);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
                RunException = e;
                scriptCompletedCallback?.Invoke(this);
            }
        }

        protected abstract void RunScript(BootstrapContext bootstrapContext, Action<BootstrapScript> scriptCompletedCallback);
	}
}
