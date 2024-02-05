using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.Common
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Roles
    {
       SuberAdmin,
       Teacher,
       Student
    }
}
