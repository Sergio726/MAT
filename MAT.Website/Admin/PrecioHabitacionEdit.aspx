<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="PrecioHabitacionEdit.aspx.cs" Inherits="PrecioHabitacionEdit" Title="PrecioHabitacion Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Precio Habitacion - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="PrecioHabitacionId" runat="server" DataSourceID="PrecioHabitacionDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/PrecioHabitacionFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/PrecioHabitacionFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>PrecioHabitacion not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:PrecioHabitacionDataSource ID="PrecioHabitacionDataSource" runat="server"
			SelectMethod="GetByPrecioHabitacionId"
		>
			<Parameters>
				<asp:QueryStringParameter Name="PrecioHabitacionId" QueryStringField="PrecioHabitacionId" Type="String" />

			</Parameters>
		</data:PrecioHabitacionDataSource>
		
		<br />

		

</asp:Content>

