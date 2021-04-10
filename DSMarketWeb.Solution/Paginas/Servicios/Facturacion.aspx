<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PaginaMaestra.Master"  AutoEventWireup="true" CodeBehind="Facturacion.aspx.cs" Inherits="DSMarketWeb.Solution.Paginas.Servicios.Facturacion" %>
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

        .BotonEspecoal2 {
           width:15%;
             font-weight:bold;
          }

         .TamanioImagen {
         width:20%;
         height:20%;
         }
    </style>

    <script type="text/javascript">
        function CantidadIgualCero() {
            alert("La cantidad a procesar no puede ser igual a cero, favor de verificar.");
        }
        function CantidadMenorCero() {
            alert("La cantidad que intentas procesar es menor a cero, favor de verificar.");
        }
        function CantidadNodisponible() {
            alert("La cantidad que intentas procesar supera la cantidad disponible en almacen, favor de verificar.");
        }
        function DescuentoMenorCero() {
            alert("El descuento aplicado no puede ser un numero menor a cero, favor de verificar.");
        }
        function DescuentoMayorDescuentoMaximo() {
            alert("El descuento no puede ser mayor al descuento maximo aplicado por el sistema, favor de verificar.");
        }
        function ItemYaAgregadoFactura() {
            alert("Este Item ya esta agregado para facturar, favor de verificar.");
        }

        function CLienteNoEncontrado() {
            alert("No se encontro ningun cliente con los parametros ingresados, favor de verificar e intentar nuevamente.");
        }
        function CamposVaciosAgregarItem() {
            alert("Has dejado campos vacios que son necesarios para realizar esta operación, favor de verificar.");
        }
        function SeleccionarItem() {
            alert("Favor de seleccionar un item para agregarlo a la factura.");
        }
        function ItemsNoEncontrados() {
            alert("No es posible proceder con este proceso, por que no se encontraron items para facturar, favor de agregar productos o servicios para proceder con el proceso.");
        }
        $(document).ready(function () {


            $("#<%=btnConsultarRegistros.ClientID%>").click(function () {
                var CodigoCliente = $("#<%=txtCodigoClienteBuscar.ClientID%>").val().length;
                var RNCedula = $("#<%=txtRNCCedulaCliente.ClientID%>").val().length;

                if (CodigoCliente < 1 && RNCedula < 1) {
                    alert("Los campos de consulta no pueden estar ambos vacios para buscar un cliente, favor de llevar uno e intentarlo nuevamente.");
                    $("#<%=txtCodigoClienteBuscar.ClientID%>").css("border-color", "red");
                    $("#<%=txtRNCCedulaCliente.ClientID%>").css("border-color", "red");
                    return false;
                }
            });

            $("#<%=btnCompletarOperacion.ClientID%>").click(function () {
                //VALIDAMOS EL CAMPO TIPO DE INGRESO
                var TipoIngreso = $("#<%=ddlSeleccionarTipoIngreso.ClientID%>").val();
                if (TipoIngreso < 1) {
                    alert("El campo tipo de ingreso no puede estar vacio para completar esta operación, favor de verificar.");
                    $("#<%=ddlSeleccionarTipoIngreso.ClientID%>").css("border-color", "red");
                    return false;
                }
                else {
                    //VALIDAMOS EL TOTAL DE PRODUCTOS
                    var TotalProductos = $("#<%=txtTotalProductosCalculos.ClientID%>").val().length;
                    if (TotalProductos < 1) {
                        alert("El campo Total de Productos no puede estar vacio para completar esta operación, favor de verificar.");
                        $("#<%=txtTotalProductosCalculos.ClientID%>").css("border-color", "red");
                        return false;
                    }
                    else {
                        //VALIDAMOS EL TOTAL DE SERVICIOS
                        var TotalServicios = $("#<%=txtCantidadServicios.ClientID%>").val().length;
                        if (TotalServicios < 1) {
                            alert("El campo total de servicios no puede estar vacio para completar esta operación, favor de verificar.");
                            $("#<%=txtCantidadServicios.ClientID%>").css("border-color", "red");
                            return false;
                        }
                        else {
                            //VALIDAMOS EL CAMPO DE AL CANTIDAD DE ITEMS AGREGADOS
                            var TotalItems = $("#<%=txtCantidadArticulos.ClientID%>").val().length;
                            if (TotalItems < 1) {
                                alert("El campo total de items no puede estar vacio para completar esta operación, favor de verificar.");
                                $("#<%=txtCantidadArticulos.ClientID%>").css("border-color", "red");
                                return false;
                            }
                            else {
                                //VALIDAMOS EL CAMPO TOTAL DE DESCUENTO
                                var ValidarTotalDescuento = $("#<%=txtTotalDescuento.ClientID%>").val().length;
                                if (ValidarTotalDescuento < 1) {
                                    alert("El campo Total de descuento no puede estar vacio para completar esta operación, favor de verificar.");
                                    $("#<%=txtTotalDescuento.ClientID%>").css("border-color", "red");
                                    return false;
                                }
                                else {
                                    //VALIDAMOS EL CAMPO SUB TOTAL
                                    var SubTotal = $("#<%=txtSubTotal.ClientID%>").val().length;
                                    if (SubTotal < 1) {
                                        alert("El campo Sub Total no puede estar vacio para completar esta operación, favor de verificar.");
                                        $("#<%=txtSubTotal.ClientID%>").css("border-color", "red");
                                        return false;
                                    }
                                    else {
                                        //VALIDAMOS EL IMPUESTO
                                        var Impuesto = $("#<%=txtImpuesto.ClientID%>").val().length;
                                        if (Impuesto < 1) {
                                            alert("El campo impuesto no puede estar vacio para completar esta operación, favor de verificar.");
                                            $("#<%=txtImpuesto.ClientID%>").css("border-color", "red");
                                            return false;
                                        }
                                        else {
                                            //VALIDAMOS EL IMPUESTO DE TIPO DE PAGO
                                            var ImpuestoTipoPago = $("#<%=txtImpuestoTipoPago.ClientID%>").val().length;
                                            if (ImpuestoTipoPago < 1) {
                                                alert("El campo Impuesto de tipo de pago no puede estar vacio para completar esta operación, favor de verificar.");
                                                $("#<%=txtImpuestoTipoPago.ClientID%>").css("border-color", "red");
                                                return false;
                                            }
                                            else {
                                                //VALIDAMOS EL CAMPO DE IMPUESTO DE COMPROBANTE
                                                var ImpuestoComprobante = $("#<%=txtImpuestoComprobante.ClientID%>").val().length;
                                                if (ImpuestoComprobante < 1) {
                                                    alert("El campo impuesto de comprobante no puede estar vacio para completar esta operación, favor de verificar.");
                                                    $("#<%=txtImpuestoComprobante.ClientID%>").css("border-color", "red");
                                                    return false;
                                                }
                                                else {
                                                    //VALIDAR EL CAMPO TOTAL 
                                                    var Total = $("#<%=txtTotal.ClientID%>").val().length;
                                                    if (Total < 1) {
                                                        alert("El campo total no puede estar vacio para completar esta operación, favor de verificar.");
                                                        $("#<%=txtTotal.ClientID%>").css("border-color", "red");
                                                        return false;
                                                    }
                                                    else {
                                                        //VALIDAMOS EL CAMPO MONTO PAGADO
                                                        var Montopagado = $("#<%=txtMontoPagar.ClientID%>").val().length;
                                                        if (Montopagado < 1) {
                                                            alert("El campo monto pagado no puede estar vacio para completar esta operación, favor de verificar.");
                                                            $("#<%=txtMontoPagar.ClientID%>").css("border-color", "red");
                                                            return false;
                                                        }
                                                        else {
                                                            //VALIDAMOS EL CAMPO CAMBIO
                                                            var Cambio = $("#<%=txtCambio.ClientID%>").val().length;
                                                            if (Cambio < 1) {
                                                                alert("El campo cambio no puede estar vacio para completar esta operación, favor de verificar.");
                                                                $("#<%=txtCambio.ClientID%>").css("border-color", "red");
                                                                return false;
                                                            }
                                                            else {
                                                                //VALIDAMOS EL CAMPO TIPO DE PAGO
                                                                var TipoPago = $("#<%=ddlTipoPago.ClientID%>").val();
                                                                if (TipoPago < 1) {
                                                                    alert("El campo tipo de pago no puede estar vacio para completar esta operación, favor de verificar.");
                                                                    $("#<%=ddlTipoPago.ClientID%>").css("border-color", "red");
                                                                    return false;
                                                                }
                                                                else {
                                                                    //VALIDAMOS EL CAMPO MONEDA
                                                                    var Moneda = $("#<%=ddlSeleccionarMoneda.ClientID%>").val();
                                                                    if (Moneda < 1) {
                                                                        alert("El campo moneda no pude estar vacio para completar esta operación, favor de verificar.");
                                                                        $("#<%=ddlSeleccionarMoneda.ClientID%>").css("border-color", "red");
                                                                        return false;
                                                                    }
                                                                    else {
                                                                        //VALIDAMOS EL CAMPO CAMBIO 
                                                                        var Cambio = $("#<%=txtTasaCambioCalculos.ClientID%>").val().length;
                                                                        if (Cambio < 1) {
                                                                            alert("El campo cambio no puede estar vacio para completar esta operación, favor de verificar.");
                                                                            $("#<%=txtTasaCambioCalculos.ClientID%>").css("border-color", "red");
                                                                            return false;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }


                    }
                }
            });
        })
    </script>

        <asp:Label ID="lbCodigoUsuarioConectado" runat="server" Text="Codigo de Usuario" Visible="false"></asp:Label>
        <asp:Label ID="lbNumeroConector" runat="server" Text="Numero de Conector" Visible="false"></asp:Label>
        <asp:Label ID="lbIdProductoSeleccionado" runat="server" Text="IdProductoSeleccionado" Visible="false"></asp:Label>
        <asp:Label ID="lbNumeroConectorProductoSeleccionado" runat="server" Text="IdProductoSeleccionado" Visible="false"></asp:Label>
        <asp:Label ID="lbCodigoClienteSeleccionado" runat="server" Text="Codigo de Cliente" Visible="false"></asp:Label>
        <asp:Label ID="lbLimiteCreditoClienteSeleccionado" runat="server" Text="LimiteCredito" Visible="false"></asp:Label>
        <asp:Label ID="lbNumeroConectorFacturacion" runat="server" Text="NumeroConector" Visible="false"></asp:Label>
    <br /><br />

    <div class="container-fluid" id="DivBloqueCheckRadios" runat="server">
        
         <div class="form-check-inline">
            <div class="form-group form-check">
                <asp:Label ID="lbTipoVenta" runat="server" Text="Tipo de Venta: " CssClass="Letranegrita"></asp:Label>
                <asp:RadioButton ID="rbContado" runat="server" Text="Contado" CssClass="form-check-input" GroupName="TipoVenta" ToolTip="Realizar Venta a Contado" />
                <asp:RadioButton ID="rbCredito" runat="server" Text="Credito" Enabled="false" CssClass="form-check-input" GroupName="TipoVenta" ToolTip="Realizar Venta a Credito" />
            </div>
        </div>
        <br />
        <div class="form-check-inline">
            <div class="form-group form-check">
                <asp:CheckBox ID="cbAgregarComprobante" runat="server" Text="Agregar Comprobante" CssClass="form-check-input" AutoPostBack="true" OnCheckedChanged="cbAgregarComprobante_CheckedChanged" ToolTip="Agregar Comprobante Fiscal a la Facturación" />
                <asp:CheckBox ID="cbBuscarCliente" runat="server" Text="Buscar Clientes" CssClass="form-check-input" AutoPostBack="true" OnCheckedChanged="cbBuscarCliente_CheckedChanged" ToolTip="Buscar Clientes para facturar" />
            </div>
        </div>
    </div>

    
    <!--ESTE BLOQUE ES PARA BUSCAR CLIENTES REGISTRADOS EN LA BASE DE DATOS-->
    <div id="DivBloqueBuscarClientes" runat="server" visible="false" >
          <div class="form-row" >
            <div class="form-group col-md-6">
                <asp:Label ID="lbCodigoClienteBuscar" runat="server" Text="Codigo de Cliente" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtCodigoClienteBuscar" MaxLength="11" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
            </div>
            <div class="form-group col-md-6">
                <asp:Label ID="lbRNCCedulaCLiente" runat="server" Text="RNC / Cedula" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtRNCCedulaCliente" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
            <div align="center">
                <asp:Button ID="btnConsultarRegistros" runat="server" Text="Buscar" ToolTip="Buscar Cliente" CssClass="btn btn-lg btn-primary btn-sm" OnClick="btnConsultarRegistros_Click" />
            </div>
            <hr />

    </div>

   

    <!--ESTE BLOQUE ES PARA LLENAR LA INFORMACION DEL CLIENTE AL QUE SE LE ESTA FACTURANDO -->
    <div id="DivBloqueInformacionCliente" runat="server" visible="false">
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
             <div class="form-group col-md-1">
                <asp:Label ID="lblCodigoCliente" runat="server" Text="Codigo" CssClass="Letranegrita"></asp:Label>
                 <asp:TextBox ID="txtCodigoClienteSeleccionado" runat="server" Enabled="false" CssClass="form-control" AutoCompleteType="Disabled" MaxLength="8000"></asp:TextBox>
            </div>
             <div class="form-group col-md-3">
                <asp:Label ID="lbLimiteCredito" runat="server" Text="Limite de Credito" CssClass="Letranegrita"></asp:Label>
                 <asp:TextBox ID="txtLimiteCreditoClienteSeleccionado" runat="server" Enabled="false" CssClass="form-control" AutoCompleteType="Disabled" MaxLength="8000"></asp:TextBox>
            </div>
        </div>

    </div>

     <!--ESTE BLOQUE ES PARA QUITAR LOS CLIENTES CONSULTADOS EN LA BASE DE DATOS Y DESBLOQUEAR CONTROLES -->
    <div id="DivBloqueQuitarClientes" runat="server" visible="false">

          <div align="center" runat="server">
            <asp:Button ID="btnQuitar" runat="server" Text="Quitar" ToolTip="Quitar Cliente Seleccionado" OnClick="btnQuitar_Click" CssClass="btn btn-danger btn-sm" />
        </div>

      
        <br />

    </div>

    <!--ESTE BLOQUE ES PARA AGREGAR Y QUITAR ITEMS DE LA FACTURA -->
    <div id="DivInformacionITEM" runat="server" visible="false">
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
                             <asp:Button ID="btnBuscarProducto" runat="server" Text="Buscar" CssClass="btn btn-lg btn-primary btn-sm" ToolTip="Buscar Producto" OnClick="btnBuscarProducto_Click" />
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
                                          <th style="width:10%" align="left"> Seleccionar </th>
                                          <th style="width:10%" align="left"> Tipo </th>
                                          <th style="width:60%" align="left"> Producto </th>
                                          <th style="width:10%" align="left"> Cantidad </th>
                                          <th style="width:10%" align="left"> Precio </th>
                                     </tr>
                                 </thead>
                                 <tbody>
                                     <asp:Repeater ID="rpListadoProductosAgregar" runat="server">
                                         <ItemTemplate>
                                             <tr>
                                                 <asp:HiddenField ID="hfIdProductoAgregar" runat="server" Value='<%# Eval("IdProducto") %>' />
                                                  <asp:HiddenField ID="hfNumeroConectorProductoAgregar" runat="server" Value='<%# Eval("NumeroConector") %>' />

                                                 <td style="width:10%"> <asp:Button ID="btnSeleccionarProductoAgregar" runat="server" Text="Seleccionar" ToolTip="Seleccionar Registro para agregarlo a factura" CssClass="btn btn-outline-primary btn-sm" OnClick="btnSeleccionarProductoAgregar_Click" /> </td>
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
                <asp:Label ID="lbCantidadPaginaTituloProductoAgregar" runat="server" Text=" De " CssClass="Letranegrita"></asp:Label>
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
                                 <asp:TextBox ID="txtCantidadUsarVistaPrevia" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtCantidadUsarVistaPrevia_TextChanged" TextMode="Number"></asp:TextBox>
                             </div>

                              <div class="form-group col-md-3">
                                 <asp:Label ID="lbPorcientoDescuentoVistaPrevia" runat="server" Text="% de Descuento" CssClass="Letranegrita"></asp:Label>
                                 <asp:TextBox ID="txtPorcientoDescuentoVistaPrevia" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                             </div>

                             <div class="form-group col-md-3" runat="server" id="DivDescuento" >
                                 <asp:Label ID="lbDescuentoVistaPrevia" runat="server" Text="Descuento" CssClass="Letranegrita"></asp:Label>
                                 <asp:TextBox ID="txtDescuentoVistaPrevia" runat="server" CssClass="form-control" TextMode="Number" step="0.01"></asp:TextBox>
                             </div>

                             <div class="form-group col-md-3">
                                 <asp:Label ID="lbDescuentoMaximoVistaPrevia" runat="server" Text="Descuento Maximo" CssClass="Letranegrita"></asp:Label>
                                 <asp:TextBox ID="txtDescuentoMaximoVistaPrevia" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                             </div>

                              <div class="form-group col-md-3">
                                 <asp:Label ID="lbImpuestoVistaPrevia" runat="server" Text="Impuesto" CssClass="Letranegrita"></asp:Label>
                                 <asp:TextBox ID="txtImpuestoVistaPrevia" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                             </div>
                         </div>

             
                         <div align="center">
                          
                             <asp:Button ID="btnAgregarRegistro" runat="server" Text="Agregar" ToolTip="Agregar Registro" CssClass="btn btn-lg btn-primary btn-sm" OnClick="btnAgregarRegistro_Click" />
                             <asp:Button ID="btnRestablecerVistaPrevia" runat="server" Text="Volver" ToolTip="Volver a la Pantalla Principal" CssClass="btn btn-lg btn-primary btn-sm" OnClick="btnRestablecerVistaPrevia_Click" />
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
                                     <th style="width:10%" align="left"> QUITAR </th>
                                     <th style="width:30%" align="left"> PRODUCTO </th>
                                         <th style="width:20%" align="left"> GARANTIA </th>
                                     <th style="width:10%" align="left"> PRECIO </th>
                                     <th style="width:10%" align="left"> DESCUENTO </th>
                                     <th style="width:10%" align="left"> CANTIDAD </th>
                                     <th style="width:10%" align="left"> TOTAL </th>
                                 </tr>
                               </thead>
                                 <tbody>
                                     <asp:Repeater ID="rpListadoProductosAgregados" runat="server">
                                         <ItemTemplate>
                                             <tr>
                                                 <asp:HiddenField ID="hfNumeroRegistroItemAgregado" runat="server" Value='<%# Eval("NumeroRegistro") %>' />
                                                 <asp:HiddenField ID="hfNumeroConectorItemAgregado" runat="server" Value='<%# Eval("NumerodeConector") %>' />
                                                 <asp:HiddenField ID="hfIdProductoRespaldo" runat="server" Value='<%# Eval("IdProductoRespaldo") %>' />
                                                 <asp:HiddenField ID="hfNumeroConectorRespaldo" runat="server" Value='<%# Eval("NumeroConectorRespaldo") %>' />
                                                


                                                 <td style="width:10%"> <asp:Button ID="btnSeleccionarRegistrosAgregadosHeaderRepeater" runat="server" Text="Quitar" ToolTip="Seleccionar Registros Agregados" OnClick="btnSeleccionarRegistrosAgregadosHeaderRepeater_Click" CssClass="btn btn-outline-secondary btn-sm" /> </td>
                                                 <td style="width:30%"> <%# Eval("Producto") %> </td>
                                                 <td style="width:20%"> <%# Eval("GarantiaProducto") %> </td>
                                                 <td style="width:10%"> <%#string.Format("{0:n2}", Eval("Precio")) %> </td>
                                                 <td style="width:10%"> <%#string.Format("{0:n2}", Eval("Descuento")) %> </td>
                                                 <td style="width:10%"> <%#string.Format("{0:n0}", Eval("Cantidad")) %> </td>
                                                 <td style="width:10%"> <%#string.Format("{0:n2}", Eval("Total")) %> </td>
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
        <br />
    </div>

    <!--ESTE BLOQUE ES PARA REALIZAR LOS CALCULOS QUE VAN PARA LA FACTURA -->
    <div id="DivBloqueCalculos" runat="server" visible="false">
        <div align="center">
            <asp:Button ID="btnAgregarItems" runat="server" Text="AGREGAR O QUITAR PRODUCTOS / SERVICIOS" CssClass="btn btn-outline-secondary btn-sm BotonEspecoal" ToolTip="Agregar / Quitar Items a la factura" OnClick="btnAgregarItems_Click" />
        </div>
        <br />
        <div class="form-row">
          
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
                <asp:TextBox ID="txtMontoPagar" runat="server" Enabled="false" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtMontoPagar_TextChanged"></asp:TextBox>
            </div>

             <div class="form-group col-md-4">
                <asp:Label ID="lbCambio" runat="server" Text="Cambio" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtCambio" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>

             <div class="form-group col-md-4">
                <asp:Label ID="lbTipoPago" runat="server" Text="Tipo de Pago" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlTipoPago" runat="server" ToolTip="Seleccionar el Tipo de Pago" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoPago_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbMoneda" runat="server" Text="Seleccionar Moneda" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarMoneda" runat="server" ToolTip="Seleccionar la moneda" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarMoneda_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
            </div>

             <div class="form-group col-md-4">
                <asp:Label ID="lbTasaCambioCalculos" runat="server" Text="Tasa Cambio" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtTasaCambioCalculos" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
         <br />
        <div align="center">
            <asp:Button ID="btnRefrescarCalculos" runat="server" Text="Actualizar" ToolTip="Actualizar Calclos" CssClass="btn btn-lg btn-primary btn-sm" OnClick="btnRefrescarCalculos_Click" />
            <asp:Button ID="btnCompletarOperacion" runat="server" Text="Completar" ToolTip="Completar Operación" CssClass="btn btn-success btn-sm" OnClick="btnCompletarOperacion_Click" />
            <asp:Button ID="brnCancelarFacturacion" runat="server" Text="Cancelar" ToolTip="Cancelar Operación" CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('¿Quieres Cancelar esta operación?');" OnClick="brnCancelarFacturacion_Click" />
        </div>
        <br />

        <div class="table-responsive">
            <table class="table table-hover">
                <thead>
                    <tr>
                        <th style="width:10%" align="left"> QUITAR </th>
                        <th style="width:30%" align="left"> PRODUCTO </th>
                        <th style="width:20%" align="left"> GARANTIA </th>
                        <th style="width:10%" align="left"> PRECIO </th>
                        <th style="width:10%" align="left"> DESCUENTO </th>
                        <th style="width:10%" align="left"> CANTIDAD </th>
                        <th style="width:10%" align="left"> TOTAL </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rpListadoProductosFacturarPrincipal" runat="server">
                        <ItemTemplate>
                            <tr>
                                                 <asp:HiddenField ID="hfNumeroRegistroItemAgregadoPrincipal" runat="server" Value='<%# Eval("NumeroRegistro") %>' />
                                                 <asp:HiddenField ID="hfNumeroConectorItemAgregadoPrincipal" runat="server" Value='<%# Eval("NumerodeConector") %>' />
                                                 <asp:HiddenField ID="hfIdProductoRespaldoPrincipal" runat="server" Value='<%# Eval("IdProductoRespaldo") %>' />
                                                 <asp:HiddenField ID="hfNumeroConectorRespaldoPrincipal" runat="server" Value='<%# Eval("NumeroConectorRespaldo") %>' />

                                                 <td style="width:10%"> <asp:Button ID="btnQuitarItemFacturaPrincipal" runat="server" Text="Quitar" ToolTip="Quitar Items Agregados" OnClick="btnQuitarItemFacturaPrincipal_Click" CssClass="btn btn-outline-secondary btn-sm" /> </td>
                                                 <td style="width:30%"> <%# Eval("Producto") %> </td>
                                                 <td style="width:20%"> <%# Eval("GarantiaProducto") %> </td>
                                                 <td style="width:10%"> <%#string.Format("{0:n2}", Eval("Precio")) %> </td>
                                                 <td style="width:10%"> <%#string.Format("{0:n2}", Eval("Descuento")) %> </td>
                                                 <td style="width:10%"> <%#string.Format("{0:n0}", Eval("Cantidad")) %> </td>
                                                 <td style="width:10%"> <%#string.Format("{0:n2}", Eval("Total")) %> </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>

         <div align="center">
                <asp:Label ID="lbPaginaActualTituloProductosFacturarPrincipal" runat="server" Text="Pagina " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbPaginaActualVariavleProductosFacturarPrincipal" runat="server" Text=" 0 " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadPaginaTituloProductosFacturarPrincipal" runat="server" Text=" DE " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadPaginaVAriableProductosFacturarPrincipal" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
            </div>
         <div id="divPaginacionProductosFacturar" runat="server" align="center">
        <div style="margin-top: 20px;">
            <table style="width: 600px">
                <tr>
                    <td> <asp:LinkButton ID="LinkPrimeraPaginaProductosFacturarPrincipal" runat="server" Text="Primero" CssClass="btn btn-outline-success btn-sm" ToolTip="Ir a la primera pagina del listado" OnClick="LinkPrimeraPaginaProductosFacturar_Click"></asp:LinkButton> </td>
                    <td> <asp:LinkButton ID="LinkAnteriorProductosFacturarPrincipal" runat="server" Text="Anterior" CssClass="btn btn-outline-success btn-sm" ToolTip="Ir a la pagina anterior del listado" OnClick="LinkAnteriorProductosFacturar_Click"></asp:LinkButton> </td>
                    <td>
                        <asp:DataList ID="dtPaginacionProductosFacturarPrincipal" runat="server" OnItemCommand="dtPaginacionProductosFacturar_ItemCommand" OnItemDataBound="dtPaginacionProductosFacturar_ItemDataBound" RepeatDirection="Horizontal">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkPaginacionCentralProductosFacturarPrincipal" runat="server" CommandArgument='<%# Eval("IndicePagina") %>' CommandName="newPage" Text='<%# Eval("TextoPagina") %>' Width="20px"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:DataList>

                    </td>
                    <td> <asp:LinkButton ID="LinkSiguienteProductosFacturarPrincipal" runat="server" Text="Siguiente" ToolTip="Ir a la siguiente pagina del listado" CssClass="btn btn-outline-success btn-sm" OnClick="LinkSiguienteProductosFacturar_Click"></asp:LinkButton> </td>
                    <td> <asp:LinkButton ID="LinkUltimoProductosFacturarPrincipal" runat="server" Text="Ultimo" ToolTip="Ir a la ultima pagina del listado" CssClass="btn btn-outline-success btn-sm" OnClick="LinkUltimoProductosFacturar_Click"></asp:LinkButton> </td>
                </tr>
            </table>
        </div>
                 <br />
        </div>
    </div>

    <!--ESTE BLOQUE SE MUESTRA AL MOMENTO COMPLETAR EL PROCESO DE FACTURACION Y DE DONDE SE PUEDE DESCARGAR LA FACTURA -->
    <div id="DivBloqueProcesoCompletado" runat="server" visible="false">
        <div align="center">
            <asp:Image ID="IMGFotoProcesoCompletado" CssClass="TamanioImagen" runat="server" ImageUrl="~/Recursos/tenor.gif" />
            <br />
            <asp:Label ID="lbLetreroProcesoCompletado" runat="server" Text="PROCESO COMPLETADO CON EXITO" CssClass="Letranegrita TamanioImagen"></asp:Label>
            <br />
            <asp:Label ID="lbNumeroFactura" runat="server" Text="Numero de Factura: " CssClass="Letranegrita TamanioImagen"></asp:Label>
            <asp:Label ID="lbNumeroFacturaVariable" runat="server" Text=" 0 " CssClass="Letranegrita TamanioImagen"></asp:Label>
        </div>
        <br />
         <div align="center">
             <asp:Button ID="btnDescargarFactura" runat="server" Text="Descrgar Factura" CssClass="btn btn-lg btn-primary btn-sm BotonEspecoal2" ToolTip="Descargar la Factura" OnClick="btnDescargarFactura_Click" />
             <asp:Button ID="btnImprimirFactura" runat="server" Text="Imprimir Directo" CssClass="btn btn-lg btn-primary btn-sm BotonEspecoal2" ToolTip="Imprimir directo a la Impresora predeterminada" OnClick="btnImprimirFactura_Click" />
             <asp:Button ID="btnNuevoProceso" runat="server" Text="Nuevo proceso" CssClass="btn btn-lg btn-primary btn-sm BotonEspecoal2" ToolTip="Crear Nueva Factura" OnClick="btnNuevoProceso_Click" />
        </div>
        <br />
    </div>


 <%--    <br />
        <div align="center">
             <button type="button" id="btnOtrosFiltros" class="btn btn-outline-secondary btn-sm BotonEspecoal" data-toggle="modal" data-target=".OtrosFiltros">PRODUCTOS / SERVICIOS</button>
        </div>

        <div class="modal fade bd-example-modal-xl OtrosFiltros" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-xl" role="document">
    <div class="modal-content">

        <asp:ScriptManager ID="ScripManagerProductosServicios" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanelProductoServicios" runat="server">
            <ContentTemplate>
          <div class="container-fluid">
                
          </div>

            </ContentTemplate>
        </asp:UpdatePanel>
      
        <br />
    </div>
  </div>
</div>--%>
</asp:Content>
