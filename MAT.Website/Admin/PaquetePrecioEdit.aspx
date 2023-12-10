<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="PaquetePrecioEdit.aspx.cs" Inherits="PaquetePrecioEdit" Title="PaquetePrecio Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Paquete Precio - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="PaquetePrecioId" runat="server" DataSourceID="PaquetePrecioDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/PaquetePrecioFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/PaquetePrecioFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>PaquetePrecio not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:PaquetePrecioDataSource ID="PaquetePrecioDataSource" runat="server"
			SelectMethod="GetByPaquetePrecioId"
		>
			<Parameters>
				<asp:QueryStringParameter Name="PaquetePrecioId" QueryStringField="PaquetePrecioId" Type="String" />

			</Parameters>
		</data:PaquetePrecioDataSource>
		
		<br />

		

</asp:Content>

