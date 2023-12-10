<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="PaqueteExcursionEdit.aspx.cs" Inherits="PaqueteExcursionEdit" Title="PaqueteExcursion Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Paquete Excursion - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="PaqueteExcursionId" runat="server" DataSourceID="PaqueteExcursionDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/PaqueteExcursionFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/PaqueteExcursionFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>PaqueteExcursion not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:PaqueteExcursionDataSource ID="PaqueteExcursionDataSource" runat="server"
			SelectMethod="GetByPaqueteExcursionId"
		>
			<Parameters>
				<asp:QueryStringParameter Name="PaqueteExcursionId" QueryStringField="PaqueteExcursionId" Type="String" />

			</Parameters>
		</data:PaqueteExcursionDataSource>
		
		<br />

		

</asp:Content>

