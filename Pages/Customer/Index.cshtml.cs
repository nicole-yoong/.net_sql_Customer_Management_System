using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication2.Pages.Customer
{

        public class IndexModel : PageModel
        {
            public List<CusInfo> listCus = new List<CusInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=LAPTOP-V0AH6EGV\\SQLEXPRESS;Initial Catalog=Bathworld;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "select * from customer";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CusInfo cusInfo = new CusInfo();
                                cusInfo.cus_id = "" + reader.GetInt32(10);
                                cusInfo.cus_name = reader.GetString(0);
                                cusInfo.int_id = "" + reader.GetInt32(1);
                                cusInfo.emp_id = "" + reader.GetInt32(2);
                                cusInfo.whatsapp = reader.GetString(3);
                                cusInfo.house_no = reader.GetString(4);
                                cusInfo.street = reader.GetString(5);
                                cusInfo.postal_code = "" + reader.GetInt32(6);
                                cusInfo.last_visit = reader.GetDateTime(7).ToString();
                                cusInfo.special_note = reader.GetString(8);
                                cusInfo.order_placed = reader.GetString(9);

                                listCus.Add(cusInfo);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
             {
                Console.WriteLine("Exception: " + ex.ToString());
             }

        }
}
    public class CusInfo
    {
        public string cus_id;
        public string cus_name;
        public string int_id;
        public string emp_id;
        public string whatsapp;
        public string house_no;
        public string street;
        public string postal_code;
        public string last_visit;
        public string special_note;
        public string order_placed;


    } }

