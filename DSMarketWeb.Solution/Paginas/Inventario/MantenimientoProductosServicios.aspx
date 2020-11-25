<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="MantenimientoProductosServicios.aspx.cs" Inherits="DSMarketWeb.Solution.Paginas.Inventario.MantenimientoProductosServicios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <style type="text/css">
        .jumbotron{
            color:#000000; 
            background:#96CEF7;
            font-size:30px;
            font-weight:bold;
            font-family:'Gill Sans';
            padding:25px;
        }

        .btn-sm{
            width:90px;
        }

        .Letranegrita {
        font-weight:bold;
        }
    </style>

    <div class="container-fluid">
        <div class="jumbotron" align="center" >
            <asp:Label ID="lbTituloConsulta" runat="server" Text="Consulta de Productos / Servicios"></asp:Label>
        </div>
        <div class="form-check-inline">
            <div class="form-group form-check">
                <asp:CheckBox ID="cbAgregarRangoFechaConsulta" runat="server" Text="Agregar Rango de Fecha" AutoPostBack="true" OnCheckedChanged="cbAgregarRangoFechaConsulta_CheckedChanged" ToolTip="Agregar Rango de fecha en la consulta" />
            </div>
        </div>

        <div class="form-row">
            <div class="form-group col-md-3">
                <asp:Label ID="lbProductoConsulta" runat="server" Text="Producto" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtProductoConsulta" runat="server" MaxLength="1000" CssClass="form-control"></asp:TextBox>
            </div>

             <div class="form-group col-md-3">
                <asp:Label ID="lbCodigoBarraConsulta" runat="server" Text="Codigo de Barra" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtcodigoBarraConsulta" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
            </div>

             <div class="form-group col-md-3">
                <asp:Label ID="lbReferenciaConsulta" runat="server" Text="Referencia" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtReferenciaConsulta" runat="server" MaxLength="1000" CssClass="form-control"></asp:TextBox>
            </div>

             <div class="form-group col-md-3">
                <asp:Label ID="lbNumeroRegumientoConsulta" runat="server" Text="Numero de Seguimiento" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtNumeroSeguimientoConsulta" runat="server" MaxLength="1000" CssClass="form-control"></asp:TextBox>
            </div>

             <div class="form-group col-md-3">
                <asp:Label ID="lbFechaDesdeConsulta" runat="server" Text="Fecha Desde" Enabled="false" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtFechaDesdeConsulta" runat="server" TextMode="Date" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>

             <div class="form-group col-md-3">
                <asp:Label ID="lbfechaHastaConsulta" runat="server" Text="Fecha Hasta" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtFechaHastaConsulta" runat="server" TextMode="Date" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>

              <div class="form-group col-md-3">
                <asp:Label ID="lbSeleccionarTipoProductoConsulta" runat="server" Text="Tipo de Producto" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarTipoProductoConsulta" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarTipoProductoConsulta_SelectedIndexChanged" ToolTip="Seleccionar Tipo de Producto" CssClass="form-control"></asp:DropDownList>
            </div>

             <div class="form-group col-md-3">
                <asp:Label ID="lbSeleccionarCategoriaConsulta" runat="server" Text="Categoria" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarCategoriaConsulta" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarCategoriaConsulta_SelectedIndexChanged" ToolTip="Seleccionar Unidad de Medida" CssClass="form-control"></asp:DropDownList>
            </div>

            

             <div class="form-group col-md-3">
                <asp:Label ID="lbSeleccionarMarcaConsulta" runat="server" Text="Marca" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarMarcaConsulta" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarMarcaConsulta_SelectedIndexChanged" runat="server" ToolTip="Seleccionar Marca" CssClass="form-control"></asp:DropDownList>
            </div>

            <div class="form-group col-md-3">
                <asp:Label ID="lbSeleccionarModeloConsulta" runat="server" Text="Modelo" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarModeloConsulta" runat="server" ToolTip="Seleccionar Modelo" CssClass="form-control"></asp:DropDownList>
            </div>

            <div class="form-group col-md-3">
                <asp:Label ID="lbSeleccionarUnidadMedidaConsulta" runat="server" Text="Unidad de Medida" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarUnidadMedidaConsulta" runat="server" ToolTip="Seleccionar Unidad de Medida" CssClass="form-control"></asp:DropDownList>
            </div>

             <div class="form-group col-md-3">
                <asp:Label ID="lbSeleccionarCapacidadConsulta" runat="server" Text="Capacidad" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarCapacidadConsulta" runat="server"  ToolTip="Seleccionar Capacidad" CssClass="form-control"></asp:DropDownList>
            </div>

             <div class="form-group col-md-3">
                <asp:Label ID="lbSeleccionarCondicionConsulta" runat="server" Text="Condición" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarCondicionConculta" runat="server"  ToolTip="Seleccionar Condición" CssClass="form-control"></asp:DropDownList>
            </div>

             <div class="form-group col-md-3">
                <asp:Label ID="lbSeleccionarColorConsulta" runat="server" Text="Color" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarColorConsulta" runat="server"  ToolTip="Seleccionar Color" CssClass="form-control"></asp:DropDownList>
            </div>

        </div>


        <!--BOTONES PRINCIPALES-->
        <div align="center">
            <asp:Button ID="btnBuscar" runat="server" Text="Consultar" OnClick="btnBuscar_Click" CssClass="btn btn-outline-secondary btn-sm Custom" ToolTip="Consultar Registros" />
            <button type="button" id="btnNuevo" class="btn btn-outline-secondary btn-sm Custom" data-toggle="modal" data-target=".MantenimientoModelos">Nuevo</button>
            <button type="button" id="btnModificar" class="btn btn-outline-secondary btn-sm Custom" data-toggle="modal" data-target=".MantenimientoModelos">Editar</button>
            <button type="button" id="btnEliminar" class="btn btn-outline-secondary btn-sm Custom" data-toggle="modal" data-target=".MantenimientoModelos">Eliminar</button>
            <button type="button" id="btnSuplir" class="btn btn-outline-secondary btn-sm Custom" data-toggle="modal" data-target=".MantenimientoModelos">Suplir</button>
            <asp:Button ID="btnReporte" runat="server" Text="Reporte" OnClick="btnReporte_Click" CssClass="btn btn-outline-secondary btn-sm Custom" ToolTip="Reporte de Inventario" />
            <button type="button" id="btnDescartar" class="btn btn-outline-secondary btn-sm Custom" data-toggle="modal" data-target=".MantenimientoModelos">Descartar</button>
            <asp:Button ID="btndetalle" runat="server" Text="Detalle" OnClick="btndetalle_Click" CssClass="btn btn-outline-secondary btn-sm Custom" ToolTip="Detalle del Producto" />
            <br />
            <asp:Label ID="lbCantidadProductosTitulo" runat="server" Text="Cantidad de Productos (" CssClass="Letranegrita"></asp:Label>
            <asp:Label ID="lbCantidadProductosVariable" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
            <asp:Label ID="lbCantidadProductosCerrar" runat="server" Text=")" CssClass="Letranegrita"></asp:Label>
            
            <asp:Label ID="lbCantidadServiciosTitulo" runat="server" Text="Cantidad de registros (" CssClass="Letranegrita"></asp:Label>
            <asp:Label ID="lbCantidadServiciosVariable" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
            <asp:Label ID="lbCantidadServiciosCerrar" runat="server" Text=")" CssClass="Letranegrita"></asp:Label>
            <br />

            <asp:Label ID="lbCapitalInvertidoTitulo" runat="server" Text="Capital Invertido (" CssClass="Letranegrita"></asp:Label>
            <asp:Label ID="lbCapitalInvertidoVariable" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
            <asp:Label ID="lbCapitalInvertidoCerrar" runat="server" Text=")" CssClass="Letranegrita"></asp:Label>

            <asp:Label ID="lbGananciaAproximadaTitulo" runat="server" Text="Ganancia Aproximada (" CssClass="Letranegrita"></asp:Label>
            <asp:Label ID="lbGananciaAproximadaCerrar" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
            <asp:Label ID="lbGananciaAproximada" runat="server" Text=")" CssClass="Letranegrita"></asp:Label>
        </div>
        <br />
         <asp:GridView ID="gvListado" runat="server" AllowPaging="true" OnPageIndexChanging="gvListado_PageIndexChanging" OnSelectedIndexChanged="gvListado_SelectedIndexChanged" AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                   <%-- <%$ Resources:Traducciones,OrdenNivel %>--%>
                    <asp:CommandField ButtonType="Button" HeaderText="Seleccionar"  ControlStyle-CssClass="btn btn-outline-secondary btn-sm" SelectText="Seleccionar" ShowSelectButton="True" />
                    <asp:BoundField DataField="#" HeaderText="ID" />
                    <asp:BoundField DataField="#" HeaderText="Producto" />
                    <asp:BoundField DataField="#" HeaderText="Tipo" />
                    <asp:BoundField DataField="#" HeaderText="Categoria" />
                    <asp:BoundField DataField="#" HeaderText="Precio" />
              
                </Columns  >
                 <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#7BC5FF" HorizontalAlign="Center" Font-Bold="True" ForeColor="Black" />
                <PagerStyle BackColor="#7BC5FF" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" HorizontalAlign="Center" ForeColor="Black" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
            </asp:GridView>
    </div>
</asp:Content>
