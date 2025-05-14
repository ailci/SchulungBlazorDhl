using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Utilities;

public static class Util
{
    public static string LogAsJson<T>(this T obj) where T : class
    {
        return JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
    }
}