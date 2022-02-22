using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPISecurity.Models
{
    public class users
    {
        public int id;
        public string name;
        public string email;
        public string gender;
        public string status;

        public static implicit operator List<object>(users v)
        {
            throw new NotImplementedException();
        }
    }
}