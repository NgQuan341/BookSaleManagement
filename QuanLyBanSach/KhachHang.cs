using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE4QLHANGHOA_ADO
{
    class KhachHang
    {
        public string makh { get; set; }
        public string tenkh { get; set; }
        public override string ToString()
        {
            return tenkh;
        }
    }
}
