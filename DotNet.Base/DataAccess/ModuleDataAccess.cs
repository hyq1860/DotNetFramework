using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DotNet.Base.Contract;
using DotNet.Data;

namespace DotNet.Base
{
    public class ModuleDataAccess : IModuleDataAccess
    {
        public List<Module> GetModules()
        {
            List<Module> modules=new List<Module>();
            using (DataCommand cmd = DataCommandManager.GetDataCommand("GetModules"))
            {
                using(IDataReader dr=cmd.ExecuteDataReader())
                {
                    while (dr.Read())
                    {
                        Module module=new Module();
                        if (!Convert.IsDBNull(dr["ModuleId"]))
                        {
                            module.ModuleId = Convert.ToInt32(dr["ModuleId"]);
                        }
                        if (!Convert.IsDBNull(dr["ModuleName"]))
                        {
                            module.ModuleName = dr["ModuleName"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["ParentId"]))
                        {
                            module.ParentId = Convert.ToInt32(dr["ParentId"]) ;
                        }
                        if (!Convert.IsDBNull(dr["Url"]))
                        {
                            module.Url = dr["Url"].ToString();
                        }
                        if(!Convert.IsDBNull(dr["Icon"]))
                        {
                            module.Icon = dr["Icon"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["Icon"]))
                        {
                            module.Icon = dr["Icon"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["SortCode"]))
                        {
                            module.Sort = Convert.ToInt32(dr["SortCode"]);
                        }
                        if (!Convert.IsDBNull(dr["Enabled"]))
                        {
                            module.Enable = Convert.ToBoolean(dr["Enabled"]) ;
                        }
                        if (!Convert.IsDBNull(dr["IsIframe"]))
                        {
                            module.IsIframe = Convert.ToBoolean(dr["IsIframe"]);
                        }
                        modules.Add(module);
                    }
                }
            }
            return modules;
        }

        public List<Module> GetTreeModules()
        {
            var result = new List<Module>();
            var data = GetModules();
            var root = data.Where(item=>item.ParentId==0);
            foreach (Module module in root)
            {
                module.Child = GetChilds(module, module.ModuleId, data);
                result.Add(module);
            }
            return result;
        }

        public List<Module> GetChilds(Module module,int parentId,List<Module> data)
        {
            module.Child = data.Where(item => item.ParentId == parentId).ToList();

            foreach (var module1 in module.Child)
            {
                GetChilds(module1, module1.ModuleId, data);
            }
            return module.Child;      
  }
    }
}
