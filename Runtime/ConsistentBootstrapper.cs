using System;

namespace SimpleBootstrap
{
    public class ConsistentBootstrapper : Bootstrapper<BootstrapScript>
    {
        private int _previousScriptId = -1;

        protected override void OnRunScripts(Action allScriptsCompletedCallback = null)
        {
            RunNextScript(allScriptsCompletedCallback);
        }

        private void RunNextScript(Action allScriptsCompletedCallback = null)
        {
            int currentScriptId = _previousScriptId + 1;
            int textScriptId = currentScriptId + 1;

            if (currentScriptId >= BootstrapScripts.Count)
            {
                allScriptsCompletedCallback?.Invoke();
                return;
            }

            BootstrapScript previousScript = _previousScriptId == -1 ? null : BootstrapScripts[_previousScriptId];
            BootstrapScript currentScript = BootstrapScripts[currentScriptId];
            BootstrapScript nextScript = textScriptId >= BootstrapScripts.Count ? null : BootstrapScripts[textScriptId];

            var context = new BootstrapContext(
                currentScriptId, previousScript, currentScript, nextScript, this);

            currentScript.Run(context, (x) => OnScriptCompleted(x, allScriptsCompletedCallback));
        }

        private void OnScriptCompleted(BootstrapScript currentScript, Action allScriptsCompletedCallback = null)
        {
            _previousScriptId++;
            RegisterBootstrapScriptAsCompleted(currentScript);
            RunNextScript(allScriptsCompletedCallback);
        }
    }
}
