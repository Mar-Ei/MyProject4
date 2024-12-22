using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Domain.Entities
{
   
    public class Employee
    {
        [Key] // Указываем, что свойство EmployeeId будет использоваться как первичный ключ
        public int EmployeeId { get; set; }
        public string Name { get; set; }
    }

}

