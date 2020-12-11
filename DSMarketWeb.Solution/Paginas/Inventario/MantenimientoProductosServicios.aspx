<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PaginaMaestra.Master" Debug="true" AutoEventWireup="true" CodeBehind="MantenimientoProductosServicios.aspx.cs" Inherits="DSMarketWeb.Solution.Paginas.Inventario.MantenimientoProductosServicios" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
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

        .hiddenGrid
     {
         display:none;
     }
    </style>

    <script type="text/javascript">

        function Prueba() {
            $("#lb").val("INSERT");
        }
        function CamposFechaVacios() {
            alert("No puede dejar los campos fechas vacios para buscar por este tipo de filtro");
        }
        function CampoFechaDesdeVacio() {
            $("#<%=txtFechaDesdeConsulta.ClientID%>").css("border-color", "red");
        }
        function CampoFechaHastaVacio() {
            $("#<%=txtFechaHastaConsulta.ClientID%>").css("border-color", "red");
        }

        function BotonesModoNormal() {
            $("#<%=btnBuscar.ClientID%>").removeAttr("disabled", true);
            $("#<%=btnExportar.ClientID%>").removeAttr("disabled", true);
            $("#btnNuevo").removeAttr("disabled", true);
            $("#<%=btnReporte.ClientID%>").removeAttr("disabled", true);


            $("#btnModificar").attr("disabled", "disabled");
            $("#btnEliminar").attr("disabled", "disabled");
            $("#btnSuplir").attr("disabled", "disabled");
            $("#btnDescartar").attr("disabled", "disabled");
            $("#<%=btndetalle.ClientID%>").attr("disabled", "disabled");
        }
        function BotonesModoSelect() {
            $("#btnModificar").removeAttr("disabled", true);
            $("#btnEliminar").removeAttr("disabled", true);
            $("#btnSuplir").removeAttr("disabled", true);
            $("#btnDescartar").removeAttr("disabled", true);
            $("#<%=btndetalle.ClientID%>").removeAttr("disabled", true);


            $("#<%=btnBuscar.ClientID%>").attr("disabled", "disabled");
            $("#<%=btnExportar.ClientID%>").attr("disabled", "disabled");
            $("#btnNuevo").attr("disabled", "disabled");
            $("#<%=btnReporte.ClientID%>").attr("disabled", "disabled");

        }

        $(document).ready(function () {
            $("#btnNuevo").click(function () {
                $("#<%=btnGuardarMantenimiento.ClientID%>").show();

                $("#<%=btnModificarMantenimiento.ClientID%>").hide();
                $("#<%=btnEliminarMantenimiento.ClientID%>").hide();
            });

            $("#btnModificar").click(function () {
                $("#<%=btnModificarMantenimiento.ClientID%>").show();

                $("#<%=btnGuardarMantenimiento.ClientID%>").hide();
                $("#<%=btnEliminarMantenimiento.ClientID%>").hide();
            });

            $("#btnEliminar").click(function () {
                $("#<%=btnEliminarMantenimiento.ClientID%>").show();

                $("#<%=btnModificarMantenimiento.ClientID%>").hide();
                $("#<%=btnGuardarMantenimiento.ClientID%>").hide();
            });

            //FUNCION DE VALIDACON DEL BOTON GUARDAR
            $("#<%=btnGuardarMantenimiento.ClientID%>").click(function () {
                //VALIDAMOS EL CAMPO TIPO DE PRODUCTO
                var ValidarTipoProducto = $("#<%=ddlSeleccionarTipoProductoMantenimiento.ClientID%>").val();
                if (ValidarTipoProducto < 1) {
                    alert("El campo Tipo de Prducto no puede estar vacio, favor de verificar");
                    $("#<%=ddlSeleccionarTipoProductoMantenimiento.ClientID%>").css("border-color", "Red");
                    return false;
                }
                else {
                    //VALIDAMOS EL CAMPO CATEGORIA
                    var ValidarCategoria = $("#<%=ddlSeleccionarCategoriaMantenimiento.ClientID%>").val();
                    if (ValidarCategoria < 1) {
                        alert("El campo categoria no puede estar vacio para realizar esta operación, favor de veriicar");
                        $("#<%=ddlSeleccionarCategoriaMantenimiento.ClientID%>").css("border-color", "red");
                        return false;
                    }
                    else {
                        //VALIDAMOS EL CAMPO UNIDAD DE MEDIDA
                        var ValidarUnidadMedida = $("#<%=ddlSeleccionarUnidadMedidaMantenimiento.ClientID%>").val();
                        if (ValidarUnidadMedida < 1) {
                            alert("El campo Unidad de medida no puede estar vacio para realizar esta operación, favor de verificar");
                            $("#<%=ddlSeleccionarUnidadMedidaMantenimiento.ClientID%>").css("border-color", "red");
                            return false;
                        }
                        else {
                            var ValidarCampoMarca = $("#<%=ddlseleccionarMarca.ClientID%>").val();
                            if (ValidarCampoMarca < 1) {
                                alert("El campo marca no puede estar vacio, favor de verificar");
                                $("#<%=ddlseleccionarMarca.ClientID%>").css("border-color", "red");
                                return false;
                            }
                            else {
                                var ValidarModelo = $("#<%=ddlSeleccionarModelo.ClientID%>").val();
                                if (ValidarModelo < 1) {
                                    alert("El campo modelo no puede estar vacio, favor de verificar");
                                    $("#<%=ddlSeleccionarModelo.ClientID%>").css("border-color", "red");
                                    return false;
                                }
                                else {
                                    var ValidarColor = $("#<%=ddlSeleccionarColorMantenimiento.ClientID%>").val();
                                    if (ValidarColor < 1) {
                                        alert("El campo color no puede estar vacio, favor de verificar");
                                        $("#<%=ddlSeleccionarColorMantenimiento.ClientID%>").css("border-color", "red");
                                        return false;
                                    }
                                    else {
                                        var ValidarCapacidad = $("#<%=ddlSeleccionarCapacidadMantenimiento.ClientID%>").val();
                                        if (ValidarCapacidad < 1) {
                                            alert("El campo capacidad no puede estar vacio, favor de verificar");
                                            $("#<%=ddlSeleccionarCapacidadMantenimiento.ClientID%>").css("border-color", "red");
                                            return false;
                                        }
                                        else {
                                            var ValidarCondicion = $("#<%=ddlSeleccionarCondicionMantenimiento.ClientID%>").val();
                                            if (ValidarCondicion < 1) {
                                                alert("El campo condicion no puede estar vacio, favor de verificar");
                                                $("#<%=ddlSeleccionarCondicionMantenimiento.ClientID%>").css("border-color", "red");
                                                return false;
                                            }
                                            else {
                                                var ValidarPrecioCompra = $("#<%=txtPrecioCompraMantenimiento.ClientID%>").val().length;
                                                if (ValidarPrecioCompra < 1) {
                                                    alert("El campo precio de compra no puede estar vacio para guardar este registro");
                                                    $("#<%=txtPrecioCompraMantenimiento.ClientID%>").css("border-color", "red");
                                                    return false;
                                                }
                                                else {
                                                    var ValidarPrecioVenta = $("#<%=txtPrecioVentaMantenimiento.ClientID%>").val().length;
                                                    if (ValidarPrecioVenta < 1) {
                                                        alert("El campo precio de venta no puede estar vacio para realizar esta operación");
                                                        $("#<%=txtPrecioVentaMantenimiento.ClientID%>").css("border-color", "red");
                                                        return false;
                                                    }
                                                    else {
                                                        var ValidarStock = $("#<%=txtstockMantenimiento.ClientID%>").val().length;
                                                        if (ValidarStock < 1) {
                                                            alert("El campo stock no puede estar vacio, favor de verificar");
                                                            $("#<%=txtstockMantenimiento.ClientID%>").css("border-color", "red");
                                                            return false;
                                                        }
                                                        else {
                                                            var ValidarStockMinimo = $("#<%=txtStockMinimoMantenimiento.ClientID%>").val().length;
                                                            if (ValidarStockMinimo < 1) {
                                                                alert("El campo stock minimo no puede estar vacio para guardar este rgistro, favor de verificar");
                                                                $("#<%=txtStockMinimoMantenimiento.ClientID%>").css("border-color", "red");
                                                                return false;
                                                            }
                                                            else {
                                                                var ValidarTipoSuplidor = $("#<%=ddlSeleccionarTipoSuplidor.ClientID%>").val();
                                                                if (ValidarTipoSuplidor < 1) {
                                                                    alert("El campo tipo de suplidor no puede estar vacio para realizar esta operación, favor de verificar");
                                                                    $("#<%=ddlSeleccionarTipoSuplidor.ClientID%>").css("border-color", "red");
                                                                    return false;
                                                                }
                                                                else {
                                                                    var ValidarSuplidores = $("#<%=ddlSuplidorMantenimiento.ClientID%>").val();
                                                                    if (ValidarSuplidores < 1) {
                                                                        alert("El campo suplidor no puede estar vacio para realizar esta operación");
                                                                        $("#<%=ddlSuplidorMantenimiento.ClientID%>").css("border-color", "red");
                                                                        return false;
                                                                    }
                                                                    else {
                                                                        var ValidarPorcientoDescuento = $("#<%=txtPorcientoDescuentoMantenimiento.ClientID%>").val().length;
                                                                        if (ValidarPorcientoDescuento < 1) {
                                                                            alert("El campo Porciento de descuento no puede estar vacio, favor de verificar");
                                                                            $("#<%=txtPorcientoDescuentoMantenimiento.ClientID%>").css("border-color", "red");
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

            //FUNCION DE VALIDACION DEL BOTON MODIFICAR
            $("#<%=btnModificarMantenimiento.ClientID%>").click(function () {
                //VALIDAMOS EL CAMPO TIPO DE PRODUCTO
                var ValidarTipoProducto = $("#<%=ddlSeleccionarTipoProductoMantenimiento.ClientID%>").val();
                if (ValidarTipoProducto < 1) {
                    alert("El campo Tipo de Prducto no puede estar vacio, favor de verificar");
                    $("#<%=ddlSeleccionarTipoProductoMantenimiento.ClientID%>").css("border-color", "Red");
                    return false;
                }
                else {
                    //VALIDAMOS EL CAMPO CATEGORIA
                    var ValidarCategoria = $("#<%=ddlSeleccionarCategoriaMantenimiento.ClientID%>").val();
                    if (ValidarCategoria < 1) {
                        alert("El campo categoria no puede estar vacio para realizar esta operación, favor de veriicar");
                        $("#<%=ddlSeleccionarCategoriaMantenimiento.ClientID%>").css("border-color", "red");
                        return false;
                    }
                    else {
                        //VALIDAMOS EL CAMPO UNIDAD DE MEDIDA
                        var ValidarUnidadMedida = $("#<%=ddlSeleccionarUnidadMedidaMantenimiento.ClientID%>").val();
                        if (ValidarUnidadMedida < 1) {
                            alert("El campo Unidad de medida no puede estar vacio para realizar esta operación, favor de verificar");
                            $("#<%=ddlSeleccionarUnidadMedidaMantenimiento.ClientID%>").css("border-color", "red");
                            return false;
                        }
                        else {
                            var ValidarCampoMarca = $("#<%=ddlseleccionarMarca.ClientID%>").val();
                            if (ValidarCampoMarca < 1) {
                                alert("El campo marca no puede estar vacio, favor de verificar");
                                $("#<%=ddlseleccionarMarca.ClientID%>").css("border-color", "red");
                                return false;
                            }
                            else {
                                var ValidarModelo = $("#<%=ddlSeleccionarModelo.ClientID%>").val();
                                if (ValidarModelo < 1) {
                                    alert("El campo modelo no puede estar vacio, favor de verificar");
                                    $("#<%=ddlSeleccionarModelo.ClientID%>").css("border-color", "red");
                                    return false;
                                }
                                else {
                                    var ValidarColor = $("#<%=ddlSeleccionarColorMantenimiento.ClientID%>").val();
                                    if (ValidarColor < 1) {
                                        alert("El campo color no puede estar vacio, favor de verificar");
                                        $("#<%=ddlSeleccionarColorMantenimiento.ClientID%>").css("border-color", "red");
                                        return false;
                                    }
                                    else {
                                        var ValidarCapacidad = $("#<%=ddlSeleccionarCapacidadMantenimiento.ClientID%>").val();
                                        if (ValidarCapacidad < 1) {
                                            alert("El campo capacidad no puede estar vacio, favor de verificar");
                                            $("#<%=ddlSeleccionarCapacidadMantenimiento.ClientID%>").css("border-color", "red");
                                            return false;
                                        }
                                        else {
                                            var ValidarCondicion = $("#<%=ddlSeleccionarCondicionMantenimiento.ClientID%>").val();
                                            if (ValidarCondicion < 1) {
                                                alert("El campo condicion no puede estar vacio, favor de verificar");
                                                $("#<%=ddlSeleccionarCondicionMantenimiento.ClientID%>").css("border-color", "red");
                                                return false;
                                            }
                                            else {
                                                var ValidarPrecioCompra = $("#<%=txtPrecioCompraMantenimiento.ClientID%>").val().length;
                                                if (ValidarPrecioCompra < 1) {
                                                    alert("El campo precio de compra no puede estar vacio para guardar este registro");
                                                    $("#<%=txtPrecioCompraMantenimiento.ClientID%>").css("border-color", "red");
                                                    return false;
                                                }
                                                else {
                                                    var ValidarPrecioVenta = $("#<%=txtPrecioVentaMantenimiento.ClientID%>").val().length;
                                                    if (ValidarPrecioVenta < 1) {
                                                        alert("El campo precio de venta no puede estar vacio para realizar esta operación");
                                                        $("#<%=txtPrecioVentaMantenimiento.ClientID%>").css("border-color", "red");
                                                        return false;
                                                    }
                                                    else {
                                                        var ValidarStock = $("#<%=txtstockMantenimiento.ClientID%>").val().length;
                                                        if (ValidarStock < 1) {
                                                            alert("El campo stock no puede estar vacio, favor de verificar");
                                                            $("#<%=txtstockMantenimiento.ClientID%>").css("border-color", "red");
                                                            return false;
                                                        }
                                                        else {
                                                            var ValidarStockMinimo = $("#<%=txtStockMinimoMantenimiento.ClientID%>").val().length;
                                                            if (ValidarStockMinimo < 1) {
                                                                alert("El campo stock minimo no puede estar vacio para guardar este rgistro, favor de verificar");
                                                                $("#<%=txtStockMinimoMantenimiento.ClientID%>").css("border-color", "red");
                                                                return false;
                                                            }
                                                            else {
                                                                var ValidarTipoSuplidor = $("#<%=ddlSeleccionarTipoSuplidor.ClientID%>").val();
                                                                if (ValidarTipoSuplidor < 1) {
                                                                    alert("El campo tipo de suplidor no puede estar vacio para realizar esta operación, favor de verificar");
                                                                    $("#<%=ddlSeleccionarTipoSuplidor.ClientID%>").css("border-color", "red");
                                                                    return false;
                                                                }
                                                                else {
                                                                    var ValidarSuplidores = $("#<%=ddlSuplidorMantenimiento.ClientID%>").val();
                                                                    if (ValidarSuplidores < 1) {
                                                                        alert("El campo suplidor no puede estar vacio para realizar esta operación");
                                                                        $("#<%=ddlSuplidorMantenimiento.ClientID%>").css("border-color", "red");
                                                                        return false;
                                                                    }
                                                                    else {
                                                                        var ValidarPorcientoDescuento = $("#<%=txtPorcientoDescuentoMantenimiento.ClientID%>").val().length;
                                                                        if (ValidarPorcientoDescuento < 1) {
                                                                            alert("El campo Porciento de descuento no puede estar vacio, favor de verificar");
                                                                            $("#<%=txtPorcientoDescuentoMantenimiento.ClientID%>").css("border-color", "red");
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

            //EVENTO DEL BOTON NUEVO
            $("#btnNuevo").click(function () {
           <%--     //LIMPIAMOS LOS CONTROLES NECESARIOS
                $("#<%=txtNumeroSeguimientoMantenimiento.ClientID%>").val("");
                $("#<%=txtPrecioCompraMantenimiento.ClientID%>").val("");
                $("#<%=txtPrecioVentaMantenimiento.ClientID%>").val("");
                $("#<%=txtCodigoBarraMantenimiento.ClientID%>").val("");
                $("#<%=txtstockMantenimiento.ClientID%>").val("1");
                $("#<%=txtStockMinimoMantenimiento.ClientID%>").val("1");
                $("#<%=txtReferenciaMantenimiento.ClientID%>").val("");
                $("#<%=txtPorcientoDescuentoMantenimiento.ClientID%>").val("");
                $("#<%=txtDescripcionMantenimiento.ClientID%>").val("");
                $("#<%=txtComentarioMantenimiento.ClientID%>").val("");
                $("#<%=cbAcumulativoMantenimiento.ClientID%>").prop("checked", false);
                $("#<%=cbAplicaImpuestoMantenimiento.ClientID%>").prop("checked", false);--%>
            })
        })
        
    </script>

    <div class="container-fluid">
        <div class="jumbotron" align="center" >
            <asp:Label ID="lbTituloConsulta" runat="server" Text="Consulta de Productos / Servicios"></asp:Label>
            <asp:Label ID="lbIdUsuario" runat="server" Visible="false" Text="IdUsuario"></asp:Label><br />
             <asp:Label ID="lbIdProducto" runat="server" Visible="false" Text="IdProducto"></asp:Label><br />
             <asp:Label ID="lbNumeroConector" runat="server" Visible="false" Text="Numero de Conector"></asp:Label>
        </div>
        <div class="form-check-inline">
            <div class="form-group form-check">
                <asp:CheckBox ID="cbAgregarRangoFechaConsulta" runat="server" Text="Agregar Rango de Fecha" AutoPostBack="true" OnCheckedChanged="cbAgregarRangoFechaConsulta_CheckedChanged" ToolTip="Agregar Rango de fecha en la consulta" />
                <asp:CheckBox ID="cbReporteInventarioCompleto" runat="server" Text="Reporte Inventario Completo" ToolTip="Generar Reporte del Inventario Completo" />
                <asp:CheckBox ID="cbGraficar" runat="server" Text="Graficar" ToolTip="Graficar Registros" AutoPostBack="true" OnCheckedChanged="cbGraficar_CheckedChanged" />
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
            <asp:Button ID="btnBuscar" runat="server" Text="Consultar"  OnClick="btnBuscar_Click" CssClass="btn btn-outline-secondary btn-sm Custom" ToolTip="Consultar Registros" />
            <asp:Button ID="btnExportar" runat="server" Text="Exportar" OnClick="btnExportar_Click" CssClass="btn btn-outline-secondary btn-sm Custom" ToolTip="Exportar Registros" />
            <asp:Button ID="btnRestabelcer" runat="server" Text="Restablecer" OnClick="btnRestabelcer_Click" CssClass="btn btn-outline-secondary btn-sm Custom" ToolTip="Restablecer Pantalla" />
            <button type="button" id="btnNuevo" class="btn btn-outline-secondary btn-sm Custom" data-toggle="modal" data-target=".POPOPMantenimiento">Nuevo</button>
            <button type="button" id="btnModificar" class="btn btn-outline-secondary btn-sm Custom" data-toggle="modal" data-target=".POPOPMantenimiento">Editar</button>
            <button type="button" id="btnEliminar" class="btn btn-outline-secondary btn-sm Custom" data-toggle="modal" data-target=".POPOPMantenimiento">Eliminar</button>
            <button type="button" id="btnSuplir" class="btn btn-outline-secondary btn-sm Custom" data-toggle="modal" data-target=".POPOPSuplirProducto">Suplir</button>
            <asp:Button ID="btnReporte" runat="server" Text="Reporte" OnClick="btnReporte_Click" CssClass="btn btn-outline-secondary btn-sm Custom" ToolTip="Reporte de Inventario" />
            <button type="button" id="btnDescartar" class="btn btn-outline-secondary btn-sm Custom" data-toggle="modal" data-target=".POPOPDescartarProducto">Descartar</button>
            <asp:Button ID="btndetalle" runat="server" Text="Detalle" OnClick="btndetalle_Click" CssClass="btn btn-outline-secondary btn-sm Custom" ToolTip="Detalle del Producto" />
            <br />
        <asp:Label ID="lbCapitalInvertidoTitulo" runat="server" Text="Capital Invertido (" CssClass="Letranegrita" Font-Size="Small"></asp:Label>
            <asp:Label ID="lbCapitalInvertidoVariable" runat="server" Text="0" CssClass="Letranegrita" Font-Size="Small"></asp:Label>
            <asp:Label ID="lbCapitalInvertidoCerrar" runat="server" Text=")" CssClass="Letranegrita" Font-Size="Small"></asp:Label>

            <asp:Label ID="lbGananciaAproximadaTitulo" runat="server" Text="Ganancia Aproximada (" CssClass="Letranegrita" Font-Size="Small"></asp:Label>
            <asp:Label ID="lbGananciaAproximadaVariable" runat="server" Text="0" CssClass="Letranegrita" Font-Size="Small"></asp:Label>
            <asp:Label ID="lbGananciaAproximadaCerrar" runat="server" Text=")" CssClass="Letranegrita" Font-Size="Small"></asp:Label>

           <%--  <asp:Label ID="lbCantidadProductosTitulo" runat="server" Text="Total de Productos (" CssClass="Letranegrita" Font-Size="Small"></asp:Label>
            <asp:Label ID="lbCantidadProductosVariable" runat="server" Text="0" CssClass="Letranegrita" Font-Size="Small"></asp:Label>
            <asp:Label ID="lbCantidadProductosCerrar" runat="server" Text=")" CssClass="Letranegrita" Font-Size="Small"></asp:Label>


             <asp:Label ID="lbCantidadServiciosTitulo" runat="server" Text="Total de Servicios (" CssClass="Letranegrita" Font-Size="Small"></asp:Label>
            <asp:Label ID="lbCantidadServiciosVariable" runat="server" Text="0" CssClass="Letranegrita" Font-Size="Small"></asp:Label>
            <asp:Label ID="lbCantidadServiciosCerrar" runat="server" Text=")" CssClass="Letranegrita" Font-Size="Small"></asp:Label>--%>



        </div>
        <br />
         <asp:GridView ID="gvListado" runat="server" AllowPaging="true" OnPageIndexChanging="gvListado_PageIndexChanging" OnSelectedIndexChanged="gvListado_SelectedIndexChanged" AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                   <%-- <%$ Resources:Traducciones,OrdenNivel %>--%>
                    <asp:CommandField ButtonType="Button" HeaderText="Seleccionar"  ControlStyle-CssClass="btn btn-outline-secondary btn-sm" SelectText="Seleccionar" ShowSelectButton="True" />
                    <asp:BoundField DataField="IdProducto" HeaderText="ID" HeaderStyle-CssClass="hiddenGrid" ItemStyle-CssClass="hiddenGrid" />
                    <asp:BoundField DataField="NumeroConector" HeaderText="Numero de Conector" HeaderStyle-CssClass="hiddenGrid" ItemStyle-CssClass="hiddenGrid" />
                    <asp:BoundField DataField="Producto" HeaderText="Producto" />
                    <asp:BoundField DataField="TipoProducto" HeaderText="Tipo" />
                    <asp:BoundField DataField="Categoria" HeaderText="Categoria" />
                    <asp:BoundField DataField="Stock" HeaderText="Stock" DataFormatString="{0:N0}" HtmlEncode="false" />
                    <asp:BoundField DataField="PrecioVenta" HeaderText="Precio" DataFormatString="{0:N2}" HtmlEncode="false" />
              
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
          <asp:Label ID="lbGraficoProductoServicio" runat="server" Visible="false" Text="Productos / Servicios" CssClass="Letranegrita"></asp:Label>
           <br />
            <asp:Chart ID="GraProductoServicio" Width="1012px" Visible="False" runat="server" Palette="Pastel">
           <Series>
               <asp:Series Name="Serie" XValueMember="1" YValueMembers="2" IsValueShownAsLabel="true" Label="#VAL{N}" YValuesPerPoint="2"></asp:Series>
           </Series>
           <ChartAreas>
               <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
           </ChartAreas>
       </asp:Chart>


              <asp:Label ID="lbEstadisticaCapital" runat="server" Visible="false" Text="Estadistica Capital" CssClass="Letranegrita"></asp:Label>
           <br />
            <asp:Chart ID="GraEstadisticaCapital" Width="1012px" Visible="False" runat="server" Palette="Pastel">
           <Series>
               <asp:Series Name="Serie" XValueMember="1" YValueMembers="2" IsValueShownAsLabel="true" Label="#VAL{N}" YValuesPerPoint="2"></asp:Series>
           </Series>
           <ChartAreas>
               <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
           </ChartAreas>
       </asp:Chart>
        <br />
        <!--CONTROLES PARA EL DETALLE-->
        <div class="form-row">
            <div class="form-group col-md-12">
                <asp:Label ID="lbProductoDetalle" runat="server" Visible="false" Text="Producto" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtProductoDetalle" runat="server" Visible="false" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group col-md-4">
                <asp:Label ID="lbTipoProductoDetalle" runat="server" Text="Tipo de Producto" Visible="false" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtTipoProductoDetalle" runat="server" MaxLength="100" Visible="false" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbCategoriaDetalle" runat="server" Text="Categoria" Visible="false" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtCategoriaDetalle" runat="server" MaxLength="100" Visible="false" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbUnidadMedidaDetalle" runat="server" Visible="false" Text="Unidad de Medida" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtUnidadMedidaDetalle" runat="server" Visible="false" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbTipoSuplidorDetalle" runat="server" Visible="false" Text="Tipo de Suplidor" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtTipoSuplidorDetalle" runat="server" Visible="false" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbSuplidorDetalle" runat="server" Text="Suplidor" Visible="false" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtSuplidorDetalle" runat="server" Visible="false" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>

               <div class="form-group col-md-4">
                <asp:Label ID="lbCodigoBarraDetalle" runat="server" Text="Codigo de Barra" Visible="false" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtCodigoBarraDetalle" runat="server" Enabled="false" Visible="false" CssClass="form-control"></asp:TextBox>
            </div>

               <div class="form-group col-md-4">
                <asp:Label ID="lbReferenciaDetalle" runat="server" Text="Referencia" Visible="false" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtReferenciaDetalle" runat="server" Enabled="false" Visible="false" CssClass="form-control"></asp:TextBox>
            </div>

               <div class="form-group col-md-4">
                <asp:Label ID="lbStockDetalle" runat="server" Text="Stock" Visible="false" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtStockDetalle" runat="server" Enabled="false" Visible="false" CssClass="form-control"></asp:TextBox>
            </div>

               <div class="form-group col-md-4">
                <asp:Label ID="lbStockMinimoDetalle" runat="server" Text="Stock Minimo" Visible="false" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtStockMinimoDetalle" runat="server" Enabled="false" Visible="false" CssClass="form-control"></asp:TextBox>
            </div>

                           <div class="form-group col-md-4">
                <asp:Label ID="lbPrecioCompraDetalle" runat="server" Text="Precio de Compra" Visible="false" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtPrecioCompraDetalle" runat="server" Enabled="false" Visible="false" CssClass="form-control"></asp:TextBox>
            </div>

                           <div class="form-group col-md-4">
                <asp:Label ID="lbPrecioVentaDetalle" runat="server" Text="Precio de Venta" Visible="false" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtPrecioVentaDetalle" runat="server" Enabled="false" Visible="false" CssClass="form-control"></asp:TextBox>
            </div>

                           <div class="form-group col-md-4">
                <asp:Label ID="lbAcumulativoDetalle" runat="server" Text="Acumulativo" Visible="false" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtAcumulativoDetalle" runat="server" Enabled="false" Visible="false" CssClass="form-control"></asp:TextBox>
            </div>

                           <div class="form-group col-md-4">
                <asp:Label ID="lbAplicaDescuentoDetalle" runat="server" Text="Aplica para Descuento" Visible="false" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtAplicaDescuentoDetalle" runat="server" Enabled="false" Visible="false" CssClass="form-control"></asp:TextBox>
            </div>

                           <div class="form-group col-md-4">
                <asp:Label ID="lbPorcientoDescuentoDetalle" runat="server" Visible="false" Text="% de Descuento" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtPorcientoDescuentoDetalle" runat="server" Visible="false" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>

                           <div class="form-group col-md-4">
                                  <!--ESPACIO DISPONIBLE-->
            </div>

                           <div class="form-group col-md-12">
                <asp:Label ID="lbComentarioDetalle" runat="server" Visible="false" Text="Comentario" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtComentarioDetalle" runat="server" Visible="false" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>

        </div>
        <br />
        <div align="center">
            <asp:Button ID="btnRegresarDetalle" runat="server" Text="Regresar" Visible="false" ToolTip="Ocultar los controles del detalle" OnClick="btnRegresarDetalle_Click" CssClass="btn btn-outline-secondary btn-sm Custom" />
        </div>
        <br />
    </div>

    <!--AQUI VAN TODOS LOS POPOS CARAJO, POR SI UN PENDEJO SE PONE A ANALIZAR MI CODIGO-->

    <asp:ScriptManager ID="ScripManagerInventario" runat="server"></asp:ScriptManager>
        <div class="modal fade bd-example-modal-xl POPOPMantenimiento" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-xl" role="document">
    <div class="modal-content">
        <asp:UpdatePanel ID="UpdatePanelMantenimiento" runat="server">
            <ContentTemplate>
                <div class="container-fluid">
                    <div class="jumbotron" align="center">
                        <asp:Label ID="lbTituloMantenimiento" runat="server" Text="Mantenimiento de Producto"></asp:Label>
                    </div>
                    <div align="center">
                        <asp:Label ID="lbNumeroRegistroInventario" runat="server" Text="Numero de Registro: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbNumeroRegistroVariable" runat="server" Text="0000000000" CssClass="Letranegrita"></asp:Label>
                         <asp:Label ID="lbRegistroDisponible" runat="server" Visible="false" Text="Registro Disponible" CssClass="Letranegrita"></asp:Label>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-md-4">
                            <asp:Label ID="lbTipoProductoMntenimiento" runat="server" Text="Tipo de Producto" CssClass="Letranegrita"></asp:Label>
                            <asp:DropDownList ID="ddlSeleccionarTipoProductoMantenimiento" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarTipoProductoMantenimiento_SelectedIndexChanged" ToolTip="Seleccionar el tipo de producto" CssClass="form-control"></asp:DropDownList>
                        </div>

                        <div class="form-group col-md-4">
                            <asp:Label ID="lbCategoriaMantenimiento" runat="server" Text="Categoria" CssClass="Letranegrita"></asp:Label>
                            <asp:DropDownList ID="ddlSeleccionarCategoriaMantenimiento" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarCategoriaMantenimiento_SelectedIndexChanged" ToolTip="Seleccionar Categorias" CssClass="form-control"></asp:DropDownList>
                        </div>

                        <div class="form-group col-md-4">
                            <asp:Label ID="lbSeleccionarUnidadMedida" runat="server" Text="Unidad de Medida" CssClass="Letranegrita"></asp:Label>
                            <asp:DropDownList ID="ddlSeleccionarUnidadMedidaMantenimiento" runat="server" ToolTip="Seleccionar Unidad de Medida" CssClass="form-control"></asp:DropDownList>
                        </div>

                        <div class="form-group col-md-4">
                            <asp:Label ID="lbMarcaMantenimiento" runat="server" Text="Marca" CssClass="Letranegrita"></asp:Label>
                            <asp:DropDownList ID="ddlseleccionarMarca" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlseleccionarMarca_SelectedIndexChanged" ToolTip="Seleccionar Marca" CssClass="form-control"></asp:DropDownList>
                        </div>

                        <div class="form-group col-md-4">
                            <asp:Label ID="lbSeleccionarModeloMantenimiento" runat="server" Text="Modelo" CssClass="Letranegrita"></asp:Label>
                            <asp:DropDownList ID="ddlSeleccionarModelo" runat="server" ToolTip="Seleccionar Modelo" CssClass="form-control"></asp:DropDownList>
                        </div>

                        <div class="form-group col-md-4">
                            <asp:Label ID="lbSeleccionarColorMantenimiento" runat="server" Text="Color" CssClass="Letranegrita"></asp:Label>
                            <asp:DropDownList ID="ddlSeleccionarColorMantenimiento" runat="server" ToolTip="Seleccionar Color" CssClass="form-control"></asp:DropDownList>
                        </div>

                        <div class="form-group col-md-4">
                            <asp:Label ID="lbSeleccionarCapacidadMantenimiento" runat="server" Text="Capacidad" CssClass="Letranegrita"></asp:Label>
                            <asp:DropDownList ID="ddlSeleccionarCapacidadMantenimiento" runat="server" ToolTip="Seleccionar Capacidad" CssClass="form-control"></asp:DropDownList>
                        </div>

                        <div class="form-group col-md-4">
                            <asp:Label ID="lbSeleccionarCondicionMantenimiento" runat="server" Text="Condición" CssClass="Letranegrita"></asp:Label>
                            <asp:DropDownList ID="ddlSeleccionarCondicionMantenimiento" runat="server" ToolTip="Seleccionar Condición" CssClass="form-control"></asp:DropDownList>
                        </div>

                         <div class="form-group col-md-4">
                            <asp:Label ID="lbNumeroSeguimientoMantenimiento" runat="server" Text="Numero de Seguimiento" CssClass="Letranegrita"></asp:Label>
                          <asp:TextBox ID="txtNumeroSeguimientoMantenimiento" runat="server" TextMode="SingleLine" AutoCompleteType="Disabled" MaxLength="100" CssClass="form-control"></asp:TextBox>
                        </div>

                         <div class="form-group col-md-4">
                            <asp:Label ID="lbPrecioCompraMantenimiento" runat="server" Text="Precio de Compra" CssClass="Letranegrita"></asp:Label>
                            <asp:TextBox ID="txtPrecioCompraMantenimiento" runat="server" AutoCompleteType="Disabled" TextMode="Number" step="0.01" MaxLength="20" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="form-group col-md-4">
                            <asp:Label ID="lbPrecioVentaMantenimiento" runat="server" Text="Precio de Venta" CssClass="Letranegrita"></asp:Label>
                            <asp:TextBox ID="txtPrecioVentaMantenimiento" runat="server" AutoCompleteType="Disabled" TextMode="Number" step="0.01" MaxLength="20" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="form-group col-md-4">
                            <asp:Label ID="lbCodigoBarraMantenimiento" runat="server" Text="Codigo de Barra" CssClass="Letranegrita"></asp:Label>
                            <asp:TextBox ID="txtCodigoBarraMantenimiento" runat="server" AutoCompleteType="Disabled" MaxLength="50" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="form-group col-md-4">
                            <asp:Label ID="lbStockMantenimiento" runat="server" Text="Stock" CssClass="Letranegrita"></asp:Label>
                            <asp:TextBox ID="txtstockMantenimiento" runat="server" Enabled="false" Text="1" TextMode="Number" AutoCompleteType="Disabled"  CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="form-group col-md-4">
                            <asp:Label ID="lbStockMinimoMantenimiento" runat="server" Text="Stock Minimo" CssClass="Letranegrita"></asp:Label>
                            <asp:TextBox ID="txtStockMinimoMantenimiento" runat="server" Enabled="false" Text="1" TextMode="Number" AutoCompleteType="Disabled"  CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="form-group col-md-4">
                            <asp:Label ID="lbReferenciaMantenimiento" runat="server" Text="Referencia" CssClass="Letranegrita"></asp:Label>
                            <asp:TextBox ID="txtReferenciaMantenimiento" runat="server"  AutoCompleteType="Disabled"  CssClass="form-control"></asp:TextBox>
                        </div>

                         <div class="form-group col-md-4">
                            <asp:Label ID="lbSeleccionarTipoSuplidorMantenimiento" runat="server" Text="Tipo de Suplidor" CssClass="Letranegrita"></asp:Label>
                            <asp:DropDownList ID="ddlSeleccionarTipoSuplidor" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarTipoSuplidor_SelectedIndexChanged" ToolTip="Seleccionar Tipo de Suplidor" CssClass="form-control"></asp:DropDownList>
                        </div>

                         <div class="form-group col-md-4">
                            <asp:Label ID="lbSeleccionarSuplidorMantenimiento" runat="server" Text="Suplidor" CssClass="Letranegrita"></asp:Label>
                            <asp:DropDownList ID="ddlSuplidorMantenimiento" runat="server" ToolTip="Seleccionar Suplidor" CssClass="form-control"></asp:DropDownList>
                        </div>

                         <div class="form-group col-md-4">
                            <asp:Label ID="lbPorcientoDescuentoMantenimiento" runat="server" Text="% de descuento" CssClass="Letranegrita"></asp:Label>
                            <asp:TextBox ID="txtPorcientoDescuentoMantenimiento" runat="server"  AutoCompleteType="Disabled"  CssClass="form-control"></asp:TextBox>
                        </div>

                         <div class="form-group col-md-12">
                            <asp:Label ID="lbDescripcionMantenimiento" runat="server" Text="Descripción" CssClass="Letranegrita"></asp:Label>
                            <asp:TextBox ID="txtDescripcionMantenimiento" runat="server" Enabled="false"  AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                        </div>

                         <div class="form-group col-md-12">
                            <asp:Label ID="lbComentarioMantenimiento" runat="server" Text="Comentario" CssClass="Letranegrita"></asp:Label>
                            <asp:TextBox ID="txtComentarioMantenimiento" runat="server" AutoCompleteType="Disabled"   CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="form-group col-md-12">
                            <asp:Label ID="lbClaveSeguridad" runat="server" Text="Clave de Seguridad" CssClass="Letranegrita"></asp:Label>
                            <asp:TextBox ID="txtClaveSeguridad" runat="server" AutoCompleteType="Disabled" TextMode="Password"  CssClass="form-control"></asp:TextBox>
                        </div>

                    </div>

                    <div class="form-check-inline">
                        <div class="form-group form-check">
                            <asp:CheckBox ID="cbAplicaImpuestoMantenimiento" runat="server" Text="Aplica para Impuesto" ToolTip="Esta opcion es para establecer los productos a los cuales aplican para impuesto" CssClass="form-check-input" />
                              <asp:CheckBox ID="cbAcumulativoMantenimiento" runat="server" Text="Producto Acumulativo" AutoPostBack="true" OnCheckedChanged="cbAcumulativoMantenimiento_CheckedChanged" ToolTip="Esta opcion es para establecer los productos que son acumulativos" CssClass="form-check-input" />
                              <asp:CheckBox ID="cbNoLimpiarControles" runat="server" Text="No limpiar controles" ToolTip="Esta Opcion es para no limpiar los controles al momento guardar un registro" CssClass="form-check-input" />
                        </div>
                    </div>

                    <asp:Panel ID="Panel1" runat="server"></asp:Panel>

                </div>


              
            </ContentTemplate>
        </asp:UpdatePanel>
          <div align="center">
            <asp:Button ID="btnGuardarMantenimiento" runat="server" Text="Guardar" CssClass="btn btn-outline-secondary btn-sm Custom" ToolTip="Guardar Registro" OnClick="btnGuardarMantenimiento_Click" />
             <asp:Button ID="btnModificarMantenimiento" runat="server" Text="Modificar" CssClass="btn btn-outline-secondary btn-sm Custom" ToolTip="Modificar Registro" OnClick="btnModificarMantenimiento_Click" />
             <asp:Button ID="btnEliminarMantenimiento" runat="server" Text="Eliminar" CssClass="btn btn-outline-secondary btn-sm Custom" ToolTip="Eliminar Registro" OnClick="btnEliminarMantenimiento_Click"/>
        </div><br />
    </div>
  </div>
</div>


        <div class="modal fade bd-example-modal-xl POPOPSuplirProducto" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-xl" role="document">
    <div class="modal-content">
        <div class="container-fluid">
            <div class="jumbotron" align="center">
                <asp:Label ID="lbTituloSuplirProducto" runat="server" Text="Suplir / Sacar Productos"></asp:Label>
            </div>
            <div class="form-row">
                <div class="form-group col-md-6">
                    <asp:Label ID="lbTipoProducoSuplirTitulo" runat="server" Text="Tipo de Producto: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbTipoProducoSuplirVariable" runat="server" Text="DATO" CssClass="Letranegrita"></asp:Label>
                </div>
                 <div class="form-group col-md-6">
                    <asp:Label ID="lbCategoriaSuplirTitulo" runat="server" Text="Categoria: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbCategoriaSuplirVariable" runat="server" Text="DATO" CssClass="Letranegrita"></asp:Label>
                </div>
                <div class="form-group col-md-6">
                    <asp:Label ID="lbUnidadMedidaSuplirTitulo" runat="server" Text="Unidad de Medida: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbUnidadMedidaSuplirVariable" runat="server" Text="DATO" CssClass="Letranegrita"></asp:Label>
                </div>
                <div class="form-group col-md-6">
                    <asp:Label ID="lbCodigoBarrasSuplirTitulo" runat="server" Text="Codigo de Barras: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbUnidadCodigoBarraVariable" runat="server" Text="DATO" CssClass="Letranegrita"></asp:Label>
                </div>
                  <div class="form-group col-md-6">
                    <asp:Label ID="lbPrecioCompraTitulo" runat="server" Text="Precio de Compra: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbPrecioCompraVariable" runat="server" Text="DATO" CssClass="Letranegrita"></asp:Label>
                </div>
                  <div class="form-group col-md-6">
                    <asp:Label ID="lbPrecioVentaTitulo" runat="server" Text="Precio de Venta: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbprecioVentaVariable" runat="server" Text="DATO" CssClass="Letranegrita"></asp:Label>
                </div>
                 <div class="form-group col-md-6">
                    <asp:Label ID="lbStockSuplirTitulo" runat="server" Text="Stock: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbStockSuplirVariable" runat="server" Text="DATO" CssClass="Letranegrita"></asp:Label>
                </div>
                 <div class="form-group col-md-6">
                    <asp:Label ID="lbStockMinimoTitulo" runat="server" Text="Stock Minimo: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbStockMinimoVariable" runat="server" Text="DATO" CssClass="Letranegrita"></asp:Label>
                </div>
                  <div class="form-group col-md-6">
                    <asp:Label ID="lbMarcaSuplitTitulo" runat="server" Text="Marca: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbMarcaSuplirVariable" runat="server" Text="DATO" CssClass="Letranegrita"></asp:Label>
                </div>
                  <div class="form-group col-md-6">
                    <asp:Label ID="lbModeloSuplirTitulo" runat="server" Text="Modelo: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbModeloVariable" runat="server" Text="DATO" CssClass="Letranegrita"></asp:Label>
                </div>
                  <div class="form-group col-md-6">
                    <asp:Label ID="lbTipoSuplidorSuplirTitulo" runat="server" Text="Tipo de Suplidor: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbTipoSuplidorVariable" runat="server" Text="DATO" CssClass="Letranegrita"></asp:Label>
                </div>
                  <div class="form-group col-md-6">
                    <asp:Label ID="lbSuplidorSuplirTitulo" runat="server" Text="Suplidor: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbSuplidorSuplirVariable" runat="server" Text="DATO" CssClass="Letranegrita"></asp:Label>
                </div>
                  <div class="form-group col-md-6">
                    <asp:Label ID="lbReferenciaSuplirTitulo" runat="server" Text="Referencia: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbReferenciaSuplirVariable" runat="server" Text="DATO" CssClass="Letranegrita"></asp:Label>
                </div>
                  <div class="form-group col-md-6">
                    <asp:Label ID="lbPorcientoDescuentoSuplirTitulo" runat="server" Text="% de Descuento: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbPorcientoDescuentoSuplirVariable" runat="server" Text="DATO" CssClass="Letranegrita"></asp:Label>
                </div>
                <div class="form-group col-md-12">
                    <asp:Label ID="lbDescripcionSuplirTitulo" runat="server" Text="Descripción: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbDescripcionSuplirVariable" runat="server" Text="DATO" CssClass="Letranegrita"></asp:Label>
                </div>
                 <div class="form-group col-md-12">
                    <asp:Label ID="lbComentarioSuplirTitulo" runat="server" Text="Comentario: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbComentarioSuplirVariable" runat="server" Text="DATO" CssClass="Letranegrita"></asp:Label>
                </div>

            </div>
            <div class="form-check-inline">
                <div class="form-group form-check">
                    <asp:RadioButton ID="rbIngresarRegistros" runat="server" Text="Ingresar Producto" ToolTip="Ingresar Productos" GroupName="SUPLIRPRODUCTOS" CssClass="form-check-input" />
                    <asp:RadioButton ID="rbSacarProductos" runat="server" Text="Sacar Producto" ToolTip="Sacar Productos" GroupName="SUPLIRPRODUCTOS" CssClass="form-check-input" />
                </div>
            </div>
            <div class="form-row">
                <div class="form-group col-md-6">
                    <asp:Label ID="lbCantidadRegistrosSuplir" runat="server" Text="Cantidad" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtCantidadSuplir" runat="server" TextMode="Number" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                </div>
                <div class="form-group col-md-6">
                    <asp:Label ID="lbClaveSeguridadSuplir" runat="server" Text="Clave de Seguridad" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtClaveSeguridadSuplir" runat="server" TextMode="Password" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                </div>
            </div>

            <div align="center">
                <asp:Button ID="btnProcesarSuplr" runat="server" Text="Procesar" ToolTip="Procesar Información" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnProcesarSuplr_Click" />
            </div>
            <br />
        </div>
    </div>
  </div>
</div>


        <div class="modal fade bd-example-modal-xl POPOPDescartarProducto" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-xl" role="document">
    <div class="modal-content">
        <div class="container-fluid">
            <div class="jumbotron" align="center">
                <asp:Label ID="lbTituloDescartarProducto" runat="server" Text="Descartar Productos"></asp:Label>
            </div>
            <div class="form-row">
                <div class="form-group col-md-6">
                    <asp:Label ID="lbTipoProducoDescartarTitulo" runat="server" Text="Tipo de Producto: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbTipoProducoDescartarVariable" runat="server" Text="DATO" CssClass="Letranegrita"></asp:Label>
                </div>
                 <div class="form-group col-md-6">
                    <asp:Label ID="lbCategoriaDescartarTitulo" runat="server" Text="Categoria: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbCategoriaDescartarVariable" runat="server" Text="DATO" CssClass="Letranegrita"></asp:Label>
                </div>
                <div class="form-group col-md-6">
                    <asp:Label ID="lbUnidadMedidaDescartarTitulo" runat="server" Text="Unidad de Medida: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbUnidadMedidaDescartarVariable" runat="server" Text="DATO" CssClass="Letranegrita"></asp:Label>
                </div>
                <div class="form-group col-md-6">
                    <asp:Label ID="lbCodigoBarrasDescartarTitulo" runat="server" Text="Codigo de Barras: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbCodigoBarrasDescartarVariable" runat="server" Text="DATO" CssClass="Letranegrita"></asp:Label>
                </div>
                  <div class="form-group col-md-6">
                    <asp:Label ID="lbPrecioCompraDescartarTitulo" runat="server" Text="Precio de Compra: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbPrecioCompraDescartarVariable" runat="server" Text="DATO" CssClass="Letranegrita"></asp:Label>
                </div>
                  <div class="form-group col-md-6">
                    <asp:Label ID="lbPrecioVentaDescartarTitulo" runat="server" Text="Precio de Venta: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbprecioVentaDescartarVariable" runat="server" Text="DATO" CssClass="Letranegrita"></asp:Label>
                </div>
                 <div class="form-group col-md-6">
                    <asp:Label ID="lbStockDescartarTitulo" runat="server" Text="Stock: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbStockDescartarVariable" runat="server" Text="DATO" CssClass="Letranegrita"></asp:Label>
                </div>
                 <div class="form-group col-md-6">
                    <asp:Label ID="lbStockMinimoDescartarTitulo" runat="server" Text="Stock Minimo: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbStockMinimoDescartarVariable" runat="server" Text="DATO" CssClass="Letranegrita"></asp:Label>
                </div>
                  <div class="form-group col-md-6">
                    <asp:Label ID="lbMarcaDescartarTitulo" runat="server" Text="Marca: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbMarcaDescartarVariable" runat="server" Text="DATO" CssClass="Letranegrita"></asp:Label>
                </div>
                  <div class="form-group col-md-6">
                    <asp:Label ID="lbModeloDescartarTitulo" runat="server" Text="Modelo: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbModeloDescartarVariable" runat="server" Text="DATO" CssClass="Letranegrita"></asp:Label>
                </div>
                  <div class="form-group col-md-6">
                    <asp:Label ID="lbTipoSuplidorDescartarTitulo" runat="server" Text="Tipo de Suplidor: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbTipoSuplidorDescartarVariable" runat="server" Text="DATO" CssClass="Letranegrita"></asp:Label>
                </div>
                  <div class="form-group col-md-6">
                    <asp:Label ID="lbSuplidorDescartarTitulo" runat="server" Text="Suplidor: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbSuplidorDescartarVariable" runat="server" Text="DATO" CssClass="Letranegrita"></asp:Label>
                </div>
                  <div class="form-group col-md-6">
                    <asp:Label ID="lbReferenciaDescartarTitulo" runat="server" Text="Referencia: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbReferenciaDescartarVariable" runat="server" Text="DATO" CssClass="Letranegrita"></asp:Label>
                </div>
                  <div class="form-group col-md-6">
                    <asp:Label ID="lbPorcientoDescuentoDescartarTitulo" runat="server" Text="% de Descuento: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbPorcientoDescuentoDescartarVariable" runat="server" Text="DATO" CssClass="Letranegrita"></asp:Label>
                </div>
                <div class="form-group col-md-12">
                    <asp:Label ID="lbDescripcionDescartarTitulo" runat="server" Text="Descripción: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbDescripcionDescartarVariable" runat="server" Text="DATO" CssClass="Letranegrita"></asp:Label>
                </div>
                 <div class="form-group col-md-12">
                    <asp:Label ID="lbComentarioDescartarTitulo" runat="server" Text="Comentario: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbComentarioDescartarVariable" runat="server" Text="DATO" CssClass="Letranegrita"></asp:Label>
                </div>

            </div>
            <div class="form-row">
                <div class="form-group col-md-6">
                    <asp:Label ID="lbClaveSeguridadDescartar" runat="server" Text="Clave de Seguridad" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtClaveSeguridadDescartar" runat="server" TextMode="Password" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                </div>
            </div>

            <div align="center">
                <asp:Button ID="btnProcesarDescartar" runat="server" Text="Procesar" ToolTip="Descartar Producto" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnProcesarDescartar_Click" />
            </div>
            <br />
        </div>
    </div>
  </div>
</div>
</asp:Content>
