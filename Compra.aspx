<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Compra.aspx.cs" Inherits="Compra" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>ABM Compras</title></head>
<body>
<form id="form1" runat="server">
    <div>
        <h2>ABM Compras</h2>
        <asp:Label ID="lblFecha" runat="server" Text="Fecha:"></asp:Label>
        <asp:TextBox ID="txtFecha" runat="server" TextMode="Date"></asp:TextBox><br /><br />

        <asp:Label ID="lblMonto" runat="server" Text="Monto Gravado:"></asp:Label>
        <asp:TextBox ID="txtMontoGravado" runat="server"></asp:TextBox><br /><br />

        <asp:Label ID="lblIva" runat="server" Text="IVA:"></asp:Label>
        <asp:TextBox ID="txtIVA" runat="server"></asp:TextBox><br /><br />

        <asp:Label ID="lblFactura" runat="server" Text="Número de Factura:"></asp:Label>
        <asp:TextBox ID="txtFactura" runat="server"></asp:TextBox><br /><br />

        <asp:Label ID="lblPuntoVenta" runat="server" Text="Punto de Venta:"></asp:Label>
        <asp:TextBox ID="txtPuntoVenta" runat="server"></asp:TextBox><br /><br />

        <asp:Label ID="lblProveedor" runat="server" Text="Proveedor:"></asp:Label>
        <asp:DropDownList ID="ddlProveedores" runat="server"></asp:DropDownList><br /><br />

        <asp:Label ID="lblMoneda" runat="server" Text="Moneda:"></asp:Label>
        <asp:DropDownList ID="ddlMonedas" runat="server"></asp:DropDownList><br /><br />

        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" /><br /><br />

        <asp:Label ID="lblFiltro" runat="server" Text="Filtrar por Proveedor o Moneda:"></asp:Label><br />
        <asp:DropDownList ID="ddlFiltroProveedor" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFiltroProveedor_SelectedIndexChanged"></asp:DropDownList>
        <asp:DropDownList ID="ddlFiltroMoneda" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFiltroMoneda_SelectedIndexChanged"></asp:DropDownList>
        <br /><br />

        <asp:GridView ID="gvCompras" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
                      OnRowDeleting="gvCompras_RowDeleting">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" />
                <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                <asp:BoundField DataField="montoGravado" HeaderText="Monto Gravado" />
                <asp:BoundField DataField="iva" HeaderText="IVA" />
                <asp:BoundField DataField="numeroFactura" HeaderText="Factura" />
                <asp:BoundField DataField="puntoVenta" HeaderText="Punto Venta" />
                <asp:BoundField DataField="razonSocial" HeaderText="Proveedor" />
                <asp:BoundField DataField="detalle" HeaderText="Moneda" />
                <asp:CommandField ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
    </div>
</form>
</body>
</html>
