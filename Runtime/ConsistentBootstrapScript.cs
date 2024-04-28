using System;

namespace SimpleBootstrap
{
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
