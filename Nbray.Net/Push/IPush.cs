using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nbray.Net.Push
{
   public interface IPush
   {
       string Single(SingleNotification notify);

       string Multi(MultipleNotification notify);

       string All(AllNotification notify);
    }
}
