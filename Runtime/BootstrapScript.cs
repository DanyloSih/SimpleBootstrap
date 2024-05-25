using System;
using UnityEngine;

namespace SimpleBootstrap
{
    /// <summary>
    /// Base class for all bootstrap scripts.
    /// </summary>
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
                ApplyException(e);
                scriptCompletedCallback?.Invoke(this);
            }
        }

        protected void ApplyException(Exception e)
        {
            Debug.LogException(e);
            RunException = e;
        }

        protected abstract void RunScript(BootstrapContext bootstrapContext, Action<BootstrapScript> scriptCompletedCallback);
	}
}
