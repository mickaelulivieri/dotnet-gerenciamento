using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.API.DTOs.user
{
    public class UserUpdateDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}