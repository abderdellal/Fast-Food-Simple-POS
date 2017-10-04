using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Model
{
    public class User : ModelBase
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string MotDePass { get; set; }
        public typeUser UserType { get; set; }
    }

   public  enum typeUser { Admin, Simple}
}

