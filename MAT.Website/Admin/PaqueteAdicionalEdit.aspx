<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="PaqueteAdicionalEdit.aspx.cs" Inherits="PaqueteAdicionalEdit" Title="PaqueteAdicional Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Paquete Adicional - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="PaqueteAdicionalId" runat="server" DataSourceID="PaqueteAdicionalDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/PaqueteAdicionalFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/PaqueteAdicionalFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>PaqueteAdicional not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:PaqueteAdicionalDataSource ID="PaqueteAdicionalDataSource" runat="server"
			SelectMethod="GetByPaqueteAdicionalId"
		>
			<Parameters>
				<asp:QueryStringParameter Name="PaqueteAdicionalId" QueryStringField="PaqueteAdicionalId" Type="String" />

			</Parameters>
		</data:PaqueteAdicionalDataSource>
		
		<br />

		

</asp:Content>

