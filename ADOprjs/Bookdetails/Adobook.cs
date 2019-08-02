using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Bookdetails
{
    public class Adobook
    {
        SqlConnection con;
        SqlCommand cmd;

        public Adobook()
        {
            con = new SqlConnection();
            con.ConnectionString= "Data Source=VDC01LTC4531;Initial Catalog=BookShop;User ID=sa;Password=welcome1@";
            cmd = new SqlCommand();
            cmd.Connection = con;
            

        }
        public List<Book> bookmaintain()
        {
            List<Book> lbk = new List<Book>();
            cmd.CommandText = "select * from tbl_book";
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            while(sdr.Read())
            {
                Book b = new Book();
                b.bookcode = (int)sdr[0];
                b.bookname = sdr[1].ToString();
                b.pname = sdr[2].ToString();
                b.aname = sdr[3].ToString();
                b.price = (int)sdr[4];
                lbk.Add(b);
            }
            sdr.Close();
            con.Close();
            return lbk;

        }
        public List<Book> displaybyinfo(int bookid)
        {
            List<Book> lbk = new List<Book>();
            cmd.CommandText = "select * from tbl_book where bookcode="+bookid;
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                Book b = new Book();
                b.bookcode = (int)sdr[0];
                b.bookname = sdr[1].ToString();
                b.pname = sdr[2].ToString();
                b.aname = sdr[3].ToString();
                b.price = (int)sdr[4];
                lbk.Add(b);
            }
            sdr.Close();
            con.Close();
            return lbk;
        }
        public List<Book> pricerange(int s1,int s2)
        {
            List<Book> lbk = new List<Book>();
            cmd.CommandText = "select * from tbl_book where price>@s1 and price<@s2" ;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@s1", s1);
            cmd.Parameters.AddWithValue("@s2", s2);

            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                Book b = new Book();
                b.bookcode = (int)sdr[0];
                b.bookname = sdr[1].ToString();
                b.pname = sdr[2].ToString();
                b.aname = sdr[3].ToString();
                b.price = (int)sdr[4];
                lbk.Add(b);
            }
            sdr.Close();
            con.Close();
            return lbk;

        }
    }
    public class Book
    {
        public int bookcode { get; set; }
        public string bookname { get; set; }
        public string pname { get; set; }
        public string aname { get; set; }
        public int price { get; set; }


    }
}

