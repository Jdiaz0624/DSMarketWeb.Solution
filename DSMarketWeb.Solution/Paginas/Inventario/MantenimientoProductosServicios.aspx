<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PaginaMaestra.Master"  Debug="true" AutoEventWireup="true" CodeBehind="MantenimientoProductosServicios.aspx.cs" Inherits="DSMarketWeb.Solution.Paginas.Inventario.MantenimientoProductosServicios" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
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

        .hiddenGrid
     {
         display:none;
     }


        table {
            border-collapse: collapse;
        }
        

        th {
            background-color: dodgerblue;
            color: white;
        }


    </style>

    <script type="text/javascript">


        function filterFloat(evt, input) {
            // Backspace = 8, Enter = 13, ‘0′ = 48, ‘9′ = 57, ‘.’ = 46, ‘-’ = 43
            var key = window.Event ? evt.which : evt.keyCode;
            var chark = String.fromCharCode(key);
            var tempValue = input.value + chark;
            if (key >= 48 && key <= 57) {
                if (filter(tempValue) === false) {
                    return false;
                } else {
                    return true;
                }
            } else {
                if (key == 8 || key == 13 || key == 0) {
                    return true;
                } else if (key == 46) {
                    if (filter(tempValue) === false) {
                        return false;
                    } else {
                        return true;
                    }
                } else {
                    return false;
                }
            }
        }
        function filter(__val__) {
            var preg = /^([0-9]+\.?[0-9]{0,2})$/;
            if (preg.test(__val__) === true) {
                return true;
            } else {
                return false;
            }

        }
     
        function CamposFechaVAcios() {
            alert("Los campos fecha no pueden estar vacios para buscar registros mediante este metodo.");
        }
        function CampoFechaDesdeVAcio() {
            $("#<%=txtFechaDesdeConsulta.ClientID%>").css("border-color", "red");
        }
        function CampoFechaHastaVacio() {
            $("#<%=txtFechaHAstaConsulta.ClientID%>").css("border-color", "red");
        }
        function RegistroGuardadoConExito() {
            alert("Registro Guardado con Exito.");
        }
        function RegistroProcesadoConExito() {
            alert("Registro procesado con exito.");
        }

        function ClaveSeguridadIngresadaNoValida() {
            alert("La clave de seguridad ingresada no es valida, favor de verificar.");
        }

        function RegistroModificadoConExito() {
            alert("Registro Modificado con Exito.");
        }
        function RegistroEliminadoConExito() {
            alert("Registro Eliminado con Exito.");
        }
        function ProcesoCompletadoCOnExito() {
            alert("Proceso completado con exito.");
        }
        function BloquearSuplir() {
  
            $("#btnSuplirConsulta").attr("disabled", "disabled");
        }
        function HabilitarSuplir() {
            $("#btnSuplirConsulta").removeAttr("disabled", true);
        }
        function ImagenPorDefectoNoEncontrada() {
            alert("La imagen por defecto no se encuentra, favor de contactar con el administrador del sistema para solucionar este problema.");
        }

        function CantidadSacarNoDisponible() {
            alert("La cantidad que intentas sacar supera la cantidad existente en almacen, favor de verificar.");
            $("#<%=txtCantidadSuplir.ClientID%>").css("border-color", "red");
        }

        function ErrorActualizarFotoProducto() {
            alert("Error al actualizar la foto del producto, favor de verificar si selecciono la imagen correctamente en el equipo.");
        }
        $(document).ready(function () {
            $('.solo-numero').keyup(function () {
                this.value = (this.value + '').replace(/[^0-9]/g, '');
            });

            $(".numeric").numeric();

            //VALIDAMOS LOS CAMPOS EN EL EVENTO DEL BOTON GUARDAR
            $("#<%=btnProcesarMantenimiento.ClientID%>").click(function () {
                //TIPO DE PRODUCTO
                var ValidarTipoProducto = $("#<%=ddlSeleccionarTipoProductoMantenimiento.ClientID%>").val();
                if (ValidarTipoProducto < 1) {
                    alert("El campo tipo de producto no puede estar vacio para guardar este registro, favor de verificar");
                    $("#<%=ddlSeleccionarTipoProductoMantenimiento.ClientID%>").css("border-color", "red");
                    return false;
                }
                else {
                    //CATEGORIA
                    var ValidarCategoria = $("#<%=ddlSeleccionarCategoriaMantenimiento.ClientID%>").val();
                    if (ValidarCategoria < 1) {
                        alert("EL campo categoria no puede estar vacio para guardar un nuevo registro, favor de verificar");
                        $("#<%=ddlSeleccionarCategoriaMantenimiento.ClientID%>").css("border-color", "red");
                        return false;
                    }
                    else {
                        //UNIDAD DE MEDIDA
                        var ValidarUnidadMedida = $("#<%=ddlSeleccionarUnidadMedidaMantenimiento.ClientID%>").val();
                        if (ValidarUnidadMedida < 1) {
                            alert("El campo unidad de medida no puede estar vacio para gaurdar este registro, favor de verificar");
                            $("#<%=ddlSeleccionarUnidadMedidaMantenimiento.ClientID%>").css("border-color", "red");
                            return false;
                        }
                        else {
                            //MARCA
                            var ValidarMarca = $("#<%=ddlSeleccionarMarcaMantenimiento.ClientID%>").val();
                            if (ValidarMarca < 1) {
                                alert("El campo marca no puede estar vacio para guardar este registro, favor de verificar");
                                $("#<%=ddlSeleccionarMarcaMantenimiento.ClientID%>").css("border-color", "red");
                                return false;
                            }
                            else {
                                //MODELO
                                var ValidarModelo = $("#<%=ddlSeleccionarModeloMantenimiento.ClientID%>").val();
                                if (ValidarModelo < 1) {
                                    alert("El campo modelo no puede estar vacio para guardar este registro, favor de verificar");
                                    $("#<%=ddlSeleccionarModeloMantenimiento.ClientID%>").css("border-color", "red");
                                    return false;
                                }
                                else {
                                    //TIPO DE SUPLIDOR
                                    var ValidarTipoSuplidor = $("#<%=ddlSeleccionarTipoSuplidorMantenimiento.ClientID%>").val();
                                    if (ValidarTipoSuplidor < 1) {
                                        alert("El campo tipo de suplidor no puede estar vacio para guardar este registro, favor de verficar");
                                        $("#<%=ddlSeleccionarTipoSuplidorMantenimiento.ClientID%>").css("border-color", "red");
                                        return false;
                                    }
                                    else {
                                        //SUPLIDOR
                                        var ValidarSuplidor = $("#<%=ddlSeleccionarSuplidorMantenimiento.ClientID%>").val();
                                        if (ValidarSuplidor < 1) {
                                            alert("El campo suplidor no puede estar vacio para guardar este registro, favor de verificar");
                                            $("#<%=ddlSeleccionarSuplidorMantenimiento.ClientID%>").css("border-color", "red");
                                            return false;
                                        }
                                        else {
                                            //PRECIO DE COMPRA
                                            var ValidarPrecioCompra = $("#<%=txtPrecioCompraMantenimiento.ClientID%>").val().length;
                                            if (ValidarPrecioCompra < 1) {
                                                alert("El campo precio de compra no puede estar vacio para guardar este registro, favor de verificar");
                                                $("#<%=txtPrecioCompraMantenimiento.ClientID%>").css("border-color", "red");
                                                return false;
                                            }
                                            else {
                                                //PRECIO DE VENTA
                                                var ValidarPrecioVenta = $("#<%=txtPrecioVentaMantenimiento.ClientID%>").val().length;
                                                if (ValidarPrecioVenta < 1) {
                                                    alert("El campo precio de venta no puede estar vacio para guardar este registro, favor de verificar");
                                                    $("#<%=txtPrecioVentaMantenimiento.ClientID%>").css("border-color", "red");
                                                    return false;
                                                }
                                                else {
                                                    //STOCK
                                                    var ValidarStock = $("#<%=txtStockMantenimiento.ClientID%>").val().length;
                                                    if (ValidarStock < 1) {
                                                        alert("El campo Stock no puede estar vacio para gaurdar este registro, favor de verificar");
                                                        $("#<%=txtStockMantenimiento.ClientID%>").css("border-color", "red");
                                                        return false;
                                                    }
                                                    else {
                                                        //STOCK MINIMO
                                                        var ValidarStockMinimo = $("#<%=txtStockMinimoMantenimiento.ClientID%>").val().length;
                                                        if (ValidarStockMinimo < 1) {
                                                            alert("El campo Stock Minimo no puede estar vacio para guardar este registro, favor de verificar");
                                                            $("#<%=txtStockMinimoMantenimiento.ClientID%>").css("border-color", "red");
                                                            return false;
                                                        }
                                                        else {
                                                            //PORCIENTO DE DESCUENTO
                                                            var PorcientoDescuento = $("#<%=txtPorcientoDescuentoMantenimiento.ClientID%>").val().length;
                                                            if (PorcientoDescuento < 1) {
                                                                alert("El campo porciento de descuento no puede estar vacio para guardar este registro, favor de verificar");
                                                                $("#<%=txtPorcientoDescuentoMantenimiento.ClientID%>").css("border-color", "red");
                                                                return false;
                                                            }
                                                            else {
                                                                //COLOR
                                                                var ValidarColor = $("#<%=ddlSeleccionarColorMantenimiento.ClientID%>").val();
                                                                if (ValidarColor < 1) {
                                                                    alert("El campo color no puede estar vacio para guardar este registro, favor de verificar");
                                                                    $("#<%=ddlSeleccionarColorMantenimiento.ClientID%>").css("border-color", "red");
                                                                    return false;
                                                                }
                                                                else {
                                                                    //CONDICON
                                                                    var ValidarCondicion = $("#<%=ddlSeleccionarCondicionMantenimiento.ClientID%>").val();
                                                                    if (ValidarCondicion < 1) {
                                                                        alert("El campo color no puede estar vacio para guardar este registro, favor de veriricar");
                                                                        $("#<%=ddlSeleccionarCondicionMantenimiento.ClientID%>").css("border-color", "red");
                                                                        return false;
                                                                    }
                                                                    else {
                                                                        //CAPACIDAD
                                                                        var ValidarCapacidad = $("#<%=ddlSeleccionarCapacidadMantenimiento.ClientID%>").val();
                                                                        if (ValidarCapacidad < 1) {
                                                                            alert("El campo capacidad no puede estar vacio para guardar este registro, favor de verificar");
                                                                            $("#<%=ddlSeleccionarCapacidadMantenimiento.ClientID%>").css("border-color", "red");
                                                                            return false;
                                                                        }
                                                                        else { }
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

            //VALIDAMOS LOS CAMPOS EN EL BOTON MODIFICAR
            $("#<%=btnModificarMantenimiento.ClientID%>").click(function () {
                //TIPO DE PRODUCTO
                var ValidarTipoProducto = $("#<%=ddlSeleccionarTipoProductoMantenimiento.ClientID%>").val();
                if (ValidarTipoProducto < 1) {
                    alert("El campo tipo de producto no puede estar vacio para guardar este registro, favor de verificar");
                    $("#<%=ddlSeleccionarTipoProductoMantenimiento.ClientID%>").css("border-color", "red");
                    return false;
                }
                else {
                    //CATEGORIA
                    var ValidarCategoria = $("#<%=ddlSeleccionarCategoriaMantenimiento.ClientID%>").val();
                    if (ValidarCategoria < 1) {
                        alert("EL campo categoria no puede estar vacio para guardar un nuevo registro, favor de verificar");
                        $("#<%=ddlSeleccionarCategoriaMantenimiento.ClientID%>").css("border-color", "red");
                        return false;
                    }
                    else {
                        //UNIDAD DE MEDIDA
                        var ValidarUnidadMedida = $("#<%=ddlSeleccionarUnidadMedidaMantenimiento.ClientID%>").val();
                        if (ValidarUnidadMedida < 1) {
                            alert("El campo unidad de medida no puede estar vacio para gaurdar este registro, favor de verificar");
                            $("#<%=ddlSeleccionarUnidadMedidaMantenimiento.ClientID%>").css("border-color", "red");
                            return false;
                        }
                        else {
                            //MARCA
                            var ValidarMarca = $("#<%=ddlSeleccionarMarcaMantenimiento.ClientID%>").val();
                            if (ValidarMarca < 1) {
                                alert("El campo marca no puede estar vacio para guardar este registro, favor de verificar");
                                $("#<%=ddlSeleccionarMarcaMantenimiento.ClientID%>").css("border-color", "red");
                                return false;
                            }
                            else {
                                //MODELO
                                var ValidarModelo = $("#<%=ddlSeleccionarModeloMantenimiento.ClientID%>").val();
                                if (ValidarModelo < 1) {
                                    alert("El campo modelo no puede estar vacio para guardar este registro, favor de verificar");
                                    $("#<%=ddlSeleccionarModeloMantenimiento.ClientID%>").css("border-color", "red");
                                    return false;
                                }
                                else {
                                    //TIPO DE SUPLIDOR
                                    var ValidarTipoSuplidor = $("#<%=ddlSeleccionarTipoSuplidorMantenimiento.ClientID%>").val();
                                    if (ValidarTipoSuplidor < 1) {
                                        alert("El campo tipo de suplidor no puede estar vacio para guardar este registro, favor de verficar");
                                        $("#<%=ddlSeleccionarTipoSuplidorMantenimiento.ClientID%>").css("border-color", "red");
                                        return false;
                                    }
                                    else {
                                        //SUPLIDOR
                                        var ValidarSuplidor = $("#<%=ddlSeleccionarSuplidorMantenimiento.ClientID%>").val();
                                        if (ValidarSuplidor < 1) {
                                            alert("El campo suplidor no puede estar vacio para guardar este registro, favor de verificar");
                                            $("#<%=ddlSeleccionarSuplidorMantenimiento.ClientID%>").css("border-color", "red");
                                            return false;
                                        }
                                        else {
                                            //PRECIO DE COMPRA
                                            var ValidarPrecioCompra = $("#<%=txtPrecioCompraMantenimiento.ClientID%>").val().length;
                                            if (ValidarPrecioCompra < 1) {
                                                alert("El campo precio de compra no puede estar vacio para guardar este registro, favor de verificar");
                                                $("#<%=txtPrecioCompraMantenimiento.ClientID%>").css("border-color", "red");
                                                return false;
                                            }
                                            else {
                                                //PRECIO DE VENTA
                                                var ValidarPrecioVenta = $("#<%=txtPrecioVentaMantenimiento.ClientID%>").val().length;
                                                if (ValidarPrecioVenta < 1) {
                                                    alert("El campo precio de venta no puede estar vacio para guardar este registro, favor de verificar");
                                                    $("#<%=txtPrecioVentaMantenimiento.ClientID%>").css("border-color", "red");
                                                    return false;
                                                }
                                                else {
                                                    //STOCK
                                                    var ValidarStock = $("#<%=txtStockMantenimiento.ClientID%>").val().length;
                                                    if (ValidarStock < 1) {
                                                        alert("El campo Stock no puede estar vacio para gaurdar este registro, favor de verificar");
                                                        $("#<%=txtStockMantenimiento.ClientID%>").css("border-color", "red");
                                                        return false;
                                                    }
                                                    else {
                                                        //STOCK MINIMO
                                                        var ValidarStockMinimo = $("#<%=txtStockMinimoMantenimiento.ClientID%>").val().length;
                                                        if (ValidarStockMinimo < 1) {
                                                            alert("El campo Stock Minimo no puede estar vacio para guardar este registro, favor de verificar");
                                                            $("#<%=txtStockMinimoMantenimiento.ClientID%>").css("border-color", "red");
                                                            return false;
                                                        }
                                                        else {
                                                            //PORCIENTO DE DESCUENTO
                                                            var PorcientoDescuento = $("#<%=txtPorcientoDescuentoMantenimiento.ClientID%>").val().length;
                                                            if (PorcientoDescuento < 1) {
                                                                alert("El campo porciento de descuento no puede estar vacio para guardar este registro, favor de verificar");
                                                                $("#<%=txtPorcientoDescuentoMantenimiento.ClientID%>").css("border-color", "red");
                                                                return false;
                                                            }
                                                            else {
                                                                //COLOR
                                                                var ValidarColor = $("#<%=ddlSeleccionarColorMantenimiento.ClientID%>").val();
                                                                if (ValidarColor < 1) {
                                                                    alert("El campo color no puede estar vacio para guardar este registro, favor de verificar");
                                                                    $("#<%=ddlSeleccionarColorMantenimiento.ClientID%>").css("border-color", "red");
                                                                    return false;
                                                                }
                                                                else {
                                                                    //CONDICON
                                                                    var ValidarCondicion = $("#<%=ddlSeleccionarCondicionMantenimiento.ClientID%>").val();
                                                                    if (ValidarCondicion < 1) {
                                                                        alert("El campo color no puede estar vacio para guardar este registro, favor de veriricar");
                                                                        $("#<%=ddlSeleccionarCondicionMantenimiento.ClientID%>").css("border-color", "red");
                                                                        return false;
                                                                    }
                                                                    else {
                                                                        //CAPACIDAD
                                                                        var ValidarCapacidad = $("#<%=ddlSeleccionarCapacidadMantenimiento.ClientID%>").val();
                                                                        if (ValidarCapacidad < 1) {
                                                                            alert("El campo capacidad no puede estar vacio para guardar este registro, favor de verificar");
                                                                            $("#<%=ddlSeleccionarCapacidadMantenimiento.ClientID%>").css("border-color", "red");
                                                                            return false;
                                                                        }
                                                                        else {
                                                                            //VALIDAMOS LA CLAVE DE SEGURIDAD
                                                                            var ValidarClaveSeguridad = $("#<%=txtclaveSeguridadMantenimiento.ClientID%>").val().length;
                                                                            if (ValidarClaveSeguridad < 1) {
                                                                                alert("El campo clave de seguridad no puede estar vacio para modificar este registro, favor de verificar");
                                                                                $("#<%=txtclaveSeguridadMantenimiento.ClientID%>").css("border-color", "red");
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
                }
            });

            $("#<%=btnEliminarMantenimiento.ClientID%>").click(function () {
                var ValidarClaveSeguridad = $("#<%=txtclaveSeguridadMantenimiento.ClientID%>").val().length;

                if (ValidarClaveSeguridad < 1) {
                    alert("El campo clave de seguridad no puede estar vacio para eliminar este registro, favor de verificar.");
                    $("#<%=txtclaveSeguridadMantenimiento.ClientID%>").css("border-color", "red");
                    return false;
                }
            });

            $("#<%=btnProcesarSuplirSacar.ClientID%>").click(function () {
                var ValidarCantidaSuplir = $("#<%=txtCantidadSuplir.ClientID%>").val().length;
                if (ValidarCantidaSuplir < 1) {
                    alert("El campo Cantidad no puede estar vacio, favor de verificar.");
                    $("#<%=txtCantidadSuplir.ClientID%>").css("border-color", "red");
                    return false;
                }
                else {
                    var ValidarClaveSeguridadSuplir = $("#<%=txtClaveSeguridadSuplir.ClientID%>").val().length;
                if (ValidarClaveSeguridadSuplir < 1) {
                    alert("El campo clave de seguridad no puede estar vacio, favor de verificar.");
                    $("#<%=txtClaveSeguridadSuplir.ClientID%>").css("border-color", "red");
                    return false;
                }
                }
            });

            $("#<%=btnCambiarEstatus.ClientID%>").click(function () {
                var ValidarClaveseguridadCambioEstatus = $("#<%=txtClaveSeguridadCambioEstatus.ClientID%>").val().length;
                if (ValidarClaveseguridadCambioEstatus < 1) {
                    alert("El campo clave de seguridad no puede estar vacio, para procesar este producto, favor de verificar");
                    $("#<%=txtClaveSeguridadCambioEstatus.ClientID%>").css("border-color", "red");
                    return false;
                }
            });

            $("#<%=btnEliminarTodoHistorialFueraStock.ClientID%>").click(function () {
                var ValidarClaveSeguridadConsulta = $("#<%=txtClaveSeguridadConsulta.ClientID%>").val().length;
                if (ValidarClaveSeguridadConsulta < 1) {
                    alert("El campo clave de seguridad no puede estar vacio para realizar este proceso, favor de verificar.");
                    $("#<%=txtClaveSeguridadConsulta.ClientID%>").css("border-color", "red");
                    return false;
                }
            });
        })
    </script>

    <div id="divBloqueConsulta" runat="server">
        <div class="container-fluid">
         <br /><br />
            <div class="form-check-inline">
                <div class="form-group form-check">
                    <asp:CheckBox ID="cbAgregarRangoFecha" runat="server" Text="Agregar Rango de Fecha" CssClass="form-check-input Letranegrita" ToolTip="Agregar Rango de Fecha a la Consulta" AutoPostBack="true" OnCheckedChanged="cbAgregarRangoFecha_CheckedChanged" />
                    <asp:CheckBox ID="cbMostrarTodoHistorialVenta" runat="server" Text="Mostrar todo el Inventario (Reporte)" AutoPostBack="true" OnCheckedChanged="cbMostrarTodoHistorialVenta_CheckedChanged" ToolTip="Mostrar Todo el Inventario en el reprte" CssClass="form-check-input Letranegrita" />
                    <asp:CheckBox ID="cbProductosVendisodDescartados" runat="server" Text="Productos Vendididos / Descartados" ToolTip="Mostrar el Historial de Productos Vendidos y Descartados" CssClass="form-check-input Letranegrita" AutoPostBack="true" OnCheckedChanged="cbProductosVendisodDescartados_CheckedChanged" />
                    <asp:CheckBox ID="cbELiminarProductosVendidosDescartados" runat="server" CssClass="Letranegrita" Text="Eliminar Productos fuera de Stock" ToolTip="Sacar todos los prductos que esten fuera del inventario para aligerar el inventario general" Visible="false" AutoPostBack="true" OnCheckedChanged="cbELiminarProductosVendidosDescartados_CheckedChanged" />
                </div>
            </div>
            <br />
            <div id="DivBloqueEliminarProductosDescartados" visible="false" runat="server">
                <div class="form-row">
                    <div class="form-group col-md-4">
                        <asp:Label ID="lbClaveSeguridadConsulta" runat="server" Text="Ingresar Clave de Seguridad" CssClass="Letranegrita"></asp:Label>
                        <asp:TextBox ID="txtClaveSeguridadConsulta" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox><br />
                        <asp:Button ID="btnEliminarTodoHistorialFueraStock" runat="server" Text="Eliminar" ToolTip="Eliminar Todos los registros fuera de inventario" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnEliminarTodoHistorialFueraStock_Click" />
                    </div>
                </div>
               
            </div>
            <br />
            <asp:Label ID="lbExportarA" runat="server" Text="Exportar A:" CssClass="Letranegrita"></asp:Label>
            <div class="form-check-inline">
                <div class="form-group form-check">
                    <asp:RadioButton ID="rbExportarPDF" runat="server" Text="PDF" ToolTip="Exportar Información a PDF" CssClass="form-check-input Letranegrita" GroupName="Exportar" />
                    <asp:RadioButton ID="rbExportarExcel" runat="server" Text="Excel" ToolTip="Exportar Información a Excel" CssClass="form-check-input Letranegrita" GroupName="Exportar" />
                    <asp:RadioButton ID="rbExportarWord" runat="server" Text="Word" ToolTip="Exportar Información a Word" CssClass="form-check-input Letranegrita" GroupName="Exportar" />
                    <asp:RadioButton ID="rbExportarTXT" runat="server" Text="TXT" ToolTip="Exportar Información a TXT" CssClass="form-check-input Letranegrita" GroupName="Exportar" />
                    <asp:RadioButton ID="rbExportarCSV" runat="server" Text="CSV" ToolTip="Exportar Información a CSV" CssClass="form-check-input Letranegrita" GroupName="Exportar" />
                </div>
            </div>
            <div class="form-row">
                <div class="form-group col-md-6">
                    <asp:Label ID="lbFechaDesdeConsulta" runat="server" Text="Fecha Desde" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtFechaDesdeConsulta" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                </div>

                <div class="form-group col-md-6">
                    <asp:Label ID="lbFechaHastaConsulta" runat="server" Text="Fecha Hasta" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtFechaHAstaConsulta" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-3">
                    <asp:Label ID="lbDescripcionConsulta" runat="server" Text="Descripción" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtDescripcionConsulta" runat="server" MaxLength="200" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-3">
                    <asp:Label ID="lbCodigoBarraCOnsulta" runat="server" Text="Codigo de Barra" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtCodigoBarra" runat="server" MaxLength="100" AutoPostBack="true" OnTextChanged="txtCodigoBarra_TextChanged" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-3">
                    <asp:Label ID="lbRefeenciaConsulta" runat="server" Text="Referencia" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtReferenciaConsulta" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-3">
                    <asp:Label ID="lbNumeroSeguimientoConsulta" runat="server" Text="Numero de Seguimiento" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtNumeroSeguimientoConsulta" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-3">
                    <asp:Label ID="lbSeleccionarTipoProductoConsulta" runat="server" Text="Seleccionar Tipo de Producto" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarTipoProductoCOnsulta" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarTipoProductoCOnsulta_SelectedIndexChanged" ToolTip="Seleccionar Tipo de Producto" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group col-md-3">
                    <asp:Label ID="lbSeleccionarCategoriaConsulta" runat="server" Text="Seleccionar Categoria" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarCategoria" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarCategoria_SelectedIndexChanged" ToolTip="Seleccionar Categoria" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group col-md-3">
                    <asp:Label ID="lbSeleccionarMarcaConsulta" runat="server" Text="Seleccionar Marca" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarMarcaConsulta" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarMarcaConsulta_SelectedIndexChanged" ToolTip="Seleccionar Marca" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group col-md-3">
                    <asp:Label ID="lbSeleccionarModeloConsulta" runat="server" Text="Seleccionar Modelo" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarModelosConsulta" runat="server" ToolTip="Seleccionar Modelos" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group col-md-3">
                    <asp:Label ID="lbSeleccionarUnidadMedida" runat="server" Text="Seleccionar Unidad de Medida" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarUnidadMedida" runat="server" ToolTip="Seleccionar Unidad de Medida" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <!--BOTONES DE LA PANTALLA DE CONSULTA-->
            <div align="center">
                <asp:Button ID="btnConsultarRegistrosConsulta" runat="server" Text="Consultar" ToolTip="Consultar Registros" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnConsultarRegistros_Click" />
                <asp:Button ID="btnNuevoConsulta" runat="server" Text="Nuevo" ToolTip="Crear Nuevo Registro" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnNuevoConsulta_Click" />
                <asp:Button ID="btnModificarConsulta" runat="server" Text="Modificar" ToolTip="Modificar Registros Seleccionados" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnModificarConsulta_Click" />
                <asp:Button ID="btnEliminarConsulta" runat="server" Text="Eliminar" ToolTip="Eliminar Registros Seleccionados" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnEliminarConsulta_Click" />
                <button type="button" id="btnSuplirConsulta" runat="server" class="btn btn-outline-secondary btn-sm Custom" data-toggle="modal" data-target=".SacarSuplirProductos">Suplir</button>
                <asp:Button ID="btnExportarConsulta" runat="server" Text="Exportar" ToolTip="Exportar Registros" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnExportarConsulta_Click" />
                <button type="button" id="btnDescartarConsulta" runat="server" class="btn btn-outline-secondary btn-sm Custom" data-toggle="modal" data-target=".DescartarProductos">Descartar</button>
                <asp:Button ID="btnRestablecerPantallaConsulta" runat="server" Text="Restablecer" ToolTip="Restablecer la Pantalla" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnRestablecerPantallaConsulta_Click" />

                <br />
                <asp:CheckBox ID="cbGraficarConsulta" runat="server" Text="Graficar" ToolTip="Graficar Consulta" AutoPostBack="true" OnCheckedChanged="cbGraficarConsulta_CheckedChanged" />
                <hr />
                <asp:Label ID="lbCantidadRegistrosConsultaTitulo" runat="server" Text="Cantidad de Registros (" CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadRegistrosConsultaVariable" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadRegistrosConsultaCerrar" runat="server" Text=" )" CssClass="Letranegrita"></asp:Label>

                <asp:Label ID="lbCantidadInventidoTitulo" runat="server" Text="Capital Invertido (" CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadInventidoVariable" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadInventidoCerrar" runat="server" Text=" )" CssClass="Letranegrita"></asp:Label>

                <asp:Label ID="lbGananciaAproximadaTitulo" runat="server" Text="Ganancia Aproximada (" CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbGananciaAproximadaVariable" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbGananciaAproximadaCerrar" runat="server" Text=" )" CssClass="Letranegrita"></asp:Label>
            </div>
            <br />
             

            <br />
            <!--INICIO DEL REPEATER-->
                <div>                
                <div class="table-responsive mT20">
                    <table class="table table-striped table-hover">
                        <thead>
                            <tr>
                               <th style="width:5%">
                                    <asp:Label ID="lbSelect" runat="server" Text="Select" CssClass="Letranegrita"></asp:Label>
                                <th style="width:35%">
                                    <asp:Label ID="lbProductoHeader" runat="server" Text="Producto" CssClass="Letranegrita"></asp:Label>
                                </th>
                                <th style="width:20%">
                                    <asp:Label ID="lbTipoProductoHeader" runat="server" Text="Tipo Producto" CssClass="Letranegrita"></asp:Label>
                                </th>
                                <th style="width:20%">
                                    <asp:Label ID="lbCategoriaHeader" runat="server" Text="Categoria" CssClass="Letranegrita"></asp:Label>
                                </th>
                                <th style="width:10%">
                                    <asp:Label ID="lbStockHeader" runat="server" Text="Stock" CssClass="Letranegrita"></asp:Label>
                                </th>
                                <th style="width:10%">
                                    <asp:Label ID="lbPrecio" runat="server" Text="Precio" CssClass="Letranegrita"></asp:Label>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="RVListadoProducto" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <asp:HiddenField ID="hfIdProducto" runat="server" Value='<%# Eval("IdProducto") %>' />
                                        <asp:HiddenField ID="hfNumeroConector" runat="server" Value='<%# Eval("NumeroConector") %>' />
                                           <td style="width:5%"><asp:Button ID="btnSeleccionarRegistro" runat="server" Text="Seleccionar" ToolTip="Seleccionar registo" OnClick="btnSeleccionarRegistro_Click" CssClass="btn btn-outline-secondary btn-sm" /> </th> </td>
                                           <td style="width:35%"><%#Eval("Producto") %></td>
                                           <td style="width:20%"><%#Eval("TipoProducto") %></td>
                                           <td style="width:20%"><%#Eval("Categoria") %></td>
                                           <td style="width:10%"><%#string.Format("{0:n0}", Eval("Stock")) %></td>
                                           <td style="width:10%"><asp:Label ID="lbPrecioVentaRepeater" runat="server" Text='<%#string.Format("{0:n2}", Eval("PrecioVenta")) %>'></asp:Label> </td>
                                           
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
                </div>
            <div align="center">
                    <asp:Label ID="lbPaginaActualTitulo" runat="server" Text="Pagina " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbNumeroVariable" runat="server" CssClass="Letranegrita" Text=" 0 "></asp:Label>
                    <asp:Label ID="lbDeTitulo" runat="server" CssClass="Letranegrita" Text="De "></asp:Label>
                    <asp:Label ID="lbCantidadPaginasVariable" CssClass="Letranegrita" runat="server" Text=" 0 "></asp:Label>
                </div>
            <!--FIN DEL REPEATER-->

            <!--INICIO DE LA PAGINACION-->
                <div id="divPaginacion" runat="server" align="center">
                 <div style="margin-top: 20px;">
                    <table style="width: 600px;">
                        <tr>
                            <td>
                                <asp:LinkButton ID="lbPrimeraPagina" runat="server" CssClass="btn btn-outline-success btn-sm" OnClick="lbFirst_Click" >Primero</asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbPaginaAnterior" runat="server" CssClass="btn btn-outline-success btn-sm" OnClick="lbPrevious_Click" >Atras</asp:LinkButton>
                            </td>
                            <td>
                                <asp:DataList ID="rptPaging" runat="server"
                                    OnItemCommand="rptPaging_ItemCommand"
                                    OnItemDataBound="rptPaging_ItemDataBound" RepeatDirection="Horizontal">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbPaging" runat="server"
                                            CommandArgument='<%# Eval("PageIndex") %>' CommandName="newPage"
                                            Text='<%# Eval("PageText") %> ' Width="20px"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbPaginaGuguiente" runat="server" CssClass="btn btn-outline-success btn-sm" OnClick="lbNext_Click" >Siguiente</asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbUltimaPagina" runat="server" CssClass="btn btn-outline-success btn-sm" OnClick="lbLast_Click" >Ultimo</asp:LinkButton>
                            </td>
                            
                        </tr>
                    </table>

                </div>
             </div>
            <!--FIN DE LA PAGINACION-->


            <br />
            <!--GRAFICO DE TIPO DE PRODUCTOS-->
       <div align="center">
            <div id="divTipoProducto" runat="server"  >

             <asp:Label ID="lbGraTipoProducto" runat="server"  Text="Estadistica de Tipo de Producto" CssClass="Letranegrita"></asp:Label>
             <br />
            <asp:Chart ID="GraTipoProductos" Width="800px" runat="server" Palette="Excel">
           <Series>
               <asp:Series Name="Serie" XValueMember="1" YValueMembers="2" IsValueShownAsLabel="true" ChartType="Pie"></asp:Series>
              
           </Series>
                <Legends>
                    <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="false" Name="Default" LegendStyle="Row"></asp:Legend>
                </Legends>
           <ChartAreas>
               <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
              
           </ChartAreas>
                <BorderSkin SkinStyle="Emboss" />
       </asp:Chart>
        </div>
       </div>


        <div align="center">
            <div id="divGraficoMarcas"  runat="server" align="center" >

             <asp:Label ID="lbGraficoMarcas" runat="server"  Text="Top 10 Marcas" CssClass="Letranegrita"></asp:Label>
             <br />
            <asp:Chart ID="GraMarcas" Width="800px" runat="server" Palette="Excel">
           <Series>
               <asp:Series Name="Serie" XValueMember="1" YValueMembers="2" IsValueShownAsLabel="true" YValuesPerPoint="6"></asp:Series>
           </Series>
                 <Legends>
                    <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="false" Name="Default" LegendStyle="Row"></asp:Legend>
                </Legends>
           <ChartAreas>
               <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
           </ChartAreas>
                <BorderSkin SkinStyle="Emboss" />
       </asp:Chart>
        </div>
        </div>

            
        <div align="center">
            <div id="divGraficoServicios" runat="server" align="center" >

             <asp:Label ID="lbGraficarServicios" runat="server"  Text="Top 10 Servicios" CssClass="Letranegrita"></asp:Label>
             <br />
            <asp:Chart ID="GraServicios" Width="800px" runat="server" Palette="Excel">
           <Series>
               <asp:Series Name="Serie" XValueMember="1" YValueMembers="2" IsValueShownAsLabel="true" Label="#VAL{N}"></asp:Series>
           </Series>
                 <Legends>
                    <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="false" Name="Default" LegendStyle="Row"></asp:Legend>
                </Legends>
           <ChartAreas>
               <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
           </ChartAreas>
                <BorderSkin SkinStyle="Emboss" />
       </asp:Chart>
        </div>
        </div>
        </div>
    </div>

    <div id="divBloqueMantenimiento" runat="server">
        <br /><br />
        <asp:Label ID="lbIdProductoSeleccionado" runat="server" Text="IdProducto" Visible="false"></asp:Label>
                <asp:Label ID="lbNumeroConectorProducto" runat="server" Text="NumeroConector" Visible="false"></asp:Label>
        <div class="container-fluid">
         
            <div class="form-row">
                <div class="form-group col-md-4">
                    <asp:Label ID="lbSeleccionarTipoProductoMantenimiento" runat="server" Text="Seleccionar Tipo de Producto" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarTipoProductoMantenimiento" runat="server" ToolTip="Seleccionar Tipo de Producto" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarTipoProductoMantenimiento_SelectedIndexChanged"></asp:DropDownList>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbSeleccionarCategoriaMantenimiento" runat="server" Text="Seleccionar Categoria" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarCategoriaMantenimiento" runat="server" ToolTip="Seleccionar Categoria" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarCategoriaMantenimiento_SelectedIndexChanged"></asp:DropDownList>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbSeleccionarUnidadMedidaMantenimiento" runat="server" Text="Seleccionar Unidad de Medida" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarUnidadMedidaMantenimiento" runat="server" ToolTip="Seleccionar Unidad de Medida" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbSeleccionarMarcaMantenimiento" runat="server" Text="Seleccionar Marca" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarMarcaMantenimiento" runat="server" ToolTip="Seleccionar Marca" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarMarcaMantenimiento_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbSeleccionarModeloMantenimiento" runat="server" Text="Seleccionar Modelo" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarModeloMantenimiento" runat="server" ToolTip="Seleccionar Modelo" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarModeloMantenimiento_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbSeleccionarTipoSuplidorMantenimiento" runat="server" Text="Seleccionar Tipo de Suplidor" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarTipoSuplidorMantenimiento" runat="server" ToolTip="Seleccionar Tipo de Suplidor" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbSeleccionarSuplidorMantenimiento" runat="server" Text="Seleccionar Suplidor" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarSuplidorMantenimiento" runat="server" ToolTip="Seleccionar Suplidor" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbDescripcionMantenimiento" runat="server" Text="Descripción" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtDescripcionMantenimiento" runat="server" AutoCompleteType="Disabled" CssClass="form-control" MaxLength="200"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbCodigoBarraMantenimiento" runat="server" Text="Codigo de Barra" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtCodigoBarraMantenimiento" runat="server" AutoCompleteType="Disabled" CssClass="form-control" MaxLength="100"></asp:TextBox>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbReferenciaMantenimiento" runat="server" Text="Referencia" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtReferenciaMantenimiento" runat="server" AutoCompleteType="Disabled" CssClass="form-control" MaxLength="100"></asp:TextBox>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbPrecioCompraMantenimiento" runat="server" Text="Precio de Compra" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtPrecioCompraMantenimiento" runat="server" AutoCompleteType="Disabled" CssClass="form-control" TextMode="Number" Step="0.01"></asp:TextBox>

                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbPrecioVentaMantenimiento" runat="server" Text="Precio de Venta" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtPrecioVentaMantenimiento" runat="server" AutoCompleteType="Disabled"   CssClass="form-control" TextMode="Number" Step="0.01"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbStockMantenimiento" runat="server" Text="Stock" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtStockMantenimiento" runat="server" AutoCompleteType="Disabled" CssClass="form-control" TextMode="Number"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbStockMinimoMantenimiento" runat="server" Text="Stock Minimo" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtStockMinimoMantenimiento" runat="server" AutoCompleteType="Disabled" CssClass="form-control" TextMode="Number"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbPorcientoDescuentoMantenimiento" runat="server" Text="% de Descuento" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtPorcientoDescuentoMantenimiento" runat="server" AutoCompleteType="Disabled" CssClass="form-control" TextMode="Number" Step="0.01"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbNumeroSeguimientoMantenimiento" runat="server" Text="Numero de Seguimiento" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtNumeroSeguimientoMantenimiento" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbSeleccionarColorMantenimiento" runat="server" Text="Seleccionar Color" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarColorMantenimiento" runat="server" ToolTip="Seleccionar Color" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbSeleccionarCondicionMantenimiento" runat="server" Text="Seleccionar Condición" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarCondicionMantenimiento" runat="server" ToolTip="Seleccionar Condicion" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbSeleccionarCapacidadMantenimiento" runat="server" Text="Seleccionar Capacidad" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarCapacidadMantenimiento" runat="server" ToolTip="Seleccionar Capacidad" CssClass="form-control"></asp:DropDownList>
                </div>

                 <div class="form-group col-md-12">
                    <asp:Label ID="lbComentarioMantenimiento" runat="server" Text="Comentario" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtComentarioMantenimiento" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                </div>
                 <div class="form-group col-md-6">
                    <asp:Label ID="lbClaveSeguridadMantenimiento" runat="server" Text="Clave de Seguridad" Visible="false" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtclaveSeguridadMantenimiento" runat="server" TextMode="Password" Visible="false" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                </div>

               

            </div>
            <div class="form-check-inline">
                <div class="form-group form-check">
                    <asp:CheckBox ID="cbProductoAcumulativoMantenimiento" runat="server" Text="Producto Acumulativo" AutoPostBack="true" OnCheckedChanged="cbProductoAcumulativoMantenimiento_CheckedChanged" CssClass="form-check-input" ToolTip="Establecer si el producto es acumulativo" />
                    <asp:CheckBox ID="cbAplicaImpuestoMantenimiento" runat="server" Text="Aplica para Impuesto" CssClass="form-check-input" ToolTip="Establecer si este producto aplica para impuesto" />
                    <asp:CheckBox ID="cbAgregarImagenArticulo" runat="server"  Text="Agregar Imagen" AutoPostBack="true" OnCheckedChanged="cbAgregarImagenArticulo_CheckedChanged" CssClass="form-check-input" ToolTip="Asignarle una imagen al producto" />
                    <asp:CheckBox ID="cbActualizarFotoProducto" runat="server" Text="Actualizar Imagen" CssClass="form-check-input" ToolTip="Actualizar la imagen del producto" />
                    <asp:CheckBox ID="cbNoLimpiarPantalla" runat="server" Text="No Limpiar Pantalla" CssClass="form-check-input" ToolTip="No limpiar Pantalla al momento de realizar el mantenimiento" />
                </div>
            </div>
            <hr />
            <!--ESTE ESTE BLOQUE ES PARA COLOCAR LA IMAGEN A UN PRODUCTO-->
            <div id="DivBloqueImagenProducto"   runat="server">
                <div class="container" >
                    <div class="row" >
                        <div class="col-md-4 col-md-offset-4" align="center">
                            <asp:Label ID="lbTituloImagen" runat="server" Text="Imagen de Producto" CssClass="Letranegrita"></asp:Label>
                            <br />
                            <asp:Image ID="IMGProducto" runat="server"  onmouseover="this.width=500;this.height=500;" onmouseout="this.width=400;this.height=400;" width="300" height="300" ImageUrl="~/Recursos/ImagenPorDefecto.jpg" />
                            <br />
                            <br />
                            <asp:Label ID="lbBuscarImagen" runat="server" Text="Subir Imagen" CssClass="Letranegrita"></asp:Label>
                            <asp:FileUpload ID="UpImagen" runat="server"  accept=".jpg" CssClass="form-control" />
                            <br />
                            <br />
                        </div>
                    </div>
                </div>

            </div>
            <div align="center">
                <asp:Button ID="btnProcesarMantenimiento" runat="server" Text="Guardar" ToolTip="Guardar Registro" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnProcesarMantenimiento_Click" />
                <asp:Button ID="btnModificarMantenimiento" runat="server" Text="Modificar" ToolTip="Modificar Registro" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnModificarMantenimiento_Click" />
                <asp:Button ID="btnEliminarMantenimiento" runat="server" Text="Eliminar" ToolTip="Eliminar Registro" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnEliminarMantenimiento_Click" />
                <asp:Button ID="btnVolverMantenimiento" runat="server" Text="Volver" ToolTip="Volver a la pantalla de conulta" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnVolverMantenimiento_Click" />
            </div><br />
        </div>
    </div>

    <div id="divBloqueDetalle" runat="server">
        <div class="container-fluid">
            <div align="center">
                <asp:Label ID="lbDetalleProductoSeleccionado" runat="server" Text="Detalle del Producto Seleccionado" CssClass="Letranegrita"></asp:Label>
            </div>
            <div class="form-row">
                <div class="form-group col-md-4">
                    <asp:Label ID="lbTipoProductoDetalle" runat="server" Text="Tipo de Producto" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtTipoProductoDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbCategoriaDetalle" runat="server" Text="Categoria" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtCategoriaDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbUnidadMedidaDetalle" runat="server" Text="Unidad de Medida" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtUnidadMedidaDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbMarcaDetalle" runat="server" Text="Marca" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtMarcaDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbModeloDetalle" runat="server" Text="Modelo" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtModeloDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbTipoSuplidorDetalle" runat="server" Text="Tipo de Suplidor" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtTipoSuplidorDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbSuplidorDetalle" runat="server" Text="Suplidor" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtSuplidorDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbDescripcionDetalle" runat="server" Text="Detalle" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtDescripcionDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbCodigoBarradetalle" runat="server" Text="Codigo de Barra" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtCodigoBarraDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbReferenciaDetalle" runat="server" Text="Referencia" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtReferenciaDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbPrecioCompraDetalle" runat="server" Text="Precio de Compra" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtPrecioCompraDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>

                     
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbPrecioVentaDetalle" runat="server" Text="Precio de Venta" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtPrecioVentaDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbStockDetalle" runat="server" Text="Stock" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtStockDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbStockMinimoDetalle" runat="server" Text="Stock Minimo" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtStockMinimo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbPorcientoDescuentoDetalle" runat="server" Text="Porciento de Descuento" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtPorcientoDescuentoDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbNumeroSeguimientoDetalle" runat="server" Text="Numero de Seguimiento" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtNumeroSeguimientoDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbColorDetalle" runat="server" Text="Color" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtColorDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbCondicionDetalle" runat="server" Text="Condición" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtCondcionDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbCapacidadDetalle" runat="server" Text="Capacidad" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtCapacidadDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbProductoAcumulativoDetalle" runat="server" Text="Producto Acumulativo" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtProductoAcumulativoDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>

                     
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbAplicaParaImpuestoDetalle" runat="server" Text="Aplica para impuesto" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtAplicaParaImpuestoDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-12">
                    <asp:Label ID="lbComentarioDetalle" runat="server" Text="Comentario" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtComentarioDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

            </div>
            <div id="BloqueImagenProductoSeleccionado"  runat="server">
                  <div class="container" >
                    <div class="row" >
                        <div class="col-md-4 col-md-offset-4" align="center">
                            <asp:Label ID="lbFotoProductoSeleccionado" runat="server" Text="Imagen de Producto" CssClass="Letranegrita"></asp:Label>
                            <br />
                            <asp:Image ID="imgFotoProductoSeleccionado" runat="server"  onmouseover="this.width=500;this.height=500;" onmouseout="this.width=400;this.height=400;" width="300" height="300" ImageUrl="~/Recursos/ImagenPorDefecto.jpg" />
                            <br />
                            <br />
                            <br />
                            <br />
                        </div>
                    </div>
                </div>
                <br />
             
            </div>
            <div align="center">
                <asp:Button ID="btnVolverDetalle" runat="server" Text="Volver" ToolTip="Ocultar la Información del detalle del producto seleccionado" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnVolverDetalle_Click" />
            </div>
            <br />
        </div>

    </div>

    <div id="divBloqueSuplir" runat="server">

          <div class="modal fade bd-example-modal-lg SacarSuplirProductos" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-xl" role="document">
    <div class="modal-content">
        <div class="container-fluid">
            <div class="jumbotron" align="center">
                <asp:Label ID="lbSuplirProducto" runat="server" Text="Sacar / Suplir Productos"></asp:Label>
            </div>
          <asp:ScriptManager ID="ScripManagerSuplir" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanelSuplir" runat="server">
                <ContentTemplate>
                    <div class="form-row">
                <div class="form-group col-md-4">
                    <asp:Label ID="lbTipoProductoSuplir" runat="server" Text="Tipo de Producto: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbTipoProductoSuplirDato" runat="server" Text="Dato"></asp:Label>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbCategoriaSuplir" runat="server" Text="Categoria: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbCategoriaSuplirDato" runat="server" Text="Dato"></asp:Label>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbUnidadMedidaSuplir" runat="server" Text="Unidad de Medida: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbUnidadMedidaSuplirDato" runat="server" Text="Dato"></asp:Label>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbMarcaSuplir" runat="server" Text="Marca: " CssClass="Letranegrita"></asp:Label>
                     <asp:Label ID="lbMarcaSuplirDato" runat="server" Text="Dato"></asp:Label>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbModeloSuplir" runat="server" Text="Modelo: " CssClass="Letranegrita"></asp:Label>
                     <asp:Label ID="lbModeloSuplirDato" runat="server" Text="Dato"></asp:Label>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbTipoSuplidorSuplir" runat="server" Text="Tipo de Suplidor: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbTipoSuplidorSuplirDato" runat="server" Text="Dato"></asp:Label>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbSuplidorSuplir" runat="server" Text="Suplidor: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbSuplidorSuplirDato" runat="server" Text="Dato"></asp:Label>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbDescripcionSuplir" runat="server" Text="Producto: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbDescripcionSuplirDato" runat="server" Text="Dato"></asp:Label>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbCodigoBarraSuplir" runat="server" Text="Codigo de Barra: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbCodigoBarraSuplirDato" runat="server" Text="Dato"></asp:Label>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbReferenciaSuplir" runat="server" Text="Referencia: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbReferenciaSuplirDato" runat="server" Text="Dato"></asp:Label>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbPrecioCompraSuplir" runat="server" Text="Precio de Compra: " CssClass="Letranegrita"></asp:Label>
                   <asp:Label ID="lbPrecioCompraSuplirDato" runat="server" Text="Dato"></asp:Label>

                     
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbPrecioVentaSuplir" runat="server" Text="Precio de Venta: " CssClass="Letranegrita"></asp:Label>
             <asp:Label ID="lbPrecioVentaSuplirDato" runat="server" Text="Dato"></asp:Label>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbStockSuplir" runat="server" Text="Stock: " CssClass="Letranegrita"></asp:Label>
         <asp:Label ID="lbStockSuplirDato" runat="server" Text="Dato"></asp:Label>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbStockMinimoSuplir" runat="server" Text="Stock Minimo: " CssClass="Letranegrita"></asp:Label>
              <asp:Label ID="lbStockMinimoSuplirDato" runat="server" Text="Dato"></asp:Label>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbPorcientoDescuentoSuplir" runat="server" Text="Porciento de Descuento: " CssClass="Letranegrita"></asp:Label>
             <asp:Label ID="lbPorcientoDescuentoSuplirDato" runat="server" Text="Dato"></asp:Label>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbNumeroSeguiientoSuplir" runat="server" Text="Numero de Seguimiento: " CssClass="Letranegrita"></asp:Label>
        <asp:Label ID="lbNumeroSeguiientoSuplirDato" runat="server" Text="Dato"></asp:Label>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbColorSUplir" runat="server" Text="Color: " CssClass="Letranegrita"></asp:Label>
                     <asp:Label ID="lbColorSUplirDato" runat="server" Text="Dato"></asp:Label>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbCondicionSuplir" runat="server" Text="Condición: " CssClass="Letranegrita"></asp:Label>
                     <asp:Label ID="lbCondicionSuplirDato" runat="server" Text="Dato"></asp:Label>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbCapacidadSuplir" runat="server" Text="Capacidad: " CssClass="Letranegrita"></asp:Label>
              <asp:Label ID="lbCapacidadSuplirDato" runat="server" Text="Dato"></asp:Label>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbProductoAcumulativoSuplir" runat="server" Text="Producto Acumulativo: " CssClass="Letranegrita"></asp:Label>
             <asp:Label ID="lbProductoAcumulativoSuplirDato" runat="server" Text="Dato"></asp:Label>

                     
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbAplicaImpuestoSuplir" runat="server" Text="Aplica para impuesto: " CssClass="Letranegrita"></asp:Label>
              <asp:Label ID="lbAplicaImpuestoSuplirDato" runat="server" Text="Dato"></asp:Label>
                </div>

                <div class="form-group col-md-12">
                    <asp:Label ID="lbComentarioSuplir" runat="server" Text="Comentario: " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbComentarioSuplirDato" runat="server" Text="Dato"></asp:Label>
                </div>

            </div>
                    <div class="form-check-inline">
                        <div class="form-group form-check">
                            <asp:RadioButton ID="rbSuplirProducto" runat="server" Text="Ingresar Producto" ToolTip="Ingresar Productos a Inventario" GroupName="TipoOperacion" CssClass="form-check-input Letranegrita" />
                            <asp:RadioButton ID="rbSacarProducto" runat="server" Text="Sacar Producto" ToolTip="Sacar Productos de Inventario" GroupName="TipoOperacion" CssClass="form-check-input Letranegrita" />
                        </div>
                    </div>

                    <div class="form-row">
                        <div class="form-group col-md-3">
                            <asp:Label ID="lbCantidadSuplir" runat="server" Text="Cantidad" CssClass="Letranegrita"></asp:Label>
                            <asp:TextBox ID="txtCantidadSuplir" runat="server" TextMode="Number" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>

                       <div class="form-group col-md-6">
                            <asp:Label ID="lbClaveSeguridadSuplir" runat="server" Text="Clave de Seguridad" CssClass="Letranegrita"></asp:Label>
                            <asp:TextBox ID="txtClaveSeguridadSuplir" runat="server" TextMode="Password" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>
                        </div>
                        
                        <br />
                </ContentTemplate>
            </asp:UpdatePanel>
            <div align="center">
                <asp:Button ID="btnProcesarSuplirSacar" runat="server" Text="Procesar" ToolTip="Procesar Información" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnProcesarSuplirSacar_Click" />
            </div>
            <br />


        </div>
    </div>
  </div>
</div>
    </div>

    <div id="divBloqueDescartar" runat="server">
         <div class="modal fade bd-example-modal-lg DescartarProductos" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-xl" role="document">
    <div class="modal-content">
       <div class="container">
           <div class="jumbotron" align="center">
               <asp:Label ID="lbTituloCambiarEstatus" runat="server" Text="CAMBIO DE ESTATUS DE PRODUCTO"></asp:Label><br />
               <asp:Label ID="lbEstatusProductoCambioEstatusTitulo" runat="server" Text="Estatus de Producto: "></asp:Label>
               <asp:Label  ID="lbCambioEtatusProductoCambioEstatusVariable" runat="server" Text="Dato"></asp:Label>
           </div>
           <div class="form-row">
               <div class="form-group col-md-4">
                   <asp:Label ID="lbTipoProductoCambioEstatus" runat="server" Text="Tipo de Producto: " CssClass="Letranegrita"></asp:Label>
                   <asp:Label ID="lbTipoProductoCambioEstatusDato" runat="server" Text="Dato"></asp:Label>
               </div>

                <div class="form-group col-md-4">
                   <asp:Label ID="lbCategoriaCambioEstatus" runat="server" Text="Categoria: " CssClass="Letranegrita"></asp:Label>
                   <asp:Label ID="lbCategoriaCambioEstatusDato" runat="server" Text="Dato"></asp:Label>
               </div>

                <div class="form-group col-md-4">
                   <asp:Label ID="lbUnidadMedidaCambioEstatus" runat="server" Text="Unidad de Medida: " CssClass="Letranegrita"></asp:Label>
                   <asp:Label ID="lbUnidadMedidaCambioEstatusDato" runat="server" Text="Dato"></asp:Label>
               </div>

               <div class="form-group col-md-4">
                   <asp:Label ID="lbMarcaCambioEstatus" runat="server" Text="Marca: " CssClass="Letranegrita"></asp:Label>
                   <asp:Label ID="lbMarcaCambioEstatusDato" runat="server" Text="Dato"></asp:Label>
               </div>

               <div class="form-group col-md-4">
                   <asp:Label ID="lbModeloCambioEstatus" runat="server" Text="Modelo: " CssClass="Letranegrita"></asp:Label>
                   <asp:Label ID="lbModeloCambioEstatusDato" runat="server" Text="Dato"></asp:Label>
               </div>

               <div class="form-group col-md-4">
                   <asp:Label ID="lbTipoSuplidorCambioEstatus" runat="server" Text="Tipo de Suplidor: " CssClass="Letranegrita"></asp:Label>
                   <asp:Label ID="lbTipoSuplidorCambioEstatusDato" runat="server" Text="Dato"></asp:Label>
               </div>

               <div class="form-group col-md-4">
                   <asp:Label ID="lbSuplidorCambioEstatus" runat="server" Text="Suplidor: " CssClass="Letranegrita"></asp:Label>
                   <asp:Label ID="lbSuplidorCambioEstatusDato" runat="server" Text="Dato"></asp:Label>
               </div>

               <div class="form-group col-md-4">
                   <asp:Label ID="lbProductoCambioEstatus" runat="server" Text="Producto: " CssClass="Letranegrita"></asp:Label>
                   <asp:Label ID="lbProductoCambioEstatusDato" runat="server" Text="Dato"></asp:Label>
               </div>

               <div class="form-group col-md-4">
                   <asp:Label ID="lbCodigoBarraCambioEstatus" runat="server" Text="Codigo de Barras: " CssClass="Letranegrita"></asp:Label>
                   <asp:Label ID="lbCodigoBarrasCambioEstatusDato" runat="server" Text="Dato"></asp:Label>
               </div>

               <div class="form-group col-md-4">
                   <asp:Label ID="lbReferenciaCambiEstatus" runat="server" Text="Referencia: " CssClass="Letranegrita"></asp:Label>
                   <asp:Label ID="lbReferenciaCambioEstatusDato" runat="server" Text="Dato"></asp:Label>
               </div>

               <div class="form-group col-md-4">
                   <asp:Label ID="lbPrecioCompraCambioEstatus" runat="server" Text="Precio de Compra: " CssClass="Letranegrita"></asp:Label>
                   <asp:Label ID="lbPrecioCompraCambioEstatusDato" runat="server" Text="Dato"></asp:Label>
               </div>

               <div class="form-group col-md-4">
                   <asp:Label ID="lbPrecioVentaCambioEstatus" runat="server" Text="Precio de Venta: " CssClass="Letranegrita"></asp:Label>
                   <asp:Label ID="lbPrecioVentaCambioEstatusDato" runat="server" Text="Dato"></asp:Label>
               </div>

               <div class="form-group col-md-4">
                   <asp:Label ID="lbStockCambioEstatus" runat="server" Text="Stock: " CssClass="Letranegrita"></asp:Label>
                   <asp:Label ID="lbStockCambioEstatusDato" runat="server" Text="Dato"></asp:Label>
               </div>

               <div class="form-group col-md-4">
                   <asp:Label ID="lbStockminimoCambioEstatus" runat="server" Text="Stock Minimo: " CssClass="Letranegrita"></asp:Label>
                   <asp:Label ID="lbStockMinimoCambioEstatusDato" runat="server" Text="Dato"></asp:Label>
               </div>

               <div class="form-group col-md-4">
                   <asp:Label ID="lbPorcientoDescuentoCambioEstatus" runat="server" Text="% de Descuento: " CssClass="Letranegrita"></asp:Label>
                   <asp:Label ID="lbPorcientoDescuentoCambioEstatusDato" runat="server" Text="Dato"></asp:Label>
               </div>

               <div class="form-group col-md-4">
                   <asp:Label ID="lbNumeroSeguimientoCambioEstatus" runat="server" Text="Numero de Seguimiento: " CssClass="Letranegrita"></asp:Label>
                   <asp:Label ID="lbNumeroSeguimientoCambioEstatusDato" runat="server" Text="Dato"></asp:Label>
               </div>

               <div class="form-group col-md-4">
                   <asp:Label ID="lbColorCambioEstatus" runat="server" Text="Color: " CssClass="Letranegrita"></asp:Label>
                   <asp:Label ID="lbColorCambioEstatusDato" runat="server" Text="Dato"></asp:Label>
               </div>

               <div class="form-group col-md-4">
                   <asp:Label ID="lbCondicionCambioEstatus" runat="server" Text="Condición: " CssClass="Letranegrita"></asp:Label>
                   <asp:Label ID="lbCondicionCambioEstatusDato" runat="server" Text="Dato"></asp:Label>
               </div>

                <div class="form-group col-md-4">
                   <asp:Label ID="lbCapacidadCambioEstatus" runat="server" Text="Capacidad: " CssClass="Letranegrita"></asp:Label>
                   <asp:Label ID="lbCapacidadCambioEstatusDato" runat="server" Text="Dato"></asp:Label>
               </div>

                <div class="form-group col-md-4">
                   <asp:Label ID="lbProductoAcumulativoCambioEstatus" runat="server" Text="Producto Acumulativo: " CssClass="Letranegrita"></asp:Label>
                   <asp:Label ID="lbProductoAcumulativoCambioEstatusDato" runat="server" Text="Dato"></asp:Label>
               </div>

                <div class="form-group col-md-4">
                   <asp:Label ID="lbAplicaParaImpuestoCambioEstatus" runat="server" Text="Aplica Para Impuesto: " CssClass="Letranegrita"></asp:Label>
    <asp:Label ID="lbIdAplicaParaImpuestoCambioDato" runat="server" Text="Dato"></asp:Label>
               </div>

               <div class="form-group col-md-8">
                   <asp:Label ID="lbComentarioCambioEstatus" runat="server" Text="Comentario: " CssClass="Letranegrita"></asp:Label>
                   <asp:TextBox ID="txtComentarioCambioEstatus" runat="server" CssClass="form-control" AutoCompleteType="Disabled" MaxLength="8000"></asp:TextBox>
               </div>
               <div class="form-group col-md-4">
                   <asp:Label ID="lbClaveSeguridadCambioEstatus" runat="server" Text="Clave de Seguridad: " CssClass="Letranegrita"></asp:Label>
                   <asp:TextBox ID="txtClaveSeguridadCambioEstatus" runat="server" CssClass="form-control" AutoCompleteType="Disabled" TextMode="Password"></asp:TextBox>
               </div>
           </div>
           <div class="form-check">
               <div class="form-group form-check">
                   <asp:CheckBox ID="cbEliminarRegistroDescartado" runat="server" Text="Eliminar Registro" CssClass="form-check"  ToolTip="Eliminar este registro permanentemente" />
               </div>
           </div>
           <div align="center">
               <asp:Button ID="btnCambiarEstatus" runat="server" Text="Procesar" ToolTip="Cambiar Estatus" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnCambiarEstatus_Click" />
           </div>
           <br />
       </div>
    </div>
  </div>
</div>

    </div>


</asp:Content>
