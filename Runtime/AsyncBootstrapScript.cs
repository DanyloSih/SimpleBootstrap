using System;
using System.Threading.Tasks;

namespace SimpleBootstrap
{
    /// <summary>
    /// You can inherit from this class if you need a bootstrap script that will be executed asynchronously.
    /// </summary>
    public abstract class AsyncBootstrapScript : BootstrapScript
    {
        protected sealed async override void RunScript(BootstrapContext bootstrapContext, Action<BootstrapScript> scriptCompletedCallback)
        {
            try
            {
                await OnRunAsync(bootstrapContext);
            }
            catch (Exception ex)
            {
                ApplyException(ex);
            }
            finally
            {
                scriptCompletedCallback?.Invoke(this);
            }
        }

        protected abstract Task OnRunAsync(BootstrapContext bootstrapContext);
    }
}
