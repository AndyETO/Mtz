using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaritzaData.Base;
namespace MaritzaBusness.Base
{
    public class BaseBusnessB
    {
        public string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        public MaritzaDB db = new MaritzaDB();
        //poner codigo para generar querys

    }
}
