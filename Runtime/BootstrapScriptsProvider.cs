using System.Collections.Generic;
using UnityEngine;

namespace SimpleBootstrap
{

    /// <summary>
    /// Interface for all bootstrap scripts providers.
    /// </summary>
    public abstract class BootstrapScriptsProvider<T> : MonoBehaviour
        where T : BootstrapScript
    {
        public abstract IReadOnlyList<T> GetBootstrapScripts();

    }
}
