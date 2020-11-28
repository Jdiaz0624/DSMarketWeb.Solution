<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginSistema.aspx.cs" Inherits="DSMarketWeb.Solution.LoginSistema" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DSMarket</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/css/bootstrap.min.css" />
    <link rel="icon" type="image/png" href="Recursos/Facturación-Electrónica-Icono-1024x1024.ico" />
    

    <style>
body {
  background-color:#465268;
  padding: 0;
  margin: 0;
}
body { background-color: #465268; }

#CentralizarLogin {
       position:absolute;
       top: 40%;
       left: 50%;
       width:30em;
       height:18em;
       margin-top: -9em; /*set to a negative number 1/2 of your height*/
       margin-left: -15em; /*set to a negative number 1/2 of your width*/
       /*border: 0px solid #ccc;
       background-color: #f3f3f3;*/
}
.page-lock {
  margin: 90px auto 30px auto;
  width: 450px;
}

.page-lock .page-logo {
  text-align: center;
  margin-bottom: 15px;
}

.page-lock .page-body {
  width: 100%;
  margin-top: 50px;
  background-color: #3a4554;
  -webkit-border-radius: 7px;
  -moz-border-radius: 7px;
  -ms-border-radius: 7px;
  -o-border-radius: 7px;
  border-radius: 7px;
}

.lock-head {
  display: block;
  background-color: #323d4b;
  text-align: center;
  padding-top: 15px;
  padding-bottom: 15px;
  font-size: 22px;
  font-weight: 400;
  color: #4db3a5;
}

.lock-body {
  display: block;
  margin: 35px;
  overflow: hidden;
}

.lock-avatar {
  margin-top: 15px;
  height: 110px;
  -webkit-border-radius: 50%;
  -moz-border-radius: 50%;
  border-radius: 50%;
}

.lock-form {
  padding-left: 40px;
}

.lock-form h4 {
  margin-top: 0px;
  color: #dbe2ea;
  font-size: 18px;
  font-weight: 400;
}

.lock-form .form-group {
  margin-top: 20px;
}

.lock-form .form-group .form-control {
  background-color: #303a48;
  border: none;
  width: 220px;
  height: 40px;
  color: #697687;
  border-radius: 0;
}

.lock-form .form-group .form-control::-moz-placeholder {
  color: #556376;
  opacity: 1;
}

.lock-form .form-group .form-control:-ms-input-placeholder {
  color: #556376;
}

.lock-form .form-group .form-control::-webkit-input-placeholder {
  color: #556376;
}

.lock-form .form-group .form-control:focus {
  background-color: #2b3542;
}

.lock-form .form-actions {
  margin-top: 20px;
}

.lock-form .form-actions .btn-success {
  background-color: #e76070 !important;
  width: 220px;
  font-weight: 600;
  padding: 10px;
  border: none;
  border-radius: 0;
}

.lock-form .form-actions .btn-success:hover {
  background-color: #d75464 !important;
}

.lock-bottom {
  display: block;
  background-color: #323d4b;
  text-align: center;
  padding-top: 20px;
  padding-bottom: 20px;
}

.lock-bottom a {
  font-size: 14px;
  font-weight: 400;
  color: #638cac;
}

.lock-bottom a:hover {
  color: #7ba2c0;
}

