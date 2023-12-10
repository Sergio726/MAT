<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="PasajeAdicionalEdit.aspx.cs" Inherits="PasajeAdicionalEdit" Title="PasajeAdicional Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Pasaje Adicional - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="PasajeAdicionalId" runat="server" DataSourceID="PasajeAdicionalDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/PasajeAdicionalFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/PasajeAdicionalFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>PasajeAdicional not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:PasajeAdicionalDataSource ID="PasajeAdicionalDataSource" runat="server"
			SelectMethod="GetByPasajeAdicionalId"
		>
			<Parameters>
				<asp:QueryStringParameter Name="PasajeAdicionalId" QueryStringField="PasajeAdicionalId" Type="String" />

			</Parameters>
		</data:PasajeAdicionalDataSource>
		
		<br />

		

</asp:Content>

