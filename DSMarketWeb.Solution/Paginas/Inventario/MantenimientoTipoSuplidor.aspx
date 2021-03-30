<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="MantenimientoTipoSuplidor.aspx.cs" Inherits="DSMarketWeb.Solution.Paginas.Inventario.MantenimientoTipoSuplidor" %>
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
            alert("Registro guardado con exito.");
        }
        function RegistroModificado() {
            alert("Registro modificado con exito.");
        }
        function ClaveSeguridaNoValida() {
            alert("La clave de seguridad ingresada no es valida, favor de verificar.");
        }

        $(document).ready(function () {
            $("#<%=btnGuardar.ClientID%>").click(function () {

                //PRECIO DE COMPRA
                var TipoSuplidorGuardar = $("#<%=txtTipoSuplidorMantenimiento.ClientID%>").val().length;
                if (TipoSuplidorGuardar < 1) {
                    alert("El campo tipo de suplidor no puede estar vacio para gaurdar este registro, favor de verificar.");
                    $("#<%=txtTipoSuplidorMantenimiento.ClientID%>").css("border-color", "red");
                    return false;
                }
            });

            $("#<%=btnModificar.ClientID%>").click(function () {
                var TipoSuplidorModificar = $("#<%=txtTipoSuplidorMantenimiento.ClientID%>").val().length;
                if (TipoSuplidorModificar < 1) {
                    alert("El campo tipo de suplidor no puede estar vacio para modificar este registro, favor de verificar.");
                    $("#<%=txtTipoSuplidorMantenimiento.ClientID%>").css("border-color", "red");
                    return false;
                }
                else {
                    var ClaveSeguridad = $("#<%=txtClaveseguridad.ClientID%>").val().length;
                    if (ClaveSeguridad < 1) {
                        alert("El campo clave de seguridad no puede estar vacio para modificar este registro, favor de verificar.");
                        $("#<%=txtClaveseguridad.ClientID%>").css("border-color", "red");
                        return false;
                    }
                }
            });

        })
    </script>

    <div class="container-fluid">
        <div id="DivBloqueConsulta" runat="server">
            <br /><br />

            <!--CONTROLES DE FILTROS Y BOTONES-->
            <div class="form-row">
                <div class="form-group col-md-4">
                    <asp:Label ID="lbTipoSuplidorConsulta" runat="server" Text="Tipo de Suplidor" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtTipoSuplidorConsulta" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                </div>
            </div>

            <!--BOTONES DE LA PANTALLA DE CONSULTA-->
            <div align="center">
                <asp:Button ID="btnConsultar" runat="server" Text="Buscar" CssClass="btn btn-outline-secondary btn-sm" ToolTip="Consultar Registros" OnClick="btnConsultar_Click" />
                <asp:Button ID="btnNuevoRegistro" runat="server" Text="Nuevo" CssClass="btn btn-outline-secondary btn-sm" ToolTip="Crear Nuevo Registro" OnClick="btnNuevoRegistro_Click" />
                <asp:Button ID="btnModificarRegistro" runat="server" Text="Modificar" CssClass="btn btn-outline-secondary btn-sm" ToolTip="Modificar Registro Seleccionado" OnClick="btnModificarRegistro_Click" />
                <asp:Button ID="btnRestablecerPantalla" runat="server" Text="Restablecer" CssClass="btn btn-outline-secondary btn-sm" ToolTip="Restablecer Pantalla" OnClick="btnRestablecerPantalla_Click" />


            </div>
            <br />
            <!--INICIO DEL REPEATER-->
            <div align="center">
                <asp:Label ID="lbCantidadRegistrisTitulos" runat="server" Text="Cantidad de Registros ( " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadRegistrsVariable" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadRegistrosCerrar" runat="server" Text=" )" CssClass="Letranegrita"></asp:Label>

            </div>
            <br />
            <div class="table-responsive mT20">
                <table class="table table-striped table-hover">
                    <thead>
                        <tr>
                             <th style="width:10%" align="left" > <asp:Label ID="lbSeleccionarHeaderRepeater" runat="server" Text="Seleccionar" CssClass="Letranegrita"></asp:Label> </th>
                             <th style="width:80%" align="left" > <asp:Label ID="lbTipoSuplidorHeaderRepeater" runat="server" Text="Tipo de Suplidor" CssClass="Letranegrita"></asp:Label> </th>
                             <th style="width:10%" align="left" > <asp:Label ID="lbEstatusHEaderRepeater" runat="server" Text="Estatus" CssClass="Letranegrita"></asp:Label> </th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rpListadoTipoSuplidores" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <asp:HiddenField ID="hfIdTipoSuplidor" runat="server" Value='<%# Eval("IdTipoSuplidor") %>' />

                                    <td style="width:10%" align="left" > <asp:Button ID="btnSeleccionarRegistro" runat="server" Text="Seleccionar" ToolTip="Seleccionar Registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnSeleccionarRegistro_Click" /> </td>
                                    <td style="width:80%" align="left" > <asp:Label ID="lbTipoSuplidorBodyRepeater" runat="server" Text='<%# Eval("TipoSuplidor") %>'></asp:Label> </td>
                                    <td style="width:10%" align="left"> <asp:Label ID="lbEstatusBodyRepeater" runat="server" Text='<%# Eval("Estatus") %>'></asp:Label> </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>

            <!--PAGINACION DEL REPEATER-->
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
            <asp:Label ID="lbIdRegistroSeleccionado" runat="server" Text="IdRegistroSeleccionado" Visible="false"></asp:Label>

            <div class="form-row">
                <div class="form-group col-md-4">
                    
                <asp:Label ID="lbDescripcion" runat="server" Text="Tipo de Suplidor" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtTipoSuplidorMantenimiento" runat="server" CssClass="form-control" AutoCompleteType="Disabled" MaxLength="100"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbClaveSegruidd" runat="server" Text="Clave de Seguridad" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtClaveseguridad" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            
            <div class="form-check-inline">
                <div class="form-group form-check">
                    <asp:CheckBox ID="cbEstatus" runat="server" Text="Estatus" CssClass="form-check-input" ToolTip="Estatus de Tipo de Suplidor" />
                </div>
            </div>

            <div align="center">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" ToolTip="Guardar Registro" CssClass="btn btn-outline-success btn-sm" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" ToolTip="Modificar Registro" CssClass="btn btn-outline-success btn-sm" OnClick="btnModificar_Click" />
                <asp:Button ID="btnVolver" runat="server" Text="Volver" ToolTip="Regresar a la Pantalla Anterior" CssClass="btn btn-outline-success btn-sm" OnClick="btnVolver_Click" />
            </div>
            <br />
        </div>


    </div>
</asp:Content>