@media (max-width: 768px) {
  .page-lock {
    margin: 100px auto;
  }
}
@media (max-width: 560px) {
  .page-lock {
    margin: 50px auto;
    width: 400px;
  }

  .lock-body {
    margin: 30px;
  }

  .lock-avatar {
    width: 120px;
    height: 120px;
    margin-top: 20px;
  }

  .lock-form h4 {
    font-size: 16px;
  }

  .lock-form .form-group .form-control {
    padding: 20px 20px;
    width: 170px;
  }

  .lock-form .form-actions .btn-success {
    width: 170px;
    margin-bottom: 10px;
  }
}
@media (max-width: 420px) {
  .page-lock {
    margin: 30px auto;
    width: 280px;
  }

  .lock-body {
    margin: 0px;
  }

  .page-lock .page-body {
    margin-top: 30px;
  }

  .lock-avatar {
    margin-top: 20px;
  }

  .lock-avatar-block {
    display: block;
    width: 100%;
    text-align: center;
    margin-bottom: 10px;
  }

  .lock-form {
    padding-left: 20px;
  }

  .lock-form h4 {
    font-size: 16px;
    text-align: center;
  }

  .lock-form .form-group .form-control {
    padding: 20px 20px;
    width: 240px;
  }

  .lock-form .form-actions .btn-success {
    width: 240px;
    margin-bottom: 20px;
  }
}
 .btn-sm{
            width:300px;
            height:40px;
        }
</style>

        <script type="text/javascript">
            function CamposVacios() {
                alert("Has dejado campos vacios que son necesarios para realizar este proceso, favor de verificar");

            }
            function ClavesNoConcuerdan() {
                alert("Las claves ingresada no concuerdan, favor de verificar");
            }
            function ErrorIngresoSistema() {
                alert("Error al ingresar al sistema, favor de contactar con el administrador del sistema para solucionar este inconveniente");
            }
            function UsuarioNoValido() {
                alert("El usuario ingresado no es valido, favor de verificar");
            }
            function UsuarioBloqueado() {
                alert("Este usuario ha sido bloqueado, favor de contactar un administrador para desbloquear la cuenta");
            }
        </script>


</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
	<div class="page-lock"  id="CentralizarLogin">
    	<div class="page-body">
    		<div class="lock-head">
    			<asp:Label ID="lbNombreEmpresa" runat="server" Text="Tu Nombre Aqui"></asp:Label>
                <asp:Label ID="lbContador" Visible="false" runat="server" Text="0"></asp:Label>
    		</div>
    		<div class="lock-body">
    			

    				<div class="form-group">
                         <!--TEXBOX-->
                        <div class="form-row">
                            <div class="form-group col-md-12">
                                <asp:TextBox ID="txtUsuarioLogin" AutoCompleteType="Disabled" runat="server" Placeholder="<%$ Resources:Traducciones,Usuario %>" CssClass="form-control" MaxLength="20"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-12">
                                <asp:TextBox ID="txtClaveLogin" runat="server" Placeholder="<%$ Resources:Traducciones,Clave %>" TextMode="Password" CssClass="form-control" MaxLength="40"></asp:TextBox>
                            </div>
                             <div class="form-group col-md-12">
                                <asp:TextBox ID="txtNuevaClave" Visible="false" runat="server" Placeholder="<%$ Resources:Traducciones,NuevaClave %>" TextMode="Password" CssClass="form-control" MaxLength="40"></asp:TextBox>
                            </div>
                             <div class="form-group col-md-12">
                                <asp:TextBox ID="txtConfirmarClave" Visible="false" runat="server" Placeholder="<%$ Resources:Traducciones,ConfirmarClave %>" TextMode="Password" CssClass="form-control" MaxLength="40"></asp:TextBox>
                            </div>
                        </div>
    				</div>
    				<div class="form-actions">
    					<div align="center">
                            <asp:Button ID="btnIngresarSistema" runat="server" Text="<%$ Resources:Traducciones,Ingresar %>" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnIngresarSistema_Click" ToolTip="<%$Resources:Traducciones,IngresarSistema %>" />
                            <asp:Button ID="btnCambiarClave" runat="server" Visible="false" Text="<%$Resources:Traducciones,CambiarClave %>" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnCambiarClave_Click" ToolTip="<%$Resources:Traducciones,CambiarClave %>" />
    					</div>
    				</div>
    			
    		</div>
    		<div class="lock-bottom">
    			
    		</div>
    	</div>
    </div>
</div>
    </form>
</body>
</html>
