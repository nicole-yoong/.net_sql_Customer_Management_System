using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication2.Pages.Customer
{
    public class EditModel : PageModel
    {
        public CusInfo cusInfo = new CusInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String cus_id = Request.Query["ID"];

            try
            {
                String connectionString = "Data Source=LAPTOP-V0AH6EGV\\SQLEXPRESS;Initial Catalog=Bathworld;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "select * from customer where cus_id = @cus_id;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@cus_id", cus_id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
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


                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
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
            cusInfo.cus_id = Request.Form["cus_id"];


            if (cusInfo.cus_id.Length == 0 || cusInfo.cus_name.Length == 0 || cusInfo.int_id.Length == 0 || cusInfo.emp_id.Length == 0 || cusInfo.whatsapp.Length == 0 || cusInfo.house_no.Length == 0 || cusInfo.street.Length == 0 || cusInfo.postal_code.Length == 0 || cusInfo.last_visit.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=LAPTOP-V0AH6EGV\\SQLEXPRESS;Initial Catalog=Bathworld;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE customer SET cus_name=@cus_name, int_id=@int_id, emp_id=@emp_id, whatsapp=@whatsapp, house_no=@house_no, street=@street, postal_code=@postal_code, last_visit=@last_visit, special_note=@special_note, order_placed=@order_placed WHERE cus_id=@cus_id;";


                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

              
                        command.Parameters.AddWithValue("@cus_name", cusInfo.cus_name);
                        command.Parameters.AddWithValue("@int_id", cusInfo.int_id);
                        command.Parameters.AddWithValue("@emp_id", cusInfo.emp_id);
                        command.Parameters.AddWithValue("@whatsapp", cusInfo.whatsapp);
                        command.Parameters.AddWithValue("@house_no", cusInfo.house_no);
                        command.Parameters.AddWithValue("@street", cusInfo.street);
                        command.Parameters.AddWithValue("@postal_code", cusInfo.postal_code);
                        var typedate = DateTime.Parse(cusInfo.last_visit);
                        command.Parameters.AddWithValue("@last_visit", typedate);
                        command.Parameters.AddWithValue("@special_note", cusInfo.special_note);
                        command.Parameters.AddWithValue("@order_placed", cusInfo.order_placed);
                        command.Parameters.AddWithValue("@cus_id", cusInfo.cus_id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage=ex.Message;
                return;
            }

            Response.Redirect("/Customer/Index");
        }
    }
}
