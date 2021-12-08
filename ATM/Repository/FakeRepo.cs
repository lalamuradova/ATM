using ATM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Repository
{
  public  class FakeRepo
  {
        public List<Employee> GetAll()
        {
            Card card = new Card
            {
                Number = "2121434365657676",
                Balance = "1200"
            };

            Card card2 = new Card
            {
                Number = "4343656509098787",
                Balance = "1400"
            };

            Card card3 = new Card
            {
                Number = "3434656557779890",
                Balance = "2360"
            };

            List<Card> cards = new List<Card>();
            cards.Add(card);
            cards.Add(card2);
            cards.Add(card3);

            return new List<Employee>
            {
                new Employee
                {
                    Fullname = "Lala Muradova",
                    Cards=cards
                }
            };

            

        }

  }
}
