using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.VisualStudio.Web.CodeGeneration.MsBuild
{
    public static class MsBuildProjectContextExtensions
    {
        public static LibraryManager CreateLibraryManager(this MsBuildProjectContext context)
        {
            return new LibraryManager(context);
        }

        public static RoslynWorkspace CreateRoslynWorkspace(this MsBuildProjectContext context, string configuration = "debug")
        {
            return new RoslynWorkspace(context, configuration);
        }
    }
}
