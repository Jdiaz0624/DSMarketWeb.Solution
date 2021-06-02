<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="InformacionEmpresa.aspx.cs" Inherits="DSMarketWeb.Solution.Paginas.Configuracion.InformacionEmpresa" %>
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

    <div id="DivBloqueInformacionEmpresa" runat="server">
        <br />
        <div class="form-row">
            <div class="form-group col-md-6">
                <asp:Label ID="lbNombreEmpresa" runat="server" Text="Nombre de Empresa" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtNombreEmpresa" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
            </div>

             <div class="form-group col-md-6">
                <asp:Label ID="lbRNC" runat="server" Text="RNC" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtRNC" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
            </div>

             <div class="form-group col-md-6">
                <asp:Label ID="lbDireccion" runat="server" Text="Dirección" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" MaxLength="8000"></asp:TextBox>
            </div>

             <div class="form-group col-md-6">
                <asp:Label ID="lbTelefonos" runat="server" Text="Telefonos" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtTelefonos" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>
            </div>

             <div class="form-group col-md-6">
                <asp:Label ID="lbMail" runat="server" Text="Mail" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtMail" runat="server" CssClass="form-control" TextMode="Email" MaxLength="200"></asp:TextBox>
            </div>

            <div class="form-group col-md-6">
                <asp:Label ID="lbMail2" runat="server" Text="Mail 2" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtMail2" runat="server" CssClass="form-control" TextMode="Email" MaxLength="200"></asp:TextBox>
            </div>

            <div class="form-group col-md-6">
                <asp:Label ID="lbInstagram" runat="server" Text="Instagram" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtInstagram" runat="server" CssClass="form-control" TextMode="Email" MaxLength="200"></asp:TextBox>
            </div>

            <div class="form-group col-md-6">
                <asp:Label ID="lbSitioWeb" runat="server" Text="Sitio Web" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtSitioWeb" runat="server" CssClass="form-control" TextMode="Email" MaxLength="200"></asp:TextBox>
            </div>

        </div>

          <div align="center">
            <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn btn-primary btn-sm" OnClick="btnModificar_Click" />
               <asp:Button ID="btnPoliticas" runat="server" Text="Politicas" CssClass="btn btn-primary btn-sm" OnClick="btnPoliticas_Click" />
        </div>
        <br />
    </div>
</asp:Content>
