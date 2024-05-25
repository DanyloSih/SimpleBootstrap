using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleBootstrap
{
    /// <summary>
    /// This class executes bootstrap scripts found via <see cref="ConsistentBootstrapper._bootstrapScriptsProvider"/>.
    /// </summary>
    public class ConsistentBootstrapper : BootstrapperBase
    {
        [SerializeField] private BootstrapScriptsProvider<BootstrapScript> _bootstrapScriptsProvider;

        private int _previousScriptId = -1;

        public override int BootstrapScriptsCount { get => _bootstrapScriptsProvider.GetBootstrapScripts().Count; }

        protected override void OnRunScripts(Action allScriptsCompletedCallback = null)
        {
            RunNextScript(allScriptsCompletedCallback);
        }

        private void RunNextScript(Action allScriptsCompletedCallback = null)
        {
            int currentScriptId = _previousScriptId + 1;
            int textScriptId = currentScriptId + 1;

            IReadOnlyList<BootstrapScript> bootstrapScripts
                = _bootstrapScriptsProvider.GetBootstrapScripts();

            if (currentScriptId >= bootstrapScripts.Count)
            {
                allScriptsCompletedCallback?.Invoke();
                return;
            }

            BootstrapScript previousScript = _previousScriptId == -1 ? null : bootstrapScripts[_previousScriptId];
            BootstrapScript currentScript = bootstrapScripts[currentScriptId];
            BootstrapScript nextScript = textScriptId >= bootstrapScripts.Count ? null : bootstrapScripts[textScriptId];

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
