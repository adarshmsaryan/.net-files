using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookdetails;
namespace Bookinfo
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("Enter your choice\n");
                Console.WriteLine(" 1.for display\n 2.display through bookid\n 3.Based on salary range");
                int n;
                n = int.Parse(Console.ReadLine());
                Adobook b = new Adobook();

                if (n == 1)
                {
                    List<Book> bl = b.bookmaintain();
                    Console.WriteLine("Book details");
                    foreach (Book bb in bl)
                    {

                        Console.WriteLine(bb.bookcode + "\t" + bb.bookname + "\t" + bb.pname + "\t" + bb.aname + "\t" + bb.price);

                    }
                }

                else if (n == 2)
                {

                    Console.WriteLine("enter book id");
                    int i = int.Parse(Console.ReadLine());
                    List<Book> b2 = b.displaybyinfo(i);
                    //Console.WriteLine("Book details");
                    foreach (Book bb in b2)
                    {

                        Console.WriteLine(bb.bookcode + "\t" + bb.bookname + "\t" + bb.pname + "\t" + bb.aname + "\t" + bb.price);

                    }
                }
                else
                {


                    Console.WriteLine("enter price range\n");
                    int s1 = int.Parse(Console.ReadLine());
                    int s2 = int.Parse(Console.ReadLine());
                    List<Book> b3 = b.pricerange(s1, s2);
                    //Console.WriteLine("Book details");
                    foreach (Book bb in b3)
                    {

                        Console.WriteLine(bb.bookcode + "\t" + bb.bookname + "\t" + bb.pname + "\t" + bb.aname + "\t" + bb.price);

                    }
                }
            } while (true);
        }
    }
}
