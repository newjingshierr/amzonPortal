using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqBudget
{
    class Program
    {
        static void Main(string[] args)
        {
            List<BudgetDetailModel> l = new List<BudgetDetailModel>();
            BudgetDetailModel b = new BudgetDetailModel();
            b.SubjectID = Convert.ToInt64(232323);
            b.Amount = 200;
            b.BudgetMonth = Convert.ToDateTime("2017/02");
            l.Add(b);
            b = new BudgetDetailModel();
            b.SubjectID = Convert.ToInt64(232323);
            b.Amount = 300;
            b.BudgetMonth = Convert.ToDateTime("2017/03");
            l.Add(b);
            b = new BudgetDetailModel();
            b.Amount = 400;
            b.SubjectID = Convert.ToInt64(232323);
            b.BudgetMonth = Convert.ToDateTime("2017/04");
            l.Add(b);
            b = new BudgetDetailModel();
            b.Amount = 400;
            b.SubjectID = Convert.ToInt64(232324);
            b.BudgetMonth = Convert.ToDateTime("2017/04");
            l.Add(b);
            b = new BudgetDetailModel();
            b.Amount = 500;
            b.SubjectID = Convert.ToInt64(232324);
            b.BudgetMonth = Convert.ToDateTime("2017/05");
            l.Add(b);
            b = new BudgetDetailModel();
            b.Amount = 600;
            b.SubjectID = Convert.ToInt64(232324);
            b.BudgetMonth = Convert.ToDateTime("2017/06");
            l.Add(b);


            //  var ji = l.Take(100).ToList();

            //var result = from p in l.AsEnumerable()
            //             orderby p.SubjectID, p.BudgetMonth
            //             select new {p.SubjectID,p.BudgetMonth,p.Amount };
            //Console.Write(result);
           var dd =  l.GroupBy(o => o.SubjectID).Where(i => i.Count() > 1);




        }
    }


    public class BudgetDetailModel
    {
        /// <summary>
        /// BudgetID
        /// </summary>		



        /// <summary>
        /// SubjectID
        /// </summary>		
        public long SubjectID { get; set; }


        /// <summary>
        /// Amount
        /// </summary>		
        public decimal Amount { get; set; }



        /// <summary>
        /// BudgetMonth
        /// </summary>		
        public DateTime BudgetMonth { get; set; }


    }
	
     
}
