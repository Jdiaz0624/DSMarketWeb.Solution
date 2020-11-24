<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="MantenimientoCategorias.aspx.cs" Inherits="DSMarketWeb.Solution.Paginas.Inventario.MantenimientoCategorias" %>
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
        <div class="jumbotron" align="center">
            <asp:Label ID="lbTitulo" runat="server" Text="Consulta de Categorias"></asp:Label>
        </div>
        <div class="form-row">
            <div class="form-group col-md-3">
                <asp:Label ID="lbTipoProducto" runat="server" Text="Seleccionar Tipo de Producto" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarTipoProducto" runat="server" ToolTip="Seleccionar Tipo de Producto" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="form-group col-md-3">
                <asp:Label ID="CategoriaFiltro" runat="server" Text="Categoria" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtCategoriaFiltro" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
         <div align="center">
         <asp:Button ID="btnConsultar" runat="server" Text="Buscar" ToolTip="Buscar" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnConsultar_Click" />
         <button type="button" id="btnNuevo" class="btn btn-outline-secondary btn-sm Custom" data-toggle="modal" data-target=".MantenimientoEmpleados">Nuevo</button>
         <button type="button" id="btnModificar" class="btn btn-outline-secondary btn-sm Custom" data-toggle="modal" data-target=".MantenimientoEmpleados">Modificar</button>
         <asp:Button ID="btnRestabelcer" runat="server" Text="Atras" ToolTip="Atras" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnRestabelcer_Click" />
         <asp:Button ID="btnExportar" runat="server"  Text="Exportar" ToolTip="Exportar a exel" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnExportar_Click" />
        </div>
        <br />
        <div align="center">
            <asp:Label ID="lbCantidadRegistrosTitulo" runat="server" Text="Cantidad de Registros (" CssClass="Letranegrita"></asp:Label>
            <asp:Label ID="lbCantidadRegistrosVariable" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
            <asp:Label ID="lbCantidadRegistroscerrar" runat="server" Text=")" CssClass="Letranegrita"></asp:Label>
        </div>
        <br />
         <asp:GridView ID="gvListado" runat="server" AllowPaging="true" OnPageIndexChanging="gvListado_PageIndexChanging" OnSelectedIndexChanged="gvListado_SelectedIndexChanged" AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                   <%-- <%$ Resources:Traducciones,OrdenNivel %>--%>
                    <asp:CommandField ButtonType="Button" HeaderText="Seleccionar"  ControlStyle-CssClass="btn btn-outline-secondary btn-sm" SelectText="Seleccionar" ShowSelectButton="True" />
                    <asp:BoundField DataField="#" HeaderText="ID" />
                    <asp:BoundField DataField="#" HeaderText="Tipo de Producto" />
                    <asp:BoundField DataField="#" HeaderText="Categoria" />
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
        <br />

      <div class="modal fade bd-example-modal-lg MantenimientoEmpleados" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg" role="document">
    <div class="modal-content">
        <div class="jumbotron" align="center">
            <asp:Label ID="lbEncabezadoMantenimiento" runat="server" Text="Mantenimiento de Empleados"></asp:Label>
        </div>
        <!--AQUI COMIENZAN LOS CONTROLES PARA REALIZAR LOS MANTENIMIENTOS CORRESPONDIENTES-->
    <div  class="container-fluid">
       <asp:ScriptManager ID="ScripManagerEmpleado" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanelEmpleado" runat="server">
            <ContentTemplate>
                 <div " class="form-row">

              <div class="form-group col-md-6">
                <asp:Label ID="lbSeleccionarSucursalMantenimiento" runat="server" CssClass="Letranegrita" Text="Sucursal"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarSucursalmantenimiento" runat="server" CssClass="form-control" AutoPostBack="True" ToolTip="Sucursal" OnSelectedIndexChanged="ddlSeleccionarSucursalmantenimiento_SelectedIndexChanged"></asp:DropDownList>
            </div>

            <div class="form-group col-md-6">
                 <asp:Label ID="lbOficinaMantenimiento"  runat="server" CssClass="Letranegrita" Text="Oficina"></asp:Label>
                <asp:DropDownList ID="ddlOficinaMantenimiento" runat="server"  CssClass="form-control"  AutoPostBack="True"  ToolTip="Seleccionar Oficina" OnSelectedIndexChanged="ddlOficinaMantenimiento_SelectedIndexChanged"></asp:DropDownList>
            </div>
               <div class="form-group col-md-6">
                    <asp:Label ID="lbDepartamentoMantenimiento"   runat="server"  CssClass="Letranegrita" Text="Departamento"></asp:Label>
                   <asp:DropDownList ID="ddlDepartamenoMantenimiento"  runat="server" CssClass="form-control"  ToolTip="Seleccionar Departamento"></asp:DropDownList>
            </div>
               <div class="form-group col-md-6">
                    <asp:Label ID="lbNombreMantenimiento" runat="server" CssClass="Letranegrita"  Text="Nombre"></asp:Label>
            <asp:TextBox ID="txtNombreMantenimiento" runat="server"  AutoCompleteType="Disabled" CssClass="form-control"  MaxLength="100"></asp:TextBox>
            </div>
             <div class="form-group col-md-6">
             <asp:Label ID="lbClaveSeguridad" runat="server" CssClass="Letranegrita" Text="Clave de Seguridad"></asp:Label>
            <asp:TextBox ID="txtClaveSeguridad" runat="server"  AutoCompleteType="Disabled" CssClass="form-control" TextMode="Password"  MaxLength="20"></asp:TextBox>
            </div>
        </div>
      <div class="form-group form-check">
          <div class="form-check-inline">
                   <asp:CheckBox ID="cbEstatusMantenimiento"  runat="server" Text="Estatus" CssClass="form-check-input"  ToolTip="Estatus" />
          </div>
      </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div align="Center">
         <asp:Button ID="btnGuardarMantenimiento"  runat="server" CssClass="btn btn-outline-primary btn-sm Custom"  Text="Guardar" ToolTip="Guardar Registro" OnClick="btnGuardarMantenimiento_Click"/>
         <asp:Button ID="btnModificarMantenimiento"  runat="server" CssClass="btn btn-outline-primary btn-sm Custom"  Text="Modificar" ToolTip="Modificar Registro" OnClick="btnModificarMantenimiento_Click"/>
        </div>
        <br />
    </div>
    <!--AQUI TERMINAN LOS CONTROLES PARA REALIZAR LOS MANTENIMIENTOS CORRESPONDIENTES-->
    </div>
  </div>
</div>
    </div>
</asp:Content>
