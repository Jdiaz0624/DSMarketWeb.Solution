<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="MantenimientoUnidadMedida.aspx.cs" Inherits="DSMarketWeb.Solution.Paginas.Inventario.MantenimientoUnidadMedida" %>
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
        function RegistroGuardadoConExito() {
            alert("Registro guardado con exito.");
        }
        function RegistroModificadoConExito() {
            alert("Registro modificdo con exito.");
        }
        function ClaveSeguridadNoValida() {
            alert("La clave de seguridad ingresada no es valida, favor de verificar.");
        }

        $(document).ready(function () {
            $("#<%=btmGaurdar.ClientID%>").click(function () {
                var ValidarUnidadMedida = $("#<%=txtUnidadmedidaMantenimiento.ClientID%>").val().length;
                if (ValidarUnidadMedida < 1) {
                    alert("El campo unidad de medida no puede estar vacio para guardar este registro, favor de verificar.");
                    $("#<%=txtUnidadmedidaMantenimiento.ClientID%>").css("border-color", "red");
                    return false;
                }
            });


            $("#<%=btnModificar.ClientID%>").click(function () {
                var UnidadMeidaModificar = $("#<%=txtUnidadmedidaMantenimiento.ClientID%>").val().length;
                if (UnidadMeidaModificar < 1) {
                    alert("El campo unidad de medida no puede estar vacio para modificar este registro, favor de verificar.");
                    $("#<%=txtUnidadmedidaMantenimiento.ClientID%>").css("border-color", "red");
                    return false;
                }
                else {
                    var ClaveSeguridad = $("#<%=txtClaveSeguridad.ClientID%>").val().length;
                    if (ClaveSeguridad < 1) {
                        alert("El campo clave de seguridad no puede estar vacio, favor de verificar.");
                        $("#<%=txtClaveSeguridad.ClientID%>").css("border-color", "red");
                        return false;
                    }
                }
            });
        })
    </script>

    <div class="container-fluid">
       

        <div id="DivBloqueConsulta" runat="server">
            <br /><br />
        <div class="form-row">
            <div class="form-group col-md-3">
                <asp:Label ID="lbUnidadMedidaConsulta" runat="server" Text="Unidad de Medida" CssClass="Letranegrita"></asp:Label>
           <asp:TextBox ID="txtUnidadMedidaConsulta" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
         <div align="center">
         <asp:Button ID="btnConsultar" runat="server" Text="Buscar" ToolTip="Buscar" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnConsultar_Click" />
         <asp:Button ID="btnNuevoRegistro" runat="server" Text="Nuevo" ToolTip="Crear Nuevo Registro" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnNuevoRegistro_Click" />
             <asp:Button ID="btnModificarRegistro" runat="server" Text="Modificar" ToolTip="Modificar Registro Seleccionado" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnModificarRegistro_Click" />
         <asp:Button ID="btnRestabelcer" runat="server" Text="Restablecer" ToolTip="Restablecer Pantalla" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnRestabelcer_Click" />
        </div>
        <br />
        <div align="center">
            <asp:Label ID="lbCantidadRegistrosTitulo" runat="server" Text="Cantidad de Registros (" CssClass="Letranegrita"></asp:Label>
            <asp:Label ID="lbCantidadRegistrosVariable" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
            <asp:Label ID="lbCantidadRegistrosCerrar" runat="server" Text=")" CssClass="Letranegrita"></asp:Label>
        </div>
        <br />
        <!--REPEATER--> 
        <div class="table-responsive mT20">
            <table class="table table-striped table-hover">
                <thead>
                    <tr> 
                        <th style="width:10%" > <asp:Label ID="lbSeleccionarRepeaterHeader" runat="server" Text="Seleccionar" CssClass="Letranegrita"></asp:Label> </th>
                        <th style="width:80%"> <asp:Label ID="lbDescripcionHeaderRepeater" runat="server" Text="Unidad de Medida" CssClass="Letranegrita"></asp:Label> </th>
                        <th style="width:10%"> <asp:Label ID="lbEstatusHeaderRepeater" runat="server" Text="Estatus" CssClass="Letranegrita"></asp:Label> </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rpListadoUnidadMedida" runat="server">
                        <ItemTemplate>
                            <tr>
                                <asp:HiddenField ID="hfIdUnidadMedida" runat="server" Value='<%# Eval("IdUnidadMedida") %>' />
                                <td style="width:10%"> <asp:Button ID="btnSeleccioanrregistros" runat="server" Text="Seleccionar" ToolTip="Seleccionar Registro" OnClick="btnSeleccioanrregistros_Click" CssClass="btn btn-outline-success btn-sm" /> </td>
                                <td style="width:80%"> <asp:Label ID="lbDescipcionodyRepeater" runat="server" Text='<%# Eval("UnidadMedida") %>'></asp:Label> </td>
                                <td style="width:10%"> <asp:Label ID="lbEstatusBodyRepeater" runat="server" Text='<%# Eval("Estatus") %>'></asp:Label> </td>
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
        <div id="DivBloqueMantenimiento" runat="server">
            <br /><br />
           <asp:Label ID="lbIdRegistroSeleccionadoMantenimiento" runat="server" Text="Id registro Seleccionado" Visible="false"></asp:Label>

            <div class="form-row">
                <div class="form-group col-md-4">
                    <asp:Label ID="lbUnidadMedidaMantenimiento" runat="server" Text="Unidad de Medida" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtUnidadmedidaMantenimiento" runat="server" MaxLength="100" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group col-md-4">
                    <asp:Label ID="lbClaveSeguridad" runat="server" Text="Clave de Seguridad" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtClaveSeguridad" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-check-inline">
                <div class="form-group form-check">
                    <asp:CheckBox ID="cbEstatus" runat="server" Text="Estatus" CssClass="form-check-input" ToolTip="Estatus del Registro" />
                </div>
            </div>

            <div align="center">
                <asp:Button ID="btmGaurdar" runat="server" Text="Guardar" ToolTip="Guardar registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btmGaurdar_Click" />
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" ToolTip="Modificar registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnModificar_Click" />
                <asp:Button ID="btnVolver" runat="server" Text="Volver" ToolTip="Regresar a la pantalla anterior" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnVolver_Click" />
            </div>
        </div>


    </div>
</asp:Content>
