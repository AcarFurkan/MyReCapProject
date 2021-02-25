using Core.Entites;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IPersonCheckService//<TEntity> where TEntity : class,IEntity,new()
    {
        bool CheckIfRealPerson(User user);
    }
}
