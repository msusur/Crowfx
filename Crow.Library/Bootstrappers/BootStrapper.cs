using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using Crow.Library.Common;
using System.Linq;
using System.Collections.Generic;
using System;
using Crow.Library.Foundation.Bootstrapper;
using Crow.Library.InjectionContainer;
using Crow.Library.RepositoryStorage;
using Crow.Library.DatabaseLayer;

namespace Crow.Library.Bootstrappers
{
    /// <summary>
    /// Class that handles the loading operations for the library.
    /// </summary>
    public class Bootstrapper
    {
        #region Nested Types
        private class DependencyNode
        {
            public IModule Item { get; set; }

            public Dictionary<Type, DependencyNode> Nodes { get; set; }

            protected DependencyNode()
            {
                Nodes = new Dictionary<Type, DependencyNode>();
            }

            public DependencyNode(IModule item)
                : this()
            {
                this.Item = item;
            }
        }
        #endregion

        private readonly IAssemblyLoader _assemblyLoader;

        protected CompositionContainer _container;

        public Bootstrapper()
            : this(AssemblyLoaderFactory.GetLoader())
        {

        }
        public Bootstrapper(IAssemblyLoader loader)
        {
            _assemblyLoader = loader;
        }

        /// <summary>
        /// Clears the container.
        /// </summary>
        public void Clear()
        {
            _container = new CompositionContainer();
        }

        /// <summary>
        /// Stops the application and releases the unnecessery components.
        /// </summary>
        public void Stop()
        {
            UnloadModules();
        }
        
        /// <summary>
        /// Starts the application and starts module loading.
        /// </summary>
        /// <param name="externalAssemblies"></param>
        public void Start(params Assembly[] externalAssemblies)
        {
            IEnumerable<Assembly> assemblies = _assemblyLoader.LoadAssemblies();

            if (externalAssemblies != null)
            {
                assemblies = assemblies.Union(externalAssemblies);
            }
            this.LoadModules(assemblies);
        }

        /// <summary>
        /// Unloads the modules and components.
        /// </summary>
        protected virtual void UnloadModules()
        {
            //Do nothing. Any bootstrapper might override this and implement their unload logic.
            //Asp.net host overrides this method.
        }

        /// <summary>
        /// Starts the application with the given assemblies.
        /// </summary>
        /// <param name="assemblies"></param>
        protected virtual void LoadModules(IEnumerable<Assembly> assemblies)
        {

            var catalog = new AggregateCatalog();
            foreach (var assembly in assemblies)
            {
                bool addAssembly = CanAddAssembly(assembly);
                if (addAssembly)
                {
                    catalog.Catalogs.Add(new AssemblyCatalog(assembly));
                }
            }
            _container = new CompositionContainer(catalog);

            var startupInstallers = _container.GetExports<IModule>();

            Install(new CrowModule()); //since every other assemblies needs to install CrowStartupInstaller inorder to use crow, this must be done first.
            foreach (var installer in OrderInstallerDependencies(startupInstallers))
            {
                Install(installer);
            }
        }

        private bool CanAddAssembly(Assembly assembly)
        {
            try
            {
                //TODO: there must be an other way to work this around.

                if (assembly == null || string.IsNullOrEmpty(assembly.CodeBase))
                {
                    return false;
                }
                return !assembly.CodeBase.Contains("Microsoft");
            }
            catch
            {
                return false;
            }
        }

        private IEnumerable<IModule> OrderInstallerDependencies(IEnumerable<Lazy<IModule>> startupInstallers)
        {
            foreach (var item in GetByAttributes(startupInstallers, typeof(DependsOnAttribute)))
            {
                yield return item;
            }
        }

        private IEnumerable<IModule> GetByAttributes(IEnumerable<Lazy<IModule>> startupInstallers, Type type)
        {
            Dictionary<Type, DependencyNode> nodes = new Dictionary<Type, DependencyNode>();
            Dictionary<IModule, AttributeContext<DependsOnAttribute>> contextList = new Dictionary<IModule, AttributeContext<DependsOnAttribute>>();
            AttributeContext<DependsOnAttribute> context;
            foreach (var item in startupInstallers)
            {
                context = AttributesHelper.HasAttribute<DependsOnAttribute>(item.Value);
                contextList.Add(item.Value, context);
                if (context.AttributeInstance == null)
                {
                    nodes.Add(item.Value.GetType(), new DependencyNode(item.Value));
                }
            }
            foreach (var item in startupInstallers)
            {
                context = contextList[item.Value];
                if (context.AttributeInstance == null)
                {
                    continue;
                }
                bool hasNode = nodes.ContainsKey(context.AttributeInstance.DependencyType);
                if (hasNode)
                {
                    nodes[context.AttributeInstance.DependencyType].Nodes.Add(item.Value.GetType(), new DependencyNode(item.Value));
                }
                else
                {
                    bool added = false;
                    foreach (var child in nodes.Values)
                    {
                        IterateNodes(child, context, item.Value, ref added);
                    }
                    if (!added)
                    {
                        nodes.Add(item.Value.GetType(), new DependencyNode(item.Value));
                    }
                }
            }
            return VisitAndYieldReturnTree(nodes);
        }

        private IEnumerable<IModule> VisitAndYieldReturnTree(Dictionary<Type, DependencyNode> nodes)
        {
            List<IModule> items = new List<IModule>();
            foreach (var item in nodes.Values)
            {
                items.Add(item.Item);
                items.AddRange(VisitAndYieldReturnTree(item.Nodes));
            }
            return items.AsEnumerable();
        }

        private void IterateNodes(DependencyNode node, AttributeContext<DependsOnAttribute> context, IModule item, ref bool added)
        {
            if (node.Nodes.ContainsKey(context.AttributeInstance.DependencyType))
            {
                node.Nodes[context.AttributeInstance.DependencyType].Nodes.Add(item.GetType(), new DependencyNode(item));
                added = true;
            }
            else if (node.Nodes.Count == 0)
            {
                added = false;
            }
            else
            {
                foreach (var child in node.Nodes.Values)
                {
                    IterateNodes(child, context, item, ref added);
                }
            }
        }

        private void Install(IModule installer)
        {
            DependencyInjectionWrapper wrapper = new DependencyInjectionWrapper(DIContainer.DefaultContainer);
            IQueryStore store = new QueryStoreWrapper();
            installer.Install(wrapper, store);
        }
    }
}