using System;
using System.Threading;

namespace SimpleBootstrap
{
    public struct BootstrapContext
	{
		public int RunOrder {  get; private set; }
		public bool IsPreviousScriptReturnException { get; private set; }
		public Exception PreviousScriptException { get; private set; }
        public BootstrapperBase Bootstrapper { get ; private set; }
		public BootstrapScript PreviousBootstrapScript { get; private set; }
		public BootstrapScript CurrentBootstrapScript { get; private set; }
		public BootstrapScript NextBootstrapScript { get; private set; }
        public string GetCurrentScriptInfo
            => $"Bootstrapper \"{Bootstrapper.name}\" script named: \"{CurrentBootstrapScript.name}\", with run order: \"{RunOrder}\", ";

        public BootstrapContext(
            int runOrder,
            BootstrapScript previousBootstrapScript,
            BootstrapScript nextBootstrapScript,
            BootstrapScript currentBootstrapScript,
            BootstrapperBase bootstrapper)
        {
            Exception previousScriptException = previousBootstrapScript?.RunException;
            RunOrder = runOrder;
            IsPreviousScriptReturnException = previousScriptException != null;
            PreviousBootstrapScript = previousBootstrapScript;
            NextBootstrapScript = nextBootstrapScript;
            PreviousScriptException = previousScriptException;
            CurrentBootstrapScript = currentBootstrapScript;
            Bootstrapper = bootstrapper;
        }

        public bool TryCastScript<T>(BootstrapScript bootstraptScript, out T castedBootstrapScript)
			where T : BootstrapScript
        {
			castedBootstrapScript = bootstraptScript as T;
            return castedBootstrapScript != null;
        }

        public T GetPreviousScript<T>()
            where T : BootstrapScript
        {
            if (PreviousBootstrapScript == null)
            {
                throw new InvalidOperationException(
                    GetCurrentScriptInfo + 
                    $"expected bootstrap script with type: \"{typeof(T).Name}\" as previous script, " +
                    $"but there are no previous scripts!");
            }

            if (TryCastScript<T>(PreviousBootstrapScript, out var bootstrapScript))
            {
                return bootstrapScript;
            }
            else
            {
                throw new InvalidOperationException(GetCurrentScriptInfo + 
                    $"expected bootstrap script with type: \"{typeof(T).Name}\" as previous script, " +
                    $"but previous script has type \"{PreviousBootstrapScript.GetType()}\" " +
                    $"which can't be casted to \"{typeof(T).Name}\"");
            }
        }

        public T GetNextScript<T>()
            where T : BootstrapScript
        {
            if (NextBootstrapScript == null)
            {
                throw new InvalidOperationException(
                    GetCurrentScriptInfo +
                    $"expected bootstrap script with type: \"{typeof(T).Name}\" as next script, " +
                    $"but there are no next scripts!");
            }

            if (TryCastScript<T>(NextBootstrapScript, out var bootstrapScript))
            {
                return bootstrapScript;
            }
            else
            {
                throw new InvalidOperationException(GetCurrentScriptInfo +
                    $"expected bootstrap script with type: \"{typeof(T).Name}\" as next script, " +
                    $"but next script has type \"{NextBootstrapScript.GetType()}\" " +
                    $"which can't be casted to \"{typeof(T).Name}\"");
            }
        }
    }
}
