using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThyroCareX.Data.Enums
{
    public enum TestStatus
    {
        Queued=1,        // مستني في الطابور
        Processing,    // AI شغال عليه
        Completed,     // خلص بنجاح
        Failed         // حصل Error
    }
}
