using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopStyle.Domain.DTO;
using TopStyle.Domain.Entities;

namespace TopStyle.Domain.Auth.Interface
{
    public interface IJwtTokenService
    {
        public string CreateToken(UserDTO user);
    }
}
