using System;

namespace SimpleBootstrap
{
    /// <summary>
    /// You can inherit from this class if you need a bootstrap script that will be executed synchronously.
    /// </summary>
    public abstract class ConsistentBootstrapScript : BootstrapScript
	{
        protected sealed override void RunScript(BootstrapContext bootstrapContext, Action<BootstrapScript> scriptCompletedCallback)
        {
			OnRun(bootstrapContext);
            scriptCompletedCallback?.Invoke(this);
        }

		protected abstract void OnRun(BootstrapContext bootstrapContext);
    }
}
