<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="MovimientoCuentaEdit.aspx.cs" Inherits="MovimientoCuentaEdit" Title="MovimientoCuenta Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Movimiento Cuenta - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="MovimientoId" runat="server" DataSourceID="MovimientoCuentaDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/MovimientoCuentaFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/MovimientoCuentaFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>MovimientoCuenta not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:MovimientoCuentaDataSource ID="MovimientoCuentaDataSource" runat="server"
			SelectMethod="GetByMovimientoId"
		>
			<Parameters>
				<asp:QueryStringParameter Name="MovimientoId" QueryStringField="MovimientoId" Type="String" />

			</Parameters>
		</data:MovimientoCuentaDataSource>
		
		<br />

		

</asp:Content>

