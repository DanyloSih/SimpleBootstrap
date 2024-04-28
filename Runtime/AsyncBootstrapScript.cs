using System;
using System.Threading.Tasks;

namespace SimpleBootstrap
{
    public abstract class AsyncBootstrapScript : BootstrapScript
    {
        protected sealed async override void RunScript(BootstrapContext bootstrapContext, Action<BootstrapScript> scriptCompletedCallback)
        {
            await OnRunAsync(bootstrapContext).ContinueWith(task => { 
                if (task.Exception != null)
                {
                    ApplyException(task.Exception);
                }

                scriptCompletedCallback?.Invoke(this);
            });        
        }

        protected abstract Task OnRunAsync(BootstrapContext bootstrapContext);
    }
}
