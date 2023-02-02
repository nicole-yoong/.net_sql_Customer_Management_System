using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication2.Pages.Customer
{
    public class CreateModel : PageModel
    {
        public CusInfo cusInfo = new CusInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {

            cusInfo.cus_name = Request.Form["cus_name"];
            cusInfo.int_id = Request.Form["int_id"];
            cusInfo.emp_id = Request.Form["emp_id"];
            cusInfo.whatsapp = Request.Form["whatsapp"];
            cusInfo.house_no = Request.Form["house_no"];
            cusInfo.street = Request.Form["street"];
            cusInfo.postal_code = Request.Form["postal_code"];
            cusInfo.last_visit = Request.Form["last_visit"];
            cusInfo.special_note = Request.Form["special_note"];
            cusInfo.order_placed = Request.Form["order_placed"];

            if (cusInfo.cus_name.Length == 0 || cusInfo.int_id.Length == 0 || cusInfo.emp_id.Length == 0 || cusInfo.whatsapp.Length == 0 || cusInfo.house_no.Length == 0 || cusInfo.street.Length == 0 || cusInfo.postal_code.Length == 0 || cusInfo.last_visit.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            //save the new customer into the database
            try
            {
                String connectionString = "Data Source=LAPTOP-V0AH6EGV\\SQLEXPRESS;Initial Catalog=Bathworld;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO customer" +
                                 "(cus_name, int_id, emp_id, whatsapp, house_no, street, postal_code, last_visit, special_note, order_placed) VALUES" +
                                  "(@cus_name, @int_id, @emp_id, @whatsapp, @house_no, @street, @postal_code, @last_visit, @special_note, @order_placed);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@cus_name", cusInfo.cus_name);
                        command.Parameters.AddWithValue("@int_id", cusInfo.int_id);
                        command.Parameters.AddWithValue("@emp_id", cusInfo.emp_id);
                        command.Parameters.AddWithValue("@whatsapp", cusInfo.whatsapp);
                        command.Parameters.AddWithValue("@house_no", cusInfo.house_no);
                        command.Parameters.AddWithValue("@street", cusInfo.street);
                        command.Parameters.AddWithValue("@postal_code", cusInfo.postal_code);
                        command.Parameters.AddWithValue("@last_visit", cusInfo.last_visit);
                        command.Parameters.AddWithValue("@special_note", cusInfo.special_note);
                        command.Parameters.AddWithValue("@order_placed", cusInfo.order_placed);


                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            cusInfo.cus_name = ""; cusInfo.int_id = ""; cusInfo.emp_id = ""; cusInfo.whatsapp = ""; cusInfo.house_no = ""; cusInfo.street = ""; cusInfo.postal_code = ""; cusInfo.last_visit = ""; cusInfo.special_note = ""; cusInfo.order_placed = "";
            successMessage = "New Customer Added Correctly";

            Response.Redirect("/Customer/Index");
        }
    }
}
