using System.Collections.Generic;
using System;
using UnityEngine;

namespace SimpleBootstrap
{
    /// <summary>
    /// This class provides components 
    /// of bootstrap scripts found in children GameObjects
    /// </summary>
    public class BootstrapScriptsFromHierarchyProvider : BootstrapScriptsProvider<BootstrapScript>
    {
        [HideInInspector]
        [SerializeField] private bool _foldout;

        [NonSerialized] private List<BootstrapScript> _bootstrapScripts;

        public override IReadOnlyList<BootstrapScript> GetBootstrapScripts()
        {
            if (_bootstrapScripts == null)
            {
                UpdateBootstrapScriptsList();
            }

            return _bootstrapScripts;
        }

        public void UpdateBootstrapScriptsList()
        {
            _bootstrapScripts = new List<BootstrapScript>();
            var components = GetComponentsInChildren(typeof(BootstrapScript), false);
            foreach (var item in components)
            {
                if (item as BootstrapScript != null)
                {
                    _bootstrapScripts.Add(item as BootstrapScript);
                }
            }
        }
    }
}
