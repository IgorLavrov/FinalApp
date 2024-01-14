using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalApp.Model
{
    public  class Company
    {
        [PrimaryKey]
        public int ID { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string TelephoneNumber { get; set; }

      



        public override string ToString()
        {
            //return this.Name+"( Address:"+this.Address+ " TelephoneNumber:"+this.TelephoneNumber+ ")";
            return $"{Name} (Address: {Address}, PhoneNumber: {TelephoneNumber} )";

        }

    }
}
