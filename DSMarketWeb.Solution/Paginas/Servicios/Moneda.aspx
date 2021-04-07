<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="Moneda.aspx.cs" Inherits="DSMarketWeb.Solution.Paginas.Servicios.Moneda" %>
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

        .BotonEspecoal {
           width:100%;
             font-weight:bold;
          }
    </style>

    <script type="text/javascript">
        function RegistroGuardado() {
            alert("Registro guardado con exito.");         
        }
        $(document).ready(function () {
            $("#<%=btnGuardar.ClientID%>").click(function () {
                  var NombreMoneda = $("#<%=txtMonedaMantenimiento.ClientID%>").val().length;
                    if (NombreMoneda < 1) {
                        alert("El campo moneda no puede estar vacio para realizar esta operación, favor de verificar.");
                        $("#<%=txtMonedaMantenimiento.ClientID%>").css("border-color", "red");
                        return false;
                    }

                    else {
                        var Sigla = $("#<%=txtSiglaMantenimiento.ClientID%>").val().length;
                        if (Sigla < 1) {
                            alert("El campo sigla no puede estar vacio para realizar esta oparación, favor de verificar.");
                            $("#<%=txtSiglaMantenimiento.ClientID%>").css("border-color", "red");
                            return false;
                        }
                        else {
                            var Tasa = $("#<%=txtTasaMantenimiento.ClientID%>").val().length;
                            if (Tasa < 1) {
                                alert("El campo tasa no puede estar vacio para realizar esta operación, favor de verificar.");
                                $("#<%=txtTasaMantenimiento.ClientID%>").css("border-color", "red");
                                return false;
                            }
                        }
                    }

                });
          })
    </script>
    <br /><br />
    <asp:Label ID="lbAccionTomar" runat="server" Text="Accion" Visible="false"></asp:Label>
    <asp:Label ID="lbIsMantenimiento" runat="server" Text="ID" Visible="false"></asp:Label>
    <div class="container-fluid">
        <div id="DivBloqueConsulta" runat="server">
            <div class="form-row">
                <div class="form-group col-md-4">
                    <asp:Label ID="lbDescripcionMonedaConsulta" runat="server" Text="Nombre de Moneda" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtDescripcionMonedaConsulta" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-check-inline">
                <div class="form-group form-check">
                    <asp:Label ID="lbExportarReporte" runat="server" Text="Exportar a: " CssClass="Letranegrita"></asp:Label>
                    <asp:RadioButton ID="rbReportePDF" runat="server" Text="PDF" CssClass="form-check-input" ToolTip="Generar reporte en PDF" GroupName="Reporte" />
                    <asp:RadioButton ID="rbReporteExcel" runat="server" Text="EXCEL" CssClass="form-check-input" ToolTip="Generar reporte en Excel" GroupName="Reporte" />
                    <asp:RadioButton ID="rbReporteWord" runat="server" Text="WORD" CssClass="form-check-input" ToolTip="Generar reporte en Word" GroupName="Reporte" />
                </div>
            </div>
            <div align="center">
                <asp:Button ID="btnConsultar" runat="server" Text="Consultar" ToolTip="Consultar Registros" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnConsultar_Click" />
                 <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" ToolTip="Crear nuevo registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnNuevo_Click" />
                 <asp:Button ID="btnEditar" runat="server" Text="Editar" ToolTip="Editar Registro seleccionado" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnEditar_Click" />
                 <asp:Button ID="btnReporte" runat="server" Text="Reporte" ToolTip="Generar Reporte" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnReporte_Click" />
                <asp:Button ID="btnRestablecer" runat="server" Text="Restablecer" ToolTip="Restablecer Pantalla" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnRestablecer_Click" />
                <br />
                <asp:Label ID="lbCantidadRegistrosTitulo" runat="server" Text="Cantidad de Registros ( " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadRegistrosVariable" runat="server" Text=" 0 " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadRegistrosCerrar" runat="server" Text=" )" CssClass="Letranegrita"></asp:Label>
            </div>
            <br />
            <div class="table-responsive">
                <table class="table table-hover">
                    <thead>
                        <tr>
                            <th style="width:10%" align="left"> SELECCIONAR </th>
                            <th style="width:50%" align="left"> MONEDA </th>
                            <th style="width:10%" align="left"> SIGLA </th>
                            <th style="width:10%" align="left"> ESTATUS </th>
                            <th style="width:10%" align="left"> TASA </th>
                            <th style="width:10%" align="left"> POR DEFECTO </th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rpListadoMoneda" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <asp:HiddenField ID="hfIdMoneda" runat="server" Value='<%# Eval("IdMoneda") %>' />

                                    <td style="width:10%"> <asp:Button ID="btnSeleccionar" runat="server" Text="Seleccionar" CssClass="btn btn-outline-secondary btn-sm" ToolTip="Seleccionar Registro" OnClick="btnSeleccionar_Click" /> </td>
                                    <td style="width:50%"> <%# Eval("Moneda") %> </td>
                                    <td style="width:10%"> <%# Eval("Sigla") %> </td>
                                    <td style="width:10%"> <%# Eval("Estatus") %> </td>
                                    <td style="width:10%"> <%#string.Format("{0:n2}", Eval("Tasa")) %> </td>
                                    <td style="width:10%"> <%# Eval("PorDefecto") %> </td>
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
             <div id="DivPaginacionMoneda" runat="server" align="center">
        <div style="margin-top: 20px;">
            <table style="width: 600px">
                <tr>
                    <td> <asp:LinkButton ID="LinkPrimeraPaginaMoneda" runat="server" Text="Primero" CssClass="btn btn-outline-success btn-sm" ToolTip="Ir a la primera pagina del listado" OnClick="LinkPrimeraPaginaMoneda_Click"></asp:LinkButton> </td>
                    <td> <asp:LinkButton ID="LinkAnteriorMoneda" runat="server" Text="Anterior" CssClass="btn btn-outline-success btn-sm" ToolTip="Ir a la pagina anterior del listado" OnClick="LinkAnteriorMoneda_Click"></asp:LinkButton> </td>
                    <td>
                        <asp:DataList ID="dtPaginacionMoneda" runat="server" OnItemCommand="dtPaginacionMoneda_ItemCommand" OnItemDataBound="dtPaginacionMoneda_ItemDataBound" RepeatDirection="Horizontal">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkPaginacionCentralMoneda" runat="server" CommandArgument='<%# Eval("IndicePagina") %>' CommandName="newPage" Text='<%# Eval("TextoPagina") %>' Width="20px"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:DataList>

                    </td>
                    <td> <asp:LinkButton ID="LinkSiguienteMoneda" runat="server" Text="Siguiente" ToolTip="Ir a la siguiente pagina del listado" CssClass="btn btn-outline-success btn-sm" OnClick="LinkSiguienteMoneda_Click"></asp:LinkButton> </td>
                    <td> <asp:LinkButton ID="LinkUltimoMoneda" runat="server" Text="Ultimo" ToolTip="Ir a la ultima pagina del listado" CssClass="btn btn-outline-success btn-sm" OnClick="LinkUltimoMoneda_Click"></asp:LinkButton> </td>
                </tr>
            </table>
        </div>
                 <br />
        </div>
              <br />
        </div>


         <div id="DivBloqueMantenimiento" runat="server">
             <br /><br />
             
             <div class="container-fluid">
                 <div class="form-row">
                     <div class="form-group col-md-4">
                         <asp:Label ID="lbDescripcionMantenimiento" runat="server" Text="Moneda" CssClass="Letranegrita"></asp:Label>
                         <asp:TextBox ID="txtMonedaMantenimiento" runat="server" CssClass="form-control" MaxLength="100" AutoCompleteType="Disabled"></asp:TextBox>
                     </div>

                     <div class="form-group col-md-4">
                         <asp:Label ID="lbSiglaMantenimiento" runat="server" Text="Sigla" CssClass="Letranegrita"></asp:Label>
                         <asp:TextBox ID="txtSiglaMantenimiento" runat="server" CssClass="form-control" MaxLength="10" AutoCompleteType="Disabled"></asp:TextBox>
                     </div>

                     <div class="form-group col-md-4">
                         <asp:Label ID="lbTasaMantenimiento" runat="server" Text="Tasa" CssClass="Letranegrita"></asp:Label>
                         <asp:TextBox ID="txtTasaMantenimiento" runat="server" CssClass="form-control" TextMode="Number" step="0.01" ></asp:TextBox>
                     </div>
                 </div>
                 <br />
                 <div class="form-check-inline">
                     <asp:CheckBox ID="cbEstatusMantenimiento" runat="server" Text="Estatus" CssClass="form-check-input" ToolTip="Estatus de Moneda" />
                     <asp:CheckBox ID="cbPorDefectoMantenimiento" runat="server" Text="Por Defecto" CssClass="form-check-input" ToolTip="Espesificar si la moneda es por defecto" />
                 </div>
                 <br />
                 <div align="center">
                     <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnGuardar_Click" />
                     <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnVolver_Click" />
                 </div>
                 <br />
             </div>

         </div>
    </div>
</asp:Content>
