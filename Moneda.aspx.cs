using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class Moneda : System.Web.UI.Page
{
    string cadena = WebConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            CargarMonedas();
    }

    private void CargarMonedas()
    {
        using (SqlConnection con = new SqlConnection(cadena))
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Moneda", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvMonedas.DataSource = dt;
            gvMonedas.DataBind();
        }
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(cadena))
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Moneda (Nombre) VALUES (@Nombre)", con);
            cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
            con.Open();
            cmd.ExecuteNonQuery();
        }

        Logger.Registrar($"Alta Moneda: {txtNombre.Text}");
        txtNombre.Text = "";
        CargarMonedas();
    }

    protected void gvMonedas_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {
        int id = (int)gvMonedas.DataKeys[e.RowIndex].Value;

        using (SqlConnection con = new SqlConnection(cadena))
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Moneda WHERE IdMoneda = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();
            cmd.ExecuteNonQuery();
        }

        Logger.Registrar($"Baja Moneda ID: {id}");
        CargarMonedas();
    }
}
