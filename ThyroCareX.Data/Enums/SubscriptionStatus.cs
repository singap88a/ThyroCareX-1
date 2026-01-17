using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThyroCareX.Data.Enums
{
    public enum SubscriptionStatus
    {
        Free = 0,        // خطة مجانية، مش محتاج دفع
        Trialing = 1,    // فترة تجربة (اختياري)
        Active = 2,      // الاشتراك شغال والدفع ناجح
        PastDue = 3,     // الدفع فشل أو غير مكتمل
        Canceled = 4     // الاشتراك اتلغى
    }
}
