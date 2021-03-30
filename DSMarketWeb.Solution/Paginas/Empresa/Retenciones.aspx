<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="Retenciones.aspx.cs" Inherits="DSMarketWeb.Solution.Paginas.Empresa.Retenciones" %>
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

        function ClaveSeguridadNoValida() {
            alert("La clave de seguridad ingresada no es valida, favor de verificar.");
        }

        $(document).ready(function () {
            $("#<%=btnGuardar.ClientID%>").click(function () {

                var Retencion = $("#<%=txtRetencionMantenimiento.ClientID%>").val().length;
                if (Retencion < 1) {
                    alert("El campo retención no puede estar vacio para guardar, favor de verificar.");
                    $("#<%=txtRetencionMantenimiento.ClientID%>").css("border-color", "red");
                    return false;
                }
            });

            $("#<%=btnModificar.ClientID%>").click(function () {

                var Retencion = $("#<%=txtRetencionMantenimiento.ClientID%>").val().length;
                if (Retencion < 1) {
                    alert("El campo retención no puede estar vacio para modificar este registro, favor de verificar.");
                    $("#<%=txtRetencionMantenimiento.ClientID%>").css("border-color", "red");
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
            });
        })
    </script>

    <div class="container-fluid">
        <div id="DivBloqueConsulta" runat="server">
            <br /><br />
   

            <div class="form-row">
                <div class="form-group col-md-4">
                    <asp:Label ID="lbRetencionConsulta" runat="server" Text="Retención" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtRetencionConsulta" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <br />
            <div align="center">
                <asp:Button ID="btnConsultarRegistros" runat="server" Text="Consultar" ToolTip="Consultar Registros" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnConsultarRegistros_Click" />
                 <asp:Button ID="btnNuevoRegistro" runat="server" Text="Nuevo" ToolTip="Nuevo Registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnNuevoRegistro_Click" />
                 <asp:Button ID="btnModificarRegistro" runat="server" Text="Modificar" ToolTip="Modificar Registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnModificarRegistro_Click" />
                 <asp:Button ID="btnRestablecerPantalla" runat="server" Text="Restablecer" ToolTip="Restablecer Pantalla" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnRestablecerPantalla_Click" />
                <br /><br />
               <asp:Label ID="lbCantidadRegistrosTitulo" runat="server" Text="Cantidad de Registros ( " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadRegistrosVariable" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadRegistrosCerrar" runat="server" Text=" ) " CssClass="Letranegrita"></asp:Label>
            </div>
            <br />
            <div class="table-responsive">
                <table class="table table-hover">
                   <thead>
                       <tr>
                           <th style="width:10%" align="left"> <asp:Label ID="lbSeleccionarHeader" runat="server" Text="Seleccionar" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:80%" align="left"> <asp:Label ID="lbRetencionesHeader" runat="server" Text="Retención" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:10%" align="left"> <asp:Label ID="lbEstatusHeader" runat="server" Text="Estatus" CssClass="Letranegrita"></asp:Label> </th>
                       </tr>
                   </thead>
                    <tbody>
                        <asp:Repeater ID="rpListadoRetenciones" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <asp:HiddenField ID="hfIdRetencion" runat="server" Value='<%# Eval("IdRetencion") %>' />
                                    <td style="width:10%"> <asp:Button ID="btnSeleccionar" runat="server" Text="Seleccionar" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnSeleccionar_Click" /> </td>
                                    <td style="width:80%"> <%# Eval("Retencion") %> </td>
                                    <td style="width:10%"> <%# Eval("Estatus") %> </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>

              <div align="center">
                <asp:Label ID="lbPaginaActualTitulo" runat="server" Text="Pagina " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbPaginaActualVariable" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadPaginaTitulo" runat="server" Text="De " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadPaginaVariable" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
            </div>

             <div id="divPaginacionRetencion" runat="server" align="center" >
                <div style="margin-top=20px;">
                    <table style="width:600px;">
                        <tr>
                            <td> <asp:LinkButton ID="LinkPrimeraPaginaPaginacion" runat="server" Text="Primero" ToolTip="Ir a la primera pagina del listado" OnClick="LinkPrimeraPaginaPaginacion_Click" CssClass="btn btn-outline-success btn-sm"  ></asp:LinkButton> </td>
                            <td> <asp:LinkButton ID="LinkPaginaAnterior" runat="server" Text="Anterior" ToolTip="Ir a la pagina anterior del listado" OnClick="LinkPaginaAnterior_Click" CssClass="btn btn-outline-success btn-sm"></asp:LinkButton> </td>
                            <td>
                                <asp:DataList ID="dlPaginacion" runat="server" OnCancelCommand="dlPaginacion_CancelCommand" OnItemDataBound="dlPaginacion_ItemDataBound" RepeatDirection="Horizontal" >
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkIndicePaginacion" runat="server" CommandArgument='<%# Eval("IndicePagina") %>' CommandName="NuevaPagina" Text='<%# Eval("TextoPagina")%>' Width="20px"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:DataList>

                            </td>
                            <td> <asp:LinkButton ID="LinkPaginaSiguiente" runat="server" Text="Siguiente" ToolTip="Ir la Siguiente pagina del listado" OnClick="LinkPaginaSiguiente_Click" CssClass="btn btn-outline-success btn-sm"></asp:LinkButton> </td>
                            <td> <asp:LinkButton ID="LinkUltipaPagina" runat="server" Text="Ultmo" ToolTip="Ir a la Ultima Pagina del listado" OnClick="LinkUltipaPagina_Click" CssClass="btn btn-outline-success btn-sm"></asp:LinkButton> </td>
                           
                        </tr>
                    </table>
                </div>
            </div>
            <br />
        </div>



        <div id="DivBloqueMantenimiento" runat="server">
             <br /><br />
              <asp:Label ID="lbIdRegistroSeleccionado" runat="server" Text="IdRegistroSeleccionado" CssClass="Letranegrita" Visible="false"></asp:Label>
         

            <div class="form-row">
                <div class="form-group col-md-4">
                    <asp:Label ID="lbRetencionMantenimiento" runat="server" Text="Retención" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtRetencionMantenimiento" runat="server" AutoCompleteType="Disabled" CssClass="form-control" MaxLength="100"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbClaveSeguridad" runat="server" Text="Clave de Seguridad" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtClaveSeguridad" runat="server" TextMode="Password" CssClass="form-control" MaxLength="100"></asp:TextBox>
                </div>
            </div>
            <div class="form-check-inline">
                <div class="form-group form-check">
                    <asp:CheckBox ID="cbEstatus" runat="server" Text="Estatus" CssClass="Letranegrita" ToolTip="Estatus de Retención" />
                </div>
            </div>
            <br />
            <div align="center">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" ToolTip="Guardar Registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnGuardar_Click" />
                 <asp:Button ID="btnModificar" runat="server" Text="Modificar" ToolTip="Modificar Registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnModificar_Click" />
                 <asp:Button ID="btnVolver" runat="server" Text="Volver" ToolTip="Volver Atras" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnVolver_Click" />
            </div>
            <br />
        </div>
    </div>
</asp:Content>
