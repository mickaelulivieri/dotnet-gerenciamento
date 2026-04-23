using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskManager.API.Exceptions
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;

        // Método auxiliar para transformar o objeto em JSON automaticamente
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}