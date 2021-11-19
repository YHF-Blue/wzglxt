using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model.UserDTO;

namespace TokenApi.Utility
{
  public   interface IJWTService
    {
        public string GetToken(UserOutput accountOutput);
    }
}
