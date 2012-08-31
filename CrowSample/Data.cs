using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Foundation.DatabaseLayer;

namespace ConsoleApplication1
{
    [PrimaryKey("Id")]
    public class Data
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public string Surname { get; set; }
    }
}
