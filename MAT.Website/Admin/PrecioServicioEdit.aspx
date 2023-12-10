<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="PrecioServicioEdit.aspx.cs" Inherits="PrecioServicioEdit" Title="PrecioServicio Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Precio Servicio - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="PrecioServicioId" runat="server" DataSourceID="PrecioServicioDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/PrecioServicioFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/PrecioServicioFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>PrecioServicio not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:PrecioServicioDataSource ID="PrecioServicioDataSource" runat="server"
			SelectMethod="GetByPrecioServicioId"
		>
			<Parameters>
				<asp:QueryStringParameter Name="PrecioServicioId" QueryStringField="PrecioServicioId" Type="String" />

			</Parameters>
		</data:PrecioServicioDataSource>
		
		<br />

		

</asp:Content>

