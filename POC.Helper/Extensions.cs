using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static POC.Helper.AppConstants;

namespace POC.Helper
{
    public static class HelperExtensions
    {
        public static UserActivityAction GetRandomUserAction()
        {
            Array values = Enum.GetValues(typeof(UserActivityAction));
            Random random = new Random();
            return (UserActivityAction)values.GetValue(random.Next(values.Length))!;
        }
    }
}
