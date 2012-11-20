// -----------------------------------------------------------------------
// <copyright file="BasicContentService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.ContentManagement.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using DotNet.ContentManagement.Contract;
    using DotNet.ContentManagement.Contract.Entity;
    using DotNet.ContentManagement.DataAccess;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BasicContentService : IBasicContentService
    {
        private IBasicContentDataAccess basicContentDataAccess=new BasicContentDataAccess();

        public int Insert(BasicContentInfo model)
        {
            return basicContentDataAccess.Insert(model);
        }

        public int Update(BasicContentInfo model)
        {
            return basicContentDataAccess.Update(model);
        }

        public BasicContentInfo GetBasicContentById(int id)
        {
            return basicContentDataAccess.GetBasicContentById(id);
        }

        public List<BasicContentInfo> GetBasicContents()
        {
            return basicContentDataAccess.GetBasicContents();
        }
    }
}
