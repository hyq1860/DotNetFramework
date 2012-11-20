// -----------------------------------------------------------------------
// <copyright file="IFriendLinkDataAccess.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.Base.Contract.IDataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using DotNet.Base.Contract.Entity;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IFriendLinkDataAccess
    {
        int Add(FriendLinkInfo model);

        int Update(FriendLinkInfo model);

        List<FriendLinkInfo> GetFriendLinks(string sqlWhere);
    }
}
