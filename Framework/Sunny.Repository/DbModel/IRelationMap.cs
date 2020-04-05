using System;
using System.Collections.Generic;
using System.Text;

namespace Sunny.Repository.DbModel
{
    /// <summary>
    /// 用于标识用于映射多对多的类,继承此接口后可通过T4模板生成RelationMap类
    /// </summary>
    public interface IRelationMap:IDbModel
    {
    }
}
