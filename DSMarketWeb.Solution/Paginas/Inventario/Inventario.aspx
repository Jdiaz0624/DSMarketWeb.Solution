<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="Inventario.aspx.cs" Inherits="DSMarketWeb.Solution.Paginas.Inventario.Inventario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <style type="text/css">

        .btn-sm{
            width:90px;
        }

        .Letranegrita {
        font-weight:bold;
        }

         table {
            border-collapse: collapse;
        }
        
          .BotonEspecoal {
           width:100%;
             font-weight:bold;
          }

        th {
            background-color: dodgerblue;
            color: white;
        }
    </style>
    <script type="text/javascript">
        function CampoReferenciaEncontrado() { 
            alert("El campo referencia ingresado no es valido, favor de verificar.");
        }



        $(document).ready(function () {
            //VALIDAMOS LOS CAMPOS DEL BOTON GUARDAR
            $("#<%=btnGuardarRegistroMantenimientio.ClientID%>").click(function () {
                var TipoProducto = $("#<%=ddlSeleccionarTipoPrductoMantenimieto.ClientID%>").val();
                if (TipoProducto < 1) {
                    alert("El campo Tipo de Producto no puede estar vacio para guardar este registro, favor de verificar.");
                    $("#<%=ddlSeleccionarTipoPrductoMantenimieto.ClientID%>").css("border-color", "red");
                    return false
                }
                else {
                    var Categoria = $("#<%=ddlSeleccionarCategoriaMantenimiento.ClientID%>").val();
                    if (Categoria < 1) {
                        alert("El campo categoria no puede estar vacio para guardar este registro, favor de verificar.");
                        $("#<%=ddlSeleccionarCategoriaMantenimiento.ClientID%>").css("border-color", "red");
                        return false;
                    }
                    else {
                        var Marca = $("#<%=ddlSeleccionarMarcaMantenimiento.ClientID%>").val();
                        if (Marca < 1) {
                            alert("El campo marca no puede estar vacio para guardar este registro, favor de verificar.");
                            $("#<%=ddlSeleccionarMarcaMantenimiento.ClientID%>").css("border-color", "red");
                            return false;
                        }
                        else {
                            var TipoSuplidor = $("#<%=ddlSeleccionarTipoSuplidorMantenimiento.ClientID%>").val();
                            if (TipoSuplidor < 1) {
                                alert("El campo tipo de suplidor no puede estar vacio para guardar este registro, favor de verificar.");
                                $("#<%=ddlSeleccionarTipoSuplidorMantenimiento.ClientID%>").css("border-color", "red");
                                return false;
                            }
                            else {
                                var Suplidor = $("#<%=ddlSeleccionarSuplidorMantenimiento.ClientID%>").val();
                                if (Suplidor < 1) {
                                    alert("El campo suplidor no puede estar vacio para guardar este registro, favor de veriricar.");
                                    $("#<%=ddlSeleccionarSuplidorMantenimiento.ClientID%>").css("border-color", "red");
                                    return false;
                                }
                                else {
                                    var PrecioCompra = $("#<%=txtPrecioCompraMantenimiento.ClientID%>").val().length;
                                    if (PrecioCompra < 1) {
                                        alert("El campo precio de compra no puede estar vacio para guardar este registro, favor de verificar.");
                                        $("#<%=txtPrecioCompraMantenimiento.ClientID%>").css("border-color", "red")
                                        return false;
                                    }
                                    else {
                                        var PrecioVenta = $("#<%=txtPrecioVentaMantenimiento.ClientID%>").val().length;
                                        if (PrecioVenta < 1) {
                                            alert("El campo precio de venta no puede estar vacio para guardar este registro, favor de verificar.");
                                            $("#<%=txtPrecioVentaMantenimiento.ClientID%>").css("border-color", "red");
                                            return false;
                                        }
                                        else {
                                            var Stock = $("#<%=txtStockMantenimiento.ClientID%>").val().length;
                                            if (Stock < 1) {
                                                alert("El campo Stock no puede estar vacio para guardar este registro, favor de verificar.");
                                                $("#<%=txtStockMantenimiento.ClientID%>").css("border-color", "red");
                                                return false;
                                            }
                                            else {
                                                var StockMinimo = $("#<%=txtStockMinimoMantenimiento.ClientID%>").val().length;
                                                if (StockMinimo < 1) {
                                                    alert("El campo stock minimo no puede estar vacio para guardar este registro, favor de verificar.");
                                                    $("#<%=txtStockMinimoMantenimiento.ClientID%>").css("border-color", "red");
                                                    return false;
                                                }
                                                else {
                                                    var TipoGarantia = $("#<%=ddlSeleccionarTipoGarantiaMantenimiento.ClientID%>").val();
                                                    if (TipoGarantia < 1) {
                                                        alert("El campo Tipo de garantia no puede estar vacio para guardar ese registro, favor de verificar.");
                                                        $("#<%=ddlSeleccionarTipoGarantiaMantenimiento.ClientID%>").css("border-color", "red");
                                                        return false;
                                                    }
                                                    else {
                                                        var TipoTiempoGarantia = $("#<%=txtTiempoGarantiaMantenimiento.ClientID%>").val().length;
                                                        if (TipoTiempoGarantia < 1) {
                                                            alert("El campo tipo de tiempo de garantia no puede estar vacio para guardar este registro, favor de verificar.");
                                                            $("#<%=txtTiempoGarantiaMantenimiento.ClientID%>").css("border-color", "red");
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
            });


            //VALIDAMOS LOS CAMPOS DEL BOTON MODIFICAR
            $("#<%=btnEditarRegistroMantenimiento.ClientID%>").click(function () {
                var TipoProducto = $("#<%=ddlSeleccionarTipoPrductoMantenimieto.ClientID%>").val();
                   if (TipoProducto < 1) {
                       alert("El campo Tipo de Producto no puede estar vacio para modificar este registro, favor de verificar.");
                       $("#<%=ddlSeleccionarTipoPrductoMantenimieto.ClientID%>").css("border-color", "red");
                    return false
                }
                else {
                    var Categoria = $("#<%=ddlSeleccionarCategoriaMantenimiento.ClientID%>").val();
                    if (Categoria < 1) {
                        alert("El campo categoria no puede estar vacio para modificar este registro, favor de verificar.");
                        $("#<%=ddlSeleccionarCategoriaMantenimiento.ClientID%>").css("border-color", "red");
                        return false;
                    }
                    else {
                        var Marca = $("#<%=ddlSeleccionarMarcaMantenimiento.ClientID%>").val();
                        if (Marca < 1) {
                            alert("El campo marca no puede estar vacio para modificar este registro, favor de verificar.");
                            $("#<%=ddlSeleccionarMarcaMantenimiento.ClientID%>").css("border-color", "red");
                            return false;
                        }
                        else {
                            var TipoSuplidor = $("#<%=ddlSeleccionarTipoSuplidorMantenimiento.ClientID%>").val();
                            if (TipoSuplidor < 1) {
                                alert("El campo tipo de suplidor no puede estar vacio para modificar este registro, favor de verificar.");
                                $("#<%=ddlSeleccionarTipoSuplidorMantenimiento.ClientID%>").css("border-color", "red");
                                return false;
                            }
                            else {
                                var Suplidor = $("#<%=ddlSeleccionarSuplidorMantenimiento.ClientID%>").val();
                                if (Suplidor < 1) {
                                    alert("El campo suplidor no puede estar vacio para modificar este registro, favor de veriricar.");
                                    $("#<%=ddlSeleccionarSuplidorMantenimiento.ClientID%>").css("border-color", "red");
                                    return false;
                                }
                                else {
                                    var PrecioCompra = $("#<%=txtPrecioCompraMantenimiento.ClientID%>").val().length;
                                    if (PrecioCompra < 1) {
                                        alert("El campo precio de compra no puede estar vacio para modificar este registro, favor de verificar.");
                                        $("#<%=txtPrecioCompraMantenimiento.ClientID%>").css("border-color", "red")
                                        return false;
                                    }
                                    else {
                                        var PrecioVenta = $("#<%=txtPrecioVentaMantenimiento.ClientID%>").val().length;
                                        if (PrecioVenta < 1) {
                                            alert("El campo precio de venta no puede estar vacio para modificar este registro, favor de verificar.");
                                            $("#<%=txtPrecioVentaMantenimiento.ClientID%>").css("border-color", "red");
                                            return false;
                                        }
                                        else {
                                            var Stock = $("#<%=txtStockMantenimiento.ClientID%>").val().length;
                                            if (Stock < 1) {
                                                alert("El campo Stock no puede estar vacio para modificar este registro, favor de verificar.");
                                                $("#<%=txtStockMantenimiento.ClientID%>").css("border-color", "red");
                                                return false;
                                            }
                                            else {
                                                var StockMinimo = $("#<%=txtStockMinimoMantenimiento.ClientID%>").val().length;
                                                if (StockMinimo < 1) {
                                                    alert("El campo stock minimo no puede estar vacio para modificar este registro, favor de verificar.");
                                                    $("#<%=txtStockMinimoMantenimiento.ClientID%>").css("border-color", "red");
                                                    return false;
                                                }
                                                else {
                                                    var TipoGarantia = $("#<%=ddlSeleccionarTipoGarantiaMantenimiento.ClientID%>").val();
                                                    if (TipoGarantia < 1) {
                                                        alert("El campo Tipo de garantia no puede estar vacio para modificar ese registro, favor de verificar.");
                                                        $("#<%=ddlSeleccionarTipoGarantiaMantenimiento.ClientID%>").css("border-color", "red");
                                                        return false;
                                                    }
                                                    else {
                                                        var TipoTiempoGarantia = $("#<%=txtTiempoGarantiaMantenimiento.ClientID%>").val().length;
                                                        if (TipoTiempoGarantia < 1) {
                                                            alert("El campo tipo de tiempo de garantia no puede estar vacio para modificar este registro, favor de verificar.");
                                                               $("#<%=txtTiempoGarantiaMantenimiento.ClientID%>").css("border-color", "red");
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
               });
        })
    </script>


    <br /><br />

    <div id="IdBloqueConsulta" runat="server">
        <asp:Label ID="lbIdRegistro" runat="server" Text="IdRegistro" Visible="false"></asp:Label>
        <asp:Label ID="lbNumeroConector" runat="server" Text="NumeroConector" Visible="false"></asp:Label>
        <div class="form-check-inline">
            <div class="form-group form-check">
                <asp:CheckBox ID="cbAgregarRangoFecha" runat="server" Text="Agregar Rango de Fecha" CssClass="form-check-input" AutoPostBack="true" OnCheckedChanged="cbAgregarRangoFecha_CheckedChanged" />
             <asp:CheckBox ID="cbMostrarProductosAgotados" runat="server" Text="Mostrar Productos Agotados" CssClass="form-check-input"/>
            </div>
        </div>
        <div class="form-row">
            <div class="form-group col-md-4">
                <asp:Label ID="lbSeleccionarTipoProductoConsulta" runat="server" Text="Tipo de Producto" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarTipoProductoConsulta" runat="server" ToolTip="Seleccionar el tipo de producto para consultar" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarTipoProductoConsulta_SelectedIndexChanged"></asp:DropDownList>
            </div>

             <div class="form-group col-md-4">
                <asp:Label ID="lbSeleccionarCtagoriaConsulta" runat="server" Text="Categoria" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarCategoriaConsulta" runat="server" ToolTip="Seleccionar la categoria para consultar" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarCategoriaConsulta_SelectedIndexChanged"></asp:DropDownList>
            </div>

             <div class="form-group col-md-4">
                <asp:Label ID="lbSeleccionarMarcaConsulta" runat="server" Text="Marca" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarMarcaConsulta" runat="server" ToolTip="Seleccionar la marca para consultar" CssClass="form-control"></asp:DropDownList>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbDescripcionConsulta" runat="server" Text="Descripción" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtDescripcionConsulta" runat="server" MaxLength="200" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtDescripcionConsulta_TextChanged"></asp:TextBox>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbCodigoBarraConsulta" runat="server" Text="Codigo de Barra" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtCodigoBarraConsulta" runat="server" MaxLength="50" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtCodigoBarraConsulta_TextChanged"></asp:TextBox>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbReferenciaConsuklta" runat="server" Text="Referencia" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtReferenciaConsulta" runat="server" MaxLength="200" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtReferenciaConsulta_TextChanged"></asp:TextBox>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbCodigoProductoConsulta" runat="server" Text="Codigo de Producto" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtCodigoProductoConulta" runat="server" MaxLength="50" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtCodigoProductoConulta_TextChanged"></asp:TextBox>
            </div>

            <div class="form-group col-md-4" id="DivFechaDesde" runat="server">
                <asp:Label ID="lbFechaDesdeConsulta" runat="server" Text="Fecha Desde" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtFechaDesdeConsulta" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group col-md-4" id="DivFechaHasta" runat="server">
                <asp:Label ID="lbFechaHastaConsulta" runat="server" Text="Fecha Hasta" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtFechaHastaConsulta" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <br />
        <div class="form-check-inline">
            <div class="form-group form-check">
                <asp:Label ID="lbGenerarReporteA" runat="server" Text="Generar Reporte A: " CssClass="Letranegrita"></asp:Label>
                <asp:RadioButton ID="rbPDF" runat="server" Text="PDF" CssClass="form-check-input" GroupName="Reporte" ToolTip="Generar Reporte en PDF" />
                <asp:RadioButton ID="rbExcel" runat="server" Text="Excel" CssClass="form-check-input" GroupName="Reporte" ToolTip="Generar Reporte en Excel" />
                <asp:RadioButton ID="rbWord" runat="server" Text="Word" CssClass="form-check-input" GroupName="Reporte" ToolTip="Generar Reporte en Word" />
            </div>
        </div>
        <div align="center">
            <asp:Button ID="btnConsultarRegistros" runat="server" Text="Consultar" ToolTip="Consultar Registros" CssClass="btn btn-primary btn-sm" OnClick="btnConsultarRegistros_Click" />
            <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" ToolTip="Crear Nuevos Registros" CssClass="btn btn-primary btn-sm" OnClick="btnNuevo_Click" />
            <asp:Button ID="btnEditar" runat="server" Text="Editar" ToolTip="Modificar Registro Seleccionado" CssClass="btn btn-primary btn-sm" OnClick="btnEditar_Click" />
            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" ToolTip="Eliminar Registro Seleccionado" CssClass="btn btn-danger btn-sm" OnClick="btnEliminar_Click" />
            <button type="button" id="btnSuplirItem" runat="server" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".SuplirsacarProductos">Suplir</button>
            <asp:Button ID="btnReporte" runat="server" Text="Reporte" ToolTip="Reporte de Inventario" CssClass="btn btn-primary btn-sm" OnClick="btnReporte_Click" />
            <asp:Button ID="btnRestablecer" runat="server" Text="Restablecer" ToolTip="Restablecer Pantalla" CssClass="btn btn-primary btn-sm" OnClick="btnRestablecer_Click" />
            <br /><br />
        <!--ESTADISTICA DE PANTALLA-->
        <asp:Label ID="lbCantidadRegistrosTitulo" runat="server" Text="Cantidad de Registros ( " CssClass="Letranegrita"></asp:Label>
        <asp:Label ID="lbCantidadRegistrosVariable" runat="server" Text=" 0 " CssClass="Letranegrita"></asp:Label>
        <asp:Label ID="lbCantidadRegistrosCerrar" runat="server" Text=" )" CssClass="Letranegrita"></asp:Label>

        <asp:Label ID="lbCapitalnvertidoTitulo" runat="server" Text="Capital Invertido ( " CssClass="Letranegrita"></asp:Label>
        <asp:Label ID="lbCapitalInvertidovariable" runat="server" Text=" 0 " CssClass="Letranegrita"></asp:Label>
        <asp:Label ID="lbCapitalInvertidocerrar" runat="server" Text=" )" CssClass="Letranegrita"></asp:Label>

        <asp:Label ID="lbGananciaAproximadaTitulo" runat="server" Text="Ganancia Aproximada ( " CssClass="Letranegrita"></asp:Label>
        <asp:Label ID="lbGananciaAproximadaVariable" runat="server" Text=" 0 " CssClass="Letranegrita"></asp:Label>
        <asp:Label ID="lbGananciaAproximadaCerrar" runat="server" Text=" )" CssClass="Letranegrita"></asp:Label>
        </div>
        <br /><br />

        <div class="table-responsive">
            <table class="table table-hover">
                <thead>
                    <tr>
                        <th style="width:10%" align="left"> SELECCIONAR </th>
                        <th style="width:30%" align="left"> DESCRIPCION </th>
                        <th style="width:10%" align="left"> CODIGO </th>
                        <th style="width:10%" align="left"> REFERENCIA </th>
                        <th style="width:10%" align="left"> STOCK </th>
                        <th style="width:10%" align="left"> PRECIO </th>
                        <th style="width:20%" align="left"> ESTATUS </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rpListadoProductosServicios" runat="server">
                        <ItemTemplate>
                            <tr>
                                <asp:HiddenField ID="hfIdRegistro" runat="server" Value='<%# Eval("IdRegistro") %>' />
                                <asp:HiddenField ID="hfNumeroConector" runat="server" Value='<%# Eval("NumeroConector") %>' />

                                <td style="width:10%"> <asp:Button ID="btnSeleccionarRegistro" runat="server" Text="Seleccionar" CssClass="btn btn-info btn-sm" ToolTip="Seleccionar Registro" OnClick="btnSeleccionarRegistro_Click" /> </td>
                                <td style="width:30%"> <%# Eval("Descripcion") %> </td>
                                <td style="width:10%"> <%# Eval("CodigoProducto") %> </td>
                                <td style="width:10%"> <%# Eval("Referencia") %> </td>
                                <td style="width:10%"> <%#string.Format("{0:n0}", Eval("Stock")) %> </td>
                                <td style="width:10%"> <%#string.Format("{0:n2}", Eval("PrecioVenta")) %> </td>
                                <td style="width:20%"> <%# Eval("Estatus") %> </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <div align="center">
                <asp:Label ID="lbPaginaActualTitulo" runat="server" Text="Pagina " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="LinkBlbPaginaActualVariable" runat="server" Text=" 0 " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadPaginaTitulo" runat="server" Text=" de " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadPaginaVariable" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
            </div>
             <div id="divPaginacionDetalle" runat="server" align="center">
        <div style="margin-top: 20px;">
            <table style="width: 600px">
                <tr>
                    <td> <asp:LinkButton ID="LinkPrimeraPagina" runat="server" Text="Primero" CssClass="btn btn-light btn-sm" ToolTip="Ir a la primera pagina del listado" OnClick="LinkPrimeraPagina_Click"></asp:LinkButton> </td>
                    <td> <asp:LinkButton ID="LinkAnterior" runat="server" Text="Anterior" CssClass="btn btn-light btn-sm" ToolTip="Ir a la pagina anterior del listado" OnClick="LinkAnterior_Click"></asp:LinkButton> </td>
                    <td>
                        <asp:DataList ID="dtPaginacion" runat="server" OnItemCommand="dtPaginacion_ItemCommand" OnItemDataBound="dtPaginacion_ItemDataBound" RepeatDirection="Horizontal">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkPaginacionCentral" runat="server" CommandArgument='<%# Eval("IndicePagina") %>' CommandName="newPage" Text='<%# Eval("TextoPagina") %>' Width="20px"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:DataList>

                    </td>
                    <td> <asp:LinkButton ID="LinkSiguiente" runat="server" Text="Siguiente" ToolTip="Ir a la siguiente pagina del listado" CssClass="btn btn-light btn-sm" OnClick="LinkSiguiente_Click"></asp:LinkButton> </td>
                    <td> <asp:LinkButton ID="LinkUltimo" runat="server" Text="Ultimo" ToolTip="Ir a la ultima pagina del listado" CssClass="btn btn-light btn-sm" OnClick="LinkUltimo_Click"></asp:LinkButton> </td>
                </tr>
            </table>
        </div>
        </div>
        <br />

        <button class="btn btn-outline-primary btn-sm BotonEspecoal" type="button" id="btnDetalleItemSeleccionado" runat="server" data-toggle="collapse" data-target="#DetalleItemSeleccionado" aria-expanded="false" aria-controls="collapseExample">
                     MOSTRAR EL DETALLE DEL ITEM SELECCIONADO
                     </button><br />
       <div class="collapse" id="DetalleItemSeleccionado">
                <div class="card card-body">
                    <asp:ScriptManager ID="ScripManaherDetalleItemsseleccionado" runat="server"></asp:ScriptManager>
                 <asp:UpdatePanel ID="UpdatePanelItemSeleccionado" runat="server">
                     <ContentTemplate>
                           <div class="form-row">
                               <div class="form-group col-md-4">
                                   <asp:Label ID="lbTipoProductoItemSeleccionado" runat="server" Text="Tipo de Producto" CssClass="Letranegrita"></asp:Label>
                                   <asp:TextBox ID="txtTipoProductoItemSeleccionado" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                               </div>

                               <div class="form-group col-md-4">
                                   <asp:Label ID="lbCategoriaItemSeleccionado" runat="server" Text="Categoria" CssClass="Letranegrita"></asp:Label>
                                   <asp:TextBox ID="txtCategoriaItemsseleccionado" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                               </div>

                               <div class="form-group col-md-4">
                                   <asp:Label ID="lbMarcaItemSeleccionado" runat="server" Text="Marca" CssClass="Letranegrita"></asp:Label>
                                   <asp:TextBox ID="txtMarcaItemSeleccionado" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                               </div>

                               <div class="form-group col-md-4">
                                   <asp:Label ID="lbTipoSupldorItemSeleccionado" runat="server" Text="Tipo de Suplidor" CssClass="Letranegrita"></asp:Label>
                                   <asp:TextBox ID="txtTipoSuplidorItemSeleccionado" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                               </div>

                               <div class="form-group col-md-4">
                                   <asp:Label ID="lbSuplidorItemsSeleccionado" runat="server" Text="Suplidor" CssClass="Letranegrita"></asp:Label>
                                   <asp:TextBox ID="txtSuplidorItemsSeleccionado" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                               </div>

                               <div class="form-group col-md-4">
                                   <asp:Label ID="lbReferenciaItemsSeleccionado" runat="server" Text="Referencia" CssClass="Letranegrita"></asp:Label>
                                   <asp:TextBox ID="txtReferenciaItemsSeleccionado" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                               </div>

                               <div class="form-group col-md-12">
                                   <asp:Label ID="lbDescripcionItemSeleccionado" runat="server" Text="Descripción" CssClass="Letranegrita"></asp:Label>
                                   <asp:TextBox ID="txtDescripcionItemsSeleccionado" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                               </div>

                               <div class="form-group col-md-4">
                                   <asp:Label ID="lbCodigoBarraItemSeleccionado" runat="server" Text="Codigo de Barra" CssClass="Letranegrita"></asp:Label>
                                   <asp:TextBox ID="txtCodigoBarraItemSeleccionado" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                               </div>

                               <div class="form-group col-md-4">
                                   <asp:Label ID="lbCodigoProductoItemSeleccionado" runat="server" Text="Codigo de Producto" CssClass="Letranegrita"></asp:Label>
                                   <asp:TextBox ID="txtCodigoProductoItemSeleccionado" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                               </div>

                               <div class="form-group col-md-4">
                                   <asp:Label ID="lbPrecioItemSeleccionado" runat="server" Text="Precio" CssClass="Letranegrita"></asp:Label>
                                   <asp:TextBox ID="txtPrecioItemSeleccionado" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                               </div>

                               <div class="form-group col-md-4">
                                   <asp:Label ID="lbStockItemSeleccionado" runat="server" Text="Stock" CssClass="Letranegrita"></asp:Label>
                                   <asp:TextBox ID="txtStockItemSeleccionado" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                               </div>

                               <div class="form-group col-md-4">
                                   <asp:Label ID="lbStockMinimoItemSeleccionado" runat="server" Text="Stock Minimo" CssClass="Letranegrita"></asp:Label>
                                   <asp:TextBox ID="txtStockMinimoItemSeleccionado" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                               </div>

                               <div class="form-group col-md-4">
                                   <asp:Label ID="lbEstatusItemSeleccionado" runat="server" Text="Estatus" CssClass="Letranegrita"></asp:Label>
                                   <asp:TextBox ID="txtEstatusItemSeleccionado" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                               </div>

                               <div class="form-group col-md-12">
                                   <asp:Label ID="lbComentarioItemSeleccionado" runat="server" Text="Comentario" CssClass="Letranegrita"></asp:Label>
                                   <asp:TextBox ID="txtComentarioItemSeleccionado" runat="server" TextMode="MultiLine" Height="80px" Width="100%" CssClass="form-control" Enabled="false"></asp:TextBox>
                               </div>
                           </div>
                     </ContentTemplate>
                 </asp:UpdatePanel>
                   </div>
                </div>
    <br />
    </div>

     
    
    <div id="DivBloqueMantenimiento" visible="false" runat="server">
        <div class="form-check-inline">
            <div class="form-group form-check">
                <asp:CheckBox ID="cbAplicaParaImpuestoMantenimiento" runat="server" Text="Aplica para Impuesto" CssClass="form-check-input" />
                <asp:CheckBox ID="cbLlevaGarantiaMantenimiento" runat="server" Text="Lleva Garantia" AutoPostBack="true" OnCheckedChanged="cbLlevaGarantiaMantenimiento_CheckedChanged" />
            </div>
        </div>
        <div class="form-row">
            <div class="form-group col-md-4">
                <asp:Label ID="lbSeleccionarTipoProductoMantenimiento" runat="server" Text="Tipo de Producto" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarTipoPrductoMantenimieto" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarTipoPrductoMantenimieto_SelectedIndexChanged" ToolTip="Seleccionar el tipo de producto"></asp:DropDownList>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbSeleccionarCategoriaMantenimiento" runat="server" Text="Categoria" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarCategoriaMantenimiento" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarCategoriaMantenimiento_SelectedIndexChanged" ToolTip="Seleccionar la Categoria de producto"></asp:DropDownList>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbSeleccionarMarcaMantenimiento" runat="server" Text="Marca" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarMarcaMantenimiento" runat="server" CssClass="form-control" ToolTip="Seleccionar la marca del producto"></asp:DropDownList>
            </div>

              <div class="form-group col-md-4">
                <asp:Label ID="lbSeleccionarTipoSuplidorMantenimiento" runat="server" Text="Tipo de Suplidor" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarTipoSuplidorMantenimiento" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarTipoSuplidorMantenimiento_SelectedIndexChanged" ToolTip="Seleccionar el tipo de suplidor"></asp:DropDownList>
            </div>

              <div class="form-group col-md-4">
                <asp:Label ID="lbSeleccionarSuplidorMantenimiento" runat="server" Text="Suplidor" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarSuplidorMantenimiento" runat="server" CssClass="form-control" ToolTip="Seleccionar el suplidor"></asp:DropDownList>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbDescripcionMantenimiento" runat="server" Text="Descripción" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtDescripcionMantenimiento" runat="server" CssClass="form-control" AutoCompleteType="Disabled" MaxLength="200"></asp:TextBox>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbCodigoBarrasMantenimiento" runat="server" Text="Codigo de Barra" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtCodigoBarraMantenimiento" runat="server" CssClass="form-control" Placeholder="Opcional" AutoCompleteType="Disabled" MaxLength="30"></asp:TextBox>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbReferenciaMantenimiento" runat="server" Text="Referencia" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtReferenciaMantenimiento" runat="server" CssClass="form-control" Placeholder="Opcional" AutoCompleteType="Disabled" MaxLength="30"></asp:TextBox>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbNumeroSeguimientoMantenimiento" runat="server" Text="Numero de Seguimiento" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtNumeroSeguimientoMantenimiento" runat="server" CssClass="form-control" Placeholder="Opcional" AutoCompleteType="Disabled" MaxLength="30"></asp:TextBox>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbCodigoProductoMantenimiento" runat="server" Text="Codigo de Producto" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtCodigoProductoMantenimiento" runat="server" CssClass="form-control" Placeholder="Opcional" AutoCompleteType="Disabled" MaxLength="30"></asp:TextBox>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbPrecioComprMantenimiento" runat="server" Text="Precio de Compra" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtPrecioCompraMantenimiento" runat="server" CssClass="form-control" TextMode="Number" step="0.01"></asp:TextBox>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbPrecioVentaMantenimiento" runat="server" Text="Precio de Venta" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtPrecioVentaMantenimiento" runat="server" CssClass="form-control" TextMode="Number" step="0.01"></asp:TextBox>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbStockMantenimiento" runat="server" Text="Stock" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtStockMantenimiento" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbStockMinimoMantenimiento" runat="server" Text="Stock Minimo" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtStockMinimoMantenimiento" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbUnidadMedidaMantenimiento" runat="server" Text="Unidad de Medida" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtUnidadMedidaMantenimiento" runat="server" CssClass="form-control" placeholder="Opcional" AutoCompleteType="Disabled" MaxLength="100"></asp:TextBox>
                <asp:DropDownList ID="ddlSeleccionarUnidadMedidaMantenimiento" runat="server" CssClass="form-control" ToolTip="Seleccionar Unidad de Medida"></asp:DropDownList>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbModeloMantenimiento" runat="server" Text="Modelo" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtModeloMantenimiento" runat="server" CssClass="form-control" placeholder="Opcional" AutoCompleteType="Disabled" MaxLength="100"></asp:TextBox>
                <asp:DropDownList ID="ddlSeleccionarModeloMantenimiento" runat="server" CssClass="form-control" ToolTip="Seleccionar Modelos"></asp:DropDownList>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbColorMantenimiento" runat="server" Text="Color" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtColorMantenimiento" runat="server" CssClass="form-control" placeholder="Opcional" AutoCompleteType="Disabled" MaxLength="100"></asp:TextBox>
                <asp:DropDownList ID="ddlSeleccionarColoresMantenimiento" runat="server" CssClass="form-control" ToolTip="Seleccionar Colores"></asp:DropDownList>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbCondicionMantenimiento" runat="server" Text="Condición" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtCondicionMantenimiento" runat="server" CssClass="form-control" placeholder="Opcional" AutoCompleteType="Disabled" MaxLength="100"></asp:TextBox>
                <asp:DropDownList ID="ddlSeleccionarCondicionesMantenimiento" runat="server" CssClass="form-control" ToolTip="Seleccionar Condiciones"></asp:DropDownList>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbCapacidadmantenimiento" runat="server" Text="Capacidad" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtCapacidadMantenimiento" runat="server" CssClass="form-control" placeholder="Opcional" AutoCompleteType="Disabled" MaxLength="100"></asp:TextBox>
                <asp:DropDownList ID="ddlSeleccionarCapacidadMantenimiento" runat="server" CssClass="form-control" ToolTip="Seleccionar Capacidad"></asp:DropDownList>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbSeleccionarTipoGarantiaMantenimiento" runat="server" Text="Tipo de Garantia" CssClass="Letranegrita"></asp:Label>
                
                <asp:DropDownList ID="ddlSeleccionarTipoGarantiaMantenimiento" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarTipoGarantiaMantenimiento_SelectedIndexChanged" ToolTip="Seleccionar el Tipo de Garantia"></asp:DropDownList>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbTiempoarantiaMantenimiento" runat="server" Text="Tiempo de Garantia" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtTiempoGarantiaMantenimiento" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
            </div>

            <div class="form-group col-md-12">
                <asp:Label ID="lbComentarioMantenimiento" runat="server" Text="Comentario" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtComentarioMantenimiento" runat="server" CssClass="form-control" placeholder="Opcional" AutoCompleteType="Disabled" TextMode="MultiLine" Height="80px" Width="100%"></asp:TextBox>
            </div>
        </div>
        <br />
        <div align="center">
            <asp:Button ID="btnGuardarRegistroMantenimientio" runat="server" Text="Guardar" ToolTip="Guardar Registro" OnClick="btnGuardarRegistroMantenimientio_Click" CssClass="btn btn-primary btn-sm" />
            <asp:Button ID="btnEditarRegistroMantenimiento" runat="server" Text="Editar" ToolTip="Editar Registro Seleccionado" OnClick="btnEditarRegistroMantenimiento_Click" CssClass="btn btn-primary btn-sm" />
            <asp:Button ID="btnEliminarRegistroMantenimiento" runat="server" Text="Borrar" ToolTip="Borrar Registro Seleccionado" OnClick="btnEliminarRegistroMantenimiento_Click" CssClass="btn btn-danger btn-sm" />
            <asp:Button ID="btnVolverAtras" runat="server" Text="Volver" ToolTip="Volver Atras" OnClick="btnVolverAtras_Click" CssClass="btn btn-danger btn-sm" />
        </div>
        <br />
    </div>






    


    
           <div class="modal fade bd-example-modal-xl SuplirsacarProductos" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-xl" role="document">
    <div class="modal-content">

        <asp:UpdatePanel ID="UpdatePanelSuplirItem" runat="server">
            <ContentTemplate>
               <div class="container-fluid">
                    <div class="form-check-inline">
                    <div class="form-group form-check">
                        <asp:Label ID="lbTipoOperacionSuplir" runat="server" Text="Tipo de Operación: " CssClass="Letranegrita"></asp:Label>
                        <asp:RadioButton ID="rbSuplirItemsSuplir" runat="server" Text="Suplir" ToolTip="Suplir Productos a Inventario" CssClass="form-check-input" GroupName="SuplirSacarItem" />
                        <asp:RadioButton ID="rbSacarItemsSuplir" runat="server" Text="Sacar" ToolTip="Sacar Productos de Inventario" CssClass="form-check-input" GroupName="SuplirSacarItem" />
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-group col-md-12">
                        <asp:Label ID="lbNombreProductoSeleccionadoSuplir" runat="server" Text="Nombre de Producto" CssClass="Letranegrita"></asp:Label>
                        <asp:TextBox ID="txtNombreProductoSeleccionadoSuplir" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                     <div class="form-group col-md-6">
                        <asp:Label ID="lbStockSuplir" runat="server" Text="Cantidad en Stock" CssClass="Letranegrita"></asp:Label>
                        <asp:TextBox ID="txtStockSuplir" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-6">
                        <asp:Label ID="lbStockMinimoSuplir" runat="server" Text="Stock Minimo" CssClass="Letranegrita"></asp:Label>
                        <asp:TextBox ID="txtstockMinimoSuplir" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-6">
                        <asp:Label ID="lbCantidadProcesar" runat="server" Text="Cantidad a Procesar" CssClass="Letranegrita"></asp:Label>
                        <asp:TextBox ID="txtCantidadProcesarSuplir" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                    </div>
                </div>
                <br />

                <div align="center">
                    <asp:Button ID="btnProcesarSuplir" runat="server" Text="Procesar" ToolTip="Procesar Información" CssClass="btn btn-success btn-sm" OnClick="btnProcesarSuplir_Click" />
                </div>
                <br />
               </div>
            </ContentTemplate>
        </asp:UpdatePanel>
      
        <br />
    </div>
  </div>
</div>
</asp:Content>
