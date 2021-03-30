<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="MantenimientoCategorias.aspx.cs" Inherits="DSMarketWeb.Solution.Paginas.Inventario.MantenimientoCategorias" %>
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
        function RegistroGuardado() {
            alert("Registro gaurdado con exito.");
        }

        function RegistroModificado() {
            alert("Registro modificado con exito.");
        }

        function RegistroDeshabilitado() {
            alert("Registro deshabilitada con exito.");
        }
        function ClaveSeguridadIncorrecta() {
            alert("La clave de seguridad ingresada no es valida, favor de verificar.");
        }

        $(document).ready(function () {
            $("#<%=btnGuardarMantenimiento.ClientID%>").click(function () {
                var ValidarTipoProducto = $("#<%=ddlSeleccionarTipoProductoMantenimiento.ClientID%>").val();
                if (ValidarTipoProducto < 1) {
                    alert("El campo tipo de producto no puede estar vacio para realizar esta operación, favor de verificar");
                    $("#<%=ddlSeleccionarTipoProductoMantenimiento.ClientID%>").css("border-color", "red");
                    return false;
                }
                else {
                    var ValidarCategoria = $("#<%=txtCategoriaMantenimiento.ClientID%>").val().length;
                    if (ValidarCategoria < 1) {
                        alert("El campo categoria no puede estar vacio, favor de verificar.");
                        $("#<%=txtCategoriaMantenimiento.ClientID%>").css("border-color", "red");
                        return false;
                    }
                    else {
                        var ValidarClaveSeguridad = $("#<%=txtClaveSeguridadMantenimiento.ClientID%>").val().length;
                    if (ValidarClaveSeguridad < 1) {
                        alert("El campo clave de seguridad no puede estar vacio para realizar esta operación, favor de verificar");
                        $("#<%=txtClaveSeguridadMantenimiento.ClientID%>").css("border-color", "red");
                        return false;
                    }
                    }
                }
            });
        });
    </script>

    <div class="container-fluid">
        <div id="divBloqueConsulta" runat="server">
            <br /><br />
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
         <asp:Button ID="btnNuevoRegistro" runat="server" Text="Nuevo" CssClass="btn btn-outline-secondary btn-sm" ToolTip="Crear nuevo registro" OnClick="btnNuevoRegistro_Click" />
         <asp:Button ID="btnModificarRegistroSeleccionado" runat="server" Text="Modificar" CssClass="btn btn-outline-secondary btn-sm" ToolTip="Modificar Registro Seleccionado" OnClick="btnModificarRegistroSeleccionado_Click" />
         <asp:Button ID="btnRestabelcer" runat="server" Text="Restablecer" ToolTip="Restablecer" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnRestabelcer_Click" />
        </div>
        <br />
        <div align="center">
            <asp:Label ID="lbCantidadRegistrosTitulo" runat="server" Text="Cantidad de Registros (" CssClass="Letranegrita"></asp:Label>
            <asp:Label ID="lbCantidadRegistrosVariable" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
            <asp:Label ID="lbCantidadRegistroscerrar" runat="server" Text=")" CssClass="Letranegrita"></asp:Label>
        </div>
        <br />
         <!--INICIO DEL REPEATER-->
        <div>
            <div class="table-responsive mT20">
                <table class="table table-striped table-hover">
                    <thead>
                        <tr>
                            <th style="width:15%" >
                                <asp:Label ID="lbSeleccionarHeaderrepeater" runat="server" Text="Seleccionar" CssClass="Letranegrita"></asp:Label>
                            </th>
                            <th style="width:35%" >
                                 <asp:Label ID="lbTipoProductoHeaderRepeater" runat="server" Text="Tipo de Producto" CssClass="Letranegrita"></asp:Label>
                            </th>
                            <th style="width:40%" >
                                 <asp:Label ID="lbCategoriaHeaderRepeater" runat="server" Text="Categoria" CssClass="Letranegrita"></asp:Label>
                            </th>
                            <th style="width:10%" >
                                 <asp:Label ID="lbEstatusHeaderRepeater" runat="server" Text="Estatus" CssClass="Letranegrita"></asp:Label>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rpListadoCategoria" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <asp:HiddenField ID="hfIdCategoria" runat="server" Value='<%#Eval("IdCategoria") %>' />
                                    <td style="width:15%" >
                                        <asp:Button ID="btnSeleccionarRegistrosRepeater" runat="server" Text="Seleccionar" ToolTip="Seleccionar Registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnSeleccionarRegistrosRepeater_Click" />
                                    </td>

                                    <td style="width:35%" >
                                        <asp:Label ID="lbTipoProductoBodyRepeater" runat="server" Text='<%#Eval("TipoProducto") %>'></asp:Label>
                                    </td>

                                    <td style="width:40%" >
                                        <asp:Label ID="lbCategoriaBodyRepeater" runat="server" Text='<%# Eval("Categoria") %>'></asp:Label>
                                    </td>

                                    <td style="width:10%" >
                                        <asp:Label ID="lbEstatusBodyRepeater" runat="server" Text='<%# Eval("Estatus") %>'></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
        <!--FIN DEL REPEATER-->
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
            <div class="container-fluid">
                <br /><br />
                 <asp:Label ID="lbIdRegistroSeleccionado" runat="server" Visible="false" Text="IdRegistro"></asp:Label>
                <asp:Label ID="lbAccionTomarMantenimiento" runat="server" Visible="false" Text="Accion a Tomar"></asp:Label>
          
            <div class="form-row">
                <div class="form-group col-md-4">
                    <asp:Label ID="lbTipoProductoMantenimiento" runat="server" Text="Tipo de Producto" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarTipoProductoMantenimiento" runat="server" ToolTip="Seleccionar Tipo de Producto" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="form-group col-md-4">
                    <asp:Label ID="lbCategoriaMantenimiento" runat="server" Text="Categoria" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtCategoriaMantenimiento" AutoCompleteType="Disabled" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                </div>
                <div id="BloqueClaveSeguridad" runat="server" class="form-group col-md-4">
                    <asp:Label ID="lbClaveSeguridadMantenimiento" runat="server" Text="Clave de Seguridad" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtClaveSeguridadMantenimiento" AutoCompleteType="Disabled" TextMode="Password" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-check-inline">
                <div class="form-group form-check">
                    <asp:CheckBox ID="cbEstatusMantenimiento" runat="server" Text="Estatus" ToolTip="Establecer el estatus del registro" CssClass="form-check-input" />
                </div>
            </div>
            <div align="center">
                <asp:Button ID="btnGuardarMantenimiento" runat="server" Text="Guardar" ToolTip="Buscar" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnGuardarMantenimiento_Click" />
                <asp:Button ID="btnModificarMantenimiento" runat="server" Text="Volver" ToolTip="Buscar" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnModificarMantenimiento_Click" />
        
            </div>
            <br />
        </div>
        </div>


    </div>
</asp:Content>
