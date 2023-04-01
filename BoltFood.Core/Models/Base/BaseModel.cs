using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoltFood.Core.Models.Base
{
    public class BaseModel
    {
        public int Id { get; set; }
        public string name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }


        public BaseModel(string name)
        {
            this.name = name;
        }
    }
}
