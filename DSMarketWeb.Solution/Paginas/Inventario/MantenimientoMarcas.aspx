<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="MantenimientoMarcas.aspx.cs" Inherits="DSMarketWeb.Solution.Paginas.Inventario.MantenimientoMarcas" %>
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
            <asp:Label ID="lbTituloCOnsulta" runat="server" Text="Consulta de Marcas"></asp:Label>
        </div>
        <div class="form-row">
            <div class="form-group col-md-3">
                <asp:Label ID="lbTipoProductoCOnsulta" runat="server" Text="Seleccionar Tipo de Producto" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarTipoProductoConsulta" runat="server" AutoPostBack="true" OnTextChanged="ddlSeleccionarTipoProductoConsulta_TextChanged" ToolTip="Seleccionar el tipo de producto para la consulta" CssClass="form-control"></asp:DropDownList>

            </div>
            <div class="form-group col-md-3">
                <asp:Label ID="lbCategoriaConsulta" runat="server" Text="Seleccionar Categoria" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarCategoriaConsulta" runat="server" ToolTip="Seleccionar la Categoria" CssClass="form-control"></asp:DropDownList>
            </div>
           <div class="form-group col-md-3">
                <asp:Label ID="lbMarcaConsulta" runat="server" Text="Marca" CssClass="Letranegrita"></asp:Label>
            <asp:TextBox ID="txtMarcaConsulta" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
           </div>
        </div>
        <br />
        <div align="center">
            <asp:Button ID="btnConsultar" runat="server" Text="Consultar" ToolTip="Consultar Registros" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnConsultar_Click" />
             <button type="button" id="btnNuevo" class="btn btn-outline-secondary btn-sm Custom" data-toggle="modal" data-target=".MantenimientoMarcas">Nuevo</button>
             <button type="button" id="btnModificar" class="btn btn-outline-secondary btn-sm Custom" data-toggle="modal" data-target=".MantenimientoMarcas">Modificar</button>
            <asp:Button ID="btnRestablecerPantalla" runat="server" Text="Restablecer" ToolTip="Restablecer" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnRestablecerPantalla_Click" />
            <asp:Button ID="btnExportar" runat="server" Text="Exportar" ToolTip="Exportar Registros a Exel" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnExportar_Click" />
        </div>
        <br />
        <div align="center">
            <asp:Label ID="lbCantidadRegistrosTitulo" runat="server" Text="Cantidad de registros (" CssClass="Letranegrita"></asp:Label>
            <asp:Label ID="lbCantidadRegistrosVariable" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
            <asp:Label ID="lbCantidadRegistrosCerrar" runat="server" Text=")" CssClass="Letranegrita"></asp:Label>
        </div>
        <br />
         <asp:GridView ID="gvListado" runat="server" AllowPaging="true" OnPageIndexChanging="gvListado_PageIndexChanging" OnSelectedIndexChanged="gvListado_SelectedIndexChanged" AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                   <%-- <%$ Resources:Traducciones,OrdenNivel %>--%>
                    <asp:CommandField ButtonType="Button" HeaderText="Seleccionar"  ControlStyle-CssClass="btn btn-outline-secondary btn-sm" SelectText="Seleccionar" ShowSelectButton="True" />
                    <asp:BoundField DataField="#" HeaderText="ID" />
                    <asp:BoundField DataField="#" HeaderText="Marca" />
                    <asp:BoundField DataField="#" HeaderText="Tipo de Producto" />
                    <asp:BoundField DataField="#" HeaderText="Categoria" />
                    <asp:BoundField DataField="#" HeaderText="Estatus" />
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


    <div class="modal fade bd-example-modal-lg MantenimientoMarcas" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg" role="document">
    <div class="modal-content">
      <asp:ScriptManager ID="ScripManagerMarcas" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanelMarca" runat="server">
            <ContentTemplate>
                <div class="container-fluid">
          <div class="jumbotron" align="center"> 
              <asp:Label ID="lbTituloMantenimiento" runat="server" Text="Mantenimiento de Marcas"></asp:Label>

          </div>
              <div class="form-row">
                  <div class="form-group col-md-4">
                      <asp:Label ID="lbSeleccionarTipoProductoMantenimiento" runat="server" Text="Seleccionar Tipo de Producto" CssClass="Letranegrita"></asp:Label>
                      <asp:DropDownList ID="ddlSeleccionarTipoProductoMantenimiento" runat="server" AutoPostBack="true" OnTextChanged="ddlSeleccionarTipoProductoMantenimiento_TextChanged" ToolTip="Seleccionar el tipo de producto" CssClass="form-control"></asp:DropDownList>
                  </div>

                  <div class="form-group col-md-4">
                      <asp:Label ID="lbSeleccionarCateoriaMantenimiento" runat="server" Text="Seleccionar Categoria" CssClass="Letranegrita"></asp:Label>
                      <asp:DropDownList ID="ddlSeleccionarCategoriaMantenimiento" runat="server" ToolTip="Seleccionar Categoria" CssClass="form-control"></asp:DropDownList>
                  </div>

                  <div class="form-group col-md-4">
                      <asp:Label ID="lbMarcaMantenimiento" runat="server" Text="Marca" CssClass="Letranegrita"></asp:Label>
                      <asp:TextBox ID="txtMarcaMantenimiento" runat="server" AutoCompleteType="Disabled" MaxLength="100" CssClass="form-control"></asp:TextBox>
                  </div>
              </div>
              <div class="form-check-inline">
                  <div class="form-group form-check">
                      <asp:CheckBox ID="cbEstatusMantenimiento" runat="server" Text="Estatus" CssClass="form-check-input" ToolTip="Estatus de Marca" />
                  </div>
              </div>
             
        
      </div>
            </ContentTemplate>
        </asp:UpdatePanel>
         <div align="center">
                  <asp:Button ID="btnGuardarMantenimiento" runat="server" Text="Guardar" ToolTip="Guardar Registro" OnClick="btnGuardarMantenimiento_Click" CssClass="btn btn-outline-secondary btn-sm Custom" />
                  <asp:Button ID="btnModificarMantenimiento" runat="server" Text="Modificar" ToolTip="Modificar Registro" OnClick="btnModificarMantenimiento_Click" CssClass="btn btn-outline-secondary btn-sm Custom" />
              </div>
              <br />
    </div>
  </div>
</div>
    </div>
</asp:Content>
