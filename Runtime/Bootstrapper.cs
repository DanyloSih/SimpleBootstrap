using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleBootstrap
{

    public abstract class Bootstrapper<T> : BootstrapperBase
        where T : BootstrapScript
	{
		[SerializeField] private List<T> _bootstrapScripts = new List<T>();

        public override int BootstrapScriptsCount => _bootstrapScripts.Count;

        public IReadOnlyList<T> BootstrapScripts { get => _bootstrapScripts; }
    }
}
