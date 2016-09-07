using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.VisualStudio.Web.CodeGeneration.MsBuild
{
    public class LibraryManager
    {
        private string _depsFile;
        private DependencyContext _dependencyContext = null;
        public LibraryManager(MsBuildProjectContext msBuildProjectContext)
        {
            if (msBuildProjectContext == null)
            {
                throw new ArgumentNullException(nameof(msBuildProjectContext));
            }

            _depsFile = msBuildProjectContext.DepsJson;
            Init();
        }

        private void Init()
        {
            if (File.Exists(_depsFile))
            {
                using (var stream = File.OpenRead(_depsFile))
                {
                    _dependencyContext = new DependencyContextJsonReader().Read(stream);
                }
            }
        }

        public Library GetLibrary(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return _dependencyContext?.CompileLibraries?.FirstOrDefault(l => l.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Library> GetLibraries()
        {

            return _dependencyContext?.CompileLibraries;
        }

        public IEnumerable<Library> GetReferencingLibraries(string name)
        {
            return _dependencyContext.CompileLibraries.Where(l => HasDependency(l, name));
        }

        private bool HasDependency(CompilationLibrary l, string name)
        {
            return l.Dependencies.Any(d => d.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

    }
}
