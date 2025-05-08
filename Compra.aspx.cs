using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Compra : System.Web.UI.Page
{
    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ISSDTP4Connection"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarProveedores();
            CargarMonedas();
            CargarCompras();
            CargarFiltros();
        }
    }

    private void CargarProveedores()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT id, razonSocial FROM Proveedor", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ddlProveedores.DataSource = dt;
            ddlProveedores.DataTextField = "razonSocial";
            ddlProveedores.DataValueField = "id";
            ddlProveedores.DataBind();
        }
    }

    private void CargarMonedas()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT id, detalle FROM Moneda", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ddlMonedas.DataSource = dt;
            ddlMonedas.DataTextField = "detalle";
            ddlMonedas.DataValueField = "id";
            ddlMonedas.DataBind();
        }
    }

    private void CargarFiltros()
    {
        ddlFiltroProveedor.DataSource = ddlProveedores.DataSource;
        ddlFiltroProveedor.DataTextField = "razonSocial";
        ddlFiltroProveedor.DataValueField = "id";
        ddlFiltroProveedor.DataBind();
        ddlFiltroProveedor.Items.Insert(0, new ListItem("-- Todos --", "0"));

        ddlFiltroMoneda.DataSource = ddlMonedas.DataSource;
        ddlFiltroMoneda.DataTextField = "detalle";
        ddlFiltroMoneda.DataValueField = "id";
        ddlFiltroMoneda.DataBind();
        ddlFiltroMoneda.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = @"INSERT INTO Compras (fecha, montoGravado, iva, numeroFactura, puntoVenta, idProveedor, idMoneda)
                             VALUES (@fecha, @monto, @iva, @factura, @punto, @proveedor, @moneda)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@fecha", txtFecha.Text);
            cmd.Parameters.AddWithValue("@monto", Convert.ToDecimal(txtMontoGravado.Text));
            cmd.Parameters.AddWithValue("@iva", Convert.ToDecimal(txtIVA.Text));
            cmd.Parameters.AddWithValue("@factura", Convert.ToInt32(txtFactura.Text));
            cmd.Parameters.AddWithValue("@punto", Convert.ToInt32(txtPuntoVenta.Text));
            cmd.Parameters.AddWithValue("@proveedor", ddlProveedores.SelectedValue);
            cmd.Parameters.AddWithValue("@moneda", ddlMonedas.SelectedValue);

            conn.Open();
            cmd.ExecuteNonQuery();
            Logger.Log("Compra agregada");
            CargarCompras();
        }
    }

    private void CargarCompras()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string filtro = "";
            if (ddlFiltroProveedor.SelectedValue != "0")
                filtro += " AND c.idProveedor = " + ddlFiltroProveedor.SelectedValue;
            if (ddlFiltroMoneda.SelectedValue != "0")
                filtro += " AND c.idMoneda = " + ddlFiltroMoneda.SelectedValue;

            string query = $@"SELECT c.id, c.fecha, c.montoGravado, c.iva, c.numeroFactura, c.puntoVenta, 
                                     p.razonSocial, m.detalle 
                              FROM Compras c
                              INNER JOIN Proveedor p ON c.idProveedor = p.id
                              INNER JOIN Moneda m ON c.idMoneda = m.id
                              WHERE 1=1 {filtro}";

            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvCompras.DataSource = dt;
            gvCompras.DataBind();
        }
    }

    protected void gvCompras_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(gvCompras.DataKeys[e.RowIndex].Value);
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Compras WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            cmd.ExecuteNonQuery();
            Logger.Log("Compra eliminada ID: " + id);
            CargarCompras();
        }
    }

    protected void ddlFiltroProveedor_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarCompras();
    }

    protected void ddlFiltroMoneda_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarCompras();
    }
}
