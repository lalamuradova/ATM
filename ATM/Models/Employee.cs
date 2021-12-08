using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Models
{
   public class Employee
   {
        public List<Card> Cards { get; set; } = new List<Card>();
        public string Fullname { get; set; }

        public void AddCard(Card card)
        {
            Cards.Add(card);
        }
   } 

    
}
