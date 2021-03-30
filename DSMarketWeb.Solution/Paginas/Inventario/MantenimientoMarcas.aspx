<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="MantenimientoMarcas.aspx.cs" Inherits="DSMarketWeb.Solution.Paginas.Inventario.MantenimientoMarcas" %>
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
        

        th {
            background-color: dodgerblue;
            color: white;
        }
    </style>

    <script type="text/javascript">
        function GuardarRegistro() {
            alert("Registro guardado con exito.");
        }
        function ModificarRegistro() {
            alert("Registro modificado con exito.");
        }
        function ClaveSeguridadErrornea() {
            alert("La clave de seguridad ingresada no es valida, favor de verificar.");
        }

        $(document).ready(function () {
            $("#<%=btnGuardarRegistro.ClientID%>").click(function () {
                var TipoProductoGuardar = $("#<%=ddlSeleccionarTipoProductoMantenimiento.ClientID%>").val();
                if (TipoProductoGuardar < 1) {
                    alert("El campo tipo de producto no puede estar vacio para guardar este registro, favor de verificar.");
                    $("#<%=ddlSeleccionarTipoProductoMantenimiento.ClientID%>").css("border-color", "red");
                    return false;
                }
                else {
                    var CategoriaGaurdar = $("#<%=ddlSeleccionarCategoriaMantenimiento.ClientID%>").val();
                    if (CategoriaGaurdar < 1) {
                        alert("El campo categoria no puede estar vacio para guardar este registro, favor de verificar.");
                        $("#<%=ddlSeleccionarCategoriaMantenimiento.ClientID%>").css("border-color", "red");
                        return false;
                    }
                    else {
                        var MarcaGuardar = $("#<%=txtMarcaMantenimiento.ClientID%>").val().length;
                        if (MarcaGuardar < 1) {
                            alert("El campo marca no puede estar vacio para guardar este registro, favor de verificar.");
                            $("#<%=txtMarcaMantenimiento.ClientID%>").css("border-color", "red");
                            return false;
                        }
                    }
                }
            });
            $("#<%=btnModificarRegstro.ClientID%>").click(function () {
                var TipoproductoModificar = $("#<%=ddlSeleccionarTipoProductoMantenimiento.ClientID%>").val();
                if (TipoproductoModificar < 1) {
                    alert("El campo tipo de producto no puede estar vacio para modificar este registro.");
                    $("#<%=ddlSeleccionarTipoProductoMantenimiento.ClientID%>").css("border-color", "red");
                    return false;
                }
                else {
                    var CategoriaModificar = $("#<%=ddlSeleccionarCategoriaMantenimiento.ClientID%>").val();
                    if (CategoriaModificar < 1) {
                        alert("El campo categoria no puede estar vacio para modificar este registro.");
                        $("#<%=ddlSeleccionarCategoriaMantenimiento.ClientID%>").css("border-color", "red");
                        return false;
                    }
                    else {
                        var MarcaModificar = $("#<%=txtMarcaMantenimiento.ClientID%>").val().length;
                        if (MarcaModificar < 1) {
                            alert("El campo marca no puede estar vacio para modificar este registro, favor de verificar.");
                            $("#<%=txtMarcaMantenimiento.ClientID%>").css("border-color", "red");
                            return false;
                        }
                        else {
                            var ClaveSeguridad = $("#<%=txtClaveSeguridad.ClientID%>").val().length;
                            if (ClaveSeguridad < 1) {
                                alert("El campo clave de seguridad no puede estar vacio para modificar este registro, favor de verificar.");
                                $("#<%=txtClaveSeguridad.ClientID%>").css("border-color", "red");
                                return false;
                            }
                        }
                    }
                }
            });
        })
        
    </script>
    <div class="container-fluid">
        <div id="DivBloqueConsulta" runat="server">
           <br /><br />

            <div class="form-row">
                <div class="form-group col-md-4">
                    <asp:Label ID="lbSeleccionarTipoProductoonsulta" runat="server" Text="Seleccionar Tipo de Producto" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarSeleccionarTipoProductoConsulta" runat="server" ToolTip="Seleccionar el tipo de producto" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarSeleccionarTipoProductoConsulta_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbSeleccionarCategoriaConsulta" runat="server" Text="Seleccionar Categoria" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarCategoriaConsulta" runat="server" ToolTip="Seleccionar Categoria" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbMarcaConsulta" runat="server" Text="Marca" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtMarcaConsulta" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div align="center">
                <asp:Button ID="btnConsultarRegistros" runat="server" Text="Consultar" ToolTip="Consultar Registros" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnConsultarRegistros_Click" />
                <asp:Button ID="btnNuevoRegistro" runat="server" Text="Nuevo" ToolTip="Crear Nuevo registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnNuevoRegistro_Click" />
                <asp:Button ID="btnModificarRegistros" runat="server" Text="Modificar" ToolTip="Modificar Registro seleccionado" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnModificarRegistros_Click" />
                <asp:Button ID="btnRestablecerPantalla" runat="server" Text="Restablecer" ToolTip="Restablecer Pantalla" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnRestablecerPantalla_Click1" />
            </div>
            <br />
            <div align="center">
                <asp:Label ID="lbCantidadRegistrosTitulo" runat="server" Text="Cantidad de Registros( " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadRegistrosVariable" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadRegistrosCerrar" runat="server" Text=" )" CssClass="Letranegrita"></asp:Label>
            </div>
            <br />
            <!--REPEATER-->
            <div class="table-responsive mT20">
                <table class="table table-striped table-hover">
                    <thead>
                        <tr>
                            <th style="width:10%" align="left"> <asp:Label ID="lbSeleccionarHeaderRepeater" runat="server" Text="Seleccionar" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:20%" align="left"> <asp:Label ID="lbTipoProducHeaderRepeater" runat="server" Text="Tipo de Producto" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:20%" align="left"> <asp:Label ID="lbCategoriaHeaderrepeater" runat="server" Text="Categoria" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:40%" align="left"> <asp:Label ID="lbMarcaHeaderRepeater" runat="server" Text="Marca" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:10%" align="left"> <asp:Label ID="lbEstatusHeaderRepeater" runat="server" Text="Estatus" CssClass="Letranegrita"></asp:Label> </th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rpListadoMarcas" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <asp:HiddenField ID="hfIdMarca" runat="server" Value='<%# Eval("IdMarca") %>' />
                            <td style="width:10%" align="left"> <asp:Button ID="btnSeleccionarRegistrosRepeater" OnClick="btnSeleccionarRegistrosRepeater_Click" runat="server" Text="Seleccionar" CssClass="btn btn-outline-secondary btn-sm" ToolTip="Seleccionar Registro" /> </td>
                            <td style="width:20%" align="left"> <asp:Label ID="lbTipoProductoBodyRepeater" runat="server" Text='<%# Eval("TipoProducto") %>'></asp:Label> </td>
                            <td style="width:20%" align="left"> <asp:Label ID="lbCategoriaBodyRepeater" runat="server" Text='<%# Eval("Categoria") %>'></asp:Label> </td>
                            <td style="width:40%" align="left"> <asp:Label ID="lbMarcaBodyRepeater" runat="server" Text='<%# Eval("Marca") %>'></asp:Label> </td>
                            <td style="width:10%" align="left"> <asp:Label ID="lbEstatusBodyRepeater" runat="server" Text='<%# Eval("Estatus") %>'></asp:Label> </td>
                        </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>

            <!--PAGINACION-->
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
                    <td> <asp:LinkButton ID="LinkPrimeraPagina" runat="server" Text="Primero" CssClass="btn btn-outline-success btn-sm" ToolTip="Ir a la primera pagina del listado" OnClick="LinkPrimeraPagina_Click"></asp:LinkButton> </td>
                    <td> <asp:LinkButton ID="LinkAnterior" runat="server" Text="Anterior" CssClass="btn btn-outline-success btn-sm" ToolTip="Ir a la pagina anterior del listado" OnClick="LinkAnterior_Click"></asp:LinkButton> </td>
                    <td>
                        <asp:DataList ID="dtPaginacion" runat="server" OnItemCommand="dtPaginacion_ItemCommand" OnItemDataBound="dtPaginacion_ItemDataBound" RepeatDirection="Horizontal">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkPaginacionCentral" runat="server" CommandArgument='<%# Eval("IndicePagina") %>' CommandName="newPage" Text='<%# Eval("TextoPagina") %>' Width="20px"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:DataList>

                    </td>
                    <td> <asp:LinkButton ID="LinkSiguiente" runat="server" Text="Siguiente" ToolTip="Ir a la siguiente pagina del listado" CssClass="btn btn-outline-success btn-sm" OnClick="LinkSiguiente_Click"></asp:LinkButton> </td>
                    <td> <asp:LinkButton ID="LinkUltimo" runat="server" Text="Ultimo" ToolTip="Ir a la ultima pagina del listado" CssClass="btn btn-outline-success btn-sm" OnClick="LinkUltimo_Click"></asp:LinkButton> </td>
                </tr>
            </table>
        </div>
        </div>
            <br />
        </div>     



        <div id="divBloqueMantenimiento" runat="server">
             <br /><br />
           <asp:Label ID="lbIdRegistroSeleccionado" runat="server" Text="ID" Visible="false"></asp:Label>

            <div class="form-row">
                <div class="form-group col-md-4">
                    <asp:Label ID="lbSeleccionarTipoProductoMantenimiento" runat="server" Text="Seleccionar Tipo de Producto" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarTipoProductoMantenimiento" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarTipoProductoMantenimiento_SelectedIndexChanged" ToolTip="Seleccionar Tipo de Producto" CssClass="form-control"></asp:DropDownList>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbSeleccionarCategoriaMantenimiento" runat="server" Text="Seleccionar Categoria" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarCategoriaMantenimiento" runat="server" ToolTip="Seleccionar Categoria" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbMarcaMantenimiento" runat="server" Text="Marca" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtMarcaMantenimiento" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                </div>
                <div class="form-group col-md-4">
                    <asp:Label ID="lbClaveSeguridad" runat="server" Text="Clave de Seguridad" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtClaveSeguridad" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-check-inline">
                <div class="form-group form-check">
                    <asp:CheckBox ID="cbEstatus" runat="server" Text="Estatus" CssClass="form-check-input" ToolTip="Estatus del registro" />
                </div>
            </div>
            <br />
            <div align="center">
                <asp:Button ID="btnGuardarRegistro" runat="server" Text="Guardar" ToolTip="Gaurdar registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnGuardarRegistro_Click" />
                <asp:Button ID="btnModificarRegstro" runat="server" Text="Modificar" ToolTip="Modificar registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnModificarRegstro_Click" />
                <asp:Button ID="btnVolver" runat="server" Text="Volver" ToolTip="Volver" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnVolver_Click" />
            </div>

        </div>

    </div>
</asp:Content>
