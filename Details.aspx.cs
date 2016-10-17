using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class Details : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != null)
        {
            int productID = 0;
            bool validID = int.TryParse(Request.QueryString["ID"].ToString(),
                            out productID);

            if (validID)
            {
                if (!IsPostBack)
                {
                    GetData(productID);
                }
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }
    void GetData(int ID)
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = @"SELECT CatID, Category FROM Categories";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    ddlCategories.DataSource = dr;
                    ddlCategories.DataTextField = "Category";
                    ddlCategories.DataValueField = "CatID";
                    ddlCategories.DataBind();

                    ddlCategories.Items.Insert(0, new ListItem("Select atleast one.", ""));
                }
            }
        }

        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = @"SELECT p.Name, c.Category, p.Code,
                p.Description, p.Price, p.IsFeatured, p.Available,
                p.CriticalLevel, p.Maximum, p.Status, p.DateModified FROM Products p
                INNER JOIN Categories c ON p.CatID = c.CatID
                WHERE p.ProductID=@ProductID";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@ProductID", ID);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows) //record is existing
                    {
                        while (dr.Read())
                        {
                            ltID.Text = ID.ToString();
                            ltID2.Text = ID.ToString();
                            txtProductName.Text = dr["Name"].ToString();
                            ddlCategories.SelectedValue = dr["Category"].ToString();
                            txtCode.Text = dr["Code"].ToString();
                            txtDescription.Text = dr["Description"].ToString();
                            txtPrice.Text = dr["Price"].ToString();
                            txtIsFeatured.Text = dr["IsFeatured"].ToString();
                            txtIsFeatured.Text = dr["Available"].ToString();
                            txtCriticalLevel.Text = dr["CriticalLevel"].ToString();
                            txtMaximum.Text = dr["Maximum"].ToString();
                        }
                    }
                    else //record is not existing
                    {
                        Response.Redirect("Default.aspx");
                    }
                }
            }
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string fileName1 = Path.GetFileName(txtImage.PostedFile.FileName);
        txtImage.PostedFile.SaveAs(Server.MapPath("~/img/") + fileName1);

        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = @"UPDATE Products SET Name=@Name, Code=@Code,
                Image=@Image, Price=@Price, IsFeatured=@IsFeatured,
                CriticalLevel=@CriticalLevel, Maximum=@Maximum, Status=@Status,
                DateModified=@DateModified FROM Products
                WHERE ProductID=@ProductID";
            //parameterized query

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@Name", txtProductName.Text);
                cmd.Parameters.AddWithValue("@Code", txtCode.Text);
                cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                cmd.Parameters.AddWithValue("@Image", txtImage.Value);
                cmd.Parameters.AddWithValue("@Price", txtPrice.Text);
                cmd.Parameters.AddWithValue("@IsFeatured", txtIsFeatured.Text);
                cmd.Parameters.AddWithValue("@CriticalLevel", txtCriticalLevel.Text);
                cmd.Parameters.AddWithValue("@Maximum", txtMaximum.Text);
                cmd.Parameters.AddWithValue("@Status", "Active");
                cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                cmd.Parameters.AddWithValue("@ProductID",
                    Request.QueryString["ID"].ToString());

                cmd.ExecuteNonQuery();

                Response.Redirect("Default.aspx");
            }

        }
    }
}