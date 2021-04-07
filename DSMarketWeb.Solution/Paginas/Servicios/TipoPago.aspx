<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="TipoPago.aspx.cs" Inherits="DSMarketWeb.Solution.Paginas.Servicios.TipoPago" %>
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
        $(document).ready(function () {
            $("#<%=btnGuardar.ClientID%>").click(function () {
                var TipoPago = $("#<%=txtTipoPagoMantenimiento.ClientID%>").val().length;
                if (TipoPago < 1) {
                    alert("El campo tipo de pago no puede estar vacio para modificar este registro, favor de verificar.");
                    $("#<%=txtTipoPagoMantenimiento.ClientID%>").css("border-color", "red");
                    return false;
                }
                else {
                    var PorcentajeEntero = $("#<%=txtPorcentajeEntero.ClientID%>").val().length;
                    if (PorcentajeEntero < 1) {
                        alert("El campo Porcentaje entero no puede estar vacio para modificar este registro, favor de verificar.");
                        $("#<%=txtPorcentajeEntero.ClientID%>").css("border-color","red");
                        return false;
                    }
                    else {
                        var ValorDecimal = $("#<%=txtValorDecimal.ClientID%>").val().length;
                        if (ValorDecimal < 1) {
                            alert("El campo Valor decimal no puede estar vacio para modificar este registro, favor de verificar.");
                            $("#<%=txtValorDecimal.ClientID%>").css("border-color", "red");
                            return false;
                        }
                        else {
                            var CodigoTipoPago = $("#<%=txtCodigoTipoPago.ClientID%>").val().length;
                            if (CodigoTipoPago < 1) {
                                alert("El campo Codigo de Tipo de Pago no puede estar vacio para modificar este registro, favor de verificar.");
                                $("#<%=txtCodigoTipoPago.ClientID%>").css("border-color", "red");
                                return false;
                            }
                        }
                    }
                }
            });

        })
    </script>

    <br /><br />
    <asp:Label ID="lbIdMantenimiento" runat="server" Text="ID" Visible="false"></asp:Label>

    <div id="divConsulta" runat="server">
        <div class="container-fluid">
            <div class="form-row">
                <div class="form-group col-md-4">
                    <asp:Label ID="lbdescripcion" runat="server" Text="Descripción" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtdescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>



            <div align="center">
                <asp:Button ID="btnConsultar" runat="server" Text="Consultar" ToolTip="Consultar Registros" OnClick="btnConsultar_Click" CssClass="btn btn-outline-secondary btn-sm" />
                <asp:Button ID="btnRestablecer" runat="server" Text="Restablecer" ToolTip="Restablecer Pantalla" OnClick="btnRestablecer_Click" CssClass="btn btn-outline-secondary btn-sm" />
                <br /><br />
                 <asp:Label ID="lbCantidadRegistrosTitulo" runat="server" Text="Cantidad de registros ( " CssClass="Letranegrita"></asp:Label>
                 <asp:Label ID="lbCantidadRegistrosVariable" runat="server" Text=" 0 " CssClass="Letranegrita"></asp:Label>
                 <asp:Label ID="lbCantidadRegistrosCerrar" runat="server" Text=" )" CssClass="Letranegrita"></asp:Label>
            </div>
            <br />
            <div class="table-responsive">
                <table class="table table-hover">
                    <thead>
                        <tr>
                        <th style="width:10%" align="left"> EDITAR </th>
                        <th style="width:30%" align="left"> DESCRIPCION </th>
                        <th style="width:10%" align="left"> ESTATUS </th>
                        <th style="width:10%" align="left"> BLOQUEA </th>
                        <th style="width:10%" align="left"> IMPUESTO </th>
                        <th style="width:10%" align="left"> ENTERO </th>
                        <th style="width:10%" align="left"> VALOR </th>
                        <th style="width:10%" align="left"> DEFAULT </th>
                    </tr>
                    </thead>

                    <tbody>
                        <asp:Repeater ID="rpListadoTipoPago" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <asp:HiddenField ID="hfIdTipoPago" runat="server" Value='<%# Eval("IdTipoPago") %>' />

                                    <td style="width:10%"> <asp:Button ID="btnEditar" runat="server" Text="Editar" ToolTip="Editar Registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnEditar_Click" /> </td>
                                    <td style="width:30%"> <%# Eval("TipoPago") %> </td>
                                    <td style="width:10%"> <%# Eval("Estatus") %> </td>
                                    <td style="width:10%"> <%# Eval("BloqueaMonto") %> </td>
                                    <td style="width:10%"> <%# Eval("ImpuestoAdicional") %> </td>
                                    <td style="width:10%"> <%# Eval("PorcentajeEntero") %> </td>
                                    <td style="width:10%"> <%# Eval("Valor") %> </td>
                                    <td style="width:10%"> <%# Eval("PorDefecto") %> </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>

    </div>




    <div id="DivBloqueMantenimiento" runat="server">
        <br /><br />
        <div class="container-fluid">
            <div class="form-row">
                <div class="form-group col-md-3">
                    <asp:Label ID="lbTipoPagoMantenimiento" runat="server" Text="Tipo de Pago" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtTipoPagoMantenimiento" runat="server" CssClass="form-control" AutoCompleteType="Disabled" MaxLength="100"></asp:TextBox>
                </div>

                <div class="form-group col-md-3">
                    <asp:Label ID="lbPorcentajeEntero" runat="server" Text="Porcentaje Entero" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtPorcentajeEntero" runat="server" CssClass="form-control" AutoCompleteType="Disabled" TextMode="Number" ></asp:TextBox>
                </div>

                <div class="form-group col-md-3">
                    <asp:Label ID="lbValorDecimal" runat="server" Text="Valor Decimal" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtValorDecimal" runat="server" CssClass="form-control" AutoCompleteType="Disabled"  TextMode="Number" step="0.01"></asp:TextBox>
                </div>

                <div class="form-group col-md-3">
                    <asp:Label ID="lbCodigoTipoPago" runat="server" Text="Codigo" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtCodigoTipoPago" runat="server" CssClass="form-control" AutoCompleteType="Disabled" Enabled="false" MaxLength="100"></asp:TextBox>
                </div>
            </div>
            <div class="form-check-inline">
                <div class="form-group form-check">
                    <asp:CheckBox ID="cbEstatus" runat="server" Text="Estatus" CssClass="form-check-input" ToolTip="Estatus de Tipo de Pago" />
                    <asp:CheckBox ID="cbBloqueaMonto" runat="server" Text="Bloquea Monto" CssClass="form-check-input" ToolTip="Bloquea Monto" />
                    <asp:CheckBox ID="cbImpuestoAdicional" runat="server" Text="Impuesto Adicional" CssClass="form-check-input" ToolTip="Impuesto Adicional" />
                    <asp:CheckBox ID="cbPorDefecto" runat="server" Text="Por Defecto" CssClass="form-check-input" ToolTip="Por Defecto" />
                </div>
            </div>
            <div align="center">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" ToolTip="Guardar registro" OnClick="btnGuardar_Click" CssClass="btn btn-outline-secondary btn-sm" />
                <asp:Button ID="btnVolver" runat="server" Text="Volver" ToolTip="Volver Atras" OnClick="btnVolver_Click" CssClass="btn btn-outline-secondary btn-sm" />
            </div>
            <br />
        </div>

    </div>
</asp:Content>
