<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="Facturacion.aspx.cs" Inherits="DSMarketWeb.Solution.Paginas.Servicios.Facturacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <style type="text/css">
        .jumbotron{
            color:#000000; 
            background:#1E90FF;
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
        table {
            border-collapse: collapse;
        }
        

        th {
            background-color: dodgerblue;
            color: white;
        }

        .BotonEspecoal {
           width:100%;
             font-weight:bold;
          }
    </style>

    <div class="container-fluid">
        <div class="jumbotron" align="center">
            <asp:Label ID="lbTitulo" runat="server" Text="Facturación" CssClass="Letranegrita"></asp:Label>
            <asp:Label ID="lbNumeroConectorFacturacion" runat="server" Text="NumeroConector" Visible="false"></asp:Label>
        </div>

        <div class="form-check-inline">
            <div class="form-group form-check">
                <asp:Label ID="lbTipoProcesoFacturacion" runat="server" Text="Tipo de Proceso: " CssClass="Letranegrita"></asp:Label>
                <asp:RadioButton ID="rbFacturacion" runat="server" Text="Facturación" CssClass="form-check-input" GroupName="TipoProceso" ToolTip="Facturar" />
                <asp:RadioButton ID="rbCotizar" runat="server" Text="Cotización" CssClass="form-check-inpu" GroupName="TipoProceso" ToolTip="Cotizar" />
            </div>
        </div>

         <div class="form-check-inline">
            <div class="form-group form-check">
                <asp:Label ID="lbTipoVenta" runat="server" Text="Tipo de Venta: " CssClass="Letranegrita"></asp:Label>
                <asp:RadioButton ID="rbContado" runat="server" Text="Contado" CssClass="form-check-input" GroupName="TipoVenta" ToolTip="Realizar Venta a Contado" />
                <asp:RadioButton ID="rbCredito" runat="server" Text="Credito" CssClass="form-check-input" GroupName="TipoVenta" ToolTip="Realizar Venta a Credito" />
            </div>
        </div>


       <%-- <button class="btn btn-outline-primary btn-sm BotonEspecoal" type="button" id="btnInformacionCobertura" data-toggle="collapse" data-target="#InformacionCliente" aria-expanded="false" aria-controls="collapseExample">
                    TITULO AQUI
                     </button><br />
       <div class="collapse" id="InformacionCliente">
                <div class="card card-body">
                 CONTROLES AQUI
                </div>
            </div>--%>
        <asp:ScriptManager ID="ScripManagerFActuracion" runat="server"></asp:ScriptManager>
         <button class="btn btn-outline-primary btn-sm BotonEspecoal" type="button" id="btnInformacionCobertura" data-toggle="collapse" data-target="#InformacionCliente" aria-expanded="false" aria-controls="collapseExample">
                    BUSCAR CLIENTES REGISTRADOS PARA FACTURAR
                     </button><br />
       <div class="collapse" id="InformacionCliente">
                <div class="card card-body">
                 <asp:UpdatePanel ID="UpdatePanelBuscarClientes" runat="server">
                     <ContentTemplate>
                         <div class="form-row">
                             <div class="form-group col-md-4">
                                 <asp:Label ID="lbCodigoClienteConsulta" runat="server" Text="Codigo de Cliente" CssClass="Letranegrita"></asp:Label>
                                 <asp:TextBox ID="txtCodigoClienteConsulta" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                             </div>

                             <div class="form-group col-md-4">
                                  <asp:Label ID="lbRNCClienteConsulta" runat="server" Text="RNC de Cliente" CssClass="Letranegrita"></asp:Label>
                                 <asp:TextBox ID="txtRNCConsultaCliente" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                             </div>

                             <div class="form-group col-md-4">
                                  <asp:Label ID="lbNombreClienteConsulta" runat="server" Text="Nombre de Cliente" CssClass="Letranegrita"></asp:Label>
                                 <asp:TextBox ID="txtNombreClienteConsulta" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                             </div>
                         </div>
                         <div align="center">
                             <asp:Button ID="btnConsultarClientes" runat="server" Text="Consultar" CssClass="btn btn-outline-secondary btn-sm" ToolTip="Consultar Registros" OnClick="btnConsultarClientes_Click" />
                             <br /><br />
                             <asp:Label ID="lbCantidadRegistrosEncontradosClienteTitulo" runat="server" Text="Cantidad de Registros ( " CssClass="Letranegrita"></asp:Label>
                             <asp:Label ID="lbCantidadRegistrosEncontradosClientesVariables" runat="server" Text=" 0 " CssClass="Letranegrita"></asp:Label>
                             <asp:Label ID="lbCantidadRegistrosEncontradosClientesCerrar" runat="server" Text=" ) " CssClass="Letranegrita"></asp:Label>
                         </div>
                         <br />

                         <div class="table-responsive">
                             <table class="table table-hover">
                                 <thead>
                                     <tr>
                                         <th style="width:10%" align="center"> <asp:Label ID="lbSeleccionarClienteHeaderRepeater" runat="server" Text="Seleccionar" CssClass="Letranegrita"></asp:Label> </th>
                                         <th style="width:10%" align="center"> <asp:Label ID="lbCodigoClienteHeaderRepeater" runat="server" Text="Codigo" CssClass="Letranegrita"></asp:Label>  </th>
                                         <th style="width:30%" align="center"> <asp:Label ID="lbClienteClienteHeaderRepeater" runat="server" Text="Cliente" CssClass="Letranegrita"></asp:Label>  </th>
                                         <th style="width:10%" align="center"> <asp:Label ID="lbRNCClienteHeaderRepeater" runat="server" Text="NCF" CssClass="Letranegrita"></asp:Label>  </th>
                                         <th style="width:10%" align="center"> <asp:Label ID="lbTelefonoClienteHeaderRepeater" runat="server" Text="Telefono" CssClass="Letranegrita"></asp:Label>  </th>
                                         <th style="width:20%" align="center"> <asp:Label ID="lbEmailClienteHeaderRepeater" runat="server" Text="Email" CssClass="Letranegrita"></asp:Label>  </th>
                                         <th style="width:10%" align="center"> <asp:Label ID="lbCreditoClienteHeaderRepeater" runat="server" Text="Credito" CssClass="Letranegrita"></asp:Label>  </th>
                                     </tr>
                                 </thead>
                                 <tbody>
                                     <asp:Repeater ID="rpListadoClientesConsulta" runat="server">
                                         <ItemTemplate>
                                             <tr>
                                         <asp:HiddenField ID="hfIdClienteConsulta" runat="server" Value='<%# Eval("IdCliente") %>' />

                                         <td style="width:10%"> <asp:Button ID="btnSeleccioanrCliente" runat="server" Text="Seleccionar" ToolTip="Seleccionar Cliente" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnSeleccioanrCliente_Click" /> </td>
                                         <td style="width:10%"> <%# Eval("IdCliente") %> </td>
                                         <td style="width:30%"> <%# Eval("Nombre") %> </td>
                                         <td style="width:10%"> <%# Eval("Comprobante") %> </td>
                                         <td style="width:10%"> <%# Eval("Telefono") %> </td>
                                         <td style="width:20%"> <%# Eval("Email") %> </td>
                                         <td style="width:10%"> <%#string.Format("{0:n2}", Eval("MontoCredito")) %> </td>
                                     </tr>
                                         </ItemTemplate>
                                     </asp:Repeater>
                                 </tbody>
                             </table>
                         </div>
                          <!--PAGINACION DEL REPEATER-->
            <div align="center">
                <asp:Label ID="lbPaginaActualTituloClienteConsulta" runat="server" Text="Pagina " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbPaginaActualVariavleClienteConsulta" runat="server" Text=" 0 " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadPaginaTituloClienteConsulta" runat="server" Text=" DE " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadPaginaVAriableClienteConsulta" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
            </div>
             <div id="divPaginacionClienteConsulta" runat="server" align="center">
        <div style="margin-top: 20px;">
            <table style="width: 600px">
                <tr>
                    <td> <asp:LinkButton ID="LinkPrimeraPaginaClienteConsulta" runat="server" Text="Primero" CssClass="btn btn-outline-success btn-sm" ToolTip="Ir a la primera pagina del listado" OnClick="LinkPrimeraPaginaClienteConsulta_Click"></asp:LinkButton> </td>
                    <td> <asp:LinkButton ID="LinkAnteriorClienteConsulta" runat="server" Text="Anterior" CssClass="btn btn-outline-success btn-sm" ToolTip="Ir a la pagina anterior del listado" OnClick="LinkAnteriorClienteConsulta_Click"></asp:LinkButton> </td>
                    <td>
                        <asp:DataList ID="dtPaginacionClienteConsulta" runat="server" OnItemCommand="dtPaginacionClienteConsulta_ItemCommand" OnItemDataBound="dtPaginacionClienteConsulta_ItemDataBound" RepeatDirection="Horizontal">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkPaginacionCentralClienteConsulta" runat="server" CommandArgument='<%# Eval("IndicePagina") %>' CommandName="newPage" Text='<%# Eval("TextoPagina") %>' Width="20px"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:DataList>

                    </td>
                    <td> <asp:LinkButton ID="LinkSiguienteClienteConsulta" runat="server" Text="Siguiente" ToolTip="Ir a la siguiente pagina del listado" CssClass="btn btn-outline-success btn-sm" OnClick="LinkSiguienteClienteConsulta_Click"></asp:LinkButton> </td>
                    <td> <asp:LinkButton ID="LinkUltimoClienteConsulta" runat="server" Text="Ultimo" ToolTip="Ir a la ultima pagina del listado" CssClass="btn btn-outline-success btn-sm" OnClick="LinkUltimoClienteConsulta_Click"></asp:LinkButton> </td>
                </tr>
            </table>
        </div>
        </div>
                     </ContentTemplate>
                 </asp:UpdatePanel>
                </div>
            </div>

        <div class="form-check-inline">
            <div class="form-group form-check">
                <asp:CheckBox ID="cbAgregarComprobante" runat="server" Text="Agregar Comprobante" CssClass="form-check-input" AutoPostBack="true" OnCheckedChanged="cbAgregarComprobante_CheckedChanged" ToolTip="Agregar Comprobante Fiscal a la Facturación" />
                <asp:CheckBox ID="cbAgregarFechaManual" runat="server" Text="Fecha Facturación Manual" CssClass="form-check-input" AutoPostBack="true" OnCheckedChanged="cbAgregarFechaManual_CheckedChanged" ToolTip="Agregar Fecha Manual" />
            </div>
        </div>
        <div class="form-row">
            <div class="form-group col-md-3">
                <asp:Label ID="lbSeleccionarComprobante" runat="server" Text="Comprobante" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarComprobante" runat="server" CssClass="form-control" ToolTip="Seleccionar Comprobante"></asp:DropDownList>
            </div>

             <div class="form-group col-md-3">
                <asp:Label ID="lbNombreCliente" runat="server" Text="Cliente" CssClass="Letranegrita"></asp:Label>
                 <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="form-control" AutoCompleteType="Disabled" MaxLength="200"></asp:TextBox>
            </div>

             <div class="form-group col-md-3">
                <asp:Label ID="lbSeleccionarTipoRNC" runat="server" Text="Tipo de RNC" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarTipoRNC" runat="server" CssClass="form-control" ToolTip="Seleccionar RNC"></asp:DropDownList>
            </div>

            <div class="form-group col-md-3">
                <asp:Label ID="lbRNCCliente" runat="server" Text="RNC" CssClass="Letranegrita"></asp:Label>
                 <asp:TextBox ID="txtRNCCliente" runat="server" CssClass="form-control" AutoCompleteType="Disabled" MaxLength="100"></asp:TextBox>
            </div>

             <div class="form-group col-md-3">
                <asp:Label ID="lbTelefonoCliente" runat="server" Text="Telefono" CssClass="Letranegrita"></asp:Label>
                 <asp:TextBox ID="txtTelefonoCliente" runat="server" CssClass="form-control" AutoCompleteType="Disabled" MaxLength="100"></asp:TextBox>
            </div>

             <div class="form-group col-md-3">
                <asp:Label ID="lbEmailCliente" runat="server" Text="Mail" CssClass="Letranegrita"></asp:Label>
                 <asp:TextBox ID="txtMailCliente" runat="server" CssClass="form-control" AutoCompleteType="Disabled" MaxLength="100"></asp:TextBox>
            </div>

             <div class="form-group col-md-3">
                <asp:Label ID="lbDireccionCliente" runat="server" Text="Dirección" CssClass="Letranegrita"></asp:Label>
                 <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" AutoCompleteType="Disabled" MaxLength="8000"></asp:TextBox>
            </div>

             <div class="form-group col-md-3">
                <asp:Label ID="lbComentario" runat="server" Text="Comentario" CssClass="Letranegrita"></asp:Label>
                 <asp:TextBox ID="txtComentario" runat="server" CssClass="form-control" AutoCompleteType="Disabled" MaxLength="8000"></asp:TextBox>
            </div>
        </div>
       <br />
           <button class="btn btn-outline-primary btn-sm BotonEspecoal" type="button" id="btnAgegarProductosServicios" data-toggle="collapse" data-target="#AgegarProductosServicios" aria-expanded="false" aria-controls="collapseExample">
                    AGREGAR PRODUCTOS / SERVICIOS
                     </button><br />
       <div class="collapse" id="AgegarProductosServicios">
                <div class="card card-body">
                 <asp:UpdatePanel ID="UpdatePanelAgregarServiciosProductos" runat="server">
                     <ContentTemplate>
                         <div class="form-row">
                             <div class="form-group col-md-3">
                                 <asp:Label ID="lbSeleccionarTipoProducto" runat="server" Text="Tipo de Producto" CssClass="Letranegrita"></asp:Label>
                                 <asp:DropDownList ID="ddlSeleccionarTipoProducto" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarTipoProducto_SelectedIndexChanged" ToolTip="Seleccionar el Tipo de Producto" CssClass="form-control"></asp:DropDownList>
                             </div>

                              <div class="form-group col-md-3">
                                 <asp:Label ID="lbSeleccionarCategoria" runat="server" Text="Categoria" CssClass="Letranegrita"></asp:Label>
                                 <asp:DropDownList ID="ddlSeleccionarCategria" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarCategria_SelectedIndexChanged" ToolTip="Seleccionar la Categria" CssClass="form-control"></asp:DropDownList>
                             </div>

                              <div class="form-group col-md-3">
                                 <asp:Label ID="lbSeleccionarMarca" runat="server" Text="Marca" CssClass="Letranegrita"></asp:Label>
                                 <asp:DropDownList ID="ddlSeleccionarMarca" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarMarca_SelectedIndexChanged" ToolTip="Seleccionar Marca" CssClass="form-control"></asp:DropDownList>
                             </div>

                              <div class="form-group col-md-3">
                                 <asp:Label ID="lbSeleccionarModelo" runat="server" Text="Modelo" CssClass="Letranegrita"></asp:Label>
                                 <asp:DropDownList ID="ddlSeleccionarModelo" runat="server" ToolTip="Seleccionar Modelo" CssClass="form-control"></asp:DropDownList>
                             </div>

                              <div class="form-group col-md-3">
                                 <asp:Label ID="lbDescripcion" runat="server" Text="Descripción" CssClass="Letranegrita"></asp:Label>
                                 <asp:TextBox ID="txtDescripcion" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                             </div>

                             <div class="form-group col-md-3">
                                 <asp:Label ID="lbCodigoBarras" runat="server" Text="Codigo de Barras" CssClass="Letranegrita"></asp:Label>
                                 <asp:TextBox ID="txtCodigoBarras" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                             </div>

                             <div class="form-group col-md-3">
                                 <asp:Label ID="lbReferencia" runat="server" Text="Referencia" CssClass="Letranegrita"></asp:Label>
                                 <asp:TextBox ID="txtReferencia" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                             </div>
                         </div>
                         <div align="center">
                             <asp:Button ID="btnBuscarProducto" runat="server" Text="Buscar" CssClass="btn btn-outline-secondary btn-sm" ToolTip="Buscar Producto" OnClick="btnBuscarProducto_Click" />
                             <br />
                             <asp:Label ID="lbCantidadRegistrosProductosTitulo" runat="server" Text="Cantidad de Registros ( " CssClass="Letranegrita"></asp:Label>
                             <asp:Label ID="lbCantidadRegistrosProductosVariable" runat="server" Text=" 0 " CssClass="Letranegrita"></asp:Label>
                             <asp:Label ID="lbCantidadRegistrosProductosCerrar" runat="server" Text=" ) " CssClass="Letranegrita"></asp:Label>
                         </div>
                         <br />
                         <div class="table-responsive">
                             <table class="table table-hover">
                                 <thead>
                                     <tr>
                                          <th style="width:10%" align="left"> <asp:Label ID="lbSeleccionarRegistro" runat="server" Text="Seleccionar" CssClass="Letranegrita"></asp:Label> </th>
                                          <th style="width:10%" align="left"> <asp:Label ID="lbTipoProducto" runat="server" Text="Tipo Producto" CssClass="Letranegrita"></asp:Label> </th>
                                          <th style="width:60%" align="left"> <asp:Label ID="lbProdcto" runat="server" Text="Producto" CssClass="Letranegrita"></asp:Label> </th>
                                          <th style="width:10%" align="left"> <asp:Label ID="lbCantidad" runat="server" Text="Cantidad" CssClass="Letranegrita"></asp:Label> </th>
                                          <th style="width:10%" align="left"> <asp:Label ID="lbPrecio" runat="server" Text="Precio" CssClass="Letranegrita"></asp:Label> </th>
                                     </tr>
                                 </thead>
                                 <tbody>
                                     <asp:Repeater ID="rpListadoProductosAgregar" runat="server">
                                         <ItemTemplate>
                                             <tr>
                                                 <asp:HiddenField ID="hfIdProductoAgregar" runat="server" Value='<%# Eval("IdProducto") %>' />
                                                  <asp:HiddenField ID="hfNumeroConectorProductoAgregar" runat="server" Value='<%# Eval("NumeroConector") %>' />

                                                 <td style="width:10%"> <asp:Button ID="btnSeleccionarProductoAgregar" runat="server" Text="Seleccionar" ToolTip="Seleccionar Registro para agregarlo a factura" OnClick="btnSeleccionarProductoAgregar_Click" /> </td>
                                                 <td style="width:10%"> <%# Eval("TipoProducto") %> </td>
                                                 <td style="width:60%"> <%# Eval("Producto") %> </td>
                                                 <td style="width:10%"> <%#string.Format("{0:n0}", Eval("Stock")) %> </td>
                                                 <td style="width:10%"> <%#string.Format("{0:n2}", Eval("PrecioVenta")) %> </td>
                                             </tr>
                                         </ItemTemplate>
                                     </asp:Repeater>
                                 </tbody>
                             </table>
                         </div>
                                          <!--PAGINACION DEL REPEATER-->
            <div align="center">
                <asp:Label ID="lbPaginaActualTituloProductoAgregar" runat="server" Text="Pagina " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbPaginaActualVariavleProductoAgregar" runat="server" Text=" 0 " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadPaginaTituloProductoAgregar" runat="server" Text=" DE " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadPaginaVAriableProductoAgregar" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
            </div>
             <div id="divPaginacionProductoAgregar" runat="server" align="center">
        <div style="margin-top: 20px;">
            <table style="width: 600px">
                <tr>
                    <td> <asp:LinkButton ID="LinkPrimeraPaginaProductoAgregar" runat="server" Text="Primero" CssClass="btn btn-outline-success btn-sm" ToolTip="Ir a la primera pagina del listado" OnClick="LinkPrimeraPaginaProductoAgregar_Click"></asp:LinkButton> </td>
                    <td> <asp:LinkButton ID="LinkAnteriorProductoAgregar" runat="server" Text="Anterior" CssClass="btn btn-outline-success btn-sm" ToolTip="Ir a la pagina anterior del listado" OnClick="LinkAnteriorProductoAgregar_Click"></asp:LinkButton> </td>
                    <td>
                        <asp:DataList ID="dtPaginacionProductoAgregar" runat="server" OnItemCommand="dtPaginacionProductoAgregar_ItemCommand" OnItemDataBound="dtPaginacionProductoAgregar_ItemDataBound" RepeatDirection="Horizontal">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkPaginacionCentralClienteConsulta" runat="server" CommandArgument='<%# Eval("IndicePagina") %>' CommandName="newPage" Text='<%# Eval("TextoPagina") %>' Width="20px"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:DataList>

                    </td>
                    <td> <asp:LinkButton ID="LinkSiguienteProductoAgregar" runat="server" Text="Siguiente" ToolTip="Ir a la siguiente pagina del listado" CssClass="btn btn-outline-success btn-sm" OnClick="LinkSiguienteProductoAgregar_Click"></asp:LinkButton> </td>
                    <td> <asp:LinkButton ID="LinkUltimoProductoAgregar" runat="server" Text="Ultimo" ToolTip="Ir a la ultima pagina del listado" CssClass="btn btn-outline-success btn-sm" OnClick="LinkUltimoProductoAgregar_Click"></asp:LinkButton> </td>
                </tr>
            </table>
        </div>
        </div>
                         <div class="form-row">
                             <div class="form-group col-md-3">
                                 <asp:Label ID="lbTipoProductoVistaPrevia" runat="server" Text="Tipo de Producto" CssClass="Letranegrita"></asp:Label>
                                 <asp:TextBox ID="txtTipoProductoVistaPrevia" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                             </div>

                             <div class="form-group col-md-3">
                                 <asp:Label ID="lbCategoriaVistaPrevia" runat="server" Text="Categoria" CssClass="Letranegrita"></asp:Label>
                                 <asp:TextBox ID="txtCategoriaVistaPrevia" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                             </div>

                               <div class="form-group col-md-3">
                                 <asp:Label ID="lbAcumulativoVistaPrevia" runat="server" Text="Acumulativo" CssClass="Letranegrita"></asp:Label>
                                 <asp:TextBox ID="txtAcumulativoVistaPrevia" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                             </div>

                               <div class="form-group col-md-3">
                                 <asp:Label ID="lbPrecioVistaPrevia" runat="server" Text="Precio" CssClass="Letranegrita"></asp:Label>
                                 <asp:TextBox ID="txtPrecioVistaPrevia" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                             </div>

                             <div class="form-group col-md-6">
                                 <asp:Label ID="lbProductoVistaPrevia" runat="server" Text="Producto" CssClass="Letranegrita"></asp:Label>
                                 <asp:TextBox ID="txtProductoVistaPrevia" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                             </div>

                             <div class="form-group col-md-3">
                                 <asp:Label ID="lbCantidadDisponibleVistaPrevia" runat="server" Text="Cantidad Disponible" CssClass="Letranegrita"></asp:Label>
                                 <asp:TextBox ID="txtCantidadDisponibleVistaPrevia" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                             </div>

                             <div class="form-group col-md-3">
                                 <asp:Label ID="lbCantidadUsarVistaPrevia" runat="server" Text="Cantidad Usar" CssClass="Letranegrita"></asp:Label>
                                 <asp:TextBox ID="txtCantidadUsarVistaPrevia" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                             </div>

                              <div class="form-group col-md-3">
                                 <asp:Label ID="lbPorcientoDescuentoVistaPrevia" runat="server" Text="% de Descuento" CssClass="Letranegrita"></asp:Label>
                                 <asp:TextBox ID="txtPorcientoDescuentoVistaPrevia" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                             </div>

                             <div class="form-group col-md-3">
                                 <asp:Label ID="lbDescuentoVistaPrevia" runat="server" Text="Descuento" CssClass="Letranegrita"></asp:Label>
                                 <asp:TextBox ID="txtDescuentoVistaPrevia" runat="server" CssClass="form-control" TextMode="Number" step="0.01"></asp:TextBox>
                             </div>

                              <div class="form-group col-md-3">
                                 <asp:Label ID="lbImpuestoVistaPrevia" runat="server" Text="Impuesto" CssClass="Letranegrita"></asp:Label>
                                 <asp:TextBox ID="txtImpuestoVistaPrevia" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                             </div>
                         </div>

                         <div class="form-check-inline">
                             <div class="form-group form-check">
                                 <asp:CheckBox ID="cbANoplicaGarantia" runat="server" Text="No Aplica Garantia" CssClass="form-check-input" ToolTip="Espesificar si este articulo no tiene garantia" />
                             </div>
                         </div>
                         <div align="center">
                             <asp:Button ID="btnAgregarRegistro" runat="server" Text="Agregar" ToolTip="Agregar Registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnAgregarRegistro_Click" />
                             <asp:Button ID="btnEditarRegistroAgregado" runat="server" Text="Editar" ToolTip="Editar Registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnEditarRegistroAgregado_Click" />
                             <asp:Button ID="btnEliminarRegistroAgregado" runat="server" Text="Eliminar" ToolTip="Eliminar Registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnEliminarRegistroAgregado_Click" />
                             <asp:Button ID="btnRestablecerVistaPrevia" runat="server" Text="Restablecer" ToolTip="Restablecer Vista Previa" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnRestablecerVistaPrevia_Click" />
                             <br /><br />
                             <asp:Label ID="lbCantidadRegistrosAgregadosTitulo" runat="server" Text="Cantidad de Registros Agregados ( " CssClass="Letranegrita"></asp:Label>
                             <asp:Label ID="lbCantidadRegistrosAgregadosVariable" runat="server" Text=" 0 " CssClass="Letranegrita"></asp:Label>
                             <asp:Label ID="lbCantidadRegistrosAgregadosCerrar" runat="server" Text=" ) " CssClass="Letranegrita"></asp:Label>
                         </div>
                         <br />
                         <div class="table-responsive">
                             <table class="table table-hover">
                               <thead>
                                     <tr>
                                     <th style="width:10%" align="left"> <asp:Label ID="lbSeleccionarProductoAgregadoHeaderepeater" runat="server" Text="Seleccionar" CssClass="Letranegrita"></asp:Label> </th>
                                     <th style="width:50%" align="left"> <asp:Label ID="lbDescripcionProductoAgregadoHEaderRepeater" runat="server" Text="Producto" CssClass="Letranegrita"></asp:Label> </th>
                                     <th style="width:10%" align="left"> <asp:Label ID="lbPrecioProductoAgregadoHeaderRepeater" runat="server" Text="Precio" CssClass="Letranegrita"></asp:Label> </th>
                                     <th style="width:10%" align="left"> <asp:Label ID="lbDescuentoProductoAgregadoHEaderRepeater" runat="server" Text="Descuento" CssClass="Letranegrita"></asp:Label> </th>
                                     <th style="width:10%" align="left"> <asp:Label ID="lbCantidadProductoAgregadoHeaderRepeater" runat="server" Text="Cantidad" CssClass="Letranegrita"></asp:Label> </th>
                                     <th style="width:10%" align="left"> <asp:Label ID="lbTotalProductoAgregadoHeaderRepeater" runat="server" Text="Total" CssClass="Letranegrita"></asp:Label> </th>
                                 </tr>
                               </thead>
                                 <tbody>
                                     <asp:Repeater ID="rpListadoProductosAgregados" runat="server">
                                         <ItemTemplate>
                                             <tr>
                                                 <th style="width:10%"> <asp:Button ID="btnSeleccionarRegistrosAgregadosHeaderRepeater" runat="server" Text="Seleccionar" ToolTip="Seleccionar Registros Agregados" OnClick="btnSeleccionarRegistrosAgregadosHeaderRepeater_Click" CssClass="btn btn-outline-secondary btn-sm" /> </th>
                                                 <th style="width:50%"> <%# Eval("") %> </th>
                                                 <th style="width:10%"> <%#string.Format("{0:n2}", Eval("")) %> </th>
                                                 <th style="width:10%"> <%#string.Format("{0:n2}", Eval("")) %> </th>
                                                 <th style="width:10%"> <%#string.Format("{0:n0}", Eval("")) %> </th>
                                                 <th style="width:10%"> <%#string.Format("{0:n2}", Eval("")) %> </th>
                                             </tr>
                                         </ItemTemplate>
                                     </asp:Repeater>
                                 </tbody>
                             </table>
                             
                         </div>
                                                      <!--PAGINACION DEL REPEATER-->
            <div align="center">
                <asp:Label ID="lbPaginaActualTituloProductosAgregados" runat="server" Text="Pagina " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbPaginaActualVariavleProductosAgregados" runat="server" Text=" 0 " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadPaginaTituloProductosAgregados" runat="server" Text=" DE " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadPaginaVAriableProductosAgregados" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
            </div>
             <div id="divPaginacionProductosAgregados" runat="server" align="center">
        <div style="margin-top: 20px;">
            <table style="width: 600px">
                <tr>
                    <td> <asp:LinkButton ID="LinkPrimeraPaginaProductosAgregados" runat="server" Text="Primero" CssClass="btn btn-outline-success btn-sm" ToolTip="Ir a la primera pagina del listado" OnClick="LinkPrimeraPaginaProductosAgregados_Click"></asp:LinkButton> </td>
                    <td> <asp:LinkButton ID="LinkAnteriorProductosAgregados" runat="server" Text="Anterior" CssClass="btn btn-outline-success btn-sm" ToolTip="Ir a la pagina anterior del listado" OnClick="LinkAnteriorProductosAgregados_Click"></asp:LinkButton> </td>
                    <td>
                        <asp:DataList ID="dtPaginacionProductosAgregados" runat="server" OnItemCommand="dtPaginacionProductosAgregados_ItemCommand" OnItemDataBound="dtPaginacionProductosAgregados_ItemDataBound" RepeatDirection="Horizontal">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkPaginacionCentralProductosAgregados" runat="server" CommandArgument='<%# Eval("IndicePagina") %>' CommandName="newPage" Text='<%# Eval("TextoPagina") %>' Width="20px"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:DataList>

                    </td>
                    <td> <asp:LinkButton ID="LinkSiguienteProductosAgregados" runat="server" Text="Siguiente" ToolTip="Ir a la siguiente pagina del listado" CssClass="btn btn-outline-success btn-sm" OnClick="LinkSiguienteProductosAgregados_Click"></asp:LinkButton> </td>
                    <td> <asp:LinkButton ID="LinkUltimoProductosAgregados" runat="server" Text="Ultimo" ToolTip="Ir a la ultima pagina del listado" CssClass="btn btn-outline-success btn-sm" OnClick="LinkUltimoProductosAgregados_Click"></asp:LinkButton> </td>
                </tr>
            </table>
        </div>
        </div>
                     </ContentTemplate>
                 </asp:UpdatePanel>
                </div>
            </div>

        <div class="form-row">
            <div class="form-group col-md-3">
                <asp:Label ID="lbTipoGarantiaCalculos" runat="server" Text="Tipo de Garantia" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarTipoGarantiaCalculos" runat="server" ToolTip="Seleccionar el Tipo de Garantia" CssClass="form-control"></asp:DropDownList>
            </div>

            <div class="form-group col-md-3">
                <asp:Label ID="lbTiempoGarantiaCalculos" runat="server" Text="Tiempo de Garantia" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtTiempoGarantiaCalculos" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
            </div>

            <div class="form-group col-md-6">
                <asp:Label ID="lbTipoIngresoCalculos" runat="server" Text="Tipo de Ingreso" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarTipoIngreso" runat="server" ToolTip="Seleccionar el tipo de ingreso" CssClass="form-control"></asp:DropDownList>
            </div>

            <div class="form-group col-md-3">
                <asp:Label ID="lbCantidadProductos" runat="server" Text="Total de Productos" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtTotalProductosCalculos" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
            </div>

            <div class="form-group col-md-3">
                <asp:Label ID="lbCantidadServicios" runat="server" Text="Total de Servicios" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtCantidadServicios" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group col-md-3">
                <asp:Label ID="lbCantidadArticulos" runat="server" Text="Total Articulos" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtCantidadArticulos" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>

             <div class="form-group col-md-3">
                <asp:Label ID="lbFechaManual" runat="server" Text="Fecha Manual" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtFechaManual" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbTotalDescuento" runat="server" Text="Total de Descuento" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtTotalDescuento" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>

             <div class="form-group col-md-4">
                <asp:Label ID="lbSubTotal" runat="server" Text="Sub Total" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtSubTotal" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>

             <div class="form-group col-md-4">
                <asp:Label ID="lbImpuesto" runat="server" Text="Impuesto" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtImpuesto" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>

             <div class="form-group col-md-4">
                <asp:Label ID="lbImpuestoTipoPago" runat="server" Text="Impuesto de Tipo Pago" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtImpuestoTipoPago" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>

             <div class="form-group col-md-4">
                <asp:Label ID="lbImpuestoComprobante" runat="server" Text="Impuesto Comprobante" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtImpuestoComprobante" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>

             <div class="form-group col-md-4">
                <asp:Label ID="lbTotal" runat="server" Text="Total" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtTotal" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>

             <div class="form-group col-md-4">
                <asp:Label ID="lbMontoPagar" runat="server" Text="Monto a Pagar" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtMontoPagar" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>

             <div class="form-group col-md-4">
                <asp:Label ID="lbCambio" runat="server" Text="Cambio" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtCambio" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>

             <div class="form-group col-md-4">
                <asp:Label ID="lbTipoPago" runat="server" Text="Tipo de Pago" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlTipoPago" runat="server" ToolTip="Seleccionar el Tipo de Pago" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>
        <br />
        <div class="table-responsive">
            <table class="table table-hover">
                <thead>
                    <tr>
                        <th style="width:40%" align="left"> <asp:Label ID="lbProductoHeaderReteaper" runat="server" Text="Producto" CssClass="Letranegrita"></asp:Label> </th>
                        <th style="width:10%" align="left"> <asp:Label ID="lbTipoProductoHeaderRepeater" runat="server" Text="Tipo" CssClass="Letranegrita"></asp:Label> </th>
                        <th style="width:10%" align="left"> <asp:Label ID="lbCategoriaHeaderRepeater" runat="server" Text="Categoria" CssClass="Letranegrita"></asp:Label> </th>
                        <th style="width:10%" align="left"> <asp:Label ID="lbPrecioHeaderRepeater" runat="server" Text="Precio" CssClass="Letranegrita"></asp:Label> </th>
                        <th style="width:10%" align="left"> <asp:Label ID="lbCantidadHeaderRepeater" runat="server" Text="Cantidad" CssClass="Letranegrita"></asp:Label> </th>
                        <th style="width:10%" align="left"> <asp:Label ID="lbDescuentolHeaderRepeater" runat="server" Text="Descuento" CssClass="Letranegrita"></asp:Label> </th>
                        <th style="width:10%" align="left"> <asp:Label ID="lbTotalHeaderRepeater" runat="server" Text="Total" CssClass="Letranegrita"></asp:Label> </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rpListadoProductosFacturar" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td style="width:40%"> <%# Eval("") %> </td>
                                <td style="width:10%"> <%# Eval("") %> </td>
                                <td style="width:10%"> <%# Eval("") %> </td>
                                <td style="width:10%"> <%#string.Format("{0:n2}", Eval("")) %> </td>
                                <td style="width:10%"> <%#string.Format("{0:n0}", Eval("")) %> </td>
                                <td style="width:10%"> <%#string.Format("{0:n2}", Eval("")) %> </td>
                                <td style="width:10%"> <%#string.Format("{0:n2}", Eval("")) %> </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>

         <div align="center">
                <asp:Label ID="lbPaginaActualTituloProductosFacturar" runat="server" Text="Pagina " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbPaginaActualVariavleProductosFacturar" runat="server" Text=" 0 " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadPaginaTituloProductosFacturar" runat="server" Text=" DE " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadPaginaVAriableProductosFacturar" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
            </div>
             <div id="divPaginacionProductosFacturar" runat="server" align="center">
        <div style="margin-top: 20px;">
            <table style="width: 600px">
                <tr>
                    <td> <asp:LinkButton ID="LinkPrimeraPaginaProductosFacturar" runat="server" Text="Primero" CssClass="btn btn-outline-success btn-sm" ToolTip="Ir a la primera pagina del listado" OnClick="LinkPrimeraPaginaProductosFacturar_Click"></asp:LinkButton> </td>
                    <td> <asp:LinkButton ID="LinkAnteriorProductosFacturar" runat="server" Text="Anterior" CssClass="btn btn-outline-success btn-sm" ToolTip="Ir a la pagina anterior del listado" OnClick="LinkAnteriorProductosFacturar_Click"></asp:LinkButton> </td>
                    <td>
                        <asp:DataList ID="dtPaginacionProductosFacturar" runat="server" OnItemCommand="dtPaginacionProductosFacturar_ItemCommand" OnItemDataBound="dtPaginacionProductosFacturar_ItemDataBound" RepeatDirection="Horizontal">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkPaginacionCentralProductosFacturar" runat="server" CommandArgument='<%# Eval("IndicePagina") %>' CommandName="newPage" Text='<%# Eval("TextoPagina") %>' Width="20px"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:DataList>

                    </td>
                    <td> <asp:LinkButton ID="LinkSiguienteProductosFacturar" runat="server" Text="Siguiente" ToolTip="Ir a la siguiente pagina del listado" CssClass="btn btn-outline-success btn-sm" OnClick="LinkSiguienteProductosFacturar_Click"></asp:LinkButton> </td>
                    <td> <asp:LinkButton ID="LinkUltimoProductosFacturar" runat="server" Text="Ultimo" ToolTip="Ir a la ultima pagina del listado" CssClass="btn btn-outline-success btn-sm" OnClick="LinkUltimoProductosFacturar_Click"></asp:LinkButton> </td>
                </tr>
            </table>
        </div>
                 <br />
        </div>
    </div>
</asp:Content>
