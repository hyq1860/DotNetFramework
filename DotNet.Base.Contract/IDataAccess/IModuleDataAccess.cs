using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.Base.Contract
{
    public interface IModuleDataAccess
    {
        List<Module> GetModules();

        List<Module> GetTreeModules();
    }
}
