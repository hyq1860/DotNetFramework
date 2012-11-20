using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet.Base.Contract;

namespace DotNet.Base
{
    public class ModuleService:IModuleService
    {
        private static IModuleDataAccess _dataAccess=new ModuleDataAccess();
        public List<Module> GetModules()
        {
            return _dataAccess.GetModules();
        }

        public List<Module> GetTreeModules()
        {
            return _dataAccess.GetTreeModules();
        }
    }
}
