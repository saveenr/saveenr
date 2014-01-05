using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportViewerControl2010SandBox
{
    class DataGenerator
    {
        public List<Customer> GetAllCustomers()
        {
            var customers = new List<Customer>();

            customers.Add(new Customer { First = "Charlie", Last = "X", Category = 1, ID=0, Salary=1, Age=7, VacationDays=10, Bonus=10});
            customers.Add(new Customer { First = "Tim", Last = "Y", Category = 2, ID = 0, Salary = 1, Age = 7, VacationDays = 10, Bonus = 10 });
            customers.Add(new Customer { First = "Chris", Last = "1", Category = 3, ID = 0, Salary = 1, Age = 7, VacationDays = 10, Bonus = 10 });
            customers.Add(new Customer { First = "Claire", Last = "A", Category = 2, ID = 0, Salary = 1, Age = 7, VacationDays = 10, Bonus = 10 });
            customers.Add(new Customer { First = "Daine", Last = "B", Category = 1, ID = 0, Salary = 1, Age = 7, VacationDays = 10, Bonus = 10 });
            customers.Add(new Customer { First = "Prakash", Last = "C", Category = 2, ID = 0, Salary = 1, Age = 7, VacationDays = 10, Bonus = 10 });
            customers.Add(new Customer { First = "Arnie", Last = "D", Category = 4, ID = 0, Salary = 1, Age = 7, VacationDays = 10, Bonus = 10 });

            return customers;
        }
    }
}

