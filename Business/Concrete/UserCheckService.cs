using Business.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UserCheckService : IPersonCheckService
    {
        public bool CheckIfRealPerson(User user)
        {
            return true;
        }
    }
}
